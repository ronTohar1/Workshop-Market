namespace MarketBackend.ServiceLayer
{
    internal class LogoutUseCase : UseCase
    {
        public string UserIdRef { get; set; }

        public LogoutUseCase(string userIdRef, string ret = "_") : base("Logout", ret)
        {
            UserIdRef = userIdRef;
        }

        internal override void Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            PrintUseCaseStarting();
            var res = systemOperator.GetBuyerFacade().Value.Logout(userId);
            if (res.IsErrorOccured())
                Console.WriteLine("Unable to perform: " + this + "\nError message: " + res.ErrorMessage);
            else
            {
                varsEnvironment[Ret] = res.Value;
                Console.WriteLine("Success");
            }
        }

        internal override string ArgsToString()
        {
            return string.Join(',', new object[] { UserIdRef });
        }
    }
}