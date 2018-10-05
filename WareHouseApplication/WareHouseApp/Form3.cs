using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WareHouseApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void vamvakera_Click(object sender, EventArgs e)
        {
            Form1 m = new Form1();
            //  Cursor.Current = Cursors.WaitCursor;
            //   Application.DoEvents();
            m.Closed += (s, args) => this.Visible = true;

            m.Show();
           // this.Close();
            this.Visible = false;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
            //Application.Exit();
        }





    }




}
