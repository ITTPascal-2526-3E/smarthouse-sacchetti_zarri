using BlaisePascal.SmartHouse.Domain.AbstractInterfaces;
using BlaisePascal.SmartHouse.Domain.Security.SecurityAbstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Security
{
    public sealed class SecureDoor : ILockable,  IPassword
    {

        Authentication Authentication = new Authentication();
        public bool is_locked { get; private set; } 
        public string mail { get; private set; }
        public string password { get; protected set; }



        public SecureDoor(string password1, string email)
        {
            password = password1;
            mail = email;
            is_locked = true;
        }
        public void LockDoor()
        {
            is_locked = true;
        }

        public void UnlockDoor(string Password)
        {
            if (Password == password)
                is_locked = false;
        }

        public void SetPassword(string newPass)
        {
            if(Authentication.isAuthorized==true)
                password = newPass;
        }

        public void resetPassword()
        {
            SendBackupCode sendBackup = new SendBackupCode(this);
            sendBackup.Send();
        }
    }
}
