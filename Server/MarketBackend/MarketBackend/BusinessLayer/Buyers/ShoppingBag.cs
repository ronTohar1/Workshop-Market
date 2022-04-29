namespace MarketBackend.BusinessLayer.Buyers
{
    internal class ShoppingBag
    {
        private IDictionary<ProductInBag, int> productsAmounts;

        internal IDictionary<ProductInBag, int> ProductsAmounts { get { return productsAmounts; } }

        public ShoppingBag(IDictionary<ProductInBag, int> productsAmounts)
        {
            this.productsAmounts = productsAmounts;
        }

        public ShoppingBag()
        {
            productsAmounts = new Dictionary<ProductInBag, int>();
        }

        /// <summary>
        /// Add given amount of product units to the bag. If product not exist in bag, it will be added to the bag
        /// </summary>
        public void AddProductToBag(ProductInBag product, int amount)
        {
            if (product == null)
                throw new ArgumentNullException("product");
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
        public void ChangeProductAmount(ProductInBag product, int amount)
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
        public void RemoveProduct(ProductInBag product)
        {
            if (!productsAmounts.Remove(product))
                throw new ArgumentException(nameof(product) + "is not exist in cart");
        }
    }
}