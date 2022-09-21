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
        public string Checkbox => "Checkbox";
        public string Selector => "Selector";
        public string Dropdown => "Dropdown";
        public string Textarea => "Textarea";
        public string FormGrid => "Grid";

        public double DefaultFontSize { get; set; } = 13;

        public Color FormViewBackgroundColor { get; set; }
            = Color.FromArgb("FFededed");
        public Color FormEntryBackgroundColor { get; set; }
            = Color.FromArgb("bb888888");
    }
}
