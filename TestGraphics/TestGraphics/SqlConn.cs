using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace TestGraphics
{
   public class SqlConn
    {
       public static SqlConnection OpenConnectiion()
        {
            try
            {
                string attachDbfileName = @"C:\Users\bhagya.sanjeewa\Documents\Visual Studio 2010\Projects\TestGraphics\TestGraphics\dbGraphics.mdf";
                string conStr = @"Data Source=.\SQLEXPRESS;AttachDbFilename=" + attachDbfileName +";Integrated Security=True;User Instance=True";

                conStr = "Data Source=.;Initial Catalog=DyeGraphics;User ID=sa;";

                SqlConnection con = new SqlConnection(conStr);          

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    
                }
                else
                    con.Close();
                return con;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

       public static SqlConnection GetConnection()
       {
           try
           {
               SqlConnection con = new SqlConnection(ConfigurationSettings.AppSettings.Get("ConnStr").ToString());               
               return con;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
