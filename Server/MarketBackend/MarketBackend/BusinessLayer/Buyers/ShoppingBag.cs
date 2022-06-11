using MarketBackend.DataLayer.DataDTOs.Buyers;
using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;
using MarketBackend.DataLayer.DataManagers;

namespace MarketBackend.BusinessLayer.Buyers
{
    public class ShoppingBag
    {
        public virtual IDictionary<ProductInBag, int> productsAmounts { get; private set; }
        public virtual int StoreId { get; private set; }

        internal IDictionary<ProductInBag, int> ProductsAmounts { get { return productsAmounts; } }

        public ShoppingBag(int storeId, IDictionary<ProductInBag, int> productsAmounts)
        {
            this.StoreId = storeId;
            this.productsAmounts = productsAmounts;
            foreach (var prod in productsAmounts)
            {
                if (prod.Key.StoreId != storeId)
                {
                    throw new Exception("How is a product is in a shopping bag with a different store?");
                }
            }
        }

        public ShoppingBag(int storeId)
        {
            this.StoreId = storeId;
            productsAmounts = new Dictionary<ProductInBag, int>();
        }
        public ShoppingBag()
        {//for testing
            this.StoreId = -1;
            productsAmounts = new Dictionary<ProductInBag, int>();
        }

        // r S 8
        /// <summary>
        /// Add given amount of product units to the bag. If product not exist in bag, it will be added to the bag
        /// </summary>
        virtual public void AddProductToBag(ProductInBag product, int amount, int buyerId, bool isMember)
        {
            if (product == null)
                throw new ArgumentNullException("product");
            if (product.StoreId != this.StoreId)
                throw new Exception("Product id in bag cannot be different from the store id of the bag");
            if (amount < 1)
                throw new MarketException(nameof(amount) + " of product in cart cannot be lower than 1!!!!");

            if (productsAmounts.ContainsKey(product))
            {
                if (isMember)
                    UpdateProductAmountInDB(buyerId, product.ProductId, product.StoreId, amount);
                productsAmounts[product] += amount;
            }
            else
            {
                AddProductToDB(buyerId, product.ProductId, product.StoreId, amount);
                productsAmounts.Add(product, amount);
            }
        }

        // r S 8
        /// <summary>
        /// Sets new amount to the product, product must be exist
        /// </summary>
        virtual public void ChangeProductAmount(ProductInBag product, int amount, DataProductInBag? dpib)
        {
            if (product == null)
                throw new ArgumentNullException("product");
            if (amount < 1)
                throw new MarketException(nameof(amount) + " of product in cart cannot be lower than 1!!!!");
            if (!productsAmounts.ContainsKey(product))
                throw new ArgumentException(nameof(product) + "is not exist in cart");
            else
            {
                if (dpib != null)
                {
                    ProductInBagDataManager.GetInstance().Update(dpib.Id, x => x.Amount = amount);
                    ProductInBagDataManager.GetInstance().Save();
                }
                productsAmounts[product] = amount;
            }
        }

        // r S 8
        /// <summary>
        /// Removes product from the bag, product must be exist
        /// </summary>
        virtual public void RemoveProduct(ProductInBag product, int buyerId, bool isMember)
        {
            if (productsAmounts.ContainsKey(product))
            {
                if (isMember)
                {
                    RemoveProductFromDB(buyerId, product.ProductId, product.StoreId);
                    MemberDataManager.GetInstance().Save();
                }
                productsAmounts.Remove(product);
            }
            else
                throw new ArgumentException(nameof(product) + "is not exist in cart");
        }

        public bool IsEmpty()
        {
            return productsAmounts.Count == 0;
        }

        // r S 8 - database functions ---------------------------------------------------------
        public DataShoppingBag ShoppingBagToDataShoppingBag()
        {
            IList<DataProductInBag> dpib = new List<DataProductInBag>();
            foreach (ProductInBag pib in productsAmounts.Keys)
            {
                DataProductInBag d = pib.ProductInBagToDataProductInBag();
                d.Amount = productsAmounts[pib];
                dpib.Add(d);
            }
            return new DataShoppingBag()
            {
                ProductsAmounts = dpib,
                Store = StoreDataManager.GetInstance().Find(StoreId)
            };
        }

        public void UpdateProductAmountInDB(int memberId, int productId, int storeId, int amount)
        {
            DataMember dm = MemberDataManager.GetInstance().Find(memberId);
            if (dm == null) return;
            DataCart? cart = dm.Cart;
            foreach (DataShoppingBag dsb in cart.ShoppingBags)
            {
                if (dsb.Store.Id == storeId)
                {
                    foreach (DataProductInBag dpib in dsb.ProductsAmounts)
                    {
                        if (dpib.ProductId == productId)
                        {
                            dpib.Amount += amount;
                            return;
                        }
                    }
                }
            }
            MemberDataManager.GetInstance().Save();
        }

        public void AddProductToDB(int memberId, int productId, int storeId, int amount)
        {
            DataMember dm = MemberDataManager.GetInstance().Find(memberId);
            if (dm == null) return;
            DataCart? cart = dm.Cart;
            foreach (DataShoppingBag dsb in cart.ShoppingBags)
            {
                if (dsb.Store.Id == storeId)
                {
                    dsb.ProductsAmounts.Add(new DataProductInBag()
                    {
                        ProductId = productId,
                        Amount = amount
                    });
                    return;
                }
            }
            MemberDataManager.GetInstance().Save();
        }

        public void RemoveProductFromDB(int memberId, int productId, int storeId)
        {
            DataMember dm = MemberDataManager.GetInstance().Find(memberId);
            if (dm == null) return;
            DataCart? cart = dm.Cart;
            foreach (DataShoppingBag dsb in cart.ShoppingBags)
            {
                if (dsb.Store.Id == storeId)
                {
                    foreach (DataProductInBag dpib in dsb.ProductsAmounts)
                    {
                        if (dpib.ProductId == productId)
                        {
                            dsb.ProductsAmounts.Remove(dpib);
                            if (dsb.ProductsAmounts.Count == 0)
                                cart.ShoppingBags.Remove(dsb);
                            return;
                        }
                    }
                }
            }
        }
    }
}