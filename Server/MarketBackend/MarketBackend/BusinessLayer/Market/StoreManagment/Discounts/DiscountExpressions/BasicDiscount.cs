﻿using MarketBackend.BusinessLayer.Buyers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.Discounts.DiscountExpressions
{
    internal class BasicDiscount : IDiscountExpression
    {
        public int productId { get; set; }
        public int discount { get; set; }

        public BasicDiscount(int productId, int discount)
        {
            this.productId = productId;
            this.discount = discount;
        }

        public int EvaluateDiscount(ShoppingBag bag)
        {
            throw new NotImplementedException();
        }

        public bool EvaluatePredicate(ShoppingBag bag)
        {
            throw new NotImplementedException();
        }
    }
}
