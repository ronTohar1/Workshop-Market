using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.logicalOperators;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.LogicalOperators;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PredicatePolicies;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.RestrictionPolicies;
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

        public StorePurchasePolicyManager()
        {
            this.purchases = new ConcurrentDictionary<int, PurchasePolicy>();
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
        public IPurchasePolicy NewImpliesExpression(IPredicateExpression first, IPredicateExpression second)
        {
            return new ImpliesExpression(first, second);
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
