using Common;
using ReportingTool.DB;
using ReportingTool.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReportingTool
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("***Reporting Service***");
            Console.WriteLine("1. Save Hr Csv Daily Log Now");
            Console.WriteLine("2. Mail Managers Activity Log");
            Console.WriteLine("3. Trigger Hr Csv Daily Log at 23:00");
            var inKey = int.Parse(Console.ReadLine());
            switch (inKey)
            {
                case 1:
                    SaveHrCsvDailyLog();
                    break;
                case 2:
                    SendManagerDailyLogs();
                    break;
                case 3:
                    while (true)
                    {
                        Thread.Sleep(1000);
                        if (DateTime.Now.Hour == 23 && DateTime.Now.Minute == 0 && DateTime.Now.Second < 2)
                        {
                            SendManagerDailyLogs();
                            SaveHrCsvDailyLog();
                        }
                        else
                            Console.WriteLine("Waiting at " + DateTime.Now.TimeOfDay);
                    }
                default:
                    break;
            }
        }

        private static void SaveHrCsvDailyLog()
        {
            var csv = attendanceReport();
            File.WriteAllText("C:\\HRShared\\_" + DateTime.Now.ToString("ddMMyyyy"), csv);
        }
        private static string attendanceReport()
        {
            var yesterday = DateTime.Now.AddDays(-1);
            var csv = "User Id;"
                + "FullName;"
                + "Total Time in Hours;";
            var entities = new accessControlManagementEntities();
            var logs = entities.accessLog.Where(
                x => x.Time > yesterday &&
                     x.Time < DateTime.Now);

            var result = from r in logs
                         where r.Action_Id > 0
                         group r by new { r.user } into g
                         select new
                         {
                             UserId = g.Key.user.Id,
                             FullName = g.Key.user.FullName,
                             TotalTime = g.Sum(x => (x.Time-yesterday).TotalHours * (x.Action_Id == 1 ? -1 : (x.Action_Id == 2 ? 1 : 0))),
                         };

            foreach (var item in result)
                csv += item.UserId + ";"
                    + item.FullName + ";"
                    + item.TotalTime + " hours;";

            return csv;
        }

        private static void SendManagerDailyLogs()
        {
            var entities = new accessControlManagementEntities();
            var managers = entities.user.Where(x => (Roles)x.Role_Id == Roles.Manager);
            foreach (var manager in managers)
                ExtMailHelper.SendReport(manager, ManagerDailyLog(manager));
        }
        private static string ManagerDailyLog(user manager)
        {
            var yesterday = DateTime.Now.AddDays(-1);
            var csv = "Id;"
                    + "Time;"
                    + "Action_Id;"
                    + "roleId;"
                    + "roleName;"
                    + "userId;"
                    + "userFullName;"
                    + "accessPointId;"
                    + "accessPointName;"
                    + "Description;";
            var entities = new accessControlManagementEntities();
            var logs = entities.accessLog.Where(
                x => x.Time > yesterday &&
                     x.Time < DateTime.Now &&
                     x.user.Department_Id == manager.Department_Id);
            foreach (var log in logs)
                csv += log.Id + ";"
                    + log.Time + ";"
                    + log.Action_Id + ";"
                    + log.role.Id + ";"
                    + log.role.Name + ";"
                    + log.user.Id + ";"
                    + log.user.FullName + ";"
                    + log.accessPoint.Id + ";"
                    + log.accessPoint.Name + ";"
                    + log.Description + ";";

            return csv;
        }
    }
}