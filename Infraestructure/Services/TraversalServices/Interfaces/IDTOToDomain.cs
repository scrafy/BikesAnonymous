using Core.Entities;


namespace TraversalServices.Interfaces
{
    public interface IDTOToDomain<U> where U: BaseEntity
    {
        U ToDomain();
    }
}
