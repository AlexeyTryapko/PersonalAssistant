using PersonalAssistantWCF.Entity;
using PersonalAssistantWCF.Strategy.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssistantWCF.Strategy.Classes
{
    class MailExport : IExport
    {
        ServicePersonalAssistant service;
        public void ExportData(User user)
        {
            service = new ServicePersonalAssistant();
            Note[] notes = service.RefreshNotes(user.Id);
            string notesSTR = "\tNOTES\n" + string.Join("\n", Array.ConvertAll<object, string>(notes, Convert.ToString));
            Meetup[] meetups = service.RefreshMeetups(user.Id);
            string meetupsSTR = "\tMEETUPS\n" + string.Join("\n", Array.ConvertAll<object, string>(meetups, Convert.ToString));
            Contact[] contacts = service.RefreshContacts(user.Id);
            string contactsSTR = "\tCONTACTS\n" + string.Join("\n", Array.ConvertAll<object, string>(contacts, Convert.ToString));

            string res = notesSTR + "\n" + meetupsSTR + "\n" + contactsSTR;

            const string bodyRES = "";
            var fromAddress = new MailAddress("alexey.tryapko1201@gmail.com", "Personal Assistant");
            var toAddress = new MailAddress(user.Mail, user.Login);
            const string fromPassword = "";
            const string subject = "Personal Assistant data export";
            string body = res;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
    }
}
