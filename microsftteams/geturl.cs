using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace microsftteams
{
    class Geturl
    {
        int IDusr;

        string url;
        public int IDusr1 { get => IDusr; set => IDusr = value; }
        public string Url { get => url; set => url = value; }

        SqlDataAdapter da;
        DataSet ds = new DataSet();
        public Geturl(int IDusr)
        {
            //string cinstr = "initial catalog= selenium ;data source = 192.168.1.200 ;User ID=sa ; Password=1002";
            string cinstr = @"initial catalog= selenium ; data source = (localdb)\ProjectModels ; integrated security = true";
            string RQ = String.Format("select * from Monday where IDEmploi= {0}; select * from Tuesday where IDEmploi= {0} ; select * from Wednesday where IDEmploi= {0}; select * from Thursday where IDEmploi= {0}; select * from Friday where IDEmploi= {0}; select * from Saturday where IDEmploi= {0}",IDusr);
            this.IDusr1 = IDusr;
            da = new SqlDataAdapter(RQ, cinstr);
            da.Fill(ds);
        }
        public string Classeurl(int postion)
        {
            string url2;
            switch(DateTime.Now.DayOfWeek.ToString())
            {
                case "Monday":
                    url2= Time(ds.Tables[0], postion);
                    break;
                case "Tuesday":
                    url2 = Time(ds.Tables[1], postion);
                    break;
                case "Wednesday":
                    url2 = Time(ds.Tables[2], postion);
                    break;
                case "Thursday":
                    url2 = Time(ds.Tables[3], postion);
                    break;
                case "Friday":
                    url2 = Time(ds.Tables[4], postion);
                    break;
                case "Saturday":
                    url2 = Time(ds.Tables[5],postion);
                    break;
                default:
                    url2 = "";
                    break;
            }
            return url2;
        }
        public DataTable Classeur2()
        {
            switch (DateTime.Now.DayOfWeek.ToString())
            {
                case "Monday":
                    return ds.Tables[0];
                case "Tuesday":
                    return ds.Tables[1];
                case "Wednesday":
                    return ds.Tables[2];
                case "Thursday":
                    return ds.Tables[3];
                case "Friday":
                    return ds.Tables[4];
                case "Saturday":
                    return ds.Tables[5];
                default:
                    return null;
            }
        }
        public string Time(DataTable dt,int postion)
        {
            DataRow r=dt.Rows[0];
            int time = Convert.ToInt32(DateTime.Now.Hour);
            string url1;
            switch(postion)
            {
                case 1:
                    if (r[1].ToString().Equals(""))
                    {
                        url1 = "";
                    }
                    else
                    {
                        url1 = r[1].ToString();
                    }
                    break;
                case 2:
                    if (r[2].ToString().Equals(""))
                    {
                        url1 = "";
                    }
                    else
                    {
                        url1 = r[2].ToString();
                    }
                    break;
                case 3:
                    if (r[3].ToString().Equals(""))
                    {
                        url1 = "";
                    }
                    else
                    {
                        url1 = r[3].ToString();
                    }
                    break;
                case 4:
                    if (r[4].ToString().Equals(""))
                    {
                        url1 = "";
                    }
                    else
                    {
                        url1 = r[4].ToString();
                    }
                    break;
                default:
                    url1 = "";
                        break;
            }
            return url1;
        }
    }
}
