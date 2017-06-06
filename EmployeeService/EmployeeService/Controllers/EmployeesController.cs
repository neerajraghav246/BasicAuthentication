using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Employee.DAL;
using System.Web.Http.Cors;
using System.Threading;

namespace EmployeeService.Controllers
{
    [EnableCorsAttribute("http://localhost:1245", "*", "*")]
    public class EmployeesController : ApiController
    {
        [BasicAuthentication]
        public IEnumerable<Employee.DAL.Employee> Get()
        {
            string userName = Thread.CurrentPrincipal.Identity.Name; 
            using (var _context = new EmployeeDBEntities())
            {
                return _context.Employees.ToList();
            }
        }
        public Employee.DAL.Employee Get(int id)
        {
            using (var _context = new EmployeeDBEntities())
            {
                return _context.Employees.FirstOrDefault(x=> x.ID==id);
            }
        }
    }
}
