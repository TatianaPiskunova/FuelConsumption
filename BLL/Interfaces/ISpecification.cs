using DAL.Interfaces;

namespace BLL.Interfaces
{
    internal interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
    }
}
