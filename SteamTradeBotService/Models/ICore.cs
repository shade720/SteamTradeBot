using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    public interface ICore
    {
        void Notify(object sender, string ev);
    }
}
