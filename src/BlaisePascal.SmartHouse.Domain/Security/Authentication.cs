using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Security
{
    public class Authentication
    {

        public bool isAuthorized { get; private set; }
        public void intialize()
        {
            isAuthorized = false;
        }   
        public void Authenticate(string token)
        {
            isAuthorized = CheckToken(token);
        }
        private bool CheckToken(string token)
        {
            return token == "Admin";
        }
    }
}
