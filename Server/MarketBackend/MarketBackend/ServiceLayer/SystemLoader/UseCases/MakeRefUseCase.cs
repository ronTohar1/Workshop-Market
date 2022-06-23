namespace MarketBackend.ServiceLayer
{
    internal class MakeRefUseCase<T> : UseCase
    {
        public T Val { get; set; }
        public MakeRefUseCase(T val, string ret) : base("MakeRef", ret)
        {
            Val = val;
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            varsEnvironment[Ret] = Val!;
            return true;
        }

        internal override string ArgsToString()
        {
            return string.Join(',', new object[] { Val });
        }
    }
}
