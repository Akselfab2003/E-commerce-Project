﻿using E_commerce.Logic.Interfaces.Table_Interfaces;
using E_commerce.Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using E_commerce.Logic.Models;
using System.Net;
using Microsoft.EntityFrameworkCore;
using E_commerce.Logic;

namespace E_commerce_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceListController : ControllerBase
    {
        private readonly IPriceList priceList;
        private readonly IDataCollection collection;
        public PriceListController(IDataCollection _context)
        {
            priceList = _context.PriceList;
            collection = _context;
        }
        #region POST Requests
        [HttpPost]
        public async Task<HttpStatusCode> PostPriceList(PriceList priceList)
        {
            try
            {

                await collection.PriceList.Create(priceList);
            }
            catch (Exception ex)
            {

                return HttpStatusCode.BadRequest;
            }

            return HttpStatusCode.Created;
        }
        #endregion

        #region PUT Requests
        [HttpPut("UpdatePriceList")]
        public async Task<HttpStatusCode> UpdatePriceList(PriceList _priceList)
        {
            try
            {
                PriceListEntity entity = _priceList.PriceListProducts.FirstOrDefault(ele => ele.Id == 0);
                entity.Product = await collection.Products.GetById(entity.Product.Id);
                await collection.PriceListEntity.Create(entity);
                PriceList priceList = await collection.PriceList.GetById(_priceList.Id);
                priceList.PriceListProducts.Add(entity);
                await collection.PriceList.Update(priceList);
                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }
        }
        #endregion

        #region GET Requests
        [HttpGet("GetListOfPriceList")]
        public async Task<List<PriceList?>> GetListOfPriceList()
        {
            try
            {
                return await priceList.GetListOfPriceList();

            }
            catch
            {
                return null;
            }

        }
        [HttpGet("PriceList/{id}")]
        public async Task<PriceList> GetPriceList(int id)
        {
            try
            {
                return await collection.PriceList.GetById(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
        }
        [HttpGet("Product/{id}")]
        public async Task<List<Products>> GetPriceListProduct(int id)
        {
            try
            {
                List<Products> priceList = await collection.PriceList.GetProductsNotPartOfPriceList(id);
                return priceList;
            }
            catch (DbUpdateConcurrencyException)
            {
                return new List<Products>();
            }
        }
        [HttpGet("Users/{id}")]
        public async Task<List<Users>> GetPriceListUsers(int id)
        {
            try
            {
                List<Users> priceList = await collection.PriceList.GetUsersNotPartOfPriceList(id);
                return priceList;
            }
            catch (DbUpdateConcurrencyException)
            {
                return new List<Users>();
            }
        }
        [HttpGet("Companies/{id}")]
        public async Task<List<Company>> GetPriceListCompanies(int id)
        {
            try
            {
                List<Company> priceList = await collection.PriceList.GetCompaniesNotPartOfPriceList(id);
                return priceList;
            }
            catch (DbUpdateConcurrencyException)
            {
                return new List<Company>();
            }
        }
        #endregion

        #region DELETE Requests
        [HttpDelete("{id}")]
        public async Task<HttpStatusCode> DeletePriceList(int id)
        {
            var pricelist = await priceList.GetById(id);
            if (pricelist == null)
            {
                return HttpStatusCode.NotFound;
            }

            await collection.PriceList.Delete(pricelist);

            return HttpStatusCode.NoContent;
        }
        #endregion
    }
}
