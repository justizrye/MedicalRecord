using System;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MedicalRecord.Repository
{
    public class MedicalRecordDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public MedicalRecordDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
