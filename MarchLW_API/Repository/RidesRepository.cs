using MarchLW_API.Data;
using MarchLW_API.Models;
using MarchLW_API.Models.VM;
using MarchLW_API.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarchLW_API.Repository
{
    public class RidesRepository : IRides<Rides>
    {
        private readonly ApplicationDbContext _db;
        public RidesRepository(ApplicationDbContext db) 
        {
            _db = db;
        }

        public void Add(Rides rides)
        {
            _db.Rides.Add(rides);
        }

        public void Delete(Rides rides)
        {
            _db.Rides.Remove(rides);
        }

        public IEnumerable<Rides> GetRide(int id)
        {
            var ride = _db.Rides.Where(u=> u.ID == id).ToList();
            return ride;
        }

        public IEnumerable<Rides> GetRides()
        {
            var rides = _db.Rides.ToList();
            return rides;
        }

        public void Remove(Rides rides)
        {
            _db.Rides.Remove(rides);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        public void Update(Rides rides)
        {
            var local = _db.Set<Rides>()
                       .Local
                       .FirstOrDefault(entry => entry.ID.Equals(rides.ID));

            if (local != null)
            {
                _db.Entry(local).State = EntityState.Detached;
            }

            _db.Entry(rides).State = EntityState.Modified;
        }

        public IEnumerable<Rides> SearchRides(string searchQuery)
        {
            if (searchQuery == "all")
            {
                var datas = _db.Rides.ToList();
                return datas;
            }
            var data = _db.Rides
                .Where(t => t.RideName.Contains(searchQuery))
                .ToList();
            return data;
        }

        public async Task CreateTicket(TicketVM model)
        {
            var customer = new Customer
            {
                Name = model.CustomerName,
                Email = model.CustomerEmail
            };
            _db.Customer.Add(customer);
            await _db.SaveChangesAsync();

            var ticket = new Tickets
            {
                CustomerID = customer.ID
            };
            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync();

            var TD = new TicketDetails
            {
                TicketID = ticket.ID,
                CustomerID = customer.ID,
                NumberOfAdults = model.NumberOfAdults,
                NumberOfChildren = model.NumberOfChildren,
                TotalPrice = model.TotalPrice
            };
            _db.TicketDetails.Add(TD);
            await _db.SaveChangesAsync();

            foreach (var rideID in model.RideIds)
            {
                var ticketRide = new TicketRides
                {
                    TicketID = ticket.ID,
                    RideID = rideID
                };
                _db.TicketRides.Add(ticketRide);
            }
            await _db.SaveChangesAsync();
        }

        public IEnumerable<TicketDetails> GetTicketDetails()
        {
            var ticketDetails = _db.TicketDetails
                                    .Include(U=> U.Customer)
                                    .Include(U=> U.Ticket)
                                    .ToList();
            return ticketDetails;
        }
    }
}
