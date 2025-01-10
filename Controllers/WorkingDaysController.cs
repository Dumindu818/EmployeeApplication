using EmployeeApplication.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EmployeeApplication.Controllers
{
    public class WorkingDaysController : Controller
    {
        private readonly Repository _repository;

        public WorkingDaysController()
        {
            _repository = new Repository();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalculateWorkingDays(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Calculate working days using the repository method
                    int workingDays = _repository.GetWorkingDays(startDate, endDate);
                    ViewBag.WorkingDays = workingDays;
                    ViewBag.StartDate = startDate.ToString("yyyy-MM-dd");
                    ViewBag.EndDate = endDate.ToString("yyyy-MM-dd");

                    return View("Index");
                }

                // If ModelState is not valid, return the same view with errors
                return View("Index");
            }
            catch (Exception ex)
            {
                // Handle errors and display a meaningful message
                ViewBag.Error = "An error occurred while calculating working days. Please try again.";
                Console.WriteLine($"Error: {ex.Message}");
                return View("Index");
            }
        }
    }
}
