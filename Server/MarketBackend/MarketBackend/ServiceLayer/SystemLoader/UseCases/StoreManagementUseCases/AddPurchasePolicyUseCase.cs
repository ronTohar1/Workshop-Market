using MarketBackend.ServiceLayer.Parsers;
using MarketBackend.ServiceLayer.ServiceDTO.DiscountDTO;
using Newtonsoft.Json;
using MarketBackend.ServiceLayer.ServiceDTO.PurchaseDTOs;

namespace MarketBackend.ServiceLayer
{
    internal class AddPurchasePolicyUseCase : UseCase
    {
        public string UserIdRef { get; set; }
        public string StoreIdRef { get; set; }
        public string Description { get; set; }
        public ServicePurchasePolicy Expression { get; set; }

        public AddPurchasePolicyUseCase(string userIdRef, string storeIdRef, string description, string expression, string ret = "_") : base("AddPurchasePolicy", ret)
        {
            UserIdRef = userIdRef;
            StoreIdRef = storeIdRef;
            Description = description;
            Expression = DiscountAndPolicyParser.ConvertPolicyFromJson(expression);
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            int storeId = (int)varsEnvironment[StoreIdRef];
            PrintUseCaseStarting();
            var res = systemOperator.GetStoreManagementFacade().Value.AddPurchasePolicy(Expression, Description, storeId, userId);
            if (res.IsErrorOccured())
                Console.WriteLine("Unable to perform: " + this + "\nError message: " + res.ErrorMessage);
            else
            {
                varsEnvironment[Ret] = res.Value;
                Console.WriteLine("Success");
                return true;
            }
            return false;
        }

        internal override string ArgsToString()
        {
            return string.Join(',', new object[] { UserIdRef, StoreIdRef, Description, JsonConvert.SerializeObject(Expression) });
        }
    }
}