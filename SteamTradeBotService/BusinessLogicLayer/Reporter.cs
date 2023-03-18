using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBotService.BusinessLogicLayer
{
    public class Reporter
    {
        public delegate void Report<in T>(T msg);
        public event Report<string> WriteMessageEvent;
        public event Report<double> WriteBalanceEvent;

        public void WriteMessage(string msg)
        {
            WriteMessageEvent?.Invoke(msg);
        }

        public void WriteBalance(double bal)
        {
            WriteBalanceEvent?.Invoke(bal);
        }
    }
}
