﻿using Microsoft.AspNet.Identity;
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
    public class ReplyController : ApiController
    {
        private ReplyService CreateReplyService()
        {
            var Id = Guid.Parse(User.Identity.GetUserId());
            var replyService = new ReplyService(Id);
            return replyService;
        }

        public IHttpActionResult Get()
        {
            ReplyService replyService = CreateReplyService();
            var replies = replyService.GetReply();
            return Ok(replies);
        }
        public IHttpActionResult Post(ReplyCreate reply)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateReplyService();

            if (!service.CreateReply(reply))
                return InternalServerError();

            return Ok();
        }
        public IHttpActionResult Get(int id)
        {
            ReplyService replyService = CreateReplyService();
            var reply = replyService.GetReplyById(id);
            return Ok(reply);
        }

        public IHttpActionResult Put(ReplyEdit reply)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateReplyService();

            if (!service.UpdateReply(reply))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateReplyService();

            if (!service.DeleteReply(id))
                return InternalServerError();

            return Ok();
        }

    }
}
