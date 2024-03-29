﻿using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models_Logic.Table_Repo
{
    public class CompanyRepo : GenericRepo<Company>, ICompany
    {
        DBcontext context;
        public CompanyRepo(DBcontext c) : base(c)
        {
            context = c;
        }

        public async Task<List<Company>> GetAll()
        {
            return await context.Company.ToListAsync();
        }

        public async Task<Company> GetById(int id)
        {
            return await context.Company.Include(company => company.Users).FirstOrDefaultAsync(company => company.Id == id);
        }

        public async Task<Company> GetByName(string name)
        {
            return await context.Company.FirstOrDefaultAsync(company => company.Name == name);
        }

        public async Task<Company> UpdateCompany(Company Company)
        {
            context.Update(Company);
            await context.SaveChangesAsync();
            return Company;
        }

        public async Task<bool> DeleteCompany(int id)
        {
            try
            {
                Company company = await GetById(id);
                context.Company.Remove(company);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<Company> CreateCompany(Company company)
        {
            await context.Company.AddAsync(company);
            await context.SaveChangesAsync();
            return company;
        }
    }
}
