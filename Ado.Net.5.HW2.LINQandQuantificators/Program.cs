using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net._5.HW2.LINQandQuantificators
{  
    class Program
    {
        private static string _connectionString = @"Data Source = DESKTOP-PG10UGI\SQLEXPRESS; Initial Catalog = CRCMS_new; User Id = sa; Password = Mc123456";
        private static Area db = new Area();
        static void Main(string[] args)
        {
            GetData();
        }

        static void GetData()
        {
            SqlConnection con = new SqlConnection(_connectionString);
            SqlDataAdapter da = new SqlDataAdapter("select * from Area", con);
            DataSet ds = new DataSet();
            da.Fill(ds);


            //TO LIST<AREA>
            List<Area> areas = new List<Area>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Area ar = new Area();
                {

                    ar.AreaId = Int32.Parse(row["AreaId"].ToString());
                    ar.FullName = row["FullName"].ToString();
                    ar.Name = row["Name"].ToString();
                    ar.WorkingPeople = Int32.Parse(row["WorkingPeople"].ToString());
                    ar.PavilionId = Int32.Parse(row["PavilionId"].ToString());
                    ar.IP = row["IP"].ToString();
                }
                areas.Add(ar);
            }

            foreach (Area item in areas)
            {
                Console.WriteLine(item.AreaId + "  " + item.Name + "  " + item.FullName + "  " + item.PavilionId  );
            }

            //TO ARRAY
            Array areasArray= areas.ToArray();


            //FILTER WITHOUT NULLABLE IP
            var query = from a in areas where a.IP != "" select a;

            foreach (Area item in query)
            {
                Console.WriteLine(item.AreaId + "  " + item.Name + "  " + item.IP + "  " + item.PavilionId);
            }

        }

        static void ToAreaList()
        {
            

        }


    }
}
