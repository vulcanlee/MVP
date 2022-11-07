using NckuhRepair.Helpers.WebAPIs;
using NckuhRepair.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NckuhRepair.Services
{
    public class FormItemService : BaseWebAPI<FormIOModel>
    {
        public FormItemService()
            : base()
        {
            SetDefaultPersistentBehavior();
        }

        void SetDefaultPersistentBehavior()
        {
            ApiResultIsCollection = true;
            PersistentStorage = ApiResultIsCollection ? PersistentStorage.Collection : PersistentStorage.Single;
        }
    }
}
