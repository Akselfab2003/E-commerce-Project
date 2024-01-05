using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    internal interface ICompany
    {
        public Task<Company> GetById(int id);

        public Task<Company> GetByName(string name);

        public Task<Company> UpdateCompany(Company Company);

        public Task<bool> DeleteCompany(int id);

        public Task<Company> CreateCompany(Company Company);
    }
}
