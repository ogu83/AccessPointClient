using ManagementPanel.DB;
using System.Collections.Generic;

namespace ManagementPanel.Models
{
    public class AccessLog
    {
        public string AccessPointName { get; set; }
        public string Description { get; set; }
        public string ActionName { get; set; }
        public string RoleName { get; set; }
        public string UserFullName { get; set; }
        public string TimeStamp { get; set; }

        public AccessLog() { }
        public AccessLog(accessLog log)
            : this()
        {
            AccessPointName = log.accessPoint.Name;
            Description = log.Description;
            switch (log.Action_Id)
            {
                case 0:
                    ActionName = "Unsuccessful Access";
                    break;
                case 1:
                    ActionName = "Successful Access";
                    break;
                default:
                    break;
            }
            RoleName = log.role.Name;
            UserFullName = log.user.FullName;
            TimeStamp = log.Time.ToString();
        }
    }

    public class AccessLogModel : BaseModel
    {
        public AccessLogModel()
        {
            Logs = new List<AccessLog>();
        }
        public int LogCount { get { return Logs.Count; } }
        public List<AccessLog> Logs { get; set; }
    }
}