using Microsoft.AspNet.Identity;
using SocialMedia.Models;
using SocialMedia.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SocialMediaAPI.Controllers
{
    [Authorize]
    public class CommentController : ApiController
    {
        private CommentService CreateComment()
        {
            var Id = Guid.Parse(User.Identity.GetUserId());
            var commentService = new CommentService(Id);
            return commentService;
        }

        public IHttpActionResult Get()
        {
            CommentService commentService = CreateComment();
            var comments = commentService.GetComment();
            return Ok(comments);
        }
        public IHttpActionResult Post(CommentCreate comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateComment();

            if (!service.CreateComment(comment))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult Get(int id)
        {
            CommentService commentService = CreateComment();
            var comments = commentService.GetCommentById(id);
            return Ok(comments);
        }

       /* public IHttpActionResult Put(CommentEdit comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateCommentService();

            if (!service.UpdateComment(comment))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateCommentService();

            if (!service.DeleteComment(id))
                return InternalServerError();

            return Ok();
        }*/

    }
}
