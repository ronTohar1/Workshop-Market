using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions;
using MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions.LogicalDiscounts;
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
        public IDiscountExpression newAndExpression(IDiscountExpression ex1, IDiscountExpression ex2)
        {
            IDiscountExpression newExp = new AndExpression(ex1, ex2);
            return newExp;
        }

        public IDiscountExpression newXorExpression(IDiscountExpression ex1, IDiscountExpression ex2)
        {
            IDiscountExpression newExp = new XorExpression(ex1, ex2);
            return newExp;
        }

        public IDiscountExpression newOrExpression(IDiscountExpression ex1, IDiscountExpression ex2)
        {
            IDiscountExpression newExp = new OrExpression(ex1, ex2);
            return newExp;
        }

        public IDiscountExpression newBasicDiscount(int id, int discount)
        {
            IDiscountExpression newExp = new BasicDiscount(id, discount);
            return newExp;
        }

        public void AddDiscount(string description ,IDiscountExpression dis)
        {
            int id = getId();
            discounts.Add(id, new Discount(id, description, dis));
        }

        public int EvaluateDiscountForBag(ShoppingBag bag)
        {
            int sum = 0;
            foreach (Discount dis in discounts.Values)
            {
                IDiscountExpression exp = dis.discountExpression;
                
            }
            return sum;
        }

    }
}
