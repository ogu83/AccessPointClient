using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagementPanel.DB
{
    public class Operations
    {
        private static accessControlManagementEntities _entities;
        public static accessControlManagementEntities Entities
        {
            get
            {
                if (_entities == null)
                    _entities = new accessControlManagementEntities();
                return _entities;
            }
        }

        public static OperationResult<user> CheckUserLogin(string username, string password)
        {
            var users = Entities.user.Where(x => x.Username == username && x.Password == password);
            if (users != null)
                if (users.Count() > 0)
                {
                    var user = users.First();
                    if (user != null)
                    {
                        return new OperationResult<user>
                        {
                            Success = true,
                            ReturnValue = user
                        };
                    }
                }

            return new OperationResult<user>
            {
                Success = false,
                Message = "Incorrect username or password"
            };
        }
    }
}