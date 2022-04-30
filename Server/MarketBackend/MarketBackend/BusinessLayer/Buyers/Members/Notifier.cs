using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketBackend.BusinessLayer.Buyers.Members
{
    public class Notifier
    {
        private Action<string[]> notifyFunc;
         
        public Notifier(Action<string[]> notifyFunc)
        {
            this.notifyFunc = notifyFunc;
        }

        public void notify(string[] notifications)
        {
            this.notifyFunc(notifications);
        }
    }
}
