using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using Xunit;

namespace LinuxTests
{
    public class PgFixture : IDisposable
    {
        private readonly PostgresRunner _pgRunner;

        public PgFixture()
        {
            _pgRunner = PostgresRunner
                .Start(new PostgresRunnerOptions{ BinariesSearchPattern = "/pg-dist/pgsql-10.1-linux-binaries/bin"});
        }

        public void Dispose() => _pgRunner?.Dispose();

        public string ConnectionString => _pgRunner.GetConnectionString();
    }
    
    public class Fixture : IClassFixture<PgFixture>
    {
        private readonly PgFixture _fixture;

        public when_using_as_class_fixture(PgFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task should_create_new_database_and_allow_exec_query()
        {
            // PREPARE
            var dbName = "test_db";

            var cmdBuilder = new StringBuilder();
            cmdBuilder.AppendLine($"CREATE DATABASE {dbName}");
            cmdBuilder.AppendLine("CONNECTION LIMIT = -1");

            // RUN
            using (var conn = new Npgsql.NpgsqlConnection(_fixture.ConnectionString))
            {
                await conn.OpenAsync();
                var cmd = new NpgsqlCommand(cmdBuilder.ToString(), conn);
                cmd.ExecuteNonQuery();

                // ASSERT
                var dbExists = await new NpgsqlCommand($"SELECT datname FROM pg_catalog.pg_database WHERE datname = '{dbName}'",conn)
                    .ExecuteScalarAsync();

                Assert.NotNull(dbExists);
            }
        }        
    }
}
