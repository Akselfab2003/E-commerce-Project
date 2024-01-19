﻿using E_commerce.Logic;
using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models_Logic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Test
{
    public class CreateFakeDBDependencies
    {
        private readonly DBcontext FakeContext;
        private readonly IDataCollection dataCollection;
        private readonly IConfiguration configuration;

        public CreateFakeDBDependencies() 
        {
            var _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            DbContextOptions<DBcontext> contextOptions =
                new DbContextOptionsBuilder<DBcontext>().UseSqlite(_connection).Options;

         
            FileStream stream = new FileStream("..\\net8.0\\Create data for local database\\Secret.json", FileMode.Open, FileAccess.Read);
            configuration = new ConfigurationBuilder().AddJsonStream(stream).Build();

            FakeContext = new DBcontext(contextOptions);

            if (FakeContext.Database.EnsureCreated())
            {
                dataCollection = new DataCollection(FakeContext, configuration);
            }
        }
        public IDataCollection DataCollection
        {
            get { return dataCollection; }
        }

    }
}