﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO
{
    internal class ServiceAnd : ServiceLogical
    {
        public ServiceAnd(IServicePredicate firstExpression, IServicePredicate secondExpression) : base(firstExpression, secondExpression)
        {

        }
    }
}