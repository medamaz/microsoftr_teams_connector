using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading.Tasks;
namespace microsftteams
{
    public partial class Form2 : Form
    {
         Form1 f;
         bool tsth;
         int cmp;

         string cinstr = @"initial catalog= selenium ; data source = (localdb)\ProjectModels ; integrated security = true";

         string joinRQ = ";select U.ID , M.first,M.second,M.third,M.fourth from users U , Monday M where M.IDEmploi=U.ID";

         string RQ =("select * from users ; select * from Monday ; select * from Tuesday ; select * from Wednesday ; select * from Thursday ; select * from Friday ; select * from Saturday");

         SqlDataAdapter da;

         DataSet ds = new DataSet();

         SqlCommandBuilder cnd;

         string opera;

         BindingSource bs1 = new BindingSource();
         BindingSource bs2 = new BindingSource();
         BindingSource bs3 = new BindingSource();
         BindingSource bs4 = new BindingSource();
         BindingSource bs5 = new BindingSource();
         BindingSource bs6 = new BindingSource();

        public Form2(Form1 f)
        {
            InitializeComponent();
            this.f = f;
        }
        public Form2(bool tst ,int nmbr,string oper, Form1 f)
        {
            InitializeComponent();
            this.f = f;
            tsth = tst;
            cmp = nmbr;
            opera = oper;
        }
        private void Sql_cn()
        {
            da = new SqlDataAdapter(RQ + joinRQ, cinstr);

            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            da.Fill(ds);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                da = new SqlDataAdapter(RQ + joinRQ, cinstr);

                da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                da.Fill(ds);

                daynum();

                label11.Visible = false;

                label12.Visible = false;

                label13.Visible = false;

                label14.Visible = false;

                label16.Visible = false;

                bs1.DataSource = ds.Tables[1];

                bs2.DataSource = ds.Tables[2];

                bs3.DataSource = ds.Tables[3];

                bs4.DataSource = ds.Tables[4];

                bs5.DataSource = ds.Tables[5];

                bs6.DataSource = ds.Tables[6];

                label10.DataBindings.Add("text", bs1, "IDEmploi", true);

                label11.DataBindings.Add("text", bs2, "IDEmploi", true);

                label12.DataBindings.Add("text", bs3, "IDEmploi", true);

                label13.DataBindings.Add("text", bs4, "IDEmploi", true);

                label14.DataBindings.Add("text", bs5, "IDEmploi", true);

                label16.DataBindings.Add("text", bs6, "IDEmploi", true);

                //Monday-----------------------------------------------------------------------

                textBox1.DataBindings.Add("text", bs1, "first", true);

                textBox7.DataBindings.Add("text", bs1, "second", true);

                textBox13.DataBindings.Add("text", bs1, "third", true);

                textBox19.DataBindings.Add("text", bs1, "fourth", true);

                //------------------------------------------------------------------------------

                //Tuesday-----------------------------------------------------------------------

                textBox2.DataBindings.Add("text", bs2, "first", true);

                textBox8.DataBindings.Add("text", bs2, "second", true);

                textBox14.DataBindings.Add("text", bs2, "third", true);

                textBox20.DataBindings.Add("text", bs2, "fourth", true);

                //------------------------------------------------------------------------------

                //Wednesday-----------------------------------------------------------------------
                textBox3.DataBindings.Add("text", bs3, "first", true);

                textBox9.DataBindings.Add("text", bs3, "second", true);

                textBox15.DataBindings.Add("text", bs3, "third", true);

                textBox21.DataBindings.Add("text", bs3, "fourth", true);

                //------------------------------------------------------------------------------

                //Thursday-----------------------------------------------------------------------


                textBox4.DataBindings.Add("text", bs4, "first", true);

                textBox10.DataBindings.Add("text", bs4, "second", true);

                textBox16.DataBindings.Add("text", bs4, "third", true);

                textBox22.DataBindings.Add("text", bs4, "fourth", true);

                //------------------------------------------------------------------------------

                //Friday-----------------------------------------------------------------------

                textBox5.DataBindings.Add("text", bs5, "first", true);

                textBox11.DataBindings.Add("text", bs5, "second", true);

                textBox17.DataBindings.Add("text", bs5, "third", true);

                textBox23.DataBindings.Add("text", bs5, "fourth", true);

                //------------------------------------------------------------------------------

                //Saturday-----------------------------------------------------------------------
                textBox6.DataBindings.Add("text", bs6, "first", true);

                textBox12.DataBindings.Add("text", bs6, "second", true);

                textBox18.DataBindings.Add("text", bs6, "third", true);

                textBox24.DataBindings.Add("text", bs6, "fourth", true);

                //------------------------------------------------------------------------------

                if (tsth == true && opera.Equals("Ajout"))
                {
                    comboBox1.DataSource = null;
                    comboBox2.Items.Clear();
                    Adding();
                }

                if (tsth != true && opera.Equals("Del"))
                {
                    Deliting();
                }
                label10.Text = cmp.ToString();

                label11.Text = cmp.ToString();

                label12.Text = cmp.ToString();

                label13.Text = cmp.ToString();

                label14.Text = cmp.ToString();

                label16.Text = cmp.ToString();

                if(opera.Equals(""))
                {
                    remplirdgb(DateTime.Now.DayOfWeek.ToString());

                    comboBox1.DisplayMember = "Email";

                    comboBox1.ValueMember = "ID";

                    comboBox1.DataSource = ds.Tables[0];
                }
                else
                {
                    comboBox1.Enabled = false;
                    comboBox2.Enabled = false;
                }
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void daynum()
        {
            DateTime dtn = DateTime.Now;
            int d = (int)dtn.DayOfWeek;
            if(d!=0)
                comboBox2.SelectedIndex = d - 1;
        }
        //------------------add a time table -------------------------
        private void Adding()
        {
            bs1.AddNew();
            bs2.AddNew();
            bs3.AddNew();
            bs4.AddNew();
            bs5.AddNew();
            bs6.AddNew();
        }

        //------------------delete a time table -------------------------
        private void Deliting()
        {
            bs1.Position = cmp - 1;
            bs1.RemoveCurrent();
            bs2.Position = cmp - 1;
            bs2.RemoveCurrent();
            bs3.Position = cmp - 1;
            bs3.RemoveCurrent();
            bs4.Position = cmp - 1;
            bs4.RemoveCurrent();
            bs5.Position = cmp - 1;
            bs5.RemoveCurrent();
            bs6.Position = cmp - 1;
            bs6.RemoveCurrent();
            enregister();
        }

        //---------------------save changes-------------------------
        private void Enregistrer_Click(object sender, EventArgs e)
        {
            try
            {
                bs1.EndEdit();
                bs2.EndEdit();
                bs3.EndEdit();
                bs4.EndEdit();
                bs5.EndEdit();
                bs6.EndEdit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                try
                {
                    enregister();
                    f.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //-------------------------------change the day to display in data grid view -----------------------
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tsth != true && opera =="")
                {
                    string filteer = String.Format("IDEmploi = {0}",comboBox1.SelectedValue.ToString());
                    bs1.Filter = filteer;
                    bs2.Filter = filteer;
                    bs3.Filter = filteer;
                    bs5.Filter = filteer;
                    bs6.Filter = filteer;
                }
                else
                {
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //-----------------------save to data base-------------------------
        public void enregister()
        {
            da.SelectCommand.CommandText = "select * from Monday";
            cnd = new SqlCommandBuilder(da);
            da.Update(ds.Tables[1]);

            da.SelectCommand.CommandText = "select * from Tuesday";
            cnd = new SqlCommandBuilder(da);
            da.Update(ds.Tables[2]);

            da.SelectCommand.CommandText = "select * from Wednesday";
            cnd = new SqlCommandBuilder(da);
            da.Update(ds.Tables[3]);

            da.SelectCommand.CommandText = "select * from Thursday";
            cnd = new SqlCommandBuilder(da);
            da.Update(ds.Tables[4]);

            da.SelectCommand.CommandText = "select * from Friday";
            cnd = new SqlCommandBuilder(da);
            da.Update(ds.Tables[5]);

            da.SelectCommand.CommandText = "select * from Saturday";
            cnd = new SqlCommandBuilder(da);
            da.Update(ds.Tables[6]);
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (opera.Equals("Del"))
                {

                }
                else
                    f.Show();
            }
          catch
            {

            }
        }

        //-------------------------methode to fill the data grid view with the day selected------------------------
        private void remplirdgb(string day)
        {
            switch (day)
            {
                case "Monday":
                    dataGridView1.DataSource = bs1;
                    break;
                case "Tuesday":
                    dataGridView1.DataSource = bs2;
                    break;
                case "Wednesday":
                    dataGridView1.DataSource = bs3;
                    break;
                case "Thursday":
                    dataGridView1.DataSource = bs4;
                    break;
                case "Friday":
                    dataGridView1.DataSource = bs5;
                    break;
                case "Saturday":
                    dataGridView1.DataSource = bs6;
                    break;
                default:
                    dataGridView1.DataSource = ds.Tables[7];
                    break;
            }
            //dataGridView1.Columns["IDEmploi"].Visible = false;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            remplirdgb(comboBox2.Text);
        }
    }
}
