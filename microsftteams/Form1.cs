using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace microsftteams

{
    public partial class Form1 : Form
    {
        ConnectionSl sl = new ConnectionSl();


        private System.Threading.Timer timer;

        int cmp = 1; //number of the new user or existing user

        bool add_new = false; //a test for make sure the user want to add a new tabletime tor modify

        string Email; //email to login

        string Password; //password to login

        Form2 f;

        StreamWriter sw;

        //sql declaration-----------------------------------------------------------------------------------------------------

        //string cinstr = "initial catalog= selenium ; data source = 192.168.1.200 ; User ID=sa ; Password=1002";

        //connection string and sql requete

        string cinstr = @"initial catalog= selenium ; data source = (localdb)\ProjectModels ; integrated security = true";

        string RQ = ("select * from users");

        //------------------------------------

        BindingSource bs = new BindingSource();

        SqlDataAdapter da;

        DataSet ds = new DataSet();

        //--------------------------------------------------------------------------------------------------------------------

        public Form1()
        {
            InitializeComponent();
        }

        //function test if connection internet is connected befor connct the select
        //user to microsoft teams and if the connection not found
        //its start a progrss bar with time of 60s and restart
        //the app and in this time its try evry
        //secend to connect to internet 
        public bool Chek_Internet(string host)
        {
            Ping myPing = new Ping();
            byte[] buffer = new byte[32];
            int timeout = 1000;
            PingOptions pingOptions = new PingOptions();
            bool test = false;
            try
            {
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                if (reply.Status == IPStatus.Success)
                {
                    test = true;
                }
                return test;
            }
            catch
            {
                return false;
            }

        }
        //---------------------------------------------------------
        void Sqldb()
        {
            da = new SqlDataAdapter(RQ, cinstr)
            {
                MissingSchemaAction = MissingSchemaAction.AddWithKey
            };

            da.Fill(ds);

            SqlCommandBuilder cmd = new SqlCommandBuilder(da);
        }
        async private void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                comboBox1.SelectedIndex = 0;
                progressBar1.Visible = false;
                //------------------------------------- sql declaration ---------------------------------------------------------------

                await Task.Run(() => Sqldb());

                //----------------------------------------- binding data  --------------------------------------------------------------

                bs.DataSource = ds.Tables[0];
                dataGridView1.DataSource = bs;
                textBox3.DataBindings.Add("text", bs, "ID", true);
                textBox1.DataBindings.Add("text", bs, "Email", true);
                textBox2.DataBindings.Add("text", bs, "Password", true);
                checkBox1.DataBindings.Add("Checked", bs, "For_Defeult", true);

                //--------------------------------------------------- Hidden inussable comules in dgv -----------------------------------

                dataGridView1.Columns["iD"].Visible = false;
                dataGridView1.Columns["For_Defeult"].Visible = false;
                dataGridView1.Columns["Email"].Width = 250;
                dataGridView1.Columns["Password"].Width = 200;

                //---------------------------------------------------- chifrer le paasword -----------------------------------------------

                textBox2.PasswordChar = '\u25CF';
                Starup();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //a method to create timer in spesific time and run it automaticlly when time is comme
        private void SetUpTimer(TimeSpan alertTime, int position)
        {
            try
            {
                DateTime current = DateTime.Now;
                TimeSpan timeToGo = alertTime - current.TimeOfDay;
                if (timeToGo < TimeSpan.Zero)
                {
                    return;//time already passed
                }
                this.timer = new System.Threading.Timer(x =>
                {
                    this.Connector(position);
                }, null, timeToGo, Timeout.InfiniteTimeSpan);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //----------------------------------------
        //startup is a methode run in the load of form and check if the app owner is check run automaticlly if its true its creat a multip timer at spesific time on day 
        private void Starup()
        {
            try
            {
                string fil = File.Exists("stratup.txt") ? "File exists." : "File does not exist.";
                if (fil.Equals("File exists."))
                {
                    string[] condition1 = File.ReadAllLines("stratup.txt");
                    if (condition1[0].Equals("true"))
                    {
                        if (condition1[1].Equals("true"))
                        {
                            checkBox4.Checked = true;
                            if (DateTime.Now.DayOfWeek.ToString().Equals("Friday"))
                            {
                                SetUpTimer(new TimeSpan(8, 30, 00), 0);
                                SetUpTimer(new TimeSpan(10, 15, 00), 1);
                                SetUpTimer(new TimeSpan(15, 00, 00), 2);
                                SetUpTimer(new TimeSpan(17, 50, 00), 3);
                            }
                            else
                            {
                                SetUpTimer(new TimeSpan(8, 30, 00), 0);
                                SetUpTimer(new TimeSpan(11, 00, 00), 1);
                                SetUpTimer(new TimeSpan(14, 57, 00), 2);
                                SetUpTimer(new TimeSpan(16, 15, 00), 3);
                            }
                        }
                        notifyIcon1.Visible = true;
                        checkBox3.Checked = true;
                        checkBox4.Enabled = true;
                        this.WindowState = FormWindowState.Minimized;
                        this.ShowInTaskbar = false;
                        this.Hide();
                    }
                    else
                    {
                        checkBox3.Checked = false;
                        checkBox4.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //--------------------------------------
        //private bool Validat()
        //{
        //    FormCollection fo = Application.OpenForms;
        //    foreach (Form frm in fo)
        //    {
        //        if (frm.Text == "Form2")
        //        {
        //            return  true;
        //        }
        //    }
        //    return false;
        //}
        // this methode creat the id of users 
        private void Compteur(DataTable dt)
        {
            try
            {
                int i;
                bool test = true;
                DataRow r;
                if (dt.Rows.Count == 0)
                {
                    cmp = 1;
                }
                else if (dt.Rows.Count == 1)
                {
                    r = dt.Rows[0];
                    if (Convert.ToInt32(r[0]) == 2)
                    {
                        cmp = 1;
                    }
                    else
                    {
                        cmp = dt.Rows.Count + 1;
                    }
                }
                else if (dt.Rows.Count > 1)
                {
                    i = 1;
                    r = dt.Rows[dt.Rows.Count - 1];
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        bs.Position = i;
                        if (Convert.ToInt32(dataRow[0]) >= Convert.ToInt32(r[0]))
                        {
                            break;
                        }
                        else
                        {
                            if (Convert.ToInt32(dataRow[0]) + 1 == Convert.ToInt32(textBox3.Text))
                            {
                                test = true;
                            }
                            else
                            {
                                test = false;
                                cmp = i + 1;
                                break;
                            }
                            i++;
                        }
                    }
                    if (test == true)
                    {
                        cmp = dt.Rows.Count + 1;
                    }
                }
                else
                {
                    cmp = dt.Rows.Count + 1;
                }
            }
            catch (Exception ex)
            {
                var lineNumber = 0;
                const string lineSearch = ":line ";
                var index = ex.StackTrace.LastIndexOf(lineSearch);
                if (index != -1)
                {
                    var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                    if (int.TryParse(lineNumberText, out lineNumber))
                    {
                    }
                }
                MessageBox.Show(ex.Message + "\n line of erreur  :" + lineNumber);
            }
        }
        //---------------------------------------
        //if the check button is checked its creat a txt file and save it's satate and loaded at the next load
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                sw = new StreamWriter("stratup.txt");

                RegistryKey reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                if (checkBox3.Checked)
                {
                    reg.SetValue("Microsoftteamscn", Application.ExecutablePath.ToString());
                    sw.WriteLine("true");
                    if (checkBox4.Checked)
                    {
                        sw.WriteLine("true");
                    }
                    else
                    {
                        sw.WriteLine("false");
                    }
                }
                else
                {
                    sw.WriteLine("false");
                    try
                    {
                        reg.DeleteValue("Microsoftteamscn");
                    }
                    catch
                    { }
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //----------------------------------------
        //if the app minimize its make it at small icon
        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    Hide();
                    notifyIcon1.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //------------------------------------------
        //when double click at the small icon its show the app
        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.Show();
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
                notifyIcon1.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //--------------------------------
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //----------------------------------------for show  passwod with chifre normal or like a spicial character ---------
        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (checkBox2.Checked)
                {
                    textBox2.PasswordChar = '\0';
                }
                else
                    textBox2.PasswordChar = '\u25CF';
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------------
        //remove user from data base
        private void Removebt_Click(object sender, EventArgs e)
        {
            try
            {
                string ms = "Delete user with e-mail :   " + textBox1.Text;
                const string title = "Deleting";
                var result = MessageBox.Show(ms, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    f = new Form2(false, Convert.ToInt32(textBox3.Text), "Del", this)
                    {
                        Opacity = 0
                    };
                    f.Show();

                    bs.RemoveCurrent();
                    da.SelectCommand.CommandText = RQ;
                    SqlCommandBuilder cnd = new SqlCommandBuilder(da);
                    da.Update(ds);
                    MessageBox.Show("  Delete success  ");
                    f.Close();
                }
                else
                    MessageBox.Show("  Delete canceled  ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Application.Restart();
            }
        }
        //--------------------------------------
        //connect user to microsoft teams
        private void Connectbt_Click(object sender, EventArgs e)
        {
            try
            {
                switch (DateTime.Now.Hour)
                {
                    case 8:
                    case 9:
                    case 10:
                        Connector(0);
                        break;
                    case 11:
                    case 12:
                    case 13:
                        Connector(1);
                        break;
                    case 14:
                    case 15:
                        Connector(2);
                        break;
                    case 16:
                    case 17:
                    case 18:
                        Connector(3);
                        break;
                    default:
                        Connector(-1);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------
        //turn on the progress bar if connection field 
        private void progbarfield(int position)
        {
            try
            {
                int tst = -1;
                for (int i = 0; i < 60; i++)
                {
                    Thread.Sleep(1000);
                    ActuallyPerformStep(progressBar1, tst);
                    chengetxt(label3, i);
                    if (Chek_Internet("teams.microsoft.com"))
                    {
                        tst = 1;
                        ActuallyPerformStep(progressBar1, tst);
                        chengesize(this, tabControl1, progressBar1, label3);
                        Connector(position);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------
        //the connect methode for connect click
        private void Connector(int position)
        {
            try
            {
                if (Chek_Internet("teams.microsoft.com"))
                {
                    Geturl gt = new Geturl(Convert.ToInt32(textBox3.Text));
                    string corurl = gt.Classeurl(position);
                    Email = textBox1.Text;
                    Password = textBox2.Text;
                    MessageBox.Show(sl.Login(corurl, Email, Password));
                }
                else
                {
                    progressBar1.Visible = true;
                    progressBar1.Maximum = 60;
                    progressBar1.Step = 1;
                    label3.Text = "erreur";
                    this.Height = 350;
                    tabControl1.Height = 310;
                    Task t = new Task(() => progbarfield(position));
                    t.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------
        //modify click
        private void Modifybt_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("no items in table useres ");
                }
                else
                {
                    this.Hide();
                    f = new Form2(false, Convert.ToInt32(textBox3.Text), "", this);
                    f.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //--------------------------------
        // add new user button
        private void Newbt_Click(object sender, EventArgs e)
        {
            try
            {
                add_new = true;

                DataView dv = ds.Tables[0].DefaultView;
                dv.Sort = "ID";
                DataTable dt = dv.ToTable();

                checkBox1.Checked = true;
                checkBox1.Checked = false;

                Compteur(dt);

                bs.AddNew();

                bs.Sort = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                textBox3.Focus();
                textBox3.Text = cmp.ToString();
            }
        }
        //--------------------------------
        //change formating of password row in dgv
        private void dataGridView1_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2 && e.Value != null)
                {
                    e.Value = new string('\u25CF', e.Value.ToString().Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //---------------------------------
        //enregister click
        private void Enregistrerbt_Click(object sender, EventArgs e)
        {
            try
            {
                bs.EndEdit();
                bs.Position = cmp - 1;

                da.SelectCommand.CommandText = "select * from users";
                da.Update(ds.Tables[0]);
                if (add_new == true)
                {
                    this.Hide();
                    notifyIcon1.Visible = false;

                    f = new Form2(true, Convert.ToInt32(textBox3.Text), "Ajout", this);
                    f.Show();

                }
                else
                {
                }
                add_new = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //----------------------------------
        //turn on the auto connect only if the program its run at start up 
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                checkBox4.Enabled = true;
            }
            else
            {
                checkBox4.Enabled = false;
            }
        }
        //-----------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            /*
            progressBar1.Visible = true;
            progressBar1.Maximum = 60;
            progressBar1.Step = 1;
            label3.Text = "erreur";
            this.Height = 350;
            tabControl1.Height = 310;
            Task t = new Task(() => progbarfield());
            t.Start();
            */
        }

        /*------------------------- control for tasks----------------------------*/

        delegate void CallPerformStep(ProgressBar myProgressBar, int tst);

        private void ActuallyPerformStep(ProgressBar myProgressBar, int tst)
        {
            if (myProgressBar.InvokeRequired)
            {
                CallPerformStep del = ActuallyPerformStep;
                myProgressBar.Invoke(del, new object[] { myProgressBar, tst });
                return;
            }
            if (tst == (-1))
            {
                myProgressBar.PerformStep();
            }
            else
            {
                myProgressBar.Value = 60;
            }
        }

        delegate void chengesize1(Form1 f, TabControl tabc, ProgressBar pb, Label lb);

        private void chengesize(Form1 f, TabControl tabc, ProgressBar pb, Label lb)
        {
            if (textBox3.InvokeRequired)
            {
                chengesize1 del = chengesize;
                label3.Invoke(del, new object[] { f, tabc, pb, lb });
                return;
            }
            f.Height = 277;
            tabc.Height = 250;
            pb.Visible = false;
            lb.Visible = false;

        }

        delegate void chengetxt1(Label lb, int i);

        private void chengetxt(Label lb, int i)
        {
            if (textBox3.InvokeRequired)
            {
                chengetxt1 del = chengetxt;
                lb.Invoke(del, new object[] { lb, i });
                return;
            }
            lb.Text = "restart in : " + i;
        }

        /*------------------------------------------------------------------------*/

        void get_version_of()
        {
            try
            {
                if (comboBox1.SelectedIndex == 0)
                    sl.Browser = "chrome";
                else if (comboBox1.SelectedIndex == 1)
                    sl.Browser = "firefox";
                else
                    sl.Browser = "msedge";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_version_of();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "chose Web Driver directory ";
            sl.Chromedriverfile = Path.GetDirectoryName(openFileDialog.FileName);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "chose Web Driver directory ";
            sl.ChromeBinaryLocation = openFileDialog.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sl.Chromedriverfile = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sl.ChromeBinaryLocation = "";
        }
    }
}
