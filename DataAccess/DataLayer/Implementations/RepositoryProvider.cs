using Core.Entities;
using DataLayer.Interfaces;


namespace DataLayer.Implementations
{
    public class RepositoryProvider : IRepositoryProvider
    {

        #region private properties

        private IOwnerRepository<Owner> _ownerRpository;
        private ICyclistRepository<Cyclist> _cyclistRpository;

        #endregion

        #region public methods

        public RepositoryProvider()
        {
            _ownerRpository = new OwnerRepository();
            _cyclistRpository = new CyclistRepository();
        }

        public ICyclistRepository<Cyclist> GetCyclistRepository(bool singleton = false)
        {
            if (!singleton)
                return new CyclistRepository();

            return _cyclistRpository;
        }

        public IOwnerRepository<Owner> GetOwnerRepository(bool singleton = false)
        {
            if (!singleton)
                return new OwnerRepository();

            return _ownerRpository;
        }

        #endregion
    }
}
