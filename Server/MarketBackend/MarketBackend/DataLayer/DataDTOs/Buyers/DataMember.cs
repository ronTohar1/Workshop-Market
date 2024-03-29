﻿using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataDTOs.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketBackend.DataLayer.DataDTOs.Buyers
{
    public class DataMember
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Username { get; set; }
        public int Password { get; set; }
        public virtual IList<DataNotification?>? PendingNotifications { get; set; }

        public bool IsAdmin { get; set; }

        // buyer data

        public virtual DataCart? Cart { get; set; }
        public virtual IList<DataPurchase?>? PurchaseHistory { get; set; }
    }
}
