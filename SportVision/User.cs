using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SportVision
{
    public class User
    {
        private string email;
        public long UserID { get; set; }
        public string FacebookID { get; set; }
        public string GoogleID { get; set; }
        public string Username { get; set; }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (IsValid(value))
                    email = value;
                else
                    email = "";
            }
        }

        public int Points { get; set; }
        public bool IsInMatch { get; set; }
        public int MatchID { get; set; }
        public string ImageURL { get; set; }

        private bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}