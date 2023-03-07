namespace NetCoreCourse.Services;

using NetCoreCourse.Models;
using NetCoreCourse.DTOs;
using System.Collections.Generic;
using NetCoreCourse.Db;
using Microsoft.EntityFrameworkCore;

public class DbCrudService<TModel, TDto> : ICrudService<TModel, TDto>
    where TModel : BaseModel, new()
    where TDto : BaseDTO<TModel>
{
    protected readonly AppDbContext _dbContext;

    public DbCrudService(AppDbContext dbContext) => _dbContext = dbContext;

    public async Task<TModel?> CreateAsync(TDto request)
    {
        var item = new TModel();
        request.UpdateModel(item);
        _dbContext.Add(item);
        await _dbContext.SaveChangesAsync(); // tell db context to update db
        return item;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await GetByIdAsync(id);
        if(item is null)
        {
            return false;
        } 
        _dbContext.Remove(item);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<TModel?> GetByIdAsync(int id)
    {
        return await _dbContext.Set<TModel>().FindAsync(id);
    }

    public async Task<ICollection<TModel>> GetAllAsync()
    {
        return await _dbContext.Set<TModel>().ToListAsync();
    }

    public async Task<TModel?> UpdateAsync(int id, TDto request)
    {
        var item = await GetByIdAsync(id);
        if(item is null)
        {
            return null;
        }
        request.UpdateModel(item);
        await _dbContext.SaveChangesAsync();
        return item;
    }
}