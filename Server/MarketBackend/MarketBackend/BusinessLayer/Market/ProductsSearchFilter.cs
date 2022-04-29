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
            Predicate<Store> newFilter = store => store.GetName().Contains(name); 
            storePred = And(storePred, newFilter); 
        }

        public void FilterProductName(string name)
        {
            Predicate<Product> newFilter = product => product.name.Contains(name);
            productPred = And(productPred, newFilter);
        }

        public void FilterProductCategory(string category)
        {
            Predicate<Product> newFilter = product => product.category.Contains(category);
            productPred = And(productPred, newFilter);
        }

        public void FilterProductKeyword(string keyword)
        {
            Predicate<Product> newFilter = 
                product => product.name.Contains(keyword) || product.category.Contains(keyword);
            productPred = And(productPred, newFilter);
        }

        public static Predicate<T> And<T>(Predicate<T> pred1, Predicate<T> pred2)
        {
            return input => pred1(input) && pred2(input); 
        }
    }
}
