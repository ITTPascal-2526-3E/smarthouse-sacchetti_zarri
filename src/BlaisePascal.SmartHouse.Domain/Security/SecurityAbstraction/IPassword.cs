using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Security.SecurityAbstraction
{
    public interface IPassword
    {
        void SetPassword(string newPass);
        void resetPassword();

    }
}
