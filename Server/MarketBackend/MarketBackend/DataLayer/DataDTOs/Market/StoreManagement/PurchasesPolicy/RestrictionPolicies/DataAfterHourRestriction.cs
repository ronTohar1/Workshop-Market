﻿using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.PurchasesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.PurchasesPolicy.RestrictionPolicies
{
    public class DataAfterHourRestriction : DataRestrictionExpression
    {
        public int Hour { get; set; }
    }
}