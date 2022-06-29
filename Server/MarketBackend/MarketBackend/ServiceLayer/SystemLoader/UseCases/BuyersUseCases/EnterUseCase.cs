namespace MarketBackend.ServiceLayer
{
    internal class EnterUseCase : UseCase
    {
        public EnterUseCase(string ret = "_") : base("Enter", ret)
        {
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            PrintUseCaseStarting();
            var res = systemOperator.GetBuyerFacade().Value.Enter();
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
            return string.Join(',', new object[] { });
        }
    }
}