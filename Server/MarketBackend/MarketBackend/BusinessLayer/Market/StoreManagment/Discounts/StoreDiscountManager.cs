using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.BasicDiscounts;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.ConditionalDiscounts;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalOperators;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.NumericExpressions;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountInterfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts
{
    internal class StoreDiscountManager
    {
        private static Mutex idMutex = new Mutex(false);
        private static int discountId = 0;

        private IDictionary<int, Discount> discounts;

        public StoreDiscountManager()
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

        public void AddDiscount(string description ,IExpression dis)
        {
            int id = getId();
            discounts.Add(id, new Discount(id, description, dis));
        }

        public int EvaluateDiscountForBag(ShoppingBag bag)
        {
            int sum = 0;
            foreach (Discount dis in discounts.Values)
            {
                IExpression exp = dis.discountExpression;
                sum += exp.EvaluateDiscount(bag);
            }
            return sum;
        }


        //-------------------------------- builders -----------------------------------------
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

        public IDiscountExpression NewBasicDiscount(int id, int discount)
        {
            IDiscountExpression newExp = new OneProductDiscount(id, discount);
            return newExp;
        }

        public IDiscountExpression NewStoreDiscount(int discount)
        {
            IDiscountExpression newExp = new StoreDiscount(discount);
            return newExp;
        }

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

        public IDiscountExpression NewMaxExpression()
        {
            IDiscountExpression newExp = new MaxExpression();
            return newExp;
        }

        //-------------------------------- builders -----------------------------------------

    }
}
