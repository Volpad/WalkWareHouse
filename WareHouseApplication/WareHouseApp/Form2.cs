using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WareHouseApp
{
    public partial class Form2 : Form
    {
        private string order = "ColourName";

        public Form2()
        {
            InitializeComponent();

            //  string fb;
            //   string lb;
            createbox();
           




            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=WALK-SERVER;Initial Catalog=PRODUCTION;Persist Security Info=True;Integrated Security=true;";
            cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex.ToString());
            }

            string yarn = "B140first";
            string colno = "00";
            first.Text = Sqlread(yarn, colno);

        }

        private void createbox()
        {
            int num;

            string connetionString = null;
            SqlConnection cnn;
            connetionString = "Data Source=WALK-SERVER;Initial Catalog=PRODUCTION;Persist Security Info=True;Integrated Security=true;";
            cnn = new SqlConnection(connetionString);
            try
            {

                SqlCommand cmd = new SqlCommand("SELECT count(ColourNo) FROM Wharehouse", cnn);

                cnn.Open();

                num = (Int32)cmd.ExecuteScalar();
              //  MessageBox.Show("num= " + num);


                string query = "SELECT ColourNo, ColourName FROM Wharehouse ORDER BY "+ order;
                using (SqlCommand command = new SqlCommand(query, cnn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int i = 0;
                        TextBox[] txtcol = new TextBox[num];
                        TextBox[] txtcolname = new TextBox[num];


                        while (reader.Read())
                        {

                                var txt = new TextBox();
                                txtcol[i] = txt;
                                txt.Name = "col"+i;                       
                                txt.Text = reader["ColourNo"].ToString();
                                txt.Location = new Point(10, 0 + (i * 20));
                                txt.Margin = new Padding (0, 0, 0, 0);
                                txt.TextAlign = HorizontalAlignment.Right;
                                txt.Width = 25;
                                txt.Visible = true;
                                txt.ReadOnly = true;
                                panel.Controls.Add(txt);

                                var name = new TextBox();
                                txtcol[i] = name;
                                name.Name = "col" + i;
                                name.Text = reader["ColourName"].ToString();
                                name.Location = new Point(35, 0 + (i * 20));
                                name.Margin = new Padding(0, 0, 0, 0);
                                name.Width = 110;
                                name.Visible = true;
                                name.ReadOnly = true;
                                panel.Controls.Add(name);


                            int z = 0;

                            while (z < 8)
                            {

                                yarnlist(i, num, z);

                                z++;
                            }







                            i++;
                        }
                    }
                }


                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex.ToString());
            }

        }

        void yarnlist(int i, int num, int z)
        {
            
            TextBox[] txtfst = new TextBox[num];
            TextBox[] txtlst = new TextBox[num];
            TextBox[] txtbnum = new TextBox[num];

            
            string yarnf = null;
            string yarnl = null;


            if (z == 0)
            {
                yarnf = "B140first";
                yarnl = "B140last";
            }
            else if(z == 1)
            {
                yarnf = "B120first";
                yarnl = "B120last";
            }
            else if (z == 2)
            {
                yarnf = "O130first";
                yarnl = "O130last";
            }
            else if (z == 3)
            {
                yarnf = "B260first";
                yarnl = "B260last";
            }
            else if (z == 4)
            {
                yarnf = "B250first";
                yarnl = "B250last";
            }
            else if (z == 5)
            {
                yarnf = "B240first";
                yarnl = "B240last";
            }
            else if (z == 6)
            {
                yarnf = "B234first";
                yarnl = "B234last";
            }
            else if (z == 7)
            {
                yarnf = "B280first";
                yarnl = "B280last";
            }


            TextBox col = panel.Controls.Find("col" + i.ToString(), true).FirstOrDefault() as TextBox; ;

            var fst = new TextBox();
            txtfst[i] = fst;
            fst.Name = "fst" + i  + z;
            fst.Text = Sqlread(yarnf,  col.Text);
            fst.Location = new Point((185 + (z*120)), 0 + (i * 20));
            fst.Margin = new Padding(0, 0, 0, 0);
            fst.Width = 30;
            fst.TextAlign = HorizontalAlignment.Center;
            fst.Visible = true;
            fst.ReadOnly = false;
            if (string.IsNullOrEmpty(fst.Text))
            {
                fst.BackColor = Color.Gray;
            }
            else
            {
                fst.BackColor = Color.White;
            }
            fst.TextChanged += new EventHandler(lst_TextChanged);
            panel.Controls.Add(fst);


            var lst = new TextBox();
            txtlst[i] = lst;
            lst.Name = "lst" + i  + z;
            lst.Text = Sqlread(yarnl, col.Text);
            lst.Location = new Point((215 + (z * 120)), 0 + (i * 20));
            lst.Margin = new Padding(0, 0, 0, 0);
            lst.Width = 30;
            lst.TextAlign = HorizontalAlignment.Center;
            lst.Visible = true;
            lst.ReadOnly = false;
            if (string.IsNullOrEmpty(lst.Text))
            {
                lst.BackColor = Color.Gray;
            }
            else
            {
                lst.BackColor = Color.White;
            }
            lst.TextChanged += new EventHandler(lst_TextChanged);
            panel.Controls.Add(lst);



            var bnum = new TextBox();
            txtbnum[i] = bnum;
            bnum.Name = "bnum" + i + z;
            if (!string.IsNullOrEmpty(lst.Text) && !string.IsNullOrEmpty(fst.Text))
            {
                bnum.Text = (Int32.Parse(lst.Text) - Int32.Parse(fst.Text)).ToString();
            }
            else
            {
                bnum.Text = "";
            }

            bnum.Location = new Point((155 + (z * 120)), 0 + (i * 20));
            bnum.Margin = new Padding(0, 0, 0, 0);
            bnum.Width = 30;
            bnum.TextAlign = HorizontalAlignment.Center;
            bnum.Visible = true;
            bnum.ReadOnly = true;
            if (string.IsNullOrEmpty(bnum.Text))
            {
                bnum.BackColor = Color.Gray;
            }
            else
            {
                bnum.BackColor = Color.White;
            }
            panel.Controls.Add(bnum);

        }

        void lst_TextChanged (object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(TextBox))
            {
                // o arithmos tou xrwmatos
                string num = ((TextBox)sender).Name;
                // to an einai fst h lst
                string name = string.Concat(num.TakeWhile(char.IsLetter));
                string yarntype;
                int value;

                num = num.Substring(3, num.Length - 3);
                int nums = Int32.Parse(num) / 10;
                string numsf;

                if (nums < 10)
                {
                    numsf = "0"+ nums.ToString();
                }
                else
                {
                    numsf = nums.ToString();
                }

                yarntype = num.Substring(num.Length - 1);

                if (yarntype.Equals("0"))
                {
                    yarntype = "B140";
                }else if (yarntype.Equals("1"))
                {
                    yarntype = "B120";
                }
                else if (yarntype.Equals("2"))
                {
                    yarntype = "O130";
                }
                else if (yarntype.Equals("3"))
                {
                    yarntype = "B260";
                }
                else if (yarntype.Equals("4"))
                {
                    yarntype = "B250";
                }
                else if (yarntype.Equals("5"))
                {
                    yarntype = "B240";
                }
                else if (yarntype.Equals("6"))
                {
                    yarntype = "B234";
                }
                else if (yarntype.Equals("7"))
                {
                    yarntype = "B280";
                }


                






                TextBox bnum = panel.Controls.Find("bnum"+num, true).FirstOrDefault() as TextBox;
                //bnum.Text = "found!";
                TextBox fst = panel.Controls.Find("fst" + num, true).FirstOrDefault() as TextBox;
                TextBox lst = panel.Controls.Find("lst" + num, true).FirstOrDefault() as TextBox;
                
                bnum.Text =  adding(fst.Text, lst.Text);
                bnum.BackColor = Color.White;
                fst.BackColor = Color.White;
                lst.BackColor = Color.White;


                if (name.Equals("fst"))
                {
                    yarntype = yarntype + "first";

                    if (!string.IsNullOrEmpty(fst.Text))
                    {
                        value = Int32.Parse(fst.Text);
                    }
                    else
                    {
                        value = 0;
                    }

                }
                else
                {
                    yarntype = yarntype + "last";

                    if (!string.IsNullOrEmpty(lst.Text))
                    {
                        value = Int32.Parse(lst.Text);
                    }
                    else
                    {
                        value = 0;
                    }
                }






                



                string connetionString = null;
                SqlConnection cnn;
                connetionString = "Data Source=WALK-SERVER;Initial Catalog=PRODUCTION;Persist Security Info=True;Integrated Security=true;";
                cnn = new SqlConnection(connetionString);
                try
                {
                    string oString = "UPDATE Wharehouse SET "+yarntype+ " = @value WHERE ColourNo = @num";
                    SqlCommand oCmd = new SqlCommand(oString, cnn);

                    oCmd.Parameters.AddWithValue("@num", numsf);
                    if (string.IsNullOrEmpty(lst.Text) && string.IsNullOrEmpty(fst.Text))
                    {
                        oCmd.Parameters.AddWithValue("@value", System.Data.SqlTypes.SqlInt32.Null);
                    }
                    else
                    {
                        oCmd.Parameters.AddWithValue("@value", value);
                    }
                   // MessageBox.Show("name= "+name + " value= "+value + " num= "+num);
                    cnn.Open();

                    oCmd.ExecuteNonQuery();

                    cnn.Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Can not open connection ! " + ex.ToString());
                    cnn.Close();
                }


            }
        }



        private string Sqlread(string yarn, string colno)
        {

            string connetionString = null;
            string result = null;
            SqlConnection cnn;
            connetionString = "Data Source=WALK-SERVER;Initial Catalog=PRODUCTION;Persist Security Info=True;Integrated Security=true;";
            cnn = new SqlConnection(connetionString);
           

            try
            {
                string oString = "SELECT " + yarn + " FROM Wharehouse WHERE ColourNo= @Colno ";

   //             MessageBox.Show("ostring ! " + oString);

                SqlCommand oCmd = new SqlCommand(oString, cnn);
                oCmd.Parameters.AddWithValue("@Yarn", yarn);
                oCmd.Parameters.AddWithValue("@Colno", colno);
                cnn.Open();
                SqlDataReader oReader = oCmd.ExecuteReader();
              
                    while (oReader.Read())
                    {
  //                  MessageBox.Show("after While=  " + oReader[yarn].ToString());
                    result = oReader[yarn].ToString();

                    
  //                  first.Text = oReader[yarn].ToString();


                    }

                cnn.Close();
                return result;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex.ToString());
                cnn.Close();
            }

            return null;
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
            }

            if (last != "")
            {
                Last = Int32.Parse(last);
            }
            else
            {
                Last = 0;
            }
            if (Last - First > 0)
            {
                Number = (Last - First).ToString();
                //number.Text = Number;
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

        }



        private void last_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void first_TextChanged(object sender, EventArgs e)
        {
           number.Text = adding(first.Text, last.Text);

        }

        private void last_TextChanged(object sender, EventArgs e)
        {
           number.Text =  adding(first.Text, last.Text);
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



    }

    
}
