namespace MarketBackend.ServiceLayer
{
    internal class LeaveUseCase : UseCase
    {
        public string UserIdRef { get; set; }
        public LeaveUseCase(string userIdRef, string ret = "_") : base("Leave", ret)
        {
            UserIdRef = userIdRef;
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            PrintUseCaseStarting();
            var res = systemOperator.GetBuyerFacade().Value.Leave(userId);
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
            return string.Join(',', new object[] { UserIdRef });
        }
    }
}