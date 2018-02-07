using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.Net._5.HW2.LINQandQuantificators
{  
    class Program
    {
        //
        private static string _connectionString = @"Data Source=DESKTOP-PG10UGI\SQLEXPRESS; Initial Catalog = CRCMS_new; User Id =sa; Password =Mc123456";
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
                    ar.ParentId = Int32.Parse(row["ParentId"].ToString());
                    ar.HiddenArea = row["HiddenArea"].ToString();
                    
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
                Console.WriteLine(item.AreaId + "  " + item.IP + "  " + item.PavilionId);
            }
            Console.WriteLine("-----------------------------------------------------");
            //FILTER WITHOUT NULLABLE IP and parentid!=0

            IEnumerable<Area> query2 = from a in areas where a.IP != "" && a.ParentId!=0 select a;


            foreach (Area item in query2)
            {
                Console.WriteLine(item.AreaId + "  " + item.IP + "  " + item.ParentId);
            }

            //LOOKUP 

            ILookup<string, Area> lookupArea = areas.ToLookup(k => k.IP, k2 => k2);
            foreach (var item in lookupArea)
            {
                Console.WriteLine(item.Key);
                foreach (var item2 in item)
                {
                    Console.WriteLine(item2.AreaId + " " + item2.Name + " " + item2.FullName);
                }
            }

            //FIRST ROW WHERE HIDDENAREA=1

            var query3 = areas.Where(w => w.HiddenArea == "1").Select(a => a).Single();
            Console.WriteLine(query3.AreaId + " " + query3.Name + " " + query3.FullName + " " + query3.IP);

            //LAST ROW WHERE PAVILLIONID=1

            var query4 = areas.Where(w => w.PavilionId == 1).Select(a => a).Last();
            Console.WriteLine(query4.AreaId + " " + query4.Name + " " + query4.FullName + " " + query4.IP);

            //QUANTIFICATORS
            string[] IPs = {"10.53.34.85", "10.53.34.77", "10.53.34.53"};

            var query5 = areas.Where(w => w.PavilionId == 1 && IPs.Contains(w.IP));
            foreach (var item in query5)
            {
                Console.WriteLine(item.AreaId + " " + item.FullName + " " + item.Name + " ");
            }

            //QUANTIFICATORS 2

            string[] names = {"PT disassembly", "Engine testing"};
            var query6 = areas.Where(w => names.Contains(w.Name));
            foreach (var item in query6)
            {
                Console.WriteLine(item.AreaId + " " + item.FullName + " " + item.Name + " ");
            }

            //SUM

            var query7 = areas.Sum(s => s.WorkingPeople);
            Console.WriteLine("Количество работников" + query7);





        }

      


    }
}
