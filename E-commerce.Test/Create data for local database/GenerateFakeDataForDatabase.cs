using E_commerce.Logic;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models_Logic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace E_commerce.Test
{
    public class GenerateFakeDataForDatabase
    {
        private readonly DBcontext Context;
        private readonly IDataCollection dataCollection;
        private readonly IConfiguration configuration;
        public GenerateFakeDataForDatabase()
        {
            
            SecretClass test = JsonSerializer.Deserialize<SecretClass>(File.ReadAllText("..\\net8.0\\Create data for local database\\Secret.json"));
            var connection = new SqlConnection(test.Connection);
            connection.Open();
            DbContextOptions<DBcontext> contextOptions =
            new DbContextOptionsBuilder<DBcontext>().UseSqlServer(connection).Options;

          

            FileStream stream = new FileStream("..\\net8.0\\Create data for local database\\Secret.json",FileMode.Open,FileAccess.Read);
            configuration = new ConfigurationBuilder().AddJsonStream(stream).Build();
               

            Context = new DBcontext(contextOptions);
            dataCollection = new DataCollection(Context,configuration);

        }
        public IDataCollection DataCollection
        {
            get { return dataCollection; }
        }

        public class SecretClass
        {
            public string Connection { get; set; }
            public string SALT { get; set; }
        }
    }
}
