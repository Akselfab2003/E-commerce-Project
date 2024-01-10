using E_commerce.Logic.Interfaces;
using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Models_Logic.Table_Repo
{
    public class ImagesRepository : IImages
    {
        DBcontext context;
        public ImagesRepository(DBcontext c) { context = c; } // Dependency Injection - DI

        public async Task<Images> CreateImage(Images image)
        {
            context.Images.Add(image);
            await context.SaveChangesAsync();
            return image;
        }

        public async Task<bool> DeleteImage(int id)
        {
            try
            {
                Images image = await GetById(id);
                context.Images.Remove(image);
                await context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async Task<Images> GetById(int id)
        {
            return await context.Images.FirstOrDefaultAsync(image => image.Id == id);
        }

        public async Task<Images> UpdateImage(Images image)
        {
            context.Update(image);
            await context.SaveChangesAsync();
            return image;
        }
    }
}
