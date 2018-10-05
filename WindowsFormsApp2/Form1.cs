using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp2
{


    public partial class Form1 : Form
    {
        // private string order = "ColourName";

        private static Production production = new Production();

        private static IEnumerable<Vamvakera> vamvakera = from Vamvakera in production.Vamvakera
                                                          orderby Vamvakera.ColourNo ascending
                                                          select Vamvakera;

        private static IEnumerable<WharehouseChanges> wharehouseChanges = from WharehouseChanges in production.WharehouseChanges
                                                                          select WharehouseChanges;


        List<Vamvakera> listvamvakera = vamvakera.ToList();

        private bool saved = true;

        private BackgroundWorker bgWorker = new BackgroundWorker();

        public Form1()
        {
            InitializeComponent();

            CreateBox();

            //  bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            //  bgWorker.RunWorkerAsync();    

            this.ActiveControl = number;


        }

        /*
                private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
                {
                    //Do all of your work here

                    createbox();


                   // bgWorker.ReportProgress(); //percent done calculation  
                }

          */



        private void CreateBox()
        {
            int rowCounter = 0;

            foreach (Vamvakera vamvakera in listvamvakera)
            {
                CreateInitialTextboxesAndAddToPanel(rowCounter, vamvakera);
                int columnCounter = 0;
                Nullable<int> firstColumnName = null;
                Nullable<int> lastColumnName = null;
                while (columnCounter < 8)
                {
                    SelectColumn(vamvakera, columnCounter, ref firstColumnName, ref lastColumnName);
                    CreateLastTextboxesAndAddToPanel(rowCounter, columnCounter, firstColumnName, lastColumnName);
                    columnCounter++;
                }
                rowCounter++;
            }
        }

        #region CreateBoxHelpers
        private void CreateInitialTextboxesAndAddToPanel(int rowCounter, Vamvakera vamvakera)
        {
            TextBox[] txtcol = new TextBox[100];
            TextBox[] txtcolname = new TextBox[100];


            var txt = new TextBox();
            txtcol[rowCounter] = txt;
            txt.Name = "col" + rowCounter;
            txt.Text = vamvakera.ColourNo;
            txt.Location = new Point(10, 0 + (rowCounter * 20));
            txt.Margin = new Padding(0, 0, 0, 0);
            txt.TextAlign = HorizontalAlignment.Right;
            txt.Width = 25;
            txt.Visible = true;
            txt.ReadOnly = true;
            panel.Controls.Add(txt);

            var name = new TextBox();
            txtcol[rowCounter] = name;
            name.Name = "col" + rowCounter;
            name.Text = vamvakera.ColourName;
            name.Location = new Point(35, 0 + (rowCounter * 20));
            name.Margin = new Padding(0, 0, 0, 0);
            name.Width = 110;
            name.Visible = true;
            name.ReadOnly = true;
            panel.Controls.Add(name);
        }
        private static void SelectColumn(Vamvakera vamvakera, int columnCounter, ref int? firstColumnName, ref int? lastColumnName)
        {
            switch (columnCounter)
            {
                case 0:
                    firstColumnName = vamvakera.B140first;
                    lastColumnName = vamvakera.B140last;
                    break;
                case 1:
                    firstColumnName = vamvakera.B120first;
                    lastColumnName = vamvakera.B120last;
                    break;
                case 2:
                    firstColumnName = vamvakera.O130first;
                    lastColumnName = vamvakera.O130last;
                    break;
                case 3:
                    firstColumnName = vamvakera.B260first;
                    lastColumnName = vamvakera.B260last;
                    break;
                case 4:
                    firstColumnName = vamvakera.B250first;
                    lastColumnName = vamvakera.B250last;
                    break;
                case 5:
                    firstColumnName = vamvakera.B240first;
                    lastColumnName = vamvakera.B240last;
                    break;
                case 6:
                    firstColumnName = vamvakera.B234first;
                    lastColumnName = vamvakera.B234last;
                    break;
                case 7:
                    firstColumnName = vamvakera.B280first;
                    lastColumnName = vamvakera.B280last;
                    break;
                default:
                    throw new Exception("Invalid Column Counter");
            }
        }

        private void CreateLastTextboxesAndAddToPanel(int rowCounter, int columnCounter, int? firstColumnName, int? lastColumnName)
        {
            TextBox[] txtfst = new TextBox[100];
            TextBox[] txtlst = new TextBox[100];
            TextBox[] txtbnum = new TextBox[100];
            TextBox col = panel.Controls.Find("col" + rowCounter.ToString(), true).FirstOrDefault() as TextBox;

            TextBox fst = CreateFirstTextbox(rowCounter, columnCounter, firstColumnName, txtfst);
            TextBox lst = CreateSecondTextbox(rowCounter, columnCounter, lastColumnName, txtlst);
            TextBox bnum = CreateBNumTextbox(rowCounter, columnCounter, txtbnum, fst, lst);
            panel.Controls.Add(bnum);
            panel.Controls.Add(fst);
            panel.Controls.Add(lst);
        }

        private TextBox CreateBNumTextbox(int rowCounter, int columnCounter, TextBox[] txtbnum, TextBox fst, TextBox lst)
        {
            var bnum = new TextBox();
            txtbnum[rowCounter] = bnum;
            bnum.Name = "bnum" + rowCounter + columnCounter;
            if (!string.IsNullOrEmpty(lst.Text) && !string.IsNullOrEmpty(fst.Text))
            {
                if (Int32.Parse(fst.Text) != 0)
                    bnum.Text = (Int32.Parse(lst.Text) - Int32.Parse(fst.Text) + 1).ToString();
                else
                    bnum.Text = "0";
            }
            else
                bnum.Text = "";
            bnum.Location = new Point((155 + (columnCounter * 120)), 0 + (rowCounter * 20));
            bnum.Margin = new Padding(0, 0, 0, 0);
            bnum.Width = 30;
            bnum.TextAlign = HorizontalAlignment.Center;
            bnum.Visible = true;
            bnum.ReadOnly = true;
            if (string.IsNullOrEmpty(bnum.Text))
                bnum.BackColor = Color.Gray;
            else
                bnum.BackColor = Color.White;
            bnum.KeyPress += new KeyPressEventHandler(first_KeyPress);
            return bnum;
        }

        private TextBox CreateSecondTextbox(int rowCounter, int columnCounter, int? lastColumnName, TextBox[] txtlst)
        {
            var lst = new TextBox();
            txtlst[rowCounter] = lst;
            lst.Name = "lst" + rowCounter + columnCounter;
            lst.Text = lastColumnName.ToString();
            lst.Location = new Point((215 + (columnCounter * 120)), 0 + (rowCounter * 20));
            lst.Margin = new Padding(0, 0, 0, 0);
            lst.Width = 30;
            lst.TextAlign = HorizontalAlignment.Center;
            lst.Visible = true;
            lst.ReadOnly = false;
            if (string.IsNullOrEmpty(lst.Text))
                lst.BackColor = Color.Gray;
            else
                lst.BackColor = Color.White;
            lst.Leave += new EventHandler(OnTextChanged);
            lst.KeyPress += new KeyPressEventHandler(last_KeyPress);
            return lst;
        }

        private TextBox CreateFirstTextbox(int rowCounter, int columnCounter, int? firstColumnName, TextBox[] txtfst)
        {
            var fst = new TextBox();
            txtfst[rowCounter] = fst;
            fst.Name = "fst" + rowCounter + columnCounter;
            fst.Text = firstColumnName.ToString();
            fst.Location = new Point((185 + (columnCounter * 120)), 0 + (rowCounter * 20));
            fst.Margin = new Padding(0, 0, 0, 0);
            fst.Width = 30;
            fst.TextAlign = HorizontalAlignment.Center;
            fst.Visible = true;
            fst.ReadOnly = false;
            if (string.IsNullOrEmpty(fst.Text))
                fst.BackColor = Color.Gray;
            else
                fst.BackColor = Color.White;
            fst.Leave += new EventHandler(OnTextChanged);
            fst.KeyPress += new KeyPressEventHandler(first_KeyPress);
            return fst;
        }
        #endregion

        void OnTextChanged(object sender, EventArgs e)
        {
            //  last_KeyPress();

            if (sender.GetType() == typeof(TextBox))
            {
                // o arithmos tou xrwmatos
                string num = ((TextBox)sender).Name;
                string number = ((TextBox)sender).Name;

                // to an einai fst h lst
                string name = string.Concat(num.TakeWhile(char.IsLetter));
                string yarntype;
                int value;

                num = num.Substring(3, num.Length - 3);
                int nums = Int32.Parse(num) / 10;
                string numsf;
                numsf = nums.ToString();
                yarntype = num.Substring(num.Length - 1);

                yarntype = yarntype.TranslateYarnType();

                TextBox bnum = panel.Controls.Find("bnum" + num, true).FirstOrDefault() as TextBox;
                //bnum.Text = "found!";
                TextBox fst = panel.Controls.Find("fst" + num, true).FirstOrDefault() as TextBox;
                TextBox lst = panel.Controls.Find("lst" + num, true).FirstOrDefault() as TextBox;

                bnum.Text = adding(fst.Text, lst.Text);
                bnum.BackColor = Color.White;
                fst.BackColor = Color.White;
                lst.BackColor = Color.White;

                TextBox col = panel.Controls.Find("col" + numsf, true).FirstOrDefault() as TextBox;
                string cols = col.Text;

                if (name.Equals("fst"))
                {
                    if (!string.IsNullOrEmpty(fst.Text))
                        value = Int32.Parse(fst.Text);
                    else
                        value = 0;

                    Tempsave(yarntype, value, cols, "First");
                    yarntype = yarntype + "first";
                    Save(yarntype, value, cols);
                }
                else
                {
                    if (!string.IsNullOrEmpty(lst.Text))
                        value = Int32.Parse(lst.Text);
                    else
                        value = 0;

                    Tempsave(yarntype, value, cols, "Last");
                    yarntype = yarntype + "last";
                    Save(yarntype, value, cols);

                }
            }
        }

        private void Tempsave(string yarntype, int value, string col, string fstlst)
        {

            WharehouseChanges change = new WharehouseChanges() { time = DateTime.Now, yarntype = yarntype, colorno = col, fstlst = fstlst, boxnum = value };
            production.WharehouseChanges.InsertOnSubmit(change);
            // production.SubmitChanges();

        }


        private void Save(string yarntype, int value, string col)
        {
            //Puts the updated value in the db
            IEnumerable<Vamvakera> vamvakerafilter = from Vamvakera in production.Vamvakera
                                                     where Vamvakera.ColourNo == col
                                                     select Vamvakera;
            saved = false;
            vamvakera = vamvakerafilter.Select(c =>
                    { c.GetType().GetProperties().Single(x => x.Name.Equals(yarntype)).SetValue(c, value, null); return c; })
                    .ToList();
        }


        private string adding(string first, string last)
        {

            int First = 0;
            int Last = 0;
            string Number;

            if (first != "")
            {
                First = Int32.Parse(first);
            }
            else
            {
                First = 0;
                return "";
            }

            if (last != "")
            {
                Last = Int32.Parse(last);
            }
            else
            {
                Last = 0;
                return "";
            }
            if (First == 0)
            {
                Number = "0";
                //number.Text = Number;
            }
            else if (Last - First >= 0)
            {
                Number = (Last - First + 1).ToString();
            }
            else
            {
                //number.Text = "0";
                Number = "0";
            }

            return Number;

        }

        private void first_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)13)
            {
                // Enter key pressed
                SelectNextControl(ActiveControl, false, false, false, false);

                e.Handled = true;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                // ESC key pressed
                SelectNextControl(ActiveControl, false, false, false, false);
                e.Handled = true;
                // SendKeys.Send("{TAB}");
            }

        }



        private void last_KeyPress(object sender, KeyPressEventArgs e)
        {


            TextBox textBox = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            if (e.KeyChar == (char)13)
            {


                // Enter key pressed
                SelectNextControl(ActiveControl, false, false, false, false);
                e.Handled = true;
                // SendKeys.Send("{TAB}");



            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                // ESC key pressed
                SelectNextControl(ActiveControl, false, false, false, false);
                e.Handled = true;
                // SendKeys.Send("{TAB}");
            }


        }

        private void first_TextChanged(object sender, EventArgs e)
        {
            number.Text = adding(first.Text, last.Text);

        }

        private void last_TextChanged(object sender, EventArgs e)
        {
            number.Text = adding(first.Text, last.Text);
        }






        private void first1_TextChanged(object sender, EventArgs e)
        {
            number1.Text = adding(first1.Text, last1.Text);
        }

        private void last1_TextChanged(object sender, EventArgs e)
        {
            number1.Text = adding(first1.Text, last1.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=WALK-SERVER;Initial Catalog=PRODUCTION;Persist Security Info=True;Integrated Security=true;";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                MessageBox.Show("Connection Open ! ");
                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex.ToString());
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            production.SubmitChanges();
            saved = true;
            MessageBox.Show("Saved");

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!saved)
            {
                var window = MessageBox.Show(
                 "Close without saving?",
                 "Are you sure?",
                 MessageBoxButtons.YesNo);

                e.Cancel = (window == DialogResult.No);
            }

        }
    }



}
