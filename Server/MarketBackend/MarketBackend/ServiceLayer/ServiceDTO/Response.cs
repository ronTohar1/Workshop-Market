using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.ServiceLayer.ServiceDTO
{
    public class Response<T>
    {
        public T Value { get; set; }
        public string ErrorMessage { get; set; }

        public bool ErrorOccured { get; set; } = false; 

        public Response() =>
            ErrorMessage = string.Empty;

        public Response(T value) : this() =>
            Value = value;

        public Response(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ErrorOccured = true;
        }
        public Response(T value, string errorMessage) : this(value)
        {
            ErrorMessage = errorMessage;
            ErrorOccured = true;

        }

        public bool IsErrorOccured() =>
            ErrorMessage != string.Empty;

     }

}
