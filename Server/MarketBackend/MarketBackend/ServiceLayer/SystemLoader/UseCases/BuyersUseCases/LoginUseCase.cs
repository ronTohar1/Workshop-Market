namespace MarketBackend.ServiceLayer
{
    internal class LoginUseCase : UseCase
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginUseCase(string username, string password, string ret = "_") : base("Login", ret)
        {
            Username = username;
            Password = password;
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            PrintUseCaseStarting();
            var res = systemOperator.GetBuyerFacade().Value.Login(Username, Password, _ => true);
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
            return string.Join(',', new object[] { Username, Password });
        }
    }
}
