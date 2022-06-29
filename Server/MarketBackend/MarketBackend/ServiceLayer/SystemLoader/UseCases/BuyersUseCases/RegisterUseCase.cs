namespace MarketBackend.ServiceLayer
{
    internal class RegisterUseCase : UseCase
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public RegisterUseCase(string username, string password, string ret = "_") : base("Register", ret)
        {
            Username = username;
            Password = password;
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            PrintUseCaseStarting();
            var res = systemOperator.GetBuyerFacade().Value.Register(Username, Password);
            if (res.IsErrorOccured())
                Console.WriteLine("Unable to perform: " + this + "\nError message: " + res.ErrorMessage);
            else
            {
                varsEnvironment[Ret] = res.Value;
                Console.WriteLine($"Success: {Ret} = {varsEnvironment[Ret]}");
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
