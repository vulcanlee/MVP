using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaoYaoYao.Events
{
    public class ShowPopupEvent : PubSubEvent<ShowPopupPayload>
    {

    }

    public class ShowPopupPayload
    {
        public bool IsShow { get; set; }=false;
        public bool NeedClosePopup { get; set; } = false;
        public string Target { get; set; }
        public string UpdateMessage { get; set; } = "";
    }
}
