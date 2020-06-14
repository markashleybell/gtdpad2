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
                optionsMonitor?.CurrentValue ?? new Settings(),
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
             await WithConnection(async conn => {
                 await conn.ExecuteAsync(
                     sql: "DELETE FROM Pages WHERE ID = @ID AND Owner = @Owner",
                     param: new { ID = id, Owner = _userId }
                 );
             });

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
                    sql: "SELECT ID, Title, Slug FROM Pages WHERE ID = @ID AND Owner = @Owner",
                    param: new { ID = id, Owner = _userId }
                );
            });

        public async Task PersistPage(Page page) =>
            await WithConnection(async conn => {
                var existingPage = await GetPage(page.ID);

                var sql = existingPage is null
                    ? "INSERT INTO Pages (ID, Owner, Title, Slug) VALUES (@ID, @Owner, @Title, @Slug)"
                    : "UPDATE Pages SET Title = @Title, Slug = @Slug WHERE ID = @ID";

                await conn.ExecuteAsync(
                    sql: sql,
                    param: new { page.ID, Owner = _userId, page.Title, page.Slug }
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
