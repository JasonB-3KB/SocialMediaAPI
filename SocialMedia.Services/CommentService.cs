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
                        Text = model.Text,
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
            /*public bool UpdatePost(PostEdit model)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Notes
                        .Single(e => e.NoteId == model.NoteId && e.OwnerId == _userId);

                    entity.Title = model.Title;
                    entity.Content = model.Content;
                    entity.ModifiedUtc = DateTimeOffset.UtcNow;

                    return ctx.SaveChanges() == 1;
                }
            }

            public bool DeleteNote(int noteId)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Notes
                        .Single(e => e.NoteId == noteId && e.OwnerId == _userId);

                    ctx.Notes.Remove(entity);

                    return ctx.SaveChanges() == 1;
                }
            }*/
        }
    }

