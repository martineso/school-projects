using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
{
    public class CommentViewModel
    {
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;
        public string UserName { get; set; } = "";
        [Required]
        [StringLength(250, MinimumLength = 3)]
        public string UserComment { get; set; }
    }
}
