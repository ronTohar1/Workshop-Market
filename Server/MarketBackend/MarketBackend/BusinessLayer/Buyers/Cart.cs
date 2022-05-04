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
        private IDictionary<int, ShoppingBag> shoppingBags { get; }

        public virtual IDictionary<int, ShoppingBag> ShoppingBags { get { return shoppingBags; } }

        public Cart(IDictionary<int, ShoppingBag> shoppingBags) =>
            this.shoppingBags = shoppingBags;

        public Cart() => 
            this.shoppingBags = new Dictionary<int, ShoppingBag>();

        public virtual void AddProductToCart(ProductInBag product, int amount)
        {
            int storeId = product.StoreId;
            if (!shoppingBags.ContainsKey(storeId))         // creating new bag for first product from store
                shoppingBags[storeId] = new ShoppingBag();

            shoppingBags[storeId].AddProductToBag(product, amount);
        }

        public virtual void RemoveProductFromCart(ProductInBag product)
        {
            int storeId = product.StoreId;
            shoppingBags[storeId].RemoveProduct(product);
            if (shoppingBags[storeId].IsEmpty())
                shoppingBags.Remove(storeId);
        }

        public void ChangeProductAmount(ProductInBag product, int amount)
        {
            int storeId = product.StoreId;
            shoppingBags[storeId].ChangeProductAmount(product, amount);
        }
        
       public virtual ProductInBag? GetProductInBag(int storeId, int productId)
        => ShoppingBags[storeId].ProductsAmounts.Keys.Where(p => p.ProductId == productId).First();
       public virtual bool isEmpty()
       => shoppingBags.Count==0 || shoppingBags.Values.Where(p=>p.ProductsAmounts.Count>0).Count()==0;
    }
}
