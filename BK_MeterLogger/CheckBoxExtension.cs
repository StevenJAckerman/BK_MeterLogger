using System.Reflection;
using System.Windows.Forms;

namespace BK_MeterLogger
{
    // CheckBox extension method to set the checked/unchecked state w/o causing
    // a CheckChanged event to occur
    public static class CheckBoxExtension
    {
        public static void SetChecked(this CheckBox chBox, bool check)
        {
            typeof(CheckBox).GetField("checkState", BindingFlags.NonPublic |
                                                    BindingFlags.Instance)
                .SetValue(chBox, check ? CheckState.Checked :
                    CheckState.Unchecked);
            chBox.Invalidate();
        }
    }
}