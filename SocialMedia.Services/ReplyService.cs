using SocialMedia.Data;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
{
    
        public class ReplyService
        {
            private readonly Guid _userId;
            public ReplyService(Guid userId)
            {
                _userId = userId;
            }
            public bool CreateReply(ReplyCreate model)
            {
            var entity =
                new Reply()
                {
                        Author = _userId,
                        Id = model.Id,
                        Text = model.Text,
                       CommentId = model.CommentId,
                        CreatedUtc = DateTimeOffset.Now
                    };

                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Replies.Add(entity);
                    return ctx.SaveChanges() == 1;
                }
            }
            public IEnumerable<ReplyItem> GetReply()
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var query =
                        ctx
                        .Replies
                        .Where(e => e.Author == _userId)
                        .Select(
                            e =>
                            new ReplyItem
                            {
                                Id = e.Id,
                                Text = e.Text,
                                CreatedUtc = e.CreatedUtc
                            }
                            );
                    return query.ToArray();
                }

            }
            public ReplyDetail GetReplyById(int id)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Comments
                        .Single(e => e.Id == id && e.Author == _userId);
                    return
                        new ReplyDetail
                        {
                            Id = entity.Id,
                            Text = entity.Text,
                            CreatedUtc = entity.CreatedUtc,
                            ModifiedUtc = entity.ModifiedUtc
                        };
                }
            }
            public bool UpdateReply(ReplyEdit model)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Replies
                        .Single(e => e.Id == model.Id && e.Author == _userId);

                    //entity.Title = model.Title;
                    entity.Text = model.Text;
                    entity.ModifiedUtc = DateTimeOffset.UtcNow;

                    return ctx.SaveChanges() == 1;
                }
            }

            public bool DeleteReply(int Id)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Replies
                        .Single(e => e.Id == Id && e.Author == _userId);

                    ctx.Replies.Remove(entity);

                    return ctx.SaveChanges() == 1;
                }
            }
        }
    }

