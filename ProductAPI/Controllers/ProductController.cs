﻿using Microsoft.AspNetCore.Mvc;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            return Ok(ProductDataStore.Current.Products);
        }

        [HttpGet("{productId}",Name="GetProduct")]

        public ActionResult<IEnumerable<ProductDto>> GetProduct(int productId)
        {
            if (ModelState.IsValid)
            { 
            ProductDto ?Selection = ProductDataStore.Current.Products.FirstOrDefault(c => c.Id == productId);

            if (Selection == null) { return NotFound(); }
            

            return Ok(Selection);
            }
            else 
            {
                throw new Exception("ID must be a positive number");
            }
        }



        [HttpPost]             //GENERIC POST TO BE IMPROVED WITH ENTITY FRAMEWORK
        public ActionResult<ProductDto> CreateProduct(int productId, ProductCreationDto productCreation)
        {
            if (ModelState.IsValid)
            {
                ProductDto? Product = ProductDataStore.Current.Products.FirstOrDefault(c => c.Id == productId);

                if (Product == null) { return NotFound(); }

                var MaxProductId = ProductDataStore.Current.Products.Max(p => p.Id);         //TO BE IMPROVED

                var finalProduct = new ProductDto
                {
                    Id = ++MaxProductId,
                    Price = productCreation.Price,
                    Description = productCreation.Description,
                    Name = productCreation.Name
                };

                ProductDataStore.Current.Products.Add(finalProduct);

                return CreatedAtRoute("GetProduct", new { productId = productId }, finalProduct);
            }
            else return BadRequest( new { error ="Something went wrong"});

        }


        [HttpPut("{productId}")]   //GENERIC PUT TO BE IMPROVED WITH ENTITY FRAMEWORK

        public ActionResult<ProductDto> UpdateProduct(int productId, ProductUpdateDto productUpdate)
        {
            var product = ProductDataStore.Current.Products.FirstOrDefault(c => c.Id == productId);

            if(product == null) { return NotFound("This product id does not exist"); }

            product.Price = productUpdate.Price;
            product.Description = productUpdate.Description;
            product.Name = productUpdate.Name;

            return NoContent();
        }

        [HttpDelete("{productId}")]
        public ActionResult<ProductDto> DeleteProduct (int productId)
        {
            var product = ProductDataStore.Current.Products.FirstOrDefault(c => c.Id == productId);

            if (product == null) { return NotFound("This product id does not exist"); }

            ProductDataStore.Current.Products.Remove(product);

            return NoContent();
        }

    }


    

}