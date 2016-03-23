using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth
{
    public class AuthSystemUser
    {
        public string AuthSystemName { get; private set; }
        public string Token { get; private set; }
        public string AuthSystemUserId { get; private set; }

        public AuthSystemUser(string authSystemName, string authSystemUserId, string token)
        {
            this.AuthSystemName = authSystemName;
            this.AuthSystemUserId = authSystemUserId;
            this.Token = token;
        }
    }
}
