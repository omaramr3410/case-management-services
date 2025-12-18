using CaseManagement.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ServiceProvider = CaseManagement.Api.Domain.Entities.ServiceProvider;

namespace CaseManagement.Api.Infrastructure.Repositories
{
    public class ServiceProviderRepository : IServiceProviderRepository
    {
        private readonly AppDbContext _db;

        public ServiceProviderRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ServiceProvider?> GetByIdAsync(Guid id)
            => await _db.ServiceProvider.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<ServiceProvider>> SearchAsync(string? name, string? region, string? serviceType)
        {
            return await _db.ServiceProvider
                .AsNoTracking()
                .Where(x =>
                    (name == null || x.Name.Contains(name)) &&
                    (region == null || x.Region == region) &&
                    (serviceType == null || x.ServiceType == serviceType))
                .ToListAsync();
        }

        public async Task CreateAsync(ServiceProvider provider)
        {
            _db.ServiceProvider.Add(provider);
            await _db.SaveChangesAsync();
        }
    }
}
