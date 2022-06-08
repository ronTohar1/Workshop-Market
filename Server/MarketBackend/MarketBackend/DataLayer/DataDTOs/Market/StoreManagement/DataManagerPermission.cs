using MarketBackend.BusinessLayer.Market.StoreManagment;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DataDTOs.Market.StoreManagement
{
    public class DataManagerPermission
    {
        public int Id { get; set; }
        public Permission Permission { get; set; }
    }
}
