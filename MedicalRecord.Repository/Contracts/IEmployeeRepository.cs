using MedicalRecord.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MedicalRecord.Repository.Contracts
{
    public interface IEmployeeRepository
    {
        public Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        public IEnumerable<Employee> GetAllEmployees();
        public IEnumerable<Employee> SearchEmployees(int id, string firstName, string lastName, DateTime updatedDateStart, DateTime updatedDateEnd, int temperatureFrom, int temperatureTo);
        public Task<IEnumerable<Employee>> SearchEmployeesAsync(int id, string firstName, string lastName, DateTime updatedDateStart, DateTime updatedDateEnd, int temperatureFrom, int temperatureTo);
        public Task<Employee> GetEmployeeAsync(int Id);
        public Employee GetEmployee(int Id);
        public Task<Employee> CreateEmployeeAsync(Employee employee);
        public Employee CreateEmployee(Employee employee);
        public Task UpdateEmployeeAsync(int id, Employee employee);
        public void UpdateEmployee(int id, Employee employee);
        public Task DeleteEmployeeAsync(int id);
        public void DeleteEmployee(int id);
    }
}
