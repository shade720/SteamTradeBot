using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SteamTradeBotService.Models
{
    class Base
    {
        protected ICore _core;

        public Base(ICore mediator = null)
        {
            this._core = mediator;
        }

        public void SetMediator(ICore mediator)
        {
            this._core = mediator;
        }
    }
}
