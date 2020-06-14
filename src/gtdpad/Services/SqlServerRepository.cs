using System;
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

        public SqlServerRepository(IOptionsMonitor<Settings> optionsMonitor)
            : this(optionsMonitor?.CurrentValue ?? new Settings())
        {
        }

        public SqlServerRepository(Settings settings) =>
            _cfg = settings;

        public async Task DeleteImageBlock(Guid id) =>
            await WithConnection(async conn => {
                await conn.ExecuteAsync(
                    sql: "DELETE FROM Sections WHERE ID = @ID",
                    param: new { ID = id }
                );
            });

        public async Task DeleteList(Guid id) =>
            await WithConnection(async conn => {
                await conn.ExecuteAsync(
                    sql: "DELETE FROM Sections WHERE ID = @ID",
                    param: new { ID = id }
                );
            });

        public async Task DeletePage(Guid id) =>
             await WithConnection(async conn => {
                 await conn.ExecuteAsync(
                     sql: "DELETE FROM Pages WHERE ID = @ID",
                     param: new { ID = id }
                 );
             });

        public async Task DeleteRichTextBlock(Guid id) =>
            await WithConnection(async conn => {
                var sql = @"
DELETE FROM TextBlocks WHERE ID = @ID;

DELETE FROM Sections WHERE ID = @ID;";

                await conn.ExecuteAsync(
                    sql: sql,
                    param: new { ID = id }
                );
            });

        public async Task<User> FindUserByEmail(string email) =>
            await WithConnection(async conn => {
                return await conn.QuerySingleOrDefaultAsync<User>(
                    sql: "SELECT * FROM Users WHERE Email = @Email",
                    param: new { Email = email }
                );
            });

        public async Task<ImageBlock> GetImageBlock(Guid id) =>
            await WithConnection(async conn => {
                const string sql = @"
SELECT
    s.ID,
    s.Owner,
    s.Title
FROM
    Sections s
WHERE
    s.ID = @ID";

                return await conn.QuerySingleOrDefaultAsync<ImageBlock>(
                    sql: sql,
                    param: new { ID = id }
                );
            });

        public async Task<List> GetList(Guid id) =>
            await WithConnection(async conn => {
                const string sql = @"
SELECT
    s.ID,
    s.Owner,
    s.Title
FROM
    Sections s
WHERE
    s.ID = @ID";

                return await conn.QuerySingleOrDefaultAsync<List>(
                    sql: sql,
                    param: new { ID = id }
                );
            });

        public async Task<Page> GetPage(Guid id) =>
            await WithConnection(async conn => {
                return await conn.QuerySingleOrDefaultAsync<Page>(
                    sql: "SELECT ID, Owner, Title, Slug FROM Pages WHERE ID = @ID",
                    param: new { ID = id }
                );
            });

        public async Task<RichTextBlock> GetRichTextBlock(Guid id) =>
            await WithConnection(async conn => {
                const string sql = @"
SELECT
    s.ID,
    s.Owner,
    s.Title,
    tb.Text
FROM
    Sections s
INNER JOIN
    TextBlocks tb ON tb.ID = s.ID
WHERE
    s.ID = @ID";

                return await conn.QuerySingleOrDefaultAsync<RichTextBlock>(
                    sql: sql,
                    param: new { ID = id }
                );
            });

        public async Task PersistImageBlock(ImageBlock imageBlock, Guid pageID) =>
            await WithConnection(async conn => {
                var insertSql = @"
INSERT INTO Sections
    (ID, Owner, Page, Title, Type)
VALUES
    (@ID, @Owner, @Page, @Title, 2);";

                var updateSql = @"
UPDATE Sections SET Title = @Title, Page = @Page WHERE ID = @ID;
";

                var existing = await GetImageBlock(imageBlock.ID);

                var sql = existing is null
                    ? insertSql
                    : updateSql;

                await conn.ExecuteAsync(
                    sql: sql,
                    param: new {
                        imageBlock.ID,
                        imageBlock.Owner,
                        Page = pageID,
                        imageBlock.Title
                    }
                );
            });

        public async Task PersistList(List list, Guid pageID) =>
            await WithConnection(async conn => {
                var insertSql = @"
INSERT INTO Sections
    (ID, Owner, Page, Title, Type)
VALUES
    (@ID, @Owner, @Page, @Title, 3);";

                var updateSql = @"
UPDATE Sections SET Title = @Title, Page = @Page WHERE ID = @ID;
";

                var existing = await GetList(list.ID);

                var sql = existing is null
                    ? insertSql
                    : updateSql;

                await conn.ExecuteAsync(
                    sql: sql,
                    param: new {
                        list.ID,
                        list.Owner,
                        Page = pageID,
                        list.Title
                    }
                );
            });

        public async Task PersistPage(Page page) =>
            await WithConnection(async conn => {
                var existing = await GetPage(page.ID);

                var sql = existing is null
                    ? "INSERT INTO Pages (ID, Owner, Title, Slug) VALUES (@ID, @Owner, @Title, @Slug)"
                    : "UPDATE Pages SET Title = @Title, Slug = @Slug WHERE ID = @ID";

                await conn.ExecuteAsync(
                    sql: sql,
                    param: new {
                        page.ID,
                        page.Owner,
                        page.Title,
                        page.Slug
                    }
                );
            });

        public async Task PersistRichTextBlock(RichTextBlock richTextBlock, Guid pageID) =>
            await WithConnection(async conn => {
                var insertSql = @"
INSERT INTO Sections
    (ID, Owner, Page, Title, Type)
VALUES
    (@ID, @Owner, @Page, @Title, 1);

INSERT INTO TextBlocks
    (ID, Text)
VALUES
    (@ID, @Text);";

                var updateSql = @"
UPDATE Sections SET Title = @Title, Page = @Page WHERE ID = @ID;

UPDATE TextBlocks SET Text = @Text WHERE ID = @ID;
";

                var existing = await GetRichTextBlock(richTextBlock.ID);

                var sql = existing is null
                    ? insertSql
                    : updateSql;

                await conn.ExecuteAsync(
                    sql: sql,
                    param: new {
                        richTextBlock.ID,
                        richTextBlock.Owner,
                        Page = pageID,
                        richTextBlock.Title,
                        richTextBlock.Text
                    }
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
