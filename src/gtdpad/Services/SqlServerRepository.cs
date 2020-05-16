using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using gtdpad.Domain;
using gtdpad.Support;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace gtdpad.Services
{
    public class SqlServerRepository : IRepository
    {
        private readonly Settings _cfg;
        private readonly string _username;

        public SqlServerRepository(
            IOptionsMonitor<Settings> optionsMonitor,
            string username)
            : this(
                optionsMonitor.CurrentValue,
                username)
        {
        }

        public SqlServerRepository(
            Settings settings,
            string username)
        {
            _cfg = settings;
            _username = username;
        }

        public async Task<User> FindUserByEmail(string email) =>
            await WithConnection(async conn => {
                return await conn.QuerySingleOrDefaultAsync<User>(
                    sql: "SELECT * FROM Users WHERE Email = @Email",
                    param: new { Email = email }
                );
            });

        private async Task WithConnection(Func<SqlConnection, Task> action)
        {
            using var connection = new SqlConnection(_cfg.ConnectionString);

            await action(connection);
        }

        private async Task<T> WithConnection<T>(Func<SqlConnection, Task<T>> action)
        {
            using var connection = new SqlConnection(_cfg.ConnectionString);

            return await action(connection);
        }
    }
}
