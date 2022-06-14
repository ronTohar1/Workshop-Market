using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataManagers;
using MarketBackend.DataLayer.DataDTOs.Buyers;

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

        // r S 8
        public static Cart DataCartToCart(DataCart dataCart)
        {
            IDictionary<int, ShoppingBag> shoppingBags = new Dictionary<int, ShoppingBag>();

            foreach(DataShoppingBag dataShoppingBag in dataCart.ShoppingBags)
            {
                shoppingBags.Add(dataShoppingBag.Store.Id, ShoppingBag.DataShoppingBagToShoppingBag(dataShoppingBag)); 
            }

            return new Cart(shoppingBags); 
        }

        // r S 8
        public virtual void AddProductToCart(ProductInBag product, int amount, int buyerId, bool isMember)
        {
            DataMember? dm = null;
            DataShoppingBag? dsb = null;
            int storeId = product.StoreId;
            if (!shoppingBags.ContainsKey(storeId)) // creating new bag for first product from store
            {
                if (isMember)
                {
                    dm = MemberDataManager.GetInstance().Find(buyerId);
                    dsb = AddShoppingBagToDB(product.StoreId, dm); // r S 8 (no save)
                }
                shoppingBags[storeId] = new ShoppingBag(storeId);
            }
            shoppingBags[storeId].AddProductToBag(product, amount, buyerId, isMember, dm, dsb); // r S 8 (save here and revert if bad)
        }

        // r S 8
        public virtual void RemoveProductFromCart(ProductInBag product, int buyerId, bool isMember)
        {
            int storeId = product.StoreId;
            shoppingBags[storeId].RemoveProduct(product, buyerId, isMember); // r S 8
            if (shoppingBags[storeId].IsEmpty())
                shoppingBags.Remove(storeId);
        }
        
       public virtual ProductInBag? GetProductInBag(int storeId, int productId)
        => ShoppingBags[storeId].ProductsAmounts.Keys.Where(p => p.ProductId == productId).First();
       public virtual bool isEmpty()
       => shoppingBags.Count==0 || shoppingBags.Values.Where(p=>p.ProductsAmounts.Count>0).Count()==0;

       // r S 8 - database functions --------------------------------------
       public DataCart CartToDataCart()
       {
            IList<DataShoppingBag> dsp = new List<DataShoppingBag>();
            foreach (ShoppingBag sb in shoppingBags.Values)
            {
                dsp.Add(sb.ShoppingBagToDataShoppingBag());
            }

            return new DataCart()
            {
                ShoppingBags = dsp
            };
       }

        public void RemoveContentFromDB(DataCart c)
        {
            foreach (DataShoppingBag dsb in c.ShoppingBags)
            {
                foreach (DataProductInBag dpib in dsb.ProductsAmounts)
                {
                    ProductInBagDataManager.GetInstance().Remove(dpib.Id);
                }
                ShoppingBagDataManager.GetInstance().Remove(dsb.Id);
            }

        }

        public DataShoppingBag AddShoppingBagToDB(int storeId, DataMember dm)
        {
            DataShoppingBag dsb = new DataShoppingBag()
            {
                Store = StoreDataManager.GetInstance().Find(storeId),
                ProductsAmounts = new List<DataProductInBag>()
            };
            dm.Cart.ShoppingBags.Add(dsb);
            return dsb;
        }

        //for tests
        public virtual void RemoveProductFromCart(ProductInBag product)
        {
            RemoveProductFromCart(product, 0, false);
        }
    }
}
