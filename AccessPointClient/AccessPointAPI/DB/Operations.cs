using AccessPointAPI.Helpers;
using Common;
using System;
using System.Linq;

namespace AccessPointAPI.DB
{
    public class Operations
    {
        private static accessControlManagementEntities _entities;
        private static accessControlManagementEntities Entities
        {
            get
            {
                if (_entities == null)
                    _entities = new accessControlManagementEntities();
                return _entities;
            }
        }

        private static user getManagerOfUser(user user)
        {
            var manager = Entities.user.SingleOrDefault(x => (Roles)x.Role_Id == Roles.Manager && x.Department_Id == user.Department_Id);
            return manager;
        }

        public static OperationResult<user> GainAccessByAccessPoint(int userId, int accessPointId, string description)
        {
            try
            {
                var user = Entities.user.SingleOrDefault(x => x.Id == userId);
                if (user == null)
                    throw new NullReferenceException("No such user");

                var accessPoint = Entities.accessPoint.SingleOrDefault(x => x.Id == accessPointId);
                if (accessPoint == null)
                    throw new NullReferenceException("No such access point");

                if (accessPoint.IsOn == 0)
                    throw new UnauthorizedAccessException("Access point is turned off");

                string err = string.Empty;
                if (accessPoint.department_accessPoint.Count(x => x.Department_Id == user.Department_Id) == 0)
                {
                    err = "Denied: AP is not assigned user's department";
                    Entities.accessLog.Add(new accessLog
                    {
                        accessPoint = accessPoint,
                        user = user,
                        Description = err,
                        role = user.role,
                        Time = DateTime.Now,
                        Action_Id = 0
                    });
                    Entities.SaveChanges();
                    ExtMailHelper.SendUnauthorizedAccessMail(getManagerOfUser(user), user, accessPoint);
                    return new OperationResult<user>
                    {
                        Success = false,
                        ReturnValue = user,
                        Message = err
                    };                    
                }
                else if (user.WorkEndTime < DateTime.Now.TimeOfDay || user.WorkStartTime > DateTime.Now.TimeOfDay)
                {
                    err = "Denied: User is outside of working time";
                    Entities.accessLog.Add(new accessLog
                    {
                        accessPoint = accessPoint,
                        user = user,
                        Description = err,
                        role = user.role,
                        Time = DateTime.Now,
                        Action_Id = 0
                    });
                    Entities.SaveChanges();
                    ExtMailHelper.SendUnauthorizedAccessMail(getManagerOfUser(user), user, accessPoint);
                    return new OperationResult<user>
                    {
                        Success = false,
                        ReturnValue = user,
                        Message = err
                    };
                }
                else
                {
                    err = "Granted:" + description;
                    Entities.accessLog.Add(new accessLog
                    {
                        accessPoint = accessPoint,
                        user = user,
                        Description = err,
                        role = user.role,
                        Time = DateTime.Now,
                        Action_Id = 1
                    });
                    Entities.SaveChanges();
                    return new OperationResult<user>
                    {
                        Success = true,
                        ReturnValue = user,
                        Message = err
                    };
                }
            }
            catch (Exception ex)
            {
                return new OperationResult<user>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}