using AspNetStoreProcedure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetStoreProcedure.Repository
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAllEmployees();

        bool AddEmployee(Employee employee);

        bool UpdateEmployee(Employee employee);

        bool DeleteEmployee(int id);
    }
}