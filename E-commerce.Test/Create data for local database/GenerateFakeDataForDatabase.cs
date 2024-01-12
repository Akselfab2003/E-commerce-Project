using E_commerce.Logic;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models_Logic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public GenerateFakeDataForDatabase()
        {
            
            SecretClass test = JsonSerializer.Deserialize<SecretClass>(File.ReadAllText("..\\net8.0\\Create data for local database\\Secret.json"));
            var connection = new SqlConnection(test.Connection);
            connection.Open();
            DbContextOptions<DBcontext> contextOptions =
            new DbContextOptionsBuilder<DBcontext>().UseSqlServer(connection).Options;
            
            Context = new DBcontext(contextOptions);
            dataCollection = new DataCollection(Context);

        }
        public IDataCollection DataCollection
        {
            get { return dataCollection; }
        }

        public class SecretClass
        {
            public string Connection { get; set; }
        }
    }
}
