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
            switch(tag)
            {
                case "Register":
                    return JsonConvert.DeserializeObject<RegisterUseCase>(usecaseJson)!;
                case "Login":
                    return JsonConvert.DeserializeObject<LoginUseCase>(usecaseJson)!;
                case "Logout":
                    return JsonConvert.DeserializeObject<LogoutUseCase>(usecaseJson)!;
                case "OpenNewStore":
                    return JsonConvert.DeserializeObject<OpenNewStoreUseCase>(usecaseJson)!;                
                case "MakeCoOwner":
                    return JsonConvert.DeserializeObject<MakeCoOwnerUseCase>(usecaseJson)!;
                case "MakeCoManager":
                    return JsonConvert.DeserializeObject<MakeCoManagerUseCase>(usecaseJson)!;
                case "RemoveCoOwner":
                    return JsonConvert.DeserializeObject<RemoveCoOwnerUseCase>(usecaseJson)!;
                case "AddNewProduct":
                    return JsonConvert.DeserializeObject<AddNewProductUseCase>(usecaseJson)!;
                case "ChangeManagerPermission":
                    return JsonConvert.DeserializeObject<ChangeManagerPermissionUseCase>(usecaseJson)!;
            }
            throw new Exception("Unable to parse init file properly, unknown tag");
        }

        internal static void SaveAsFile(string path, ScenarioDTO scenario)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(scenario));
        }
    }
}