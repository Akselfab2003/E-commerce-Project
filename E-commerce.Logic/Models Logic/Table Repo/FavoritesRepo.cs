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
    public class FavoritesRepo : GenericRepo<Favorites>, IFavorites
    {
        DBcontext context;
        public FavoritesRepo(DBcontext c) : base(c) 
        {
            context = c;
        }
        public async Task<Favorites> GetById(int id)
        {
            return await context.Favorites.FirstOrDefaultAsync(favorites => favorites.Id == id);
        }

        public async Task<Favorites> UpdateFavorites(Favorites Favorites)
        {
            context.Update(Favorites);
            await context.SaveChangesAsync();
            return Favorites;
        }

        public async Task<bool> DeleteFavorites(int id)
        {
            try
            {
                Favorites favorites = await GetById(id);
                context.Favorites.Remove(favorites);
                await context.SaveChangesAsync();
            }catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<Favorites> CreateFavorites(Favorites Favorites)
        {
            await context.Favorites.AddAsync(Favorites);
            await context.SaveChangesAsync(); // SAveChangesAsync()
            return Favorites;
        }
    }
}
