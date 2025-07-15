using CleanArchApi.Domain.Entities;
using CleanArchApi.Domain.Interfaces;

namespace CleanArchApi.Application.Services;
public class ProductService
{
	private readonly IProductRepository _ipr;

	public ProductService(IProductRepository ipr)
	{
		_ipr = ipr;
	}


	public async Task<IEnumerable<Products>> GetAllProduct()
	{
		return await _ipr.GetProducts();
	}

	public async Task<Products> SGetById(int Id)
	{ 

		var prd = await _ipr.GetProductById(Id);
		return prd;


	}


	public async Task AddProducts(Products prod)
	{
		await _ipr.AddProduct(prod);
	}

	public async Task SDeleteProd(int Id)
	{
		if (Id != null)
		{
			await _ipr.DeleteProduct(Id);
		}

	}



}