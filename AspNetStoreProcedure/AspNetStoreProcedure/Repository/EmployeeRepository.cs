using AspNetStoreProcedure.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AspNetStoreProcedure.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private SqlConnection connection;

        private void SqlConnection()
        {
            var constr = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            connection = new SqlConnection(constr);
        }

        public List<Employee> GetAllEmployees()
        {
            SqlConnection();
            var com = new SqlCommand("GetEmployees", connection);
            com.CommandType = CommandType.StoredProcedure;
            var dataAdapter = new SqlDataAdapter(com);
            var dataTable = new DataTable();
            connection.Open();
            dataAdapter.Fill(dataTable);
            connection.Close();

            var employeeList = (from DataRow dr in dataTable.Rows

                                select new Employee()
                                {
                                    Id = Convert.ToInt32(dr["Id"]),
                                    Name = Convert.ToString(dr["Name"]),
                                    City = Convert.ToString(dr["City"]),
                                    Address = Convert.ToString(dr["Address"])
                                }

            ).ToList();

            return employeeList;
        }

        public bool AddEmployee(Employee employee)
        {
            SqlConnection();
            var com = new SqlCommand("AddNewEmpDetails", connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", employee.Name);
            com.Parameters.AddWithValue("@City", employee.City);
            com.Parameters.AddWithValue("@Address", employee.Address);
            connection.Open();
            var count = com.ExecuteNonQuery();
            connection.Close();
            return count >= 1;
        }

        public bool UpdateEmployee(Employee employee)
        {
            SqlConnection();
            var com = new SqlCommand("UpdateEmployeeDetail", connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", employee.Id);
            com.Parameters.AddWithValue("@Name", employee.Name);
            com.Parameters.AddWithValue("@City", employee.City);
            com.Parameters.AddWithValue("@Address", employee.Address);
            connection.Open();
            var count = com.ExecuteNonQuery();
            connection.Close();
            return count >= 1;
        }

        public bool DeleteEmployee(int id)
        {
            SqlConnection();
            var com = new SqlCommand("DeleteEmployee", connection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", id);
            connection.Open();
            var count = com.ExecuteNonQuery();
            connection.Close();
            return count >= 1;
        }
    }
}