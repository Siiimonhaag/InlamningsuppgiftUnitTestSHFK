using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestverktygUnitTestingSHFK
{
    public class DatabaseContext
    {
        public Customer getCustomerByPersonalNumber(string personalNumber)
        {
            string sql = "SELECT * FROM users WHERE personalNumber = " + personalNumber;
            return null;
        }
    }
}
