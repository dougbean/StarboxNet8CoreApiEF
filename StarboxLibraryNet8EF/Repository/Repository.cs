using Microsoft.EntityFrameworkCore;
using Serilog;

namespace StarboxLibraryNet8EF.Repository
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        // Flag to indicate whether Dispose has been called
        private bool _disposed = false;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }      

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
               
        // Implementing Dispose method for manual disposal of DbContext
        public void Dispose()
        {            
            Dispose(true);
            GC.SuppressFinalize(this); // Suppress finalization to avoid unnecessary overhead
        }

        // Protected Dispose method for internal cleanup
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // Dispose of managed resources, e.g., the DbContext
                _context?.Dispose();
            }

            // Dispose of unmanaged resources (if any)
            _disposed = true;
        }

        // Finalizer (destructor) for cleanup if Dispose was not called
        ~Repository() 
        {
            Log.Information("call deconstructor...");
            Dispose(false);
        }        
    }
}
