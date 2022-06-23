namespace MarketBackend.ServiceLayer
{
    internal class PurchaseCartUseCase : UseCase
    {
        public string UserIdRef { get; set; }
        public string CardNumber { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string HolderName { get; set; }
        public string CCV { get; set; }
        public string HolderIdNumber { get; set; }
        public string ReceiverName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }

        public PurchaseCartUseCase(string userIdRef, string cardNumber, string month, string year, string holderName, string cCV, string holderIdNumber, string receiverName, string address, string city, string country, string zip, string ret = "_") : base("PurchaseCart", ret)
        {
            UserIdRef = userIdRef;
            CardNumber = cardNumber;
            Month = month;
            Year = year;
            HolderName = holderName;
            CCV = cCV;
            HolderIdNumber = holderIdNumber;
            ReceiverName = receiverName;
            Address = address;
            City = city;
            Country = country;
            Zip = zip;
        }

        internal override bool Apply(SystemOperator systemOperator, IDictionary<string, object> varsEnvironment)
        {
            int userId = (int)varsEnvironment[UserIdRef];
            PrintUseCaseStarting();
            var res = systemOperator.GetBuyerFacade().Value.PurchaseCartContent(userId,
                                                                                new ServiceDTO.ServicePaymentDetails(CardNumber, Month, Year, HolderName, CCV, HolderIdNumber),
                                                                                new ServiceDTO.ServiceSupplyDetails(ReceiverName, Address, City, Country, Zip));
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
            return string.Join(',', new object[] { UserIdRef, CardNumber, Month, Year, HolderName, CCV, HolderIdNumber, ReceiverName, Address, City, Country, Zip });
        }
    }
}