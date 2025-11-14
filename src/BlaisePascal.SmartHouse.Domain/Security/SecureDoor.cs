using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Security
{
    public class SecureDoor
    {
        public bool is_locked { get; private set; } 
        private string mail;
        private string password;
        public SecureDoor(string password1, string email)
        {
            password = password1;
            mail = email;
            is_locked = true;
            
        }
        public void lockDoor()
        {
            is_locked = true;
        }
        
        public void unlockDoor(string Password)
        {
            if(Password == "secure123")
                is_locked = false;
        }

        public void resetPassword()
        {
            SendBackupCode sendBackup = new SendBackupCode();
            sendBackup.Send();
        }
    }
}
