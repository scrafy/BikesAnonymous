using Core.Entities;

namespace DataLayer.Interfaces
{
    public interface IRepositoryProvider
    {
        IOwnerRepository<Owner> GetOwnerRepository(bool singleton = false);
        ICyclistRepository<Cyclist> GetCyclistRepository(bool singleton = false);
    }
           
}
