using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationBackend.Repositories
{
    public class BloodInventoryRepo : IRepository<int, BloodInventory>
    {
        private BloodDonationContext _context;

        public BloodInventoryRepo(BloodDonationContext context)
        {
            _context = context;
        }
        public async Task<BloodInventory> Add(BloodInventory item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<BloodInventory> Delete(int key)
        {
            var BloodInventory = await Get(key);
            if (BloodInventory != null)
            {
                _context.Remove(BloodInventory);
                await _context.SaveChangesAsync();
                return BloodInventory;
            }
            throw new Exception("No BloodInventory with the given ID");
        }

        public async Task<BloodInventory> Get(int key)
        {
            return (await _context.BloodInventorys.SingleOrDefaultAsync(m => m.InventoryId == key)) ?? throw new Exception("No BloodInventory with the given ID");

        }

        public async Task<IEnumerable<BloodInventory>> Get()
        {
            return (await _context.BloodInventorys.ToListAsync());
        }

        public IQueryable<BloodInventory> Query()
        {
            throw new NotImplementedException();
        }

        public async Task<BloodInventory> Update(BloodInventory item)
        {
            try
            {
                var data = await _context.BloodInventorys.FindAsync(item.InventoryId); // Use context directly
                if (data == null)
                {
                    throw new Exception("BloodInventory not found.");
                }

                // Update properties
                data.Quantity = item.Quantity;
                _context.Entry(data).State = EntityState.Modified; // Mark entity as modified
                await _context.SaveChangesAsync(); // Save changes to database
                return data;
            }
            catch
            {
                throw new Exception("No BloodInventory with the given ID");
            }

        }
    }
}
