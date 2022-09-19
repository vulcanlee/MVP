using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonForm.Helps
{
    public class MagicHelper
    {
        public string FormTexBox => "Textbox";
        public string FormRocDate => "Rocdate";
        public string FormLabel => "Label";
        public string FormTime24 => "Time24";

        public Color FormViewBackgroundColor { get; set; }
            = Color.FromArgb("FFededed");
    }
}
