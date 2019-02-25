using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [Route("api/feedback")]
    public class CommentController : Controller
    {
        private IWorldRepository repository;

        public CommentController(IWorldRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = repository.GetAllComments();
                return Ok(Mapper.Map<IEnumerable<CommentViewModel>>(results));
            }
            catch (Exception ex)
            {
                return BadRequest("Error ocurred" + ex.Message);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]CommentViewModel comment)
        {
            if (ModelState.IsValid)
            {
                var rComment = Mapper.Map<Comment>(comment);
                rComment.UserName = User.Identity.Name;

                repository.AddComment(rComment);

                if (await repository.SaveChangesAsync())
                {
                    return Created($"api/feedback/{comment.DatePosted.ToString("d")}",
                        Mapper.Map<CommentViewModel>(rComment));
                }
            }
            return BadRequest("Failed to save the comment");
        }
    }
}
