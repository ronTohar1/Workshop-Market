using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Buyers
{
    public class Cart
    {
        private ConcurrentDictionary<int, ShoppingBag> shoppingBags;

        public Cart() => 
            this.shoppingBags = new ConcurrentDictionary<int, ShoppingBag>();

        public void AddProductToCart(ProductInBag product, int amount)
        {
            int storeId = product.StoreId;
            if (!shoppingBags.ContainsKey(storeId))         // creating new bag for first product from store
                shoppingBags[storeId] = new ShoppingBag();

            shoppingBags[storeId].AddProductToBag(product, amount);
        }
    }
}
