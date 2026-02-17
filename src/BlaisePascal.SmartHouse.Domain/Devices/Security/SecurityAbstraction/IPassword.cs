using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Devices.Security.SecurityAbstraction
{
    public interface IPassword
    {
        void SetPassword(Password newPass);
        void resetPassword();

    }
}
