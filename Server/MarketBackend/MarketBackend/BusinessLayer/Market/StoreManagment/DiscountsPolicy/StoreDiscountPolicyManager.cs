using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.BasicDiscounts;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalOperators;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.NumericExpressions;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using System.Collections.Concurrent;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts
{
    //this whole class and related classes implement r II.4.2
    public class StoreDiscountPolicyManager
    {
        private static Mutex idMutex = new Mutex(false);
        private static int discountId = 0;

        private IDictionary<int, Discount> discounts;

        public StoreDiscountPolicyManager()
        {
            this.discounts = new ConcurrentDictionary<int, Discount>();
        }

        private int getId()
        {
            int res;
            idMutex.WaitOne();
            res = discountId;
            discountId++;
            idMutex.ReleaseMutex();
            return res;
        }

        public int AddDiscount(string description ,IExpression dis)
        {
            int id = getId();
            discounts.Add(id, new Discount(id, description, dis));
            return id;
        }

        public void RemoveDiscount(int did)
        {
            if (discounts.ContainsKey(did))
                discounts.Remove(did);
            else
                throw new MarketException($"No Discount with id: {did}");
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
        //-------Discount compound operations------

        //-------------------------------- builders -----------------------------------------

    }
}
