using Azure.Core;
using AirportProject4.Project.core.Entities.main;
using AirportProject4.Project.core.NewFolder.InterfaceContrect;
using AirportProject4.Project.Repo.Data.Context;
using AirportProject4.Shared.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AirportProject4.Project.Repo.Repositories
{
    public class Repository<T> : IRepo<T> where T : BaseEntity
    {
        protected readonly AirlineDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(AirlineDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        //public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T>spec)
        //{
        //    return await specificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).ToListAsync();

        //}

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetByIdQueryable(int id)
        {
            return _dbContext.Set<T>().Where(e => e.Id==id);
        }

        //public async Task<T> GetidAsync(ISpecification<T>spec)
        //{
        //    return await specificationEvaluator<T>.GetQuery(_dbContext.Set<T>(),spec).FirstOrDefaultAsync();
        //}
        public async Task addAsync(T item)
        {
            await _dbContext.Set<T>().AddAsync(item);
        }

        public Task DeleteAsync(T item)
        {
            _dbContext.Set<T>().Remove(item);
            return Task.CompletedTask;
        }



        public Task UpdateAsync(T item)
        {
            _dbContext.Set<T>().Update(item);
            return Task.CompletedTask;
        }

        public Task<int> SaveChanges()
        {
            return _dbContext.SaveChangesAsync();
        }



        #region update Attempt 1(failed)
        //public void saveinclude(T entity, params string[] includeparameters)
        //{
        //    var localEntity = _dbSet.Local.FirstOrDefault(e => e.Id == entity.Id);
        //    EntityEntry entry;
        //    if (localEntity == null) // shart il if dh b2olo low entry ==null y3ny msh mwgod fl context abl kda
        //                             //hatha 3ade lw mwgod y3ny 3aml attach abl kda w hatha hy5od el entry bta3o w y3ml 3leha el modifications
        //    {                        // lw msh mwgod hy3ml attach w y5od el entry bta3o
        //        entry = _dbContext.Entry(entity);
        //    }
        //    else
        //    {
        //        entry = _dbContext.ChangeTracker.Entries<T>().First(e => e.Entity.Id == entity.Id);
        //        _dbContext.Entry(localEntity).CurrentValues.SetValues(entity);
        //    }
        //    foreach (var prop in entry.Properties)
        //    {
        //        if (includeparameters.Contains(prop.Metadata.Name))
        //        {
        //            prop.IsModified = true;

        //        }
        //        else
        //        {
        //            prop.IsModified = false;
        //        }
        //    }

        //    //foreach (var nav in entry.Navigations) 
        //    //{
        //    //    if (nav.CurrentValue!=null)
        //    //    {
        //    //        _dbContext.Attach(nav.CurrentValue);
        //    //        _dbContext.Entry(nav.CurrentValue).State = EntityState.Modified;
        //    //    }
        //    //}

        //} 
        #endregion

        #region Update Attempt 2 (not tested)
        //public void SaveInclude(T entity) 
        //{
        //    var existingEntity = _dbSet.Local.FirstOrDefault(e => e.Id == entity.Id);
        //    if (existingEntity == null) 
        //    {
        //        existingEntity = _dbSet.AsNoTracking().FirstOrDefault(e => e.Id == entity.Id);
        //        if (existingEntity == null)
        //            throw new Exception($"Entity of type {typeof(T).Name} with Id {entity.Id} not found.");

        //        _dbSet.Attach(existingEntity);
        //    }


        //public void SaveInclude(T entity)
        //{
        //    // 1) جرّب تجيبه من الـ Local (Tracked)
        //    var existingEntity = _dbSet.Local.FirstOrDefault(e => e.Id == entity.Id);

        //    // 2) لو مش موجود في الذاكرة، هاته من الداتابيس مع التتبع
        //    if (existingEntity == null)
        //    {
        //        existingEntity = _dbSet.AsTracking().FirstOrDefault(e => e.Id == entity.Id);
        //        if (existingEntity == null)
        //            throw new Exception($"Entity of type {typeof(T).Name} with Id {entity.Id} not found.");
        //    }

        //    // 3) Entry عشان نحدد الخصائص المعدلة
        //    var entry = _dbContext.Entry(existingEntity);
        //    var keyNames = entry.Metadata.FindPrimaryKey().Properties.Select(p => p.Name).ToList();

        //    // 4) حدّث فقط الخصائص التي تغيّرت وقيمتها جديدة ليست null
        //    foreach (var prop in typeof(T).GetProperties())
        //    {
        //        // تخطّي الخصائص التي ليست scalar (مثل الـ navigation properties)
        //        if (entry.Metadata.FindProperty(prop.Name) == null)
        //            continue;

        //        // تخطّي الـ Primary Key
        //        if (keyNames.Contains(prop.Name))
        //            continue;

        //        var oldValue = prop.GetValue(existingEntity);
        //        var newValue = prop.GetValue(entity);

        //        // حدّث فقط إذا القيمة الجديدة ليست null ومختلفة عن القديمة
        //        if (newValue != null && !object.Equals(oldValue, newValue))
        //        {
        //            prop.SetValue(existingEntity, newValue);
        //            entry.Property(prop.Name).IsModified = true;
        //        }
        //    }
        //} 
        #endregion



        public void SaveInclude(T entity)
        {
            var existingEntity = _dbSet.Local.FirstOrDefault(e => e.Id == entity.Id);
            if (existingEntity == null)
            {
                existingEntity = _dbSet.AsNoTracking().FirstOrDefault(e => e.Id == entity.Id);
                if (existingEntity == null)
                    throw new Exception($"Entity of type {typeof(T).Name} with Id {entity.Id} not found.");

                _dbSet.Attach(existingEntity);
            }

            var entry = _dbContext.Entry(existingEntity);

            var keyNames = entry.Metadata.FindPrimaryKey().Properties.Select(p => p.Name).ToList();


            foreach (var prop in typeof(T).GetProperties())
            {
                if (entry.Metadata.FindProperty(prop.Name) == null)
                    continue; // بخلي يعدي الخصاص الي مش موجوده في ال
                              // زي ال navigation properties 

                if (keyNames.Contains(prop.Name))
                    continue; // بتعدي ال primary key عشان مش عايز اعدلها

                if (prop.Name == "id")
                    continue;

                var oldvalue = prop.GetValue(existingEntity);
                var newvale = prop.GetValue(entity);

                if (newvale != null && !object.Equals(oldvalue, newvale))
                {
                    prop.SetValue(existingEntity, newvale);
                    entry.Property(prop.Name).IsModified = true;
                }
                else
                {
                    entry.Property(prop.Name).IsModified = false;
                }

            }

        }


    }
}
