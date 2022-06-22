using MarketBackend.DataLayer.DataDTOs.Buyers.Carts;

namespace MarketBackend.BusinessLayer.Buyers
{
    public class ProductInBag
    {
        public ProductInBag(int productId, int storeId)
        {
            ProductId = productId;
            StoreId = storeId;
        }

        public virtual int ProductId { get; }
        public int StoreId { get; }

        public static ProductInBag DataProductInBagToProductInBag(DataProductInBag dataProductInBag, int storeId)
        {
            return new ProductInBag(dataProductInBag.ProductId, storeId);
        }

        // override object.Equals
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var result = true;
            // Get all properties of obj
            // then compare the value of each property
            foreach (var property in obj.GetType().GetProperties())
            {
                var objValue = property.GetValue(obj);
                var thisValue = property.GetValue(this);
                if (!thisValue.Equals(objValue)) 
                    result = false;
            }

            return result;
        }

        public override int GetHashCode() => HashCode.Combine(ProductId,StoreId);

        public DataProductInBag ProductInBagToDataProductInBag()
        {
            return new DataProductInBag()
            {
                ProductId = ProductId
            };
        } 


        // optional for the future - custom features like: color, type, size...
    }
}