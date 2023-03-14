using BLL.Interfaces;
using DAL.Models;

namespace BLL.Specifications
{
    internal class IsUserPositionValid : ISpecification<User>
    {
        public bool IsSatisfiedBy(User entity)
        {
            var post = entity.PositionOffice;
            return !string.IsNullOrEmpty(post) && post.Length >= 3;
        }
    }
}