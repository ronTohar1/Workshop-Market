using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Buyers.Members
{
    // 1.5, 1.6
    public class Notifier
    {
        private Func<string[],bool> notifyFunc;
         
        public Notifier(Func<string[], bool> notifyFunc)
        {
            this.notifyFunc = notifyFunc;
        }


        public bool tryToNotify(string[] notifications)
        {
            return this.notifyFunc(notifications);
        }
    }
}
