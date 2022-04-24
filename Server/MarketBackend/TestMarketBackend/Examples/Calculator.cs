using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMarketBackend.Examples
{
    public class Calculator
    {

        public bool CheckAddition(int a, int b, int res)
        { return (a + b) == res; }

        public virtual int Add(int a, int b)
        {
            return a + b;
        }


    }
}
