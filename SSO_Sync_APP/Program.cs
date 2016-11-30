using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.IO;

namespace SSO_Sync_APP
{
    class Program
    {
        static void Main(string[] args)
        {
            int i;
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"sso_xml_dat.xml");
            string connectionString = ConfigurationManager.ConnectionStrings["xAdvertiser"].ToString();
            XmlDocument xml_data = new XmlDocument();
            xml_data.Load(path);

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                con.Open();

                SqlCommand command = new SqlCommand("spXADV_SetHasSSOStatus_mod", con);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add(new SqlParameter("@AdvertiserListXML", SqlDbType.Xml));

                command.Parameters[0].Value = xml_data.InnerXml; //passing the string form of XML generated above

                i = command.ExecuteNonQuery();

            }

            Console.WriteLine(i);
            Console.ReadLine();
        }
    }
}
