using Jibble.Employees;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Paillave.Etl.Core;
using Paillave.Etl.FileSystem;
using Paillave.Etl.SqlServer;
using Paillave.Etl.TextFile;
using Paillave.Etl.Zip;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Jibble.Services
{
    public class CSVImportService : ICSVImportService
    {
        IConfiguration Configuration { get; }
        ILogger Logger { get; }
        public CSVImportService(IConfiguration configuration, ILogger<CSVImportService> logger)
        {
            Configuration = configuration;
            Logger = logger;
        }
        public async Task ProcessAsync(string folderName, CancellationToken cancellationToken = default)
        {
            Logger.LogInformation($"Starting import - {folderName}");
            var connectionString = Configuration.GetConnectionString("Default");
            var processRunner = StreamProcessRunner.Create<string>(DefineProcess);
            processRunner.DebugNodeStream += (sender, e) => { /* place a conditional breakpoint here for debug */ };
            using (var cnx = new SqlConnection(connectionString))
            {
                cnx.Open();
                var executionOptions = new ExecutionOptions<string>
                {
                    Resolver = new SimpleDependencyResolver().Register(cnx),
                };
                var res = await processRunner.ExecuteAsync(folderName, executionOptions);
                Logger.LogInformation(res.Failed ? "Failed" : "Succeeded");
                if (res.Failed)
                    Logger.LogTrace($"{res.ErrorTraceEvent.NodeName}({res.ErrorTraceEvent.NodeTypeName}):{res.ErrorTraceEvent.Content.Message}");
            }
        }

        public void DefineProcess(ISingleStream<string> contextStream)
        {
            var zipStream = contextStream
                .CrossApplyFolderFiles("list all required files", "*.zip", true)
                .CrossApplyZipFiles("extract files from zip", "*.csv");
            var csvStream = contextStream
                .CrossApplyFolderFiles("list all required files", "*.csv", true);

            zipStream.Union("merge two streams", csvStream)
                .CrossApplyTextFile("parse file", FlatFileDefinition.Create(i => new Employee
                {
                    Id = i.ToColumn<int>(EmployeeConsts.CSV.Id),
                    FirstName = i.ToColumn(EmployeeConsts.CSV.FirstName),
                    LastName = i.ToColumn(EmployeeConsts.CSV.LastName),
                    DateOfBirth = i.ToOptionalDateColumn(EmployeeConsts.CSV.DateOfBirth, EmployeeConsts.CSV.DateFormat)
                }).IsColumnSeparated(EmployeeConsts.CSV.Separator))
                .Distinct("exclude duplicates based on the Id", i => i.Id)
                .SqlServerSave("upsert using Id as key", o => o
                    .ToTable("dbo.Employees")
                    .SeekOn(p => p.Id));
        }
    }
}
