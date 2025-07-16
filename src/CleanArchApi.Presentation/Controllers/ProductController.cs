
using CleanArchApi.Application.Services;
using CleanArchApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;

namespace CleanArchApi.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
	private readonly ProductService _pr;
	private readonly IMemoryCache _cache;
	private const string CacheKey = "AllProducts";

	public ProductController(ProductService pr, IMemoryCache cache)
	{
		_pr = pr;
		_cache = cache;
	}

	[HttpGet]
	public async Task<ActionResult> GetAllProds()
	{
		const string cacheKey = "AllProducts";
		if (_cache.TryGetValue(cacheKey, out var data))
		{
			Console.WriteLine("Loading From Cache");
			return Ok(data);
		}
		Console.WriteLine("Not Loading From Cache");
		var Prods = await _pr.GetAllProduct();
		_cache.Set(cacheKey, Prods, TimeSpan.FromMinutes(5));
		return Ok(Prods);
	}

	[HttpPost]
	public async Task<ActionResult> AddProds(Products prod)
	{
		if (ModelState.IsValid)
		{
			await _pr.AddProducts(prod);
			

			return CreatedAtAction(nameof(GetAllProds), new { id = prod.Id }, prod);

		}
		return BadRequest(ModelState);
	}
	
	[HttpGet("{Id}")]
	public async Task<ActionResult> DeletePRod(int Id)
	{
		var prod = await _pr.SGetById(Id);
		if (prod != null)
		{
			await _pr.SDeleteProd(Id);
			return Ok("Product Deleted");
		}
		throw new Exception("Wrong Prod Id.");
	}
	



}