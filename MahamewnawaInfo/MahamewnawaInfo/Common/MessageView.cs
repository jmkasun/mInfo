using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MahamewnawaInfo.Common
{
    class MessageView
    {
        // //////////////
        internal static void ShowMsg(string p,Point location)
        {
            new frmMsgOkOnly(p, MessageBoxIcon.Information,location);
        }

        internal static void ShowMsg(string p)
        {
            new frmMsgOkOnly(p, MessageBoxIcon.Information);
        }

        // ///////////////
        internal static void ShowWarningMsg(string p, Point location)
        {
            new frmMsgOkOnly(p, MessageBoxIcon.Warning, location);
        }

        internal static void ShowWarningMsg(string p)
        {
            new frmMsgOkOnly(p, MessageBoxIcon.Warning);
        }


        // //////////////
        internal static void ShowErrorMsg(string p, Point location)
        {
            Form f = new frmMsgOkOnly(p, MessageBoxIcon.Error, location);
        }

        internal static void ShowErrorMsg(string p)
        {
            Form f = new frmMsgOkOnly(p, MessageBoxIcon.Error);
        }


        // //////////////
        internal static DialogResult ShowQuestionMsg(string p, Point location)
        {
            Form f = new frmMsgYesNo(p, MessageBoxIcon.Question, location);

            return f.DialogResult;
           // return MessageBox.Show(p,"Warning",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
        }

        internal static DialogResult ShowQuestionMsg(string p)
        {
            Form f = new frmMsgYesNo(p, MessageBoxIcon.Question);

            return f.DialogResult;
            // return MessageBox.Show(p,"Warning",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
        }


        // ///////////////
        internal static void ExceptionError(Exception ex, Point location)
        {
            Utility.WriteWrrorLog(ex);

            new frmMsgOkOnly(ex.Message, MessageBoxIcon.Error, location);
        }

        internal static void ExceptionError(Exception ex)
        {
            Utility.WriteWrrorLog(ex);
            new frmMsgOkOnly(ex.Message, MessageBoxIcon.Error);
        }


    }
}
