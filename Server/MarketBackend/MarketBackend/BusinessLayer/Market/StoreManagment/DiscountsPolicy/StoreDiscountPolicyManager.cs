using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.BasicDiscounts;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalDiscounts;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalOperators;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.NumericExpressions;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy;
using MarketBackend.DataLayer.DataManagers;
using System.Collections.Concurrent;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts
{
    //this whole class and related classes implement r II.4.2
    public class StoreDiscountPolicyManager
    {

        public IDictionary<int, Discount> discounts { get; private set; }

        private static Mutex idMutex = new Mutex(false);
        private const int ID_COUNTER_NOT_INITIALIZED = -1;
        private static int discountId = ID_COUNTER_NOT_INITIALIZED;

        private static void InitializeIdCounter()
        {
            discountId = DiscountDataManager.GetInstance().GetNextId();
        }

        private static int GetNextId()
        {
            int temp;
            idMutex.WaitOne();

            if (discountId == ID_COUNTER_NOT_INITIALIZED)
                InitializeIdCounter();

            temp = discountId;
            discountId++;

            idMutex.ReleaseMutex();

            return temp;
        }

        public StoreDiscountPolicyManager() : this(new ConcurrentDictionary<int, Discount>())
        {

        }

        private StoreDiscountPolicyManager(IDictionary<int, Discount> discounts)
        {
            this.discounts = discounts; 
        }

        public int AddDiscount(string description ,IExpression dis)
        {
            int id = GetNextId();
            Discount discount = new Discount(id, description, dis);

            DataDiscount dataDiscount = DiscountToDataDiscount(discount);
            DiscountDataManager.GetInstance().Add(dataDiscount);
            DiscountDataManager.GetInstance().Save();

            discounts.Add(id, discount);
            return id;
        }

        public void RemoveDiscount(int did)
        {
            if (discounts.ContainsKey(did))
            {
                Discount dis = discounts[did];
                DataDiscount dd = DiscountDataManager.GetInstance().Find(did);
                dis.discountExpression.RemoveFromDB(dd.DiscountExpression);

                DiscountDataManager.GetInstance().Remove(did);
                DiscountDataManager.GetInstance().Save();

                discounts.Remove(did);
            }
            else
                throw new MarketException($"No Discount with id: {did}");
        }

        public IDictionary<int, string> GetDescriptions()
        {
            IDictionary<int, string> descriptions = new Dictionary<int, string>();
            foreach (Discount discount in discounts.Values)
            {
                descriptions.Add(discount.id, discount.description);
            }
            return descriptions;
        }
        public double EvaluateDiscountForBag(ShoppingBag bag, Store store)
        {
            double sum = 0;
            foreach (Discount dis in discounts.Values)
            {
                IExpression exp = dis.discountExpression;
                sum += exp.EvaluateDiscount(bag, store);
            }
            return sum;
        }

        // r S 8 - database functions
        public static StoreDiscountPolicyManager DataSDPMToSDPM(DataStoreDiscountPolicyManager dataSDPM)
        {
            int maxid = 0;
            IDictionary<int, Discount> discounts = new ConcurrentDictionary<int, Discount>();

            foreach (DataDiscount dataDiscount in dataSDPM.Discounts)
            {
                if (dataDiscount.Id > maxid)
                    maxid = dataDiscount.Id;
                discounts.Add(dataDiscount.Id, Discount.DataDiscountToDiscount(dataDiscount));
            }
            discountId = maxid + 1;
            return new StoreDiscountPolicyManager(discounts);
        }

        private DataDiscount DiscountToDataDiscount(Discount dis)
        {
            return new DataDiscount()
            {
                Id = dis.id,
                Description = dis.description,
                DiscountExpression = dis.discountExpression.IExpressionToDataExpression()
            };
        }

        public DataStoreDiscountPolicyManager SDPMToDSDPM()
        {
            IList<DataDiscount> dataDiscounts = new List<DataDiscount>();
            foreach (Discount pp in discounts.Values)
            {
                dataDiscounts.Add(DiscountToDataDiscount(pp));
            }
            return new DataStoreDiscountPolicyManager()
            {
                Discounts = dataDiscounts
            };
        }

        //-------------------------------- builders -----------------------------------------

        //-----Final expressions---------
        public IExpression NewConditionalDiscount(IPredicateExpression pred, IDiscountExpression then)
        {
            IExpression newExp = new ConditionDiscount(pred, then);
            return newExp;
        }

        public IExpression NewIfDiscount(IPredicateExpression test, IDiscountExpression thenDis, IDiscountExpression elseDis)
        {
            IExpression newExp = new IfDiscount(test, thenDis, elseDis);
            return newExp;
        }
        //-----Final expressions---------

        //-------------logical------------
        public IPredicateExpression NewAndExpression(IPredicateExpression ex1, IPredicateExpression ex2)
        {
            IPredicateExpression newExp = new AndExpression(ex1, ex2);
            return newExp;
        }

        public IPredicateExpression NewXorExpression(IPredicateExpression ex1, IPredicateExpression ex2)
        {
            IPredicateExpression newExp = new XorExpression(ex1, ex2);
            return newExp;
        }

        public IPredicateExpression NewOrExpression(IPredicateExpression ex1, IPredicateExpression ex2)
        {
            IPredicateExpression newExp = new OrExpression(ex1, ex2);
            return newExp;
        }
        //-------------logical------------

        //------------Discounts-----------
        public IDiscountExpression NewProductDiscount(int pid, int discount)
        {
            IDiscountExpression newExp = new OneProductDiscount(pid, discount);
            return newExp;
        }

        public IDiscountExpression NewStoreDiscount(int discount)
        {
            IDiscountExpression newExp = new StoreDiscount(discount);
            return newExp;
        }

        public IDiscountExpression NewDateDiscount(int discount, int year = -1, int month = -1, int day = -1)
        {
            IDiscountExpression newExp = new DateDiscount(discount, year, month, day);
            return newExp;
        }
        //------------Discounts-----------

        //------------Predicates----------
        public IPredicateExpression NewBagValuePredicate(int worth)
        {
            IPredicateExpression newExp = new BagValuePredicate(worth);
            return newExp;
        }

        public IPredicateExpression NewProductAmountPredicate(int pid, int quantity)
        {
            IPredicateExpression newExp = new ProductAmountPredicate(pid, quantity);
            return newExp;
        }
        //------------Predicates----------

        //-------Discount compound operations------
        public IDiscountExpression NewMaxExpression()
        {
            IDiscountExpression newExp = new MaxExpression();
            return newExp;
        }

        public IDiscountExpression NewMaxExpression(IList<IDiscountExpression> l)
        {
            MaxExpression newExp = new MaxExpression();
            newExp.discounts = l;
            return newExp;
        }
        public IDiscountExpression NewAddativeExpression(IList<IDiscountExpression> l)
        {
            AddativeExpression newExp = new AddativeExpression();
            newExp.discounts = l;
            return newExp;
        }
        public IDiscountExpression NewXorDiscount(IDiscountExpression firstDis, IDiscountExpression secondDis)
        {
            OrDiscount newExp = new OrDiscount(firstDis, secondDis);
            return newExp;
        }
        public IDiscountExpression NewOrDiscount(IDiscountExpression firstDis, IDiscountExpression secondDis)
        {
            OrDiscount newExp = new OrDiscount(firstDis, secondDis);
            return newExp;
        }
        public IDiscountExpression NewAndDiscount(IDiscountExpression firstDis, IDiscountExpression secondDis)
        {
            AndDiscount newExp = new AndDiscount(firstDis, secondDis);
            return newExp;
        }
        //-------Discount compound operations------

        //-------------------------------- builders -----------------------------------------

    }
}
