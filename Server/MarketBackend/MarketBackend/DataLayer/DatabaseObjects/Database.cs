using MarketBackend.DataLayer.DataDTOs;
using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.DataLayer.DatabaseObjects
{
    public class Database : DbContext
    {
        public DbSet<DataMember> Members { get; set; }
        public DbSet<DataStore> Stores { get; set; }

        private const string databaseName = "MarketDatabase";
        private const string instanceName = "SQLEXPRESS"; 
        private const string ip = "192.168.56.101";
        private const string port = "50488";
        private const string databaseUsername = "amitZivan";
        private const string databasePassword = "passMarket"; 

        // connection setup functions

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string localVMConnectionString = "Data Source = tcp:" + ip + "\\" + instanceName + "." + databaseName + "," + port + "; " +
                "Database=" + databaseName + "; " +
                "Integrated Security = False; " +
                "User Id = " + databaseUsername + "; " +
                "Password = " + databasePassword + "; " +
                "Encrypt = True; " +
                "TrustServerCertificate = True; " +
                "MultipleActiveResultSets = True";  // todo: check if need more security 

            optionsBuilder.UseSqlServer(localVMConnectionString); 
        }

        // setting (not defualt) primary keys

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataStoreMemberRoles>()
                .HasKey(storeMemberRoles => new { storeMemberRoles.MemberId, storeMemberRoles.StoreId }); 
        }
    }
}
