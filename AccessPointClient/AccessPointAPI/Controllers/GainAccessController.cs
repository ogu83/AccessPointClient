using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AccessPointAPI.DB;

namespace AccessPointAPI.Controllers
{
    public class GainAccessController : ApiController
    {
        private accessControlManagementEntities _entities = new accessControlManagementEntities();

        public string Get()
        {
            return "Running";
        }

        // POST api/gainaccess        
        public string Post(int userId, int accessPointId, string description)
        {
            var result = Operations.GainAccessByAccessPoint(userId, accessPointId, "Finger Print Readed");
            if (result.Success)
                return string.Format("{0} is entered to department {1} at {2}", result.ReturnValue.FullName, result.ReturnValue.department.Name, DateTime.Now);
            else
                return string.Format("Error Code : {0}, Message: {1}", result.ErrorCode, result.Message);
        }
    }
}
