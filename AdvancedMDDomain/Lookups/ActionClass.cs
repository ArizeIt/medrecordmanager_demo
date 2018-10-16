using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedMDDomain.Lookups
{
    public class ActionClass
    {
        private ActionClass(string value) { Value = value; }
        public string Value { get; set; }

        public static ActionClass ApiClass => new ActionClass("api");
        public static ActionClass Login => new ActionClass("login");

        public static ActionClass Demographics => new ActionClass("demographics");

        public static ActionClass LookUp => new ActionClass("lookup");

        public static ActionClass ChangeEntry => new ActionClass("chargeentry");

        public static ActionClass Files => new ActionClass("files");

        public static ActionClass Batches=> new ActionClass("batches");

        public static ActionClass Payement=> new ActionClass("paymententry");

        public static ActionClass MasterFile=> new ActionClass("masterfiles");
    }
}
