using Common;
using ReportingTool.DB;

namespace ReportingTool.Helpers
{
    public class ExtMailHelper : MailHelper
    {
        public static void SendReport(user manager, string body)
        {
            Send("system@accessPointController.com", manager.Email, "DailyReport", body);
        }
    }
}
