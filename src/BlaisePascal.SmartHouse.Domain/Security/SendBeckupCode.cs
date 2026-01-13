
using System;
using System.Net;
using System.Net.Mail;

namespace BlaisePascal.SmartHouse.Domain.Security
{
    public class SendBackupCode
    {

        private SecureDoor door;

        // Riceve la porta a cui cambiare la password
        public SendBackupCode(SecureDoor door1)
        {
            door = door1;
        }
        public void Send()
        {
            Random rnd = new Random();
            int numero = rnd.Next();
            door.SetPassword(numero.ToString());


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("nikozarro@gmail.com");
            mail.To.Add(door.mail);
            mail.Subject = "Numero random generato dal programma";
            mail.Body = $"Il numero generato è: {numero}";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;

            
            smtp.Credentials = new NetworkCredential("nikozarro@gmail.com", "qhdd gedg cztf nily");

            smtp.Send(mail);

            Console.WriteLine("Email inviata con successo!");
        }
    }
}