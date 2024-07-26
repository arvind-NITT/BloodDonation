using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Modal;
using BloodDonationBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BloodDonationBackend.Repositories
{
    public class UserRepo : IRepository<int, User>
    {
        private BloodDonationContext _context;

        public UserRepo(BloodDonationContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<User> Delete(int key)
        {
            var User = await Get(key);
            if (User != null)
            {
                _context.Remove(User);
                await _context.SaveChangesAsync();
                return User;
            }
            throw new Exception("No User with the given ID");
        }

        public async Task<User> Get(int key)
        {
            return (await _context.Users.SingleOrDefaultAsync(m => m.UserId == key)) ?? throw new Exception("No User with the given ID");

        }

        public async Task<IEnumerable<User>> Get()
        {
            return (await _context.Users.ToListAsync());
        }

        public IQueryable<User> Query()
        {
            throw new NotImplementedException();
        }

        public async Task<User> Update(User item)
        {
            try
            {
                var data = await _context.Users.FindAsync(item.UserId); // Use context directly
                if (data == null)
                {
                    throw new Exception("User not found.");
                }

                // Update properties
                data.Name = item.Name;
                data.Father_Name = item.Father_Name;
                data.Address = item.Address;
                data.PhoneNumber = item.PhoneNumber;
                data.Gender = item.Gender;
                data.Email = item.Email;
                data.state = item.state;
                data.District = item.District;
                data.Pincode = item.Pincode;
                data.BloodType = item.BloodType;
                data.DOB = item.DOB;


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
