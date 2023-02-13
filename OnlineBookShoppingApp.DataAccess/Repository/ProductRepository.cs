using OnlineBookShoppingApp.Data;
using OnlineBookShoppingApp.DataAccess.Repository.IRepository;
using OnlineBookShoppingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBookShoppingApp.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product product)
        {
            var productInDb = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if (productInDb != null)
            {
                if (productInDb.ImageUrl != "")
                    productInDb.ImageUrl = product.ImageUrl;
                productInDb.Title = product.Title;
                productInDb.Description = product.Description;
                productInDb.ISBN = product.ISBN;
                productInDb.Author = product.Author;
                productInDb.ListPrice = product.ListPrice;
                productInDb.price50 = product.price50;
                productInDb.price100 = product.price100;
                productInDb.Price = product.Price;
                productInDb.CategoryId = product.CategoryId;
                productInDb.CoverTypeId = product.CoverTypeId;


            }
            //_context.Products.Update( product);
        }
    }
}
