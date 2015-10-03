using System.Collections.Generic;

namespace ManagementPanel.Models
{
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