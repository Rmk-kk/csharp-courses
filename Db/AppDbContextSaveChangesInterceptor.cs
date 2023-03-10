namespace NetCoreCourse.Db;

using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NetCoreCourse.Models;

public class AppDbContextSaveChangesInterceptor : SaveChangesInterceptor
{
    public void UpdateTimeStamp(DbContextEventData eventData)
    {
        var entries = eventData.Context!.ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseModel && (e.State == EntityState.Modified || e.State == EntityState.Added));
        foreach(var entry in entries)
        {
            if(entry.State == EntityState.Added)
            {
                ((BaseModel)entry.Entity).CreatedAt = DateTime.Now;
            }
            else 
            {
                ((BaseModel)entry.Entity).UpdatedAt = DateTime.Now;
            }
        }
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateTimeStamp(eventData);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateTimeStamp(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}