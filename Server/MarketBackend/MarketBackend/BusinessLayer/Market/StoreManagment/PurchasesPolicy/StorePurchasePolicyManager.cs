using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.logicalOperators;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.LogicalOperators;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PredicatePolicies;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.RestrictionPolicies;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy;
using MarketBackend.DataLayer.DataManagers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy
{
    public class StorePurchasePolicyManager
    {
        public IDictionary<int, PurchasePolicy> purchases { get; set; }

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

        public StorePurchasePolicyManager() : this(new ConcurrentDictionary<int, PurchasePolicy>())
        {

        }

        private StorePurchasePolicyManager(IDictionary<int, PurchasePolicy> purchasesPolicies)
        {
            this.purchases = purchasesPolicies;
        }

        public int AddPurchasePolicy(string description, IPurchasePolicy policy, DataStorePurchasePolicyManager dataStorePurchasePolicyManager)
        {
            int id = GetNextId();
            PurchasePolicy purchasePolicy = new PurchasePolicy(id, description, policy);
            
            DataPurchasePolicy dpp = PurchasePolicyToDataPurchasePolicy(purchasePolicy);
            dataStorePurchasePolicyManager.PurchasesPolicies.Add(dpp); 
            PurchasePolicyDataManager.GetInstance().Save();

            purchases.TryAdd(id, purchasePolicy);
            return id;
        }

        public void RemovePurchasePolicy(int policyId)
        {
            if (!purchases.ContainsKey(policyId))
                throw new MarketException($"No purchase policy with id {policyId}");

            PurchasePolicy purchasePolicy = purchases[policyId];
            DataPurchasePolicy dpp = PurchasePolicyDataManager.GetInstance().Find(policyId);
            purchasePolicy.policy.RemoveFromDB(dpp.Policy);

            PurchasePolicyDataManager.GetInstance().Remove(policyId);
            PurchasePolicyDataManager.GetInstance().Save();

            purchases.Remove(policyId);
        }

        public IDictionary<int, string> GetDescriptions()
        {
            IDictionary<int, string> descriptions = new Dictionary<int, string>();
            foreach (PurchasePolicy purchasePolicy in purchases.Values)
            {
                descriptions.Add(purchasePolicy.id, purchasePolicy.description);
            }
            return descriptions;
        }

        public virtual string? CanBuy(ShoppingBag bag, string storeName)
        {
            string? problems = null;
            foreach(PurchasePolicy purchasePolicy in purchases.Values)
            {
                bool flag = purchasePolicy.CanBuy(bag);
                if (!flag)
                    problems += $"{storeName}, the policy is violated: {purchasePolicy.description}\n";
            }
            return problems;
        }

        // r S 8
        public static StorePurchasePolicyManager DataSPPMToSPPM(DataStorePurchasePolicyManager dataSPPM)
        {
            int maxid = 0;
            IDictionary<int, PurchasePolicy> purchasePolicies = new ConcurrentDictionary<int, PurchasePolicy>();

            foreach (DataPurchasePolicy dataPurchasePolicy in dataSPPM.PurchasesPolicies)
            {
                if (dataPurchasePolicy.Id > maxid)
                    maxid = dataPurchasePolicy.Id;
                purchasePolicies.Add(dataPurchasePolicy.Id, PurchasePolicy.DataPurchasePolicyToPurchasePolicy(dataPurchasePolicy));
            }
            discountId = maxid + 1;
            return new StorePurchasePolicyManager(purchasePolicies);
        }

        private DataPurchasePolicy PurchasePolicyToDataPurchasePolicy(PurchasePolicy pp)
        {
            return new DataPurchasePolicy()
            {
                Id = pp.id,
                Description = pp.description,
                Policy = pp.policy.IPurchasePolicyToDataIPurchasePolicy()
            };
        }

        public DataStorePurchasePolicyManager SPPMToDataDSPPM()
        {
            IList<DataPurchasePolicy> purchasePolicies = new List<DataPurchasePolicy>();
            foreach (PurchasePolicy pp in purchases.Values)
            {
                purchasePolicies.Add(PurchasePolicyToDataPurchasePolicy(pp));
            }
            return new DataStorePurchasePolicyManager()
            {
                PurchasesPolicies = purchasePolicies
            };
        }
        //------------------------ builders ----------------------------


        //---- restrictions ----
        public IRestrictionExpression NewAfterHourProductRestriction(int hour, int productId, int amount)
        {
            return new AfterHourProductRestriction(hour, productId, amount);
        }
        public IRestrictionExpression NewBeforeHourProductRestriction(int hour, int productId, int amount)
        {
            return new BeforeHourProductRestriction(hour, productId, amount);
        }
        public IRestrictionExpression NewAfterHourRestriction(int hour)
        {
            return new AfterHourRestriction(hour);
        }
        public IRestrictionExpression NewBeforeHourRestriction(int hour)
        {
            return new BeforeHourRestriction(hour);
        }
        public IRestrictionExpression NewAtleastAmountRestriction(int productId, int amount)
        {
            return new AtlestAmountRestriction(productId, amount);
        }
        public IRestrictionExpression NewAtmostAmountRestriction(int productId, int amount)
        {
            return new AtMostAmountRestriction(productId, amount);
        }
        public IRestrictionExpression NewDateRestriction(int year, int month, int day)
        {
            return new DateRestriction(year, month, day);
        }

        // ---- logical ----
        
        public IPurchasePolicy NewAndExpression(IRestrictionExpression first, IRestrictionExpression second)
        {
            return new AndExpression(first, second);
        }
        public IPurchasePolicy NewOrExpression(IRestrictionExpression first, IRestrictionExpression second)
        {
            return new OrExpression(first, second);
        }
        public IPurchasePolicy NewImpliesExpression(IPredicateExpression condition, IPredicateExpression allowing)
        {
            return new ImpliesExpression(condition, allowing);
        }

        // ---- predicates ----

        public IPredicateExpression NewCheckProductLessPredicate(int productId, int amount)
        {
            return new CheckProductLessPredicate(productId, amount);
        }
        public IPredicateExpression NewCheckProductMorePredicate(int productId, int amount)
        {
            return new CheckProductMoreEqualsPredicate(productId, amount);
        }

    }
}
