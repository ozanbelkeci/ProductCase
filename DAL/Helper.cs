using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Helper
    {
        public static string GetConnectionString()
        {
            return "Server=localhost;Database=CaseDb;Trusted_Connection=True;";
        }
    }
}
