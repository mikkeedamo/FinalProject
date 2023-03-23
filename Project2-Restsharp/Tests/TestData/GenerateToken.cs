using Project2_Restsharp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Restsharp.Tests.TestData
{
    public class GenerateToken
    {
        public static LoginModels newToken()
        {
            return new LoginModels
            {
                Username = "admin",
                Password =  "password123"

            };
        }
    }
}
