using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationBackend.Repositories
{
    public class DonationCenterRepo : IRepository<int, DonationCenter>
    {
        private BloodDonationContext _context;

        public DonationCenterRepo(BloodDonationContext context)
        {
            _context = context;
        }
        public async Task<DonationCenter> Add(DonationCenter item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<DonationCenter> Delete(int key)
        {
            var DonationCenter = await Get(key);
            if (DonationCenter != null)
            {
                _context.Remove(DonationCenter);
                await _context.SaveChangesAsync();
                return DonationCenter;
            }
            throw new Exception("No DonationCenter with the given ID");
        }

        public async Task<DonationCenter> Get(int key)
        {
            return (await _context.DonationCenters.SingleOrDefaultAsync(m => m.CenterId == key)) ?? throw new Exception("No DonationCenter with the given ID");

        }

        public async Task<IEnumerable<DonationCenter>> Get()
        {
            return (await _context.DonationCenters.ToListAsync());
        }

        public IQueryable<DonationCenter> Query()
        {
            throw new NotImplementedException();
        }

        public async Task<DonationCenter> Update(DonationCenter item)
        {
            try
            {
                var data = await _context.DonationCenters.FindAsync(item.CenterId); // Use context directly
                if (data == null)
                {
                    throw new Exception("DonationCenter not found.");
                }

                // Update properties
                data.Name = item.Name;
                data.ContactInfo = item.ContactInfo;
                data.Address = item.Address;
                data.OperatingHours = item.OperatingHours;

                _context.Entry(data).State = EntityState.Modified; // Mark entity as modified
                await _context.SaveChangesAsync(); // Save changes to database

                return data;
            }
            catch
            {
                throw new Exception("No item with the given ID");
            }

        }
    }
}
