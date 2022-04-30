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
        private IDictionary<int, ShoppingBag> shoppingBags;

        internal IDictionary<int, ShoppingBag> ShoppingBags { get { return shoppingBags; } }

        public Cart(IDictionary<int, ShoppingBag> shoppingBags) =>
            this.shoppingBags = shoppingBags;

        public Cart() => 
            this.shoppingBags = new Dictionary<int, ShoppingBag>();

        public void AddProductToCart(ProductInBag product, int amount)
        {
            int storeId = product.StoreId;
            if (!shoppingBags.ContainsKey(storeId))         // creating new bag for first product from store
                shoppingBags[storeId] = new ShoppingBag();

            shoppingBags[storeId].AddProductToBag(product, amount);
        }

        public void RemoveProductFromCart(ProductInBag product)
        {
            int storeId = product.StoreId;
            shoppingBags[storeId].RemoveProduct(product);
        }

        internal void changeProductAmount(ProductInBag product, int amount)
        {
            int storeId = product.StoreId;
            shoppingBags[storeId].ChangeProductAmount(product, amount);
        }
    }
}
