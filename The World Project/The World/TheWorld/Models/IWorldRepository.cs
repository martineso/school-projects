using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Comment> GetAllComments();
        void AddComment(Comment comment);
        Task<bool> SaveChangesAsync();
    }
}  