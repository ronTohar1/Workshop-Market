using MarketBackend.ServiceLayer.ServiceDTO;

namespace MarketBackend.ServiceLayer
{
    internal class FillDbUseCase : UseCase
    {
        public FillDbUseCase() : base("FillDb", "_")
        {
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            Dictionary<string, int> storesToId = new Dictionary<string, int>();
            Dictionary<string, int> productsToId = new Dictionary<string, int>();

            // Open 1000 new stores
            for (int i = 0; i < 1000; i++)
                storesToId[$"store-{i}"] = systemOperator.GetStoreManagementFacade().Value.OpenNewStore(1, $"store-{i}").Value;

            // add 1000 products in each store
            for (int i = 0; i < 1000; i++)
            {
                if (i % 10 == 0)
                    Console.WriteLine($"adding product to store {i}");
                for (int j = 0; j < 1000; j++)
                {
                    productsToId[$"product-{j}-inStore-{i}"] = systemOperator.GetStoreManagementFacade().Value
                                                                .AddNewProduct(1, storesToId[$"store-{i}"], $"product-{j}-inStore-{i}", 10, "c1").Value;
                    systemOperator.GetStoreManagementFacade().Value.AddProductToInventory(1, storesToId[$"store-{i}"], productsToId[$"product-{j}-inStore-{i}"], 10000000);
                }
            }

            // Add 10000 new guests
            for (int i = 0; i < 10000; i++)
            {
                if (i % 100 == 0)
                    Console.WriteLine($"adding product to store {i}");
                int id = systemOperator.GetBuyerFacade().Value.Enter().Value;
                for (int j = 0; j < 100; j++)
                {
                    // purchase product
                    var res1 = systemOperator.GetBuyerFacade().Value.AddProdcutToCart(id, storesToId[$"store-0"], productsToId[$"product-0-inStore-0"], 1);
                    if (!res1.Value)
                        throw new Exception($"Unable to add product to cart id={id}");

                    var res2 = systemOperator.GetBuyerFacade().Value.PurchaseCartContent(id, new ServicePaymentDetails("2222333344445555", "12", "2025", "Yossi Cohen", "262", "20444444"),
                        new ServiceSupplyDetails("Yossi Cohen", "Rager 100", "Beer Sheva", "Israel", "8458527"));
                    if (res2 is null)
                        throw new Exception($"Unable to purchase cart id={id}");
                }
            }
            return true;
        }

        internal override string ArgsToString()
        {
            return string.Join(',', new object[] { });
        }
    }
}
