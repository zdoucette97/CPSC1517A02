using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    public class JobApps
    {
        public string Fullname { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string FullOrPartTime { get; set; }
        public string Jobs { get; set; }

        public JobApps()
        {

        }

        public JobApps(string fullname, string email, string phonenumber, string time, string jobs)
        {
            Fullname = fullname;
            EmailAddress = email;
            PhoneNumber = phonenumber;
            FullOrPartTime = time;
            Jobs = jobs;
        }

    }
}