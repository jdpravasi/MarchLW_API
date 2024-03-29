using MarchLW_API.Models;
using MarchLW_API.Models.VM;
using System.Diagnostics.Metrics;

namespace MarchLW_API.Repository.IRepository
{
    public interface IRides<T> where T : class
    {
        IEnumerable<Rides> GetRides();
        IEnumerable<Rides> GetRide(int id);
        IEnumerable<Rides> SearchRides(string searchQuery);
        void Add(Rides rides);
        void Remove(Rides rides);
        void Update(Rides rides);
        void Delete(Rides rides);
        void Save();

        Task CreateTicket(TicketVM model);

        IEnumerable<TicketDetails> GetTicketDetails();

    }
}
