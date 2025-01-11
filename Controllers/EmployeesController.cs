using EmployeeApplication.Data; // Importing the Data layer for database interactions.
using EmployeeApplication.Models; // Importing the Models layer for data structures.
using Microsoft.AspNetCore.Mvc; // Importing ASP.NET Core MVC namespace for building web controllers.

namespace EmployeeApplication.Controllers
{
    public class EmployeesController : Controller
    {

        // Action method for displaying the list of all employees.
        public IActionResult Index()
        {
            List<EmployeesEntity> employees = new List<EmployeesEntity>(); // List to hold employee data.

            Repository repository = new Repository();  // Initialize the repository for database operations.

            employees  = repository.GetAllEmployees(); // Fetch all employees from the database.

            return View(employees); // Pass the employee data to the Index view.
        }

        //action method for display particular employee details before deletion
        public ActionResult DeleteEmployee(int Id) 
        {
            EmployeesEntity employees = new EmployeesEntity(); // Create an object to hold employee data.

            Repository repository = new Repository();

            employees = repository.GetEmployeeById(Id); // Fetch the employee by ID.

            return View(employees); // Pass the employee data to the DeleteEmployee view.

        }


        // action method for handle delete employee
        public IActionResult DeleteEmployeeDetails(int Id, EmployeesEntity employeeDetails)
        {
            try
            {

                if (ModelState.IsValid) // Check if the provided data is valid.
                {
                    Repository _DbEmployee = new Repository();  // Initialize the repository.
                    if (_DbEmployee.DeleteEmployeeDetails(Id, employeeDetails)) // Attempt to delete the employee.
                    {
                        return RedirectToAction("Index"); // Redirect to the Index view if successful.
                    }
                }

                return View();// If deletion fails or data is invalid, return the view.

            }
            catch (Exception ex)
            {

                return View(); // Handle any exceptions and return the view.

            }
        }

        //action method for display particular employee details before editing (same as delete)
        public IActionResult EditEmployee(int Id)
        {
            EmployeesEntity employees = new EmployeesEntity();

            Repository repository = new Repository();

            employees = repository.GetEmployeeById(Id);

            return View(employees);
        }

        // action method for handle edit employee details (same as delete)
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

        // action method for displaying the form to add a new employee.
        public ActionResult AddEmployee()
        {
            return View(); // Render the AddEmployee view.
        }

        // action method for handle add employee details (same as delete,edit)
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
