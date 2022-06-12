﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs
{
    public class ServiceAfterHour : ServiceRestriction
    {
        public int hour { get; set; }

        public ServiceAfterHour(int hour, string tag = "") : base(tag)
        {
            this.hour = hour;
        }
    }
}
