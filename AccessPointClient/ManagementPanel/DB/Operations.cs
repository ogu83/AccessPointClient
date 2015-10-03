using Common;
using ManagementPanel.Models;
using System.Collections.Generic;
using System.Linq;

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
                        return new OperationResult<user> { Success = true, ReturnValue = user };
                }

            return new OperationResult<user> { Success = false, Message = "Incorrect username or password" };
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
                    return new OperationResult<IEnumerable<accessLog>> { ErrorCode = 987, Message = "Undefined Role" };
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

        public static OperationResult<IEnumerable<accessPoint>> GetAccessPoints(user user)
        {
            if ((Roles)user.Role_Id != Roles.Admin && (Roles)user.Role_Id != Roles.Manager)
                return new OperationResult<IEnumerable<accessPoint>> { ErrorCode = 9, Message = "Unauthorized" };

            IEnumerable<accessPoint> accessPoints = null;
            if ((Roles)user.Role_Id == Roles.Manager)
                accessPoints = Entities.accessPoint.Where(x => x.department_accessPoint.Count(y => y.Department_Id == user.Department_Id) > 0);
            else if ((Roles)user.Role_Id == Roles.Admin)
                accessPoints = Entities.accessPoint;

            return new OperationResult<IEnumerable<accessPoint>> { Success = true, ReturnValue = accessPoints };
        }

        public static OperationResult<accessPoint> GetAccessPoint(user user, int accessPointId)
        {
            if ((Roles)user.Role_Id != Roles.Admin && (Roles)user.Role_Id != Roles.Manager)
                return new OperationResult<accessPoint> { ErrorCode = 9, Message = "Unauthorized" };

            accessPoint aP = null;
            if ((Roles)user.Role_Id == Roles.Manager)
                aP = Entities.accessPoint
                    .Where(x => x.department_accessPoint.Count(y => y.Department_Id == user.Department_Id) > 0)
                    .SingleOrDefault(x => x.Id == accessPointId);
            else if ((Roles)user.Role_Id == Roles.Admin)
                aP = Entities.accessPoint.SingleOrDefault(x => x.Id == accessPointId);

            return new OperationResult<accessPoint> { Success = true, ReturnValue = aP };
        }

        public static OperationResult<accessPoint> EditAccessPoint(user user, AccessPoint accessPoint)
        {
            if ((Roles)user.Role_Id != Roles.Admin && (Roles)user.Role_Id != Roles.Manager)
                return new OperationResult<accessPoint> { ErrorCode = 9, Message = "Unauthorized" };

            //if ((Roles)user.Role_Id == Roles.Manager && user.Department_Id != accessPoint.Department.Id)
            //    return new OperationResult<accessPoint> { ErrorCode = 9, Message = "Unauthorized" };

            var ap = Entities.accessPoint.SingleOrDefault(x => x.Id == accessPoint.Id);

            if ((Roles)user.Role_Id == Roles.Manager && user.Department_Id != ap.department_accessPoint.FirstOrDefault().Department_Id)
                return new OperationResult<accessPoint> { ErrorCode = 9, Message = "Unauthorized" };

            if (ap != null)
            {
                ap.IPv4 = accessPoint.IPv4;
                ap.IPv6 = accessPoint.IPv6;
                ap.IsOn = accessPoint.IsOn ? (byte)0 : (byte)1;
                ap.Location = accessPoint.Location;
                ap.Name = accessPoint.Name;
            }

            Entities.SaveChanges();
            return new OperationResult<accessPoint> { Success = true, ReturnValue = ap };
        }

        public static OperationResult<accessPoint> TurnOnOffAccessPoint(user user, int accessPointId, bool isOn)
        {
            var accessPoint = Entities.accessPoint.SingleOrDefault(x => x.Id == accessPointId);
            if (accessPoint == null)
                return new OperationResult<accessPoint> { ErrorCode = 19, Message = "No Such Access Point" };

            if ((Roles)user.Role_Id != Roles.Admin && (Roles)user.Role_Id != Roles.Manager)
                return new OperationResult<accessPoint> { ErrorCode = 9, Message = "Unauthorized" };

            if ((Roles)user.Role_Id == Roles.Manager && user.Department_Id != accessPoint.department_accessPoint.FirstOrDefault().Department_Id)
                return new OperationResult<accessPoint> { ErrorCode = 9, Message = "Unauthorized" };

            accessPoint.IsOn = (byte)(isOn ? 1 : 0);

            Entities.SaveChanges();
            return new OperationResult<accessPoint>
            {
                Success = true,
                ReturnValue = accessPoint,
                Message = "Access point turned " + (accessPoint.IsOn != 0 ? "On" : "Off")
            };
        }

        public static OperationResult<accessPoint> AddAccessPoint(user user, AccessPoint accessPoint)
        {
            if ((Roles)user.Role_Id != Roles.Admin && (Roles)user.Role_Id != Roles.Manager)
                return new OperationResult<accessPoint> { ErrorCode = 9, Message = "Unauthorized" };

            var newDbAccessPoint = Entities.accessPoint.Add(new accessPoint
            {
                IPv4 = accessPoint.IPv4,
                IPv6 = accessPoint.IPv6,
                IsOn = accessPoint.IsOn ? (byte)1 : (byte)0,
                Location = accessPoint.Location,
                Name = accessPoint.Name,
            });
            Entities.SaveChanges();

            Entities.department_accessPoint.Add(new department_accessPoint
            {
                Department_Id = user.Department_Id,
                AccessPoint_Id = accessPoint.Id
            });
            Entities.SaveChanges();

            return new OperationResult<DB.accessPoint> { Success = true, ReturnValue = newDbAccessPoint };
        }
    }
}