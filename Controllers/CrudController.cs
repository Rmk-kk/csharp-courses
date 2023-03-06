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
    public ICollection<TModel> GetAll()
    {
        return _service.GetAll();
    }

    //Get by Id
    [HttpGet("{id:int}")] 
    public virtual ActionResult<TModel?> Get(int id)
    {
       var item =  _service.Get(id);
       if(item is null)
       {
            return NotFound("Item not found.");
       }
       return item;
    }

    //Create new
    [HttpPost]
    public virtual ActionResult<TModel?> Create(TDto request)
    {
        var item = _service.Create(request);
        if(item is null)
        {
            return BadRequest();
        }
        return item;
    }

    //Update by Id
    [HttpPut("{id:int}")]
    public virtual ActionResult<TModel?> Update(int id, TDto request)
    {
        var item = _service.Update(id, request);
        if(item is null)
        {
            return NotFound("Item is not found.");
        }
        return item;
    }

    //Delete by Id
    [HttpDelete("{id}")]
    public virtual ActionResult Delete(int id)
    {
        if(_service.Delete(id)) 
        {
            return Ok(new {Message = "Item is deleted."});
        }
        return NotFound("Item is not found.");
    }
}