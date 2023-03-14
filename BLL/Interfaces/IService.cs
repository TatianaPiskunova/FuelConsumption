using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IService<T> where T : IDTO
    {
        void Add(T item);
        void Update(T item);
        void DeleteById(int id);
        T FindById(int id);
        List<T> GetAll();
    }
}
