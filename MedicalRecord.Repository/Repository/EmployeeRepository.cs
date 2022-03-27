using Dapper;
using MedicalRecord.Entity;
using MedicalRecord.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalRecord.Repository.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MedicalRecordDBContext _context;

        public EmployeeRepository(MedicalRecordDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            var query = "SELECT * FROM Employee";

            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee>(query);
                return employees.ToList();
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            var query = "SELECT * FROM Employee";

            using (var connection = _context.CreateConnection())
            {
                var employees = connection.Query<Employee>(query);
                return employees.ToList();
            }
        }
                
        public IEnumerable<Employee> SearchEmployees(int id, string firstName, string lastName, DateTime updatedDateStart, DateTime updatedDateEnd, int temperatureFrom, int temperatureTo)
        {
            int criteriaCount = 0;
            StringBuilder queryBuilder = new StringBuilder("SELECT * FROM Employee WHERE ");
            if (id > 0)
            {                 
                queryBuilder.AppendLine("(Id = @Id)");
                criteriaCount += 1;
            }
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                if (criteriaCount > 0) queryBuilder.AppendLine(" AND ");
                queryBuilder.AppendLine($"(FirstName LIKE '%{firstName}%')");
                criteriaCount += 1;
            }
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                if (criteriaCount > 0) queryBuilder.AppendLine(" AND ");
                queryBuilder.AppendLine($"(LastName LIKE '%{lastName}%')");
                criteriaCount += 1;
            }
            if (updatedDateStart != DateTime.MinValue && updatedDateEnd != DateTime.MinValue)
            {
                if (criteriaCount > 0) queryBuilder.AppendLine(" AND ");
                queryBuilder.AppendLine("(UpdatedDate BETWEEN @UpdatedDateStart AND @UpdatedDateEnd)");
                criteriaCount += 1;
            }
            if (temperatureFrom > 0 && temperatureTo > 0)
            {
                if (criteriaCount > 0) queryBuilder.AppendLine(" AND ");
                queryBuilder.AppendLine("(Temperature BETWEEN @TemperatureFrom AND @TemperatureTo)");
                criteriaCount += 1;
            }
                        
            var query = queryBuilder.ToString();

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("TemperatureFrom", temperatureFrom, DbType.Int32);
            parameters.Add("TemperatureTo", temperatureTo, DbType.Int32);
            parameters.Add("UpdatedDateStart", updatedDateStart, DbType.DateTime);
            parameters.Add("UpdatedDateEnd", updatedDateEnd, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var employees = connection.Query<Employee>(query, parameters);
                return employees.ToList();
            }
        }

        public async Task<IEnumerable<Employee>> SearchEmployeesAsync(int id, string firstName, string lastName, DateTime updatedDateStart, DateTime updatedDateEnd, int temperatureFrom, int temperatureTo)
        {
            var parameters = new DynamicParameters();
            int criteriaCount = 0;
            StringBuilder queryBuilder = new StringBuilder("SELECT * FROM Employee WHERE ");
            if (id > 0)
            {
                queryBuilder.AppendLine("(Id = @Id)");
                criteriaCount += 1;
            }
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                if (criteriaCount > 0) queryBuilder.AppendLine(" AND ");
                queryBuilder.AppendLine($"(FirstName LIKE '%{firstName}%')");
                criteriaCount += 1;
            }
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                if (criteriaCount > 0) queryBuilder.AppendLine(" AND ");
                queryBuilder.AppendLine($"(LastName LIKE '%{lastName}%')");
                criteriaCount += 1;
            }
            if (updatedDateStart != DateTime.MinValue && updatedDateEnd != DateTime.MinValue)
            {
                if (criteriaCount > 0) queryBuilder.AppendLine(" AND ");
                queryBuilder.AppendLine("(UpdatedDate BETWEEN @UpdatedDateStart AND @UpdatedDateEnd)");
                criteriaCount += 1;
                parameters.Add("UpdatedDateStart", updatedDateStart, DbType.DateTime);
                parameters.Add("UpdatedDateEnd", updatedDateEnd, DbType.DateTime);
            }
            if (temperatureFrom > 0 && temperatureTo > 0)
            {
                if (criteriaCount > 0) queryBuilder.AppendLine(" AND ");
                queryBuilder.AppendLine("(Temperature BETWEEN @TemperatureFrom AND @TemperatureTo)");
                criteriaCount += 1;
            }

            var query = queryBuilder.ToString();

            
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("TemperatureFrom", temperatureFrom, DbType.Int32);
            parameters.Add("TemperatureTo", temperatureTo, DbType.Int32);
            

            using (var connection = _context.CreateConnection())
            {
                var employees = await connection.QueryAsync<Employee>(query, parameters);
                return employees.ToList();
            }
        }
        public async Task<Employee> GetEmployeeAsync(int Id)
        {
            var query = "SELECT * FROM Employee WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var employee = await connection.QuerySingleOrDefaultAsync<Employee>(query, new { Id });
                return employee;
            }
        }

        public Employee GetEmployee(int Id)
        {
            var query = "SELECT * FROM Employee WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var employee = connection.QuerySingleOrDefault<Employee>(query, new { Id });
                return employee;
            }
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            var query = "INSERT INTO Employee (FirstName, LastName, Temperature, CreatedDate, UpdatedDate) " +
                "VALUES (@FirstName, @LastName, @Temperature, @CreatedDate, @UpdatedDate)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", employee.FirstName, DbType.String);
            parameters.Add("LastName", employee.LastName, DbType.String);
            parameters.Add("Temperature", employee.Temperature, DbType.Int32);
            parameters.Add("CreatedDate", employee.CreatedDate, DbType.DateTime);
            parameters.Add("UpdatedDate", employee.UpdatedDate, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var createdEmployee = new Employee
                {
                    Id = id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Temperature = employee.Temperature,
                    CreatedDate = employee.CreatedDate,
                    UpdatedDate = employee.UpdatedDate
                };

                return createdEmployee;
            }
        }
        public Employee CreateEmployee(Employee employee)
        {
            var query ="INSERT INTO Employee (FirstName, LastName, Temperature, CreatedDate, UpdatedDate) " +
                "VALUES (@FirstName, @LastName, @Temperature, @CreatedDate, @UpdatedDate)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("FirstName", employee.FirstName, DbType.String);
            parameters.Add("LastName", employee.LastName, DbType.String);
            parameters.Add("Temperature", employee.Temperature, DbType.Int32);
            parameters.Add("CreatedDate", employee.CreatedDate, DbType.DateTime);
            parameters.Add("UpdatedDate", employee.UpdatedDate, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                var id = connection.QuerySingle<int>(query, parameters);

                var createdEmployee = new Employee
                {
                    Id = id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Temperature = employee.Temperature,
                    CreatedDate = employee.CreatedDate,
                    UpdatedDate = employee.UpdatedDate
                };

                return createdEmployee;
            }
        }

        public void UpdateEmployee(int id, Employee employee)
        {
            var query = "UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, Temperature = @Temperature, " +
                "UpdatedDate = @UpdatedDate WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("FirstName", employee.FirstName, DbType.String);
            parameters.Add("LastName", employee.LastName, DbType.String);
            parameters.Add("Temperature", employee.Temperature, DbType.Int32);
            parameters.Add("UpdatedDate", employee.UpdatedDate, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, parameters);
            }
        }
        public async Task UpdateEmployeeAsync(int id, Employee employee)
        {
            var query = "UPDATE Employee SET FirstName = @FirstName, LastName = @LastName, Temperature = @Temperature, " +
                "UpdatedDate = @UpdatedDate WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("FirstName", employee.FirstName, DbType.String);
            parameters.Add("LastName", employee.LastName, DbType.String);
            parameters.Add("Temperature", employee.Temperature, DbType.Int32);
            parameters.Add("UpdatedDate", employee.UpdatedDate, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public void DeleteEmployee(int id)
        {
            var query = "DELETE FROM Employee WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                connection.Execute(query, parameters);
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var query = "DELETE FROM Employee WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
