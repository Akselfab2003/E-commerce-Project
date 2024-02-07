using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models_Logic.Table_Repo
{
    public class productsRepo : GenericRepo<Products>, IProducts
    {
        DBcontext context;
        public productsRepo(DBcontext c) : base(c) 
        {
            context = c;
        }

        public async Task<Products> CreateProduct(Products entity)
        {
            context.Products.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteProduct(Products entity)
        {
            try
            {
                Products product = await GetById(entity.Id);
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<List<Products>?> GetAllProducts()
        {
            return await context.Products.Include(product => product.Images)
                .Include(product => product.ProductCategories).Where(product => !product.IsDeleted)
                .ToListAsync();

        }

        public async Task<Products> GetById(int id)
        {
            return await context.Products
                .Include(product => product.Images)
                .Include(product => product.ProductCategories).Where(product => !product.IsDeleted)

                .FirstOrDefaultAsync(product => product.Id == id);
        }

        public async Task<List<Products>> GetProducts(int count)
        {   
            return await context.Products.Include(ele => ele.Images).Include(ele => ele.ProductCategories).Where(product => !product.IsDeleted).Take(count).ToListAsync(); //context.Products.Include(product => product.Images).Take(count).ToListAsync();
        }

        public async Task<Products> UpdateProduct(Products entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }


        public async Task<List<Products>> SearchForProducts(string SearchInput)
        {
            return await context.Products
                                .Include(ele => ele.Images)
                                .Include(ele => ele.ProductCategories).Where(product => !product.IsDeleted).Where(product => product.Title.ToLower().Contains(SearchInput.ToLower()) == true).ToListAsync();
        }


        public async Task<List<Products>> SearchForProductsNewWay(string SearchInput)
        {
            HashSet<string> Hashset = context.Products.Select(ele => ele.Title.ToLower()).ToHashSet();
            HashSet<int> id = new HashSet<int>(); 

            int HashsetLength = Hashset.Count();
            string SearchInputToLower = SearchInput.ToLower();


            for (int i = 0;i < HashsetLength; i++)
            {
                if (Hashset.Contains(SearchInputToLower))
                {
                    id.Add(i);
                }
            }
          

            List<Products> list = await context.Products
                                .Include(ele => ele.Images)
                                .Include(ele => ele.ProductCategories)
                                .Where(ele => id.Contains(ele.Id)).Where(product => !product.IsDeleted).ToListAsync();


            return list;
            //return await context.Products
            //                    .Include(ele => ele.Images)
            //                    .Include(ele => ele.ProductCategories).Where(product => product.Title.ToLower().Contains(SearchInput.ToLower()) == true).ToListAsync();
        }

       

        public async Task<List<Products>> SearchForProductsAsyncTest(string SearchInput)
        {
            HashSet<string> Hashset = context.Products.Select(ele => ele.Title.ToLower()).ToHashSet();

            string SearchInputToLower = SearchInput.ToLower();


            Products[][] test = (await context.Products.ToListAsync()).Chunk(10).ToArray();
            

            int IQueryableCount = test.Count();
            var tasks = new Task<List<Products>>[IQueryableCount];
            for (int i = 0; i < IQueryableCount; i++)
            {
                tasks[i] = QuickDoSearchFormInput(SearchInputToLower, test[i]);
            }


            List<Products> products = new List<Products>();

            var Results = (await Task.WhenAll(tasks)).Select(ele => products.Union(ele.ToList()));


            
            return products;
        }



        private async Task<List<Products>> QuickDoSearchFormInput(string SearchInput, Products[] ProductsArray)
        {

            HashSet<string> Hashset = ProductsArray.Select(ele => ele.Title.ToLower()).ToHashSet();

            HashSet<int> id = new HashSet<int>();

            int HashsetLength = Hashset.Count();
            string SearchInputToLower = SearchInput;


            for (int i = 0; i < HashsetLength; i++)
            {
                if (Hashset.Contains(SearchInputToLower))
                {
                    id.Add(i);
                }
            }

            return  ProductsArray.Where(ele => id.Contains(ele.Id)).ToList();





        }


    }
}
