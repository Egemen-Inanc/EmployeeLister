using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using WaterLogger.Models;

namespace WaterLogger.Pages
{
    public class IndexModel : PageModel
    {
        

        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Employee> employees { get; set; }
        public void OnGet()
        {
            employees = GetAllEmployees();
        }

        private List<Employee> GetAllEmployees() 
        {
            using (var connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                connection.Open();
                var employees = new List<Employee>();
                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $" SELECT * FROM EmployeeList "; 
                var reader = tableCmd.ExecuteReader();
                while (reader.Read())
                {
                    employees.Add(new Employee
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Surname = reader.GetString(2),
                        Salary = reader.GetInt32(3),
                    });
                }
                return employees;
            }
        }
    }
}