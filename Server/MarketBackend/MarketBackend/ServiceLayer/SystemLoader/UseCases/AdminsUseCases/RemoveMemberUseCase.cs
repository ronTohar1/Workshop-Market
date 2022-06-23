namespace MarketBackend.ServiceLayer
{
    internal class RemoveMemberUseCase : UseCase
    {
        public string UserIdRef { get; set; }
        public string TargetIdRef { get; set; }

        public RemoveMemberUseCase(string userIdRef, string targetIdRef, string ret = "_") : base("RemoveMember", ret)
        {
            UserIdRef = userIdRef;
            TargetIdRef = targetIdRef;
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            int targetId = (int)varsEnvironment[TargetIdRef];
            PrintUseCaseStarting();
            var res = systemOperator.GetAdminFacade().Value.RemoveMember(userId, targetId);
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
            return string.Join(',', new object[] { UserIdRef, TargetIdRef });
        }
    }
}