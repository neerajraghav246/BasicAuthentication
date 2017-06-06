using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace EmployeeService
{
    public class BasicAuthenticationAttribute:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string token = actionContext.Request.Headers.Authorization.Parameter;
                string decodedToken =Encoding.UTF8.GetString( Convert.FromBase64String(token));
                string[] tokenAray = decodedToken.Split(':');
                if (tokenAray.Length == 2)
                {
                    string userName = tokenAray[0];
                    string userPWD = tokenAray[1];
                    if (EmployeeSecurity.ValidateCredentials(userName, userPWD))
                    {
                        string[] roles = new string[] { "admin" };
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userName),roles);
                    }
                    else
                    {
                        actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
                
            }
        }
    }
}