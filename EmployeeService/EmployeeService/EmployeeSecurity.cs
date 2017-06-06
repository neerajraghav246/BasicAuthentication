using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeService
{
    public class EmployeeSecurity
    {
        public static bool ValidateCredentials(string userName,string password)
        {
            using(var _context=new Employee.DAL.EmployeeDBEntities())
            {
                return _context.Users.Any(x => x.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && x.Password == password);
            }
        }
    }
}