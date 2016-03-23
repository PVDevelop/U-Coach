using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth
{
    public class User
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Name { get; set; }

        public void Merge(User user)
        {
            if(string.IsNullOrEmpty(this.Name))
            {
                this.Name = user.Name;
            }
        }
    }
}
