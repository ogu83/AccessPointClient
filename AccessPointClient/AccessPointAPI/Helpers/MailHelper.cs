using AccessPointAPI.DB;
using Common;
using System;

namespace AccessPointAPI.Helpers
{
    public class ExtMailHelper : MailHelper
    {
        public static void SendUnauthorizedAccessMail(user manager, user user, accessPoint accessPoint)
        {
            try
            {
                var body = string.Format("UnauthorizedAccess by User: {0}-{1}, AccessPoint {2}-{3}, Time : {4}",
                user.Id, user.FullName, accessPoint.Id, accessPoint.Name, DateTime.Now);
                Send("system@accessPointController.com", manager.Email, "Unauthorized Access", body);
            }
            catch (Exception)
            {
                
            }
            
        }
    }
}