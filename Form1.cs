using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DpiAwarenessTest2
{
    public partial class Form1 : Form
    {
        private enum PROCESS_DPI_AWARENESS
        {
            PROCESS_DPI_UNAWARE = 0,
            PROCESS_SYSTEM_DPI_AWARE = 1,
            PROCESS_PER_MONITOR_DPI_AWARE = 2
        }
        [DllImport("Shcore.dll", ExactSpelling = true)]
        private static extern int SetProcessDpiAwareness(
          PROCESS_DPI_AWARENESS value
        );

        [DllImport("SHCore.dll", ExactSpelling = true)]
        private static extern int GetProcessDpiAwareness(SafeProcessHandle hprocess, out PROCESS_DPI_AWARENESS awareness);

        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(PROCESS_DPI_AWARENESS));
            UpdateDpiAwareness();
        }

        private void UpdateDpiAwareness()
        {
            PROCESS_DPI_AWARENESS dpiAwareness;
            int hr = GetProcessDpiAwareness(Process.GetCurrentProcess().SafeHandle, out dpiAwareness);
            if (hr != 0)
                MessageBox.Show(this, Marshal.GetExceptionForHR(hr).Message, "Error in GetProcessDpiAwareness!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            comboBox1.SelectedItem = dpiAwareness;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int hr = SetProcessDpiAwareness((PROCESS_DPI_AWARENESS)comboBox1.SelectedValue);
            if (hr != 0)
                MessageBox.Show(this, Marshal.GetExceptionForHR(hr).Message, "Error in SetProcessDpiAwareness!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
