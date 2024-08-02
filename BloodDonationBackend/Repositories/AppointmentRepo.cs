using BloodDonationBackend.Contexts;
using BloodDonationBackend.Interfaces;
using BloodDonationBackend.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace BloodDonationBackend.Repositories
{
    public class AppointmentRepo : IRepository<int, Appointment>
    {
        private BloodDonationContext _context;

        public AppointmentRepo(BloodDonationContext context)
        {
            _context = context;
        }
        public async Task<Appointment> Add(Appointment item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Appointment> Delete(int key)
        {
            var Appointment = await Get(key);
            if (Appointment != null)
            {
                _context.Remove(Appointment);
                await _context.SaveChangesAsync();
                return Appointment;
            }
            throw new Exception("No Appointment with the given ID");
        }

        public async Task<Appointment> Get(int key)
        {
            return (await _context.Appointments.SingleOrDefaultAsync(m => m.AppointmentId == key)) ?? throw new Exception("No Appointment with the given ID");

        }

        public async Task<IEnumerable<Appointment>> Get()
        {
            return (await _context.Appointments.ToListAsync());
        }
        public async Task<IEnumerable<Appointment>> Getappointmentsbyid(int donorid)
        {
            var appointments = await _context.Appointments
                 .Where(a => a.DonorId == donorid)
                 .ToListAsync();

            return appointments;
        }

        public IQueryable<Appointment> Query()
        {
            throw new NotImplementedException();
        }

        public async Task<Appointment> Update(Appointment item)
        {
            try
            {
                var data = await _context.Appointments.FindAsync(item.AppointmentId); // Use context directly
                if (data == null)
                {
                    throw new Exception("item not found.");
                }

                // Update properties
                data.Location = item.Location;
                data.AppointmentDate = item.AppointmentDate;
                data.Status = item.Status;

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
