using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext context;

        public WorldRepository(WorldContext context)
        {
            this.context = context;
        }

        public void AddComment(Comment comment)
        {
            context.Add(comment);
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return context.Comments.ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await context.SaveChangesAsync()) > 0;
        }
    }
}
