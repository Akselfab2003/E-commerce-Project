﻿using E_commerce.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Logic.Interfaces
{
    internal interface IFavorites
    {
        public Task<Favorites> GetById(int id);

        public Task<Favorites> GetByName(string name);

        public Task<Favorites> UpdateFavorites(Favorites Favorites);

        public Task<bool> DeleteFavorites(int id);

        public Task<Favorites> CreateFavorites(Favorites Favorites);
    }
}