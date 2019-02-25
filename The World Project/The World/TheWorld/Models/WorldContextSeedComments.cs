using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WorldContextSeedComments
    {
        private WorldContext context;
        private UserManager<WorldUser> userManager;

        public WorldContextSeedComments(WorldContext context, UserManager<WorldUser> userManager)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task SeedData()
        {
            if(await userManager.FindByEmailAsync("m.kontilov@gmail.com") == null)
            {
                var user = new WorldUser()
                {
                    UserName = "martinkontilov",
                    Email = "m.kontilov@gmail.com"
                };

                await userManager.CreateAsync(user, "P@ssw0rd!");
            }

            if(!context.Comments.Any())
            {
                var comment1 = new Comment()
                {
                    UserName = "Maika ti",
                    DatePosted = DateTime.UtcNow,
                    UserComment = "Браво мойто момче! Само така!"
                };

                context.Comments.Add(comment1);

                var comment2 = new Comment()
                {
                    UserName = "Your ex",
                    DatePosted = DateTime.UtcNow,
                    UserComment = "Go fuck yourself!!!"
                };

                context.Comments.Add(comment2);

                await context.SaveChangesAsync();
            }
        }
    }
}
