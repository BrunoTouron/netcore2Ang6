using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCRM.Secutiry
{
    public class User
    {
        public string UserID { get; set; }
        public string Password { get; set; }
    }

    public static class Roles
    {
        public const string ROLE_API_CRM = "Acesso-ApiCRM";
    }

    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
