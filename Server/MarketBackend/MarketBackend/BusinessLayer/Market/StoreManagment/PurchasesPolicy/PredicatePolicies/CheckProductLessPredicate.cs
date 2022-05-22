﻿using MarketBackend.BusinessLayer.Buyers;
using MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PurchaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market.StoreManagment.PurchasesPolicy.PredicatePolicies
{
    public class CheckProductLessPredicate : IPredicateExpression
    {
        public int productId { get; set; }
        public int amount { get; set; }

        public CheckProductLessPredicate(int productId, int amount)
        {
            this.productId = productId;
            this.amount = amount;
        }

        //return true if the bag has less product than the amount
        public virtual bool IsSatisfied(ShoppingBag bag)
        {
            foreach(ProductInBag productInBag in bag.ProductsAmounts.Keys)
            {
                if (productInBag.ProductId == productId)
                    return bag.ProductsAmounts[productInBag] < amount;
            }
            return true;
        }
    }
}