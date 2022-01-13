using Microsoft.Extensions.Configuration;
using OmanSOS.Core.Constants;
using System.Data.SqlClient;

namespace OmanSOS.Data.Repositories
{
    public class BaseRepository
    {
        private readonly IConfiguration _configuration;

        public BaseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString(Config.DbConnectionString));
        }
    }
}
