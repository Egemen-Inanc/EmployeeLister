using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using WaterLogger.Models;

namespace WaterLogger.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public CreateModel(IConfiguration configuration) 
        { 
            _configuration = configuration;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Employee employee { get; set; }
        public IActionResult OnPost()
        {
            using(var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = @$"INSERT INTO EmployeeList(Name,Surname,Salary) VALUES('{employee.Name}','{employee.Surname}',{employee.Salary})";
                tableCmd.ExecuteNonQuery();
                
            }
            return RedirectToPage("./Index");
        }
        
    }
}
