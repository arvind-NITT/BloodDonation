using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationBackend.Repositories
{
    public class DonorRepo : IRepository<int, Donor>
    {
        private BloodDonationContext _context;

        public DonorRepo(BloodDonationContext context)
        {
            _context = context;
        }
        public async Task<Donor> Add(Donor item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Donor> Delete(int key)
        {
            var Donor = await Get(key);
            if (Donor != null)
            {
                _context.Remove(Donor);
                await _context.SaveChangesAsync();
                return Donor;
            }
            throw new Exception("No Donor with the given ID");
        }

        public async Task<Donor> Get(int key)
        {
            return (await _context.Donors.SingleOrDefaultAsync(m => m.DonorId == key)) ?? throw new Exception("No Donor with the given ID");

        }

        public async Task<IEnumerable<Donor>> Get()
        {
            return (await _context.Donors.ToListAsync());
        }

        public IQueryable<Donor> Query()
        {
            throw new NotImplementedException();
        }

        public async Task<Donor> Update(Donor item)
        {
            try
            {
                var data = await _context.Donors.FindAsync(item.DonorId); // Use context directly
                if (data == null)
                {
                    throw new Exception("Donor not found.");
                }

                // Update properties
                data.Available = item.Available;

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
