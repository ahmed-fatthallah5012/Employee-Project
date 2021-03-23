using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DataModel.Models;

namespace EmployeeProject.Data
{
    public class EmployeeService
    {
        public IEnumerable<Employee> Employees { get; private set; }
        public Employee Employee { get; private set; } = new();
        private const string Url = "api/employee";
        private readonly HttpClient _client;

        public EmployeeService(HttpClient client) => _client = client;

        public async Task GetAllEmployeesData()
        {
            Employees = await _client.GetFromJsonAsync<IEnumerable<Employee>>(Url);
        }

        public void SetEmployee(Employee model) => Employee = model;

        public async Task RemoveEmployeeAsync()
        {
            await _client.DeleteAsync($"{Url}/{Employee.Id}");
        }

        public async Task UpdateEmployeeAsync()
        {
            await _client.PutAsJsonAsync(Url, Employee);
        }

        public async Task AddEmployeeAsync()
        {
            await _client.PostAsJsonAsync(Url, Employee);
        }

        public async Task GetSingleEmployee(int id)
        {
            Employee = await _client.GetFromJsonAsync<Employee>($"{Url}/{id}");
        }
    }
}