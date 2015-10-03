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

        public static OperationResult<IEnumerable<accessLog>> GetAccessLogByUser(user user)
        {
            var userRole = (Common.Roles)user.Role_Id;
            switch (userRole)
            {
                case Roles.Admin:
                    return accessLogsForAdmin();
                case Roles.Manager:
                    return accessLogsForManager(user);
                case Roles.Employee:
                    return accessLogsForEmployee(user);
                default:
                    return new OperationResult<IEnumerable<accessLog>>
                    {
                        ErrorCode = 987,
                        Message = "Undefined Role"
                    };
            }
        }
        private static OperationResult<IEnumerable<accessLog>> accessLogsForEmployee(user user)
        {
            var logs = Entities.accessLog.Where(x => x.user.Id == user.Id);
            return getOperationResultFromAccessLogList(logs);
        }
        private static OperationResult<IEnumerable<accessLog>> accessLogsForManager(user user)
        {
            var userDepartment = user.department;
            var logs = Entities.accessLog.Where(x => x.user.department.Id == userDepartment.Id);
            return getOperationResultFromAccessLogList(logs);
        }
        private static OperationResult<IEnumerable<accessLog>> accessLogsForAdmin()
        {
            var logs = Entities.accessLog.OrderByDescending(x => x.Time);
            return getOperationResultFromAccessLogList(logs);
        }
        private static OperationResult<IEnumerable<accessLog>> getOperationResultFromAccessLogList(IEnumerable<accessLog> logs)
        {
            return new OperationResult<IEnumerable<accessLog>> { Success = true, ReturnValue = logs, Message = string.Format("{0} Log Recieved", logs.Count()) };
        }
    }
}