using MarketBackend.BusinessLayer.Market.StoreManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Market
{
    public class ProductsSearchFilter
    {
        // r 2.2 (this class)

        private Predicate<Store> storePred;
        private Predicate<Product> productPred;

        public ProductsSearchFilter()
        {
            storePred = store => true; 
            productPred = product => true;
        }

        public bool FilterStore(Store store)
        {
            return storePred(store);
        }

        public bool FilterProduct(Product product)
        {
            return productPred(product);
        }

        public void FilterStoreName(string name)
        {
            Predicate<Store> newFilter = store => CheckStrings(store.GetName(), name); 
            storePred = And(storePred, newFilter); 
        }

        public void FilterProductName(string name)
        {
            Predicate<Product> newFilter = product => CheckStrings(product.name, name);
            productPred = And(productPred, newFilter);
        }

        public void FilterProductCategory(string category)
        {
            Predicate<Product> newFilter = product => CheckStrings(product.category, category);
            productPred = And(productPred, newFilter);
        }

        public void FilterProductKeyword(string keyword)
        {
            Predicate<Product> newFilter = 
                product => CheckStrings(product.name, keyword) || CheckStrings(product.category, keyword);
            productPred = And(productPred, newFilter);
        }

        public void FilterProductId(int id)
        {
            Predicate<Product> newFilter = product => product.id == id;
            productPred = And(productPred, newFilter);
        }

        public static Predicate<T> And<T>(Predicate<T> pred1, Predicate<T> pred2)
        {
            return input => pred1(input) && pred2(input); 
        }

        private bool CheckStrings(string resultString, string searchString)
        {
            bool result =  resultString.ToLower().Contains(searchString.ToLower());
            return result; 
        }
    }
}
