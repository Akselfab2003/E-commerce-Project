using E_commerce.Logic;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models_Logic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Test
{
    public class CreateFakeDBDependencies : IDisposable
    {
        //private readonly DBcontext FakeContext;
        private readonly IDataCollection dataCollection;
        private readonly IConfiguration configuration;
        private DbContextOptions<DBcontext> contextOptions;
        private SqliteConnection connection;

        public CreateFakeDBDependencies() 
        {
            connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            contextOptions = new DbContextOptionsBuilder<DBcontext>().UseSqlite<DBcontext>(connection).Options;


            if (File.Exists("..\\net8.0\\Create data for local database\\Secret.json"))
            {

                FileStream Filestream = new FileStream("..\\net8.0\\Create data for local database\\Secret.json", FileMode.Open, FileAccess.Read);
                configuration = new ConfigurationBuilder().AddJsonStream(Filestream).Build();


            }
            else
            {



                MemoryStream test = new MemoryStream(System.Text.Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JSONCONFIG")));

                configuration = new ConfigurationBuilder().AddJsonStream(test).Build();

            }

           using var FakeContext = new DBcontext(contextOptions);

            if (FakeContext.Database.EnsureCreated())
            {
                dataCollection = new DataCollection(FakeContext, configuration);
            }
        }

        public DBcontext CreateContext() => new DBcontext(contextOptions);

        public void Dispose()
        {
            connection.Dispose();
        }

        public IDataCollection DataCollection
        {
            get { return new DataCollection(CreateContext(), configuration); }
        }

    }
}
