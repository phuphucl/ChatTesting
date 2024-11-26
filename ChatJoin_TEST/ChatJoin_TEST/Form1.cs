using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using PLLibrary;

namespace ChatJoin_TEST
{
#pragma warning disable 
    public unsafe partial class Form1 : Form
    {
        List<CWindow> children;
        CProcessWrapper process;
        int childIdx;
        public Form1()
        {
            InitializeComponent();
            CProcessList list = PLLibrary.CProcessList.Processes;
            list = list.RemoveZeroHandle();
            process = list["explorer"]; 
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (children == null)
            {
                CWindow window = new CWindow(process);
                children = window.GetChildWindows();
                childIdx = 0;
            }
            if (childIdx >= children.Count)
            {
                childIdx = 0;
            }
            CWindow childWindow = children[childIdx];
            childIdx++;
            LblName.Text = childIdx + "/" + children.Count + ". " + childWindow.ClassName + ", " + childWindow.Handle + ", " + childWindow.Bounds;
            Rectangle rc = childWindow.Bounds;
            richTextBox1.Text = childWindow.Text;
            pictureBox1.Image = childWindow.Image;

            IntPtr parent = childWindow.Parent;
            childWindow.Parent = richTextBox1.Handle;
            childWindow.Parent = parent;

        }
    }
}
