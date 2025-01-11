using EmployeeApplication.Data; // Importing the Data layer for database interactions.
using Microsoft.AspNetCore.Mvc;// Importing ASP.NET Core MVC namespace for building web controllers.
using System;

namespace EmployeeApplication.Controllers
{
    // Controller class for handling actions related to working days.
    public class WorkingDaysController : Controller
    {
        private readonly Repository _repository; // repository for database and logic operations.

        // constructor to initialize the repository.
        public WorkingDaysController()
        {
            _repository = new Repository(); // create an instance of the Repository.
        }

        // Action method to display the default view for working days.
        public IActionResult Index()
        {
            return View();// Render the Index view.
        }

        // Action method to calculate the number of working days between two dates.
        [HttpPost]
        public IActionResult CalculateWorkingDays(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (ModelState.IsValid) // Check if the input data is valid.
                {
                    // Calculate working days using the repository method
                    int workingDays = _repository.GetWorkingDays(startDate, endDate);
                    ViewBag.WorkingDays = workingDays;
                    ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
                    ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");

                    return View("Index"); // Render the Index view with updated data.
                }

                // If ModelState is not valid, return the same view with errors
                return View("Index");
            }
            catch (Exception ex)
            {
                // Handle errors and display a meaningful message
                ViewBag.Error = "An error occurred while calculating working days. Please try again.";
                Console.WriteLine($"Error: {ex.Message}");
                return View("Index"); // Render the Index view with the error message.
            }
        }
    }
}
