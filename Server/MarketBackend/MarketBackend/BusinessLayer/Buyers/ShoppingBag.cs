namespace MarketBackend.BusinessLayer.Buyers
{
    public class ShoppingBag
    {
        private IDictionary<ProductInBag, int> productsAmounts;
        public virtual int StoreId { get; private set; }

        internal IDictionary<ProductInBag, int> ProductsAmounts { get { return productsAmounts; } }

        public ShoppingBag(int storeId,IDictionary<ProductInBag, int> productsAmounts)
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


        /// <summary>
        /// Add given amount of product units to the bag. If product not exist in bag, it will be added to the bag
        /// </summary>
        virtual public void AddProductToBag(ProductInBag product, int amount)
        {
            if (product == null)
                throw new ArgumentNullException("product");
            if (product.StoreId != this.StoreId)
                throw new Exception("Product id in bag cannot be different from the store id of the bag");
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            if (productsAmounts.ContainsKey(product))
                productsAmounts[product] += amount;
            else 
                productsAmounts.Add(product, amount);
        }

        /// <summary>
        /// Sets new amount to the product, product must be exist
        /// </summary>
        virtual public void ChangeProductAmount(ProductInBag product, int amount)
        {
            if (product == null)
                throw new ArgumentNullException("product");
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            if (!productsAmounts.ContainsKey(product))
                throw new ArgumentException(nameof(product) + "is not exist in cart");

            if (amount == 0)
                productsAmounts.Remove(product);
            else
                productsAmounts[product] = amount;
        }

        /// <summary>
        /// Removes product from the bag, product must be exist
        /// </summary>
        virtual public void RemoveProduct(ProductInBag product)
        {
            if (!productsAmounts.Remove(product))
                throw new ArgumentException(nameof(product) + "is not exist in cart");
        }

        public bool IsEmpty()
        {
            return productsAmounts.Count == 0;
        }
    }
}