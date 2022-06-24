using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace MediatR.Infra.Lite
{
    internal static class SqliteMemory
    {
        #region Properties

        public static LoggerFactory DebugLoggerFactory { get; } = new LoggerFactory(
            new[] {new DebugLoggerProvider()},
            new LoggerFilterOptions
            {
                Rules =
                {
                    new LoggerFilterRule(null,
                        DbLoggerCategory.Database.Command.Name,
                        LogLevel.Trace,
                        (s, info, l)
                            => info.Contains("Command", StringComparison.OrdinalIgnoreCase))
                }
            });

        #endregion Properties

        #region Methods

        public static DbContextOptionsBuilder UseDebugLogger(this DbContextOptionsBuilder @this)
        {
            return @this.UseLoggerFactory(DebugLoggerFactory);
        }

        public static DbContextOptionsBuilder<TDbContext> UseDebugLogger<TDbContext>(
            this DbContextOptionsBuilder<TDbContext> @this) where TDbContext : DbContext
        {
            return @this.UseLoggerFactory(DebugLoggerFactory);
        }

        public static DbContextOptionsBuilder<TDbContext> UseSqliteMemory<TDbContext>(
            this DbContextOptionsBuilder<TDbContext> @this) where TDbContext : DbContext
        {
            ((DbContextOptionsBuilder) @this).UseSqliteMemory();
            return @this;
        }

        public static DbContextOptionsBuilder UseSqliteMemory(this DbContextOptionsBuilder @this)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder {DataSource = ":memory:"};
            var sqliteConnection = new SqliteConnection(connectionStringBuilder.ToString());
            sqliteConnection.Open();

            @this.UseSqlite(sqliteConnection);
            return @this;
        }

        #endregion Methods
    }
}