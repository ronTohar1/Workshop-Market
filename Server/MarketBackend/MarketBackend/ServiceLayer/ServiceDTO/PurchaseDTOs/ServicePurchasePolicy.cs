﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    public class ServicePurchasePolicy
    {
        public string tag { get; set; }
        public ServicePurchasePolicy(string tag="") {
            this.tag = tag;
        }
    }
}
