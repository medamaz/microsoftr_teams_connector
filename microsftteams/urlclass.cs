using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace microsftteams
{
    class Urlclass
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        string cinstr = @"initial catalog= selenium ;data source = (localdb)\ProjectModels ; integrated security = true";
       
        public void Monday(string f, string s, string t, string fo ,int id)
        {   
            try
            {
                cn.ConnectionString = cinstr;
                cn.Open();
                cmd.Connection = cn;
                cmd.Connection = cn;
                cmd.CommandText = "Update Hour set first = @first, second = @second, third = @third, fourth = @fourth WHERE IDHour = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@first", f);
                cmd.Parameters.AddWithValue("@second", s);
                cmd.Parameters.AddWithValue("@third", t);
                cmd.Parameters.AddWithValue("@fourth", fo);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
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
                Console.WriteLine(ex.Message + "\n line of erreur  :" + lineNumber);
            }
            finally
            {
                cn.Close();
            }
        }
        public void Tuesday(string f, string s, string t, string fo, int id)
        {
            try
            {
                cn.ConnectionString = cinstr;
                cn.Open();
                cmd.Connection = cn;
                cmd.Connection = cn;
                cmd.CommandText = "Update Hour set first = @first, second = @second, third = @third, fourth = @fourth WHERE IDHour = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@first", f);
                cmd.Parameters.AddWithValue("@second", s);
                cmd.Parameters.AddWithValue("@third", t);
                cmd.Parameters.AddWithValue("@fourth", fo);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
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
                Console.WriteLine(ex.Message + "\n line of erreur  :" + lineNumber);
            }
            finally
            {
                cn.Close();
            }
        }
        public void Wednesday(string f, string s, string t, string fo, int id)
        {
            try
            {
                cn.ConnectionString = cinstr;
                cn.Open();
                cmd.Connection = cn;
                cmd.Connection = cn;
                cmd.CommandText = "Update Hour set first = @first, second = @second, third = @third, fourth = @fourth WHERE IDHour = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@first", f);
                cmd.Parameters.AddWithValue("@second", s);
                cmd.Parameters.AddWithValue("@third", t);
                cmd.Parameters.AddWithValue("@fourth", fo);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
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
                Console.WriteLine(ex.Message + "\n line of erreur  :" + lineNumber);
            }
            finally
            {
                cn.Close();
            }
        }
        public void Thursday(string f, string s, string t, string fo, int id)
        {
            try
            {
                cn.ConnectionString = cinstr;
                cn.Open();
                cmd.Connection = cn;
                cmd.Connection = cn;
                cmd.CommandText = "Update Hour set first = @first, second = @second, third = @third, fourth = @fourth WHERE IDHour = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@first", f);
                cmd.Parameters.AddWithValue("@second", s);
                cmd.Parameters.AddWithValue("@third", t);
                cmd.Parameters.AddWithValue("@fourth", fo);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
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
                Console.WriteLine(ex.Message + "\n line of erreur  :" + lineNumber);
            }
            finally
            {
                cn.Close();
            }
        }
        public void Friday(string f, string s, string t, string fo, int id)
        {
            try
            {
                cn.ConnectionString = cinstr;
                cn.Open();
                cmd.Connection = cn;
                cmd.Connection = cn;
                cmd.CommandText = "Update Hour set first = @first, second = @second, third = @third, fourth = @fourth WHERE IDHour = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@first", f);
                cmd.Parameters.AddWithValue("@second", s);
                cmd.Parameters.AddWithValue("@third", t);
                cmd.Parameters.AddWithValue("@fourth", fo);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
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
                Console.WriteLine(ex.Message + "\n line of erreur  :" + lineNumber);
            }
            finally
            {
                cn.Close();
            }
        }
        public void Saturday(string f, string s, string t, string fo, int id)
        {
            try
            {
                cn.ConnectionString = cinstr;
                cn.Open();
                cmd.Connection = cn;
                cmd.Connection = cn;
                cmd.CommandText = "Update Hour set first = @first, second = @second, third = @third, fourth = @fourth WHERE IDHour = @id";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@first", f);
                cmd.Parameters.AddWithValue("@second", s);
                cmd.Parameters.AddWithValue("@third", t);
                cmd.Parameters.AddWithValue("@fourth", fo);
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
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
                Console.WriteLine(ex.Message + "\n line of erreur  :" + lineNumber);
            }
            finally
            {
                cn.Close();
            }
        }
    }
}
