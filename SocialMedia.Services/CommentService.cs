using SocialMedia.Data;
using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Services
{
    
        public class CommentService
        {
            private readonly Guid _userId;
            public CommentService(Guid userId)
            {
                _userId = userId;
            }
            public bool CreateComment(CommentCreate model)
            {
            var entity =
                new Comment()
                {
                        Author = _userId,
                        //Id = model.Id,
                        Text = model.Text,
                        PostId = model.PostId,
                        CreatedUtc = DateTimeOffset.Now
                    };

                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Comments.Add(entity);
                    return ctx.SaveChanges() == 1;
                }
            }
            public IEnumerable<CommentItem> GetComment()
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var query =
                        ctx
                        .Comments
                        .Where(e => e.Author == _userId)
                        .Select(
                            e =>
                            new CommentItem
                            {
                                Id = e.Id,
                                Text = e.Text,
                                CreatedUtc = e.CreatedUtc
                            }
                            );
                    return query.ToArray();
                }

            }
            public CommentDetail GetCommentById(int id)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Comments
                        .Single(e => e.Id == id && e.Author == _userId);
                    return
                        new CommentDetail
                        {
                            Id = entity.Id,
                            Text = entity.Text,
                            CreatedUtc = entity.CreatedUtc,
                            ModifiedUtc = entity.ModifiedUtc
                        };
                }
            }
            public bool UpdateComment(CommentEdit model)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Comments
                        .Single(e => e.Id == model.Id && e.Author == _userId);

                    //entity.Title = model.Title;
                    entity.Text = model.Text;
                    entity.ModifiedUtc = DateTimeOffset.UtcNow;

                    return ctx.SaveChanges() == 1;
                }
            }

            public bool DeleteComment(int Id)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Comments
                        .Single(e => e.Id == Id && e.Author == _userId);

                    ctx.Comments.Remove(entity);

                    return ctx.SaveChanges() == 1;
                }
            }
        }
    }

