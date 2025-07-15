using CleanArchApi.Domain.Entities;

namespace CleanArchApi.Domain.Interfaces;


public interface IProductRepository
{
	Task<IEnumerable<Products>> GetProducts();
	Task<Products> GetProductById(int Id);
	Task AddProduct(Products prods);
	Task DeleteProduct(int Id);


}