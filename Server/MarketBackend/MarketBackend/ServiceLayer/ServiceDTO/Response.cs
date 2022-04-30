using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    internal class Response<T>
    {
        public T? Value { get; set; }
        public string ErrorMessage { get; set; }


        public Response()
        {
            Value = default(T);
            ErrorMessage = string.Empty;
        }

        public Response(T? value)
        {
            Value = value;
            ErrorMessage = string.Empty;
        }

        public Response(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Value = default(T);
        }

        public Response(T? value, string errorMessage) : this(value)
        {
            ErrorMessage = errorMessage;
        }

     }

}
