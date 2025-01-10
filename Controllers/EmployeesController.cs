using EmployeeApplication.Data;
using EmployeeApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApplication.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Index()
        {
            List<EmployeesEntity> employees = new List<EmployeesEntity>();

            Repository repository = new Repository();

            employees  = repository.GetAllEmployees();

            return View(employees);
        }

        public ActionResult DeleteEmployee(int Id)
        {
            EmployeesEntity employees = new EmployeesEntity();

            Repository repository = new Repository();

            employees = repository.GetEmployeeById(Id);

            return View(employees);

        }

        public IActionResult DeleteEmployeeDetails(int Id, EmployeesEntity employeeDetails)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Repository _DbEmployee = new Repository();
                    if (_DbEmployee.DeleteEmployeeDetails(Id, employeeDetails))
                    {
                        return RedirectToAction("Index");
                    }
                }

                return View();

            }
            catch (Exception ex)
            {

                return View();

            }
        }

        public IActionResult EditEmployee(int Id)
        {
            EmployeesEntity employees = new EmployeesEntity();

            Repository repository = new Repository();

            employees = repository.GetEmployeeById(Id);

            return View(employees);
        }

        public IActionResult EditEmployeeDetails(int Id, EmployeesEntity employeeDetails)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    Repository _DbEmployee = new Repository();
                    if (_DbEmployee.EditEmployeeDetails(Id, employeeDetails))
                    {
                        return RedirectToAction("Index");
                    }
                }

                return View();

            }
            catch (Exception ex)
            {

                return View();

            }
        }

        public ActionResult AddEmployee()
        {
            return View();
        }

        public ActionResult AddNewEmployee(EmployeesEntity employeeDetails)
        {
            try {

                if (ModelState.IsValid)
                {
                    Repository _DbEmployee = new Repository();
                    if (_DbEmployee.AddEmployee(employeeDetails)) { 
                        return RedirectToAction("Index");
                    }
                }

                return View();
            
            } catch (Exception ex) {

                return View();

            }
            
        }
    }
}
