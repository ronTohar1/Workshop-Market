using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MarketBackend.ServiceLayer
{
    internal class ScenarioParser
    {

        internal static ScenarioDTO Parse(string initFilePath)
        {
            try
            {
                string textJson = File.ReadAllText(initFilePath);
                dynamic scenarioDtoDict = JObject.Parse(textJson);
                string adminUsername = scenarioDtoDict["AdminUsername"];
                string adminPassword = scenarioDtoDict["AdminPassword"];
                JArray useCasesJson = scenarioDtoDict["UseCases"];

                Func<string, UseCase> convertUseCaseJsonToUseCase = (usecaseJson) =>
                {
                    var useCaseDict = JObject.Parse(usecaseJson);
                    string tag = useCaseDict["Tag"]!.ToString();
                    return ParseUseCase(tag, usecaseJson)!;
                };

                var useCases = useCasesJson.ToList().Select(usecase => convertUseCaseJsonToUseCase(usecase.ToString()));

                return new ScenarioDTO(adminUsername, adminPassword, useCases);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to parse init file properly", ex);
            }

        }

        private static UseCase ParseUseCase(string tag, string usecaseJson)
        {
            switch (tag)
            {
                // Admin use cases
                case "RemoveMember":
                    return JsonConvert.DeserializeObject<RemoveMemberUseCase>(usecaseJson)!;
                // Buyer use cases
                case "Register":
                    return JsonConvert.DeserializeObject<RegisterUseCase>(usecaseJson)!;
                case "Login":
                    return JsonConvert.DeserializeObject<LoginUseCase>(usecaseJson)!;
                case "Logout":
                    return JsonConvert.DeserializeObject<LogoutUseCase>(usecaseJson)!;
                case "AddProductToCart":
                    return JsonConvert.DeserializeObject<AddProductToCartUseCase>(usecaseJson)!;
                case "ChangeProductAmount":
                    return JsonConvert.DeserializeObject<ChangeProductAmountUseCase>(usecaseJson)!;
                case "RemoveProductFromCart":
                    return JsonConvert.DeserializeObject<RemoveProductFromCartUseCase>(usecaseJson)!;
                case "PurchaseCart":
                    return JsonConvert.DeserializeObject<PurchaseCartUseCase>(usecaseJson)!;
                case "ReviewProduct":
                    return JsonConvert.DeserializeObject<ReviewProductUseCase>(usecaseJson)!;
                case "Enter":
                    return JsonConvert.DeserializeObject<EnterUseCase>(usecaseJson)!;
                case "Leave":
                    return JsonConvert.DeserializeObject<LeaveUseCase>(usecaseJson)!;
                // System management use cases
                case "OpenNewStore":
                    return JsonConvert.DeserializeObject<OpenNewStoreUseCase>(usecaseJson)!;
                case "CloseStore":
                    return JsonConvert.DeserializeObject<CloseStoreUseCase>(usecaseJson)!;
                case "MakeCoOwner":
                    return JsonConvert.DeserializeObject<MakeCoOwnerUseCase>(usecaseJson)!;
                case "MakeCoManager":
                    return JsonConvert.DeserializeObject<MakeCoManagerUseCase>(usecaseJson)!;
                case "RemoveCoOwner":
                    return JsonConvert.DeserializeObject<RemoveCoOwnerUseCase>(usecaseJson)!;
                case "ChangeManagerPermission":
                    return JsonConvert.DeserializeObject<ChangeManagerPermissionUseCase>(usecaseJson)!;
                case "AddNewProduct":
                    return JsonConvert.DeserializeObject<AddNewProductUseCase>(usecaseJson)!;
                case "IncreaseProductAmount":
                    return JsonConvert.DeserializeObject<IncreaseProductAmountUseCase>(usecaseJson)!;
                case "DecreaseProductAmount":
                    return JsonConvert.DeserializeObject<DecreaseProductAmountUseCase>(usecaseJson)!;
                case "AddDiscountPolicy":
                    return JsonConvert.DeserializeObject<AddPurchasePolicyUseCase>(usecaseJson)!;
                case "RemoveDiscountPolicy":
                    return JsonConvert.DeserializeObject<RemoveDiscountPolicyUseCase>(usecaseJson)!;
                case "AddBid":
                    return JsonConvert.DeserializeObject<AddBidUseCase>(usecaseJson)!;

                // special use cases
                case "MakeIntRef":
                    return JsonConvert.DeserializeObject<MakeRefUseCase<int>>(usecaseJson)!;
                case "FillDb":
                    return JsonConvert.DeserializeObject<FillDbUseCase>(usecaseJson)!;
            }
            throw new Exception("Unable to parse init file properly, unknown tag");
        }

        internal static void SaveAsFile(string path, ScenarioDTO scenario, bool overwriteIfExist)
        {
            if (overwriteIfExist || !File.Exists(path))
            {
                string json = JsonConvert.SerializeObject(scenario, Formatting.Indented);
                File.WriteAllText(path, json);
            }
        }
    }
}