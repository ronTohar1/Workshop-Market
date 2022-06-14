namespace MarketBackend.ServiceLayer
{
    internal class ScenarioDTO
    {
        public ScenarioDTO(string adminUsername, string adminPassword, IEnumerable<UseCase> useCases)
        {
            AdminUsername = adminUsername;
            AdminPassword = adminPassword;
            UseCases = useCases;
        }

        public string AdminUsername { get; internal set; }
        public string AdminPassword { get; internal set; }
        public IEnumerable<UseCase> UseCases { get; internal set; }
    }
}