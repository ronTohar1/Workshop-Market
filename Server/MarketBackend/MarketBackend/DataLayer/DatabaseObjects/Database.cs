using MarketBackend.DataLayer.DataDTOs;
using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.BasicDiscounts;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.LogicalExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.NumericExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountExpressions.PredicatesExpressions;
using MarketBackend.DataLayer.DataDTOs.Market.StoreManagement.DiscountPolicy.DiscountInterfaces;
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

        private const string databaseName = "MarketDatabase";
        private const string instanceName = "SQLEXPRESS";
        private const string ip = "192.168.56.101";
        private const string port = "50488";
        private const string databaseUsername = "amitZivan";
        private const string databasePassword = "passMarket";

        public DbSet<DataMember> Members { get; set; }
        public DbSet<DataStore> Stores { get; set; }

        // discounts hierarchies

        public DbSet<DataDateDiscount> DateDiscounts { get; set; }
        public DbSet<DataOneProductDiscount> OneProductDiscounts { get; set; }
        public DbSet<DataStoreDiscount> StoreDiscounts { get; set; }
        public DbSet<DataAndExpression> AndExpressions { get; set; }
        public DbSet<DataOrExpression> OrExpressions { get; set; }
        public DbSet<DataXorExpression> XorExpressions { get; set; }
        public DbSet<DataMaxExpression> MaxExpressions { get; set; }
        public DbSet<DataBagValuePredicate> BagValuePredicates { get; set; }
        public DbSet<DataProductAmountPredicate> ProductAmountPredicates { get; set; }
        public DbSet<DataConditionExpression> ConditionDiscounts { get; set; }
        public DbSet<DataIfDiscount> IfDiscounts { get; set; }
        public DbSet<DataLogicalExpression> LogicalExpressions { get; set; }
        public DbSet<DataConditionExpression> ConditionExpressions { get; set; }
        public DbSet<DataDiscountExpression> DiscountExpressions { get; set; }
        public DbSet<DataExpression> Expressions { get; set; }
        public DbSet<DataPredicateExpression> PredicateExpressions { get; set; }
        public DbSet<DataDiscount> Discounts { get; set; }


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

        //private void DiscountsWithoutCascades(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<DataDiscount>().OnD
        //}
    }
}
