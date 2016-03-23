using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth
{
    public class Authenticator
    {
        class AuthSystemKey
        {
            public string AuthSystemName { get; private set; }
            public string AuthSystemUserId { get; private set; }

            public AuthSystemKey(string authSystemName, string authSystemUserId)
            {
                this.AuthSystemName = authSystemName;
                this.AuthSystemUserId = authSystemUserId;
            }

            public override bool Equals(object obj)
            {
                var key = obj as AuthSystemKey;
                if(key == null)
                {
                    return false;
                }
                return
                    this.AuthSystemName == key.AuthSystemName &&
                    this.AuthSystemUserId == key.AuthSystemUserId;
            }

            public override int GetHashCode()
            {
                return
                    this.AuthSystemName.GetHashCode() ^
                    this.AuthSystemUserId.GetHashCode();
            }
        }

        private static readonly Authenticator _singletone = new Authenticator();

        private readonly object _sync = new object();

        /// <summary>
        /// Пользователи внешних систем
        /// </summary>
        private readonly List<AuthSystemUser> _authSystemUsers = new List<AuthSystemUser>();

        /// <summary>
        /// Пользователи нашей системы
        /// </summary>
        private readonly List<User> _users = new List<User>();

        /// <summary>
        /// Связь пользователей внешних систем с нашими
        /// </summary>
        private readonly Dictionary<AuthSystemKey, string> _authSystemUsersToOurUsers = new Dictionary<AuthSystemKey, string>();

        private int _lastId = 0;

        public static Authenticator Value
        {
            get { return _singletone; }
        }

        public User Auth(
            User user,
            AuthSystemUser authSystemUser)
        {
            if(authSystemUser.AuthSystemName == null)
            {
                throw new ArgumentNullException(nameof(authSystemUser.AuthSystemName));
            }

            if (authSystemUser.AuthSystemUserId == null)
            {
                throw new ArgumentNullException(nameof(authSystemUser.AuthSystemUserId));
            }

            lock (_sync)
            {
                var contains = this._authSystemUsers.Any(
                    u => u.AuthSystemName == authSystemUser.AuthSystemName && u.AuthSystemUserId == authSystemUser.AuthSystemUserId);

                string ourUserId = null;
                var key = new AuthSystemKey(authSystemUser.AuthSystemName, authSystemUser.AuthSystemUserId);
                if (!contains)
                {
                    this._authSystemUsers.Add(authSystemUser);
                }
                else
                {
                    ourUserId = this._authSystemUsersToOurUsers[key];
                }

                User ourUser = null;
                if (!string.IsNullOrEmpty(ourUserId))
                {
                    ourUser = this._users.Single(u => u.Id == ourUserId);
                }

                if (ourUser == null)
                {
                    ourUser = new User()
                    {
                        Id = (++_lastId).ToString(),
                        Token = Guid.NewGuid().ToString(),
                    };
                    this._users.Add(ourUser);
                }

                ourUser.Merge(user);
                this._authSystemUsersToOurUsers[key] = ourUser.Id;

                return ourUser;
            }
        }
    }
}
