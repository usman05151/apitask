
using CleanArchApi.Application.Services;
using CleanArchApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CleanArchApi.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
	private readonly ProductService _pr;

	public ProductController(ProductService pr)
	{
		_pr = pr;
	}

	[HttpGet]
	public async Task<ActionResult> GetAllProds()
	{
		var Prods = await _pr.GetAllProduct();
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
		[HttpGet("test-exception")]
		public IActionResult TestException()
		{
			throw new Exception("Test from Exceptional!");
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