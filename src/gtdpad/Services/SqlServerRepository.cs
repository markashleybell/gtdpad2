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
        private readonly Guid? _userId;

        public SqlServerRepository(
            IOptionsMonitor<Settings> optionsMonitor,
            Guid? userId)
            : this(
                optionsMonitor.CurrentValue,
                userId)
        {
        }

        public SqlServerRepository(
            Settings settings,
            Guid? userId)
        {
            _cfg = settings;
            _userId = userId;
        }

        public async Task DeletePage(Guid id) =>
            throw new NotImplementedException();

        public async Task<User> FindUserByEmail(string email) =>
            await WithConnection(async conn => {
                return await conn.QuerySingleOrDefaultAsync<User>(
                    sql: "SELECT * FROM Users WHERE Email = @Email",
                    param: new { Email = email }
                );
            });

        public async Task<Page> GetPage(Guid id) =>
            await WithConnection(async conn => {
                return await conn.QuerySingleOrDefaultAsync<Page>(
                    sql: "SELECT ID, Title, Url FROM Pages WHERE ID = @ID AND Owner = @Owner",
                    param: new { ID = id, Owner = _userId }
                );
            });

        public async Task PersistPage(Page page) =>
            throw new NotImplementedException();

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
