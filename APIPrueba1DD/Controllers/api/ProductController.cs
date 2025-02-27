﻿using APIPrueba1DD.Models;
using Business;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace APIPrueba1DD.Controllers.api
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        private BProduct bProduct = new BProduct();
        private CultureInfo culture = new CultureInfo("es-ES");

        [ResponseType(typeof(List<Product>))]
        public IHttpActionResult GetProductActivate()
        {
            var response = new List<Product>();
            var products = bProduct.GetProductsActivate();
            response = products.Select(product => new Product
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                ProductInventory = product.ProductInventory,
                ProductExpiration = Product.reformatDate(product.ProductExpiration),
                ProductRegistered = Product.reformatDate(product.ProductRegistered)
            }).ToList();
            return Ok(response);
        }

        [ResponseType(typeof(List<Product>))]
        public IHttpActionResult GetProductDesactivate()
        {
            var response = new List<Product>();
            var products = bProduct.GetProductsDesactive();
            response = products.Select(product => new Product
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                ProductInventory = product.ProductInventory,
                ProductExpiration = Product.reformatDate(product.ProductExpiration),
                ProductRegistered = Product.reformatDate(product.ProductRegistered)
            }).ToList();
            return Ok(response);
        }

        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = bProduct.InsertProduct(new Entity.Product
            {
                ProductName = product.ProductName,
                ProductInventory = product.ProductInventory,
                ProductExpiration = DateTime.Parse(product.ProductExpiration, culture),
                ProductRegistered = DateTime.Now
            });

            if (!result)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != product.ProductID)
            {
                return BadRequest();
            }
            bool result = bProduct.UpdateProduct(new Entity.Product
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                ProductInventory = product.ProductInventory,
                ProductExpiration = DateTime.Parse(product.ProductExpiration, culture),
                ProductRegistered = DateTime.Parse(product.ProductRegistered, culture)
            });
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [ResponseType(typeof(string))]
        public IHttpActionResult PutProductDesactivate(int id)
        {
            bool result = bProduct.UpdateProductDesactivate(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductActivate(int id)
        {
            bool result = bProduct.UpdateProductActivate(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteProduct(int id)
        {
            bool result = bProduct.DeleteProduc(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
