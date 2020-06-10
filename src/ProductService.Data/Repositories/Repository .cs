using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace ProductMicroservice.Data.Repositories
{
    public class Repository 
    {
        private readonly IConfiguration _config;

        public Repository(IConfiguration config)
        {
            _config = config;

        }

        protected SqliteConnection CreateConnection()
        {
            var connectionString = _config["connectionString"];
            return new SqliteConnection(connectionString);
        }
    }
}
