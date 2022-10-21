using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NckuhRepair.Helpers
{
    public class MagicHelper
    {
        public readonly string FormName軟體叫修 = "軟體叫修";
        public readonly string FormName硬體叫修 = "硬體叫修";
        public readonly string FormNamePACS叫修 = "PACS叫修";
        public readonly string FormName報告系統叫修 = "報告系統叫修";
        public readonly string FormRecordName = "FormRecord";
        public readonly Color StatusBarBackgroundColor = Color.FromArgb("ff4bb09d");

        public readonly string PageRegistration = "RegistrationPage";
        public readonly string ClearCircleFillImageName = "circleclear_fill.png";
        public readonly string CameraImageName = "camera_fill.png";

        #region 對應表單的欄位名稱
        public string FormIOPanel => "panel";
        public string FormIOTextarea => "textarea";
        public string FormIOTextfield => "textfield";
        public string FormIOSelect => "select";
        public string FormIONumber => "number";
        public string FormIOPassword => "password";
        public string FormIOCheckbox => "checkbox";
        public string FormIOSelectboxes => "selectboxes";
        public string FormIORadio => "radio";
        public string FormIODay => "day";
        public string FormIOTime => "time";
        public string FormIOCamera => "camera";
        public string FormIOFile => "file";
        #endregion

        #region 表單會用到個全域屬性物件
        public double DefaultFontSize { get; set; } = 13;
        public double DefaultPanelTitleFontSize { get; set; } = 16;

        public Color FormViewBackgroundColor { get; set; }
            = Color.FromArgb("FFededed");
        public Color FormEntryBackgroundColor { get; set; }
            = Color.FromArgb("bb888888");
        public Color FormPanelTextColor { get; set; }
            = Color.FromArgb("858585");
        public Color DisableColor { get; set; }
            = Color.FromArgb("d1d1d1");
        #endregion
    }
}
