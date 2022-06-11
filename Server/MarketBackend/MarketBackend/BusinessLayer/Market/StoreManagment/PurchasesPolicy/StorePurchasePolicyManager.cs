using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.logicalOperators;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.LogicalOperators;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PredicatePolicies;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.RestrictionPolicies;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy;
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
        private static int discountId = 0;

        public StorePurchasePolicyManager() : this(new ConcurrentDictionary<int, PurchasePolicy>())
        {

        }

        private StorePurchasePolicyManager(IDictionary<int, PurchasePolicy> purchasesPolicies)
        {
            this.purchases = purchasesPolicies;
        }

        // r S 8
        public static StorePurchasePolicyManager DataSPPMToSPPM(DataStorePurchasePolicyManager dataSPPM)
        {
            IDictionary<int, PurchasePolicy> purchasePolicies = new ConcurrentDictionary<int, PurchasePolicy>();

            foreach (DataPurchasePolicy dataPurchasePolicy in dataSPPM.PurchasesPolicies)
            {
                purchasePolicies.Add(dataPurchasePolicy.Id, PurchasePolicy.DataPurchasePolicyToPurchasePolicy(dataPurchasePolicy));
            }

            return new StorePurchasePolicyManager(purchasePolicies);
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

        public int AddPurchasePolicy(string description, IPurchasePolicy policy)
        {
            int id = getId();
            purchases.TryAdd(id, new PurchasePolicy(id, description, policy));
            return id;
        }

        public void RemovePurchasePolicy(int policyId)
        {
            if (!purchases.ContainsKey(policyId))
                throw new MarketException($"No purchase policy with id {policyId}");
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
