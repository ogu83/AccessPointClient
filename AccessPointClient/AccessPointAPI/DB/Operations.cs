using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                if (accessPoint.department_accessPoint.Count(x => x.Department_Id == user.Department_Id) > 0)
                {
                    Entities.accessLog.Add(new accessLog
                    {
                        accessPoint = accessPoint,
                        user = user,
                        Description = "Granted:" + description,
                        role = user.role,
                        Time = DateTime.Now,
                        Action_Id = 1
                    });
                    Entities.SaveChanges();

                    return new OperationResult<user>
                    {
                        Success = true,
                        ReturnValue = user
                    };
                }
                else
                {
                    Entities.accessLog.Add(new accessLog
                    {
                        accessPoint = accessPoint,
                        user = user,
                        Description = "Denied: AP is not assigned user's department",
                        role = user.role,
                        Time = DateTime.Now,
                        Action_Id = 0
                    });
                    Entities.SaveChanges();
                    return new OperationResult<user>
                    {
                        Success = false,
                        ReturnValue = user,
                        Message = "Access point is not assigned with user's department"
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