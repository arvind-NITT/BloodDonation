using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationBackend.Repositories
{
    public class RecipientRepo : IRepository<int, Recipient>
    {
        private BloodDonationContext _context;

        public RecipientRepo(BloodDonationContext context)
        {
            _context = context;
        }
        public async Task<Recipient> Add(Recipient item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Recipient> Delete(int key)
        {
            var Recipient = await Get(key);
            if (Recipient != null)
            {
                _context.Remove(Recipient);
                await _context.SaveChangesAsync();
                return Recipient;
            }
            throw new Exception("No Recipient with the given ID");
        }

        public async Task<Recipient> Get(int key)
        {
            return (await _context.Recipients.SingleOrDefaultAsync(m => m.RecipientId == key)) ?? throw new Exception("No Recipient with the given ID");

        }

        public async Task<IEnumerable<Recipient>> Get()
        {
            return (await _context.Recipients.ToListAsync());
        }

        public IQueryable<Recipient> Query()
        {
            throw new NotImplementedException();
        }

        public async Task<Recipient> Update(Recipient item)
        {
            try
            {
                var data = await _context.Recipients.FindAsync(item.RecipientId); // Use context directly
                if (data == null)
                {
                    throw new Exception("Recipient not found.");
                }

                // Update properties
                data.MedicalCondition = item.MedicalCondition;

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
