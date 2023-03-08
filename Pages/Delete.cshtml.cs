using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using System.Globalization;
using System.Xml.Linq;
using WaterLogger.Models;

namespace EmployeeLister.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;

        [BindProperty]
        public Employee employee { get; set; }

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult OnGet(int id)
        {
            employee = GetById(id);

            return Page();
        }

        private Employee GetById(int id)
        {
            var employee = new Employee();

            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText =
                    $"SELECT * FROM EmployeeList WHERE Id = {id}";

                SqliteDataReader reader = tableCmd.ExecuteReader();

                while (reader.Read())
                {

                    employee.Id = reader.GetInt32(0);
                    employee.Name = reader.GetString(1);
                    employee.Surname = reader.GetString(2);
                    employee.Salary = reader.GetInt32(3);
                }

                return employee;

            }
        }

        public IActionResult OnPost(int id)
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = $"DELETE from EmployeeList WHERE Id = {id}";

                tableCmd.ExecuteNonQuery();
            }

            return RedirectToPage("./Index");
        }


    }
}
