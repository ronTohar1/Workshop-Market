namespace MarketBackend.ServiceLayer
{
    internal abstract class UseCase
    {
        public string Tag { get; set; }
        public string Ret { get; set; }

        public UseCase(string tag, string ret = "_")
        {
            Tag = tag;
            Ret = ret;
        }

        abstract internal bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment);
        abstract internal string ArgsToString();

        protected void PrintUseCaseStarting() => Console.WriteLine(">>> " + this);

        public override string ToString() => (Ret != "_" ? Ret + " = " : " ") + Tag + $"({ArgsToString()})";

    }
}
