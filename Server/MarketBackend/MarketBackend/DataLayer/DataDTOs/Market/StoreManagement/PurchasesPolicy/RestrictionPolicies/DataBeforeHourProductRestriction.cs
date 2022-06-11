﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.RestrictionPolicies
{
    public class DataBeforeHourProductRestriction : DataBeforeHourRestriction
    {
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}