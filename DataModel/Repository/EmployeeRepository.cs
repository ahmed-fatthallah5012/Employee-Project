using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using DataModel.Models;

namespace DataModel.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Employee>> GetAllForSelectionAsync();
        Task<Employee> GetSingleByIdAsync(int id);
        Task<int> Insert(Employee employee);
        Task<bool> Update(Employee employee);
        Task<bool> Remove(int id);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _dbConnection;

        public EmployeeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
            var query = "if not exists (select name from sys.databases where name = N'EmployeeDb')" +
                        "\r\n\tbegin" +
                        "\r\n\t\tUSE [master]" +
                        "\r\n\t\t/****** Object:  Database [EmployeeDb]    Script Date: 23-Mar-21 5:07:51 PM ******/" +
                        "\r\n\t\tCREATE DATABASE [EmployeeDb]" +
                        "\r\n\t\tUSE [EmployeeDb]" +
                        "\r\n\t\t/****** Object:  Table [dbo].[Employee]    Script Date: 23-Mar-21 5:07:51 PM ******/" +
                        "\r\n\t\tCREATE TABLE [dbo].[Employee](" +
                        "\r\n\t\t\t[Id] [int] IDENTITY(1,1) NOT NULL," +
                        "\r\n\t\t\t[Name] [nvarchar](50) NULL," +
                        "\r\n\t\t\t[Email] [nvarchar](50) NULL," +
                        "\r\n\t\t\t[Phone] [nvarchar](11) NULL," +
                        "\r\n\t\t\t[Photo] [image] NULL," +
                        "\r\n\t\t\t[ParentId] [int] NULL" +
                        "\r\n\t\t) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]" +
                        "\r\n\tend";
            _dbConnection.Execute(query);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var query = "select emp.Id, emp.Name, emp.Email, emp.Phone, emp.Photo, parent.Id , parent.Name " +
                        "from Employee emp left join Employee parent on parent.Id = emp.ParentId";

            var employees = await _dbConnection.QueryAsync<Employee, Employee, Employee>(query, (emp, parent) =>
            {
                emp.ParentEmployee = parent;
                return emp;
            });
            return  employees;
        }

        public Task<IEnumerable<Employee>> GetAllForSelectionAsync()
        {
            var query = "select emp.Id, emp.Name from Employee emp ";

            var employees = _dbConnection.QueryAsync<Employee, Employee, Employee>(query, (emp, parent) =>
            {
                emp.ParentEmployee = parent;
                return emp;
            });
            return  employees;
        }

        public Task<Employee> GetSingleByIdAsync(int id) 
            => _dbConnection.GetAsync<Employee>(id);

        public Task<int> Insert(Employee employee)
        {
            return _dbConnection.InsertAsync(employee);
        }

        public Task<bool> Update(Employee employee) 
            => _dbConnection.UpdateAsync(employee);

        public async Task<bool> Remove(int id)
        {
            var employee = await GetSingleByIdAsync(id);
            return await _dbConnection.DeleteAsync(employee);
        }
    }
}