using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationBackend.Repositories
{
    public class BloodRequestRepo : IRepository<int, BloodRequest>
    {
        private BloodDonationContext _context;

        public BloodRequestRepo(BloodDonationContext context)
        {
            _context = context;
        }
        public async Task<BloodRequest> Add(BloodRequest item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<BloodRequest> Delete(int key)
        {
            var BloodRequest = await Get(key);
            if (BloodRequest != null)
            {
                _context.Remove(BloodRequest);
                await _context.SaveChangesAsync();
                return BloodRequest;
            }
            throw new Exception("No BloodRequest with the given ID");
        }

        public async Task<BloodRequest> Get(int key)
        {
            return (await _context.BloodRequests.SingleOrDefaultAsync(m => m.RequestId == key)) ?? throw new Exception("No BloodRequest with the given ID");

        }

        public async Task<IEnumerable<BloodRequest>> Get()
        {
            return (await _context.BloodRequests.ToListAsync());
        }

        public IQueryable<BloodRequest> Query()
        {
            throw new NotImplementedException();
        }

        public async Task<BloodRequest> Update(BloodRequest item)
        {
            try
            {
                var data = await _context.BloodRequests.FindAsync(item.RequestId); // Use context directly
                if (data == null)
                {
                    throw new Exception("BloodRequest not found.");
                }

                // Update properties
                data.BloodType = item.BloodType;
                data.Quantity = item.Quantity;
                data.IsUrgent = item.IsUrgent;
                data.Status = item.Status;
                data.State = item.State;
                data.District = item.District;

                _context.Entry(data).State = EntityState.Modified; // Mark entity as modified
                await _context.SaveChangesAsync(); // Save changes to database

                return data;
            }
            catch
            {
                throw new Exception("No BloodRequest with the given ID");
            }

        }
    }
}
