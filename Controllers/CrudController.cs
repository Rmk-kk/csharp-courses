namespace NetCoreCourse.Controllers;

using NetCoreCourse.Models;
using NetCoreCourse.DTOs;
using NetCoreCourse.Services;
using Microsoft.AspNetCore.Mvc;

public abstract class CrudController<TModel, TDto> : ApiControllerBase 
        where TModel : BaseModel, new() 
        where TDto : BaseDTO<TModel>
{
    private readonly ICrudService<TModel, TDto> _service;
    // private readonly IConfiguration _config;
    // private readonly IOptions<CourseSettings> _settings; 

    public CrudController(ICrudService<TModel, TDto> service)
    { 
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    // Get all
    [HttpGet]
    public async Task<ICollection<TModel>> GetAllAsync()
    {
        return await _service.GetAllAsync();
    }

    //Get by Id
    [HttpGet("{id:int}")] 
    public async virtual Task<ActionResult<TModel?>> GetByIdAsync(int id)
    {
       var item = await _service.GetByIdAsync(id);
       if(item is null)
       {
            return NotFound("Item not found.");
       }
       return item;
    }

    //Create new
    [HttpPost]
    public async virtual Task<ActionResult<TModel?>> CreateAsync(TDto request)
    {
        var item = await _service.CreateAsync(request);
        if(item is null)
        {
            return BadRequest();
        }
        return item;
    }

    //Update by Id
    [HttpPut("{id:int}")]
    public async virtual Task<ActionResult<TModel?>> UpdateAsync(int id, TDto request)
    {
        var item = await _service.UpdateAsync(id, request);
        if(item is null)
        {
            return NotFound("Item is not found.");
        }
        return item;
    }

    //Delete by Id
    [HttpDelete("{id}")]
    public async virtual Task<ActionResult> DeleteAsync(int id)
    {
        if(await _service.DeleteAsync(id)) 
        {
            return Ok(new {Message = "Item is deleted."});
        }
        return NotFound("Item is not found.");
    }
}