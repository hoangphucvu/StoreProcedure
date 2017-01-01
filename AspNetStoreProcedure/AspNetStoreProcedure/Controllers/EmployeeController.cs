using AspNetStoreProcedure.Models;
using AspNetStoreProcedure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetStoreProcedure.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeController() : this(new EmployeeRepository())
        {
        }

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // GET: Employee
        public ActionResult Index()
        {
            var employeeList = _employeeRepository.GetAllEmployees();
            return View(employeeList);
        }

        // GET: Employee/Create
        public ActionResult AddEmployee()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_employeeRepository.AddEmployee(employee))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditEmpDetails(int id)
        {
            var employeeDetail = _employeeRepository.GetAllEmployees().Find(Emp => Emp.Id == id);
            return View(employeeDetail);
        }

        // POST: Employee/EditEmpDetails/5
        [HttpPost]
        public ActionResult EditEmpDetails(int id, Employee employee)
        {
            try
            {
                var employeDetail = _employeeRepository.UpdateEmployee(employee);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/DeleteEmp/5
        public ActionResult DeleteEmp(int id)
        {
            if (_employeeRepository.DeleteEmployee(id))
            {
                ViewBag.AlertMsg = "Employee details deleted successfully";
            }
            return RedirectToAction("Index");
        }
    }
}