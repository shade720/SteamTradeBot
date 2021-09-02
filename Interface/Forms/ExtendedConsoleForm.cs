using System;
using System.Windows.Forms;

namespace Interface.Forms
{
    public partial class ExtendedConsoleForm : Form
    {
        const int WM_NCHITTEST = 0x84;
        const int HTCAPTION = 2;
        const int HTCLIENT = 1;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
                m.Result = (IntPtr)HTCAPTION;
        }
        public ExtendedConsoleForm()
        {
            InitializeComponent();
        }

        private void HideButton_Click(object sender, System.EventArgs e)
        {
            Hide();
        }
    }
}
