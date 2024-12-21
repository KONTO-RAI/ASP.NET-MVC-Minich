using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeveloperNet.Domain.Entities.Common;

namespace DeveloperNet.Domain.Entities
{
    public class Vote : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid? PostId { get; set; }
        public Post Post { get; set; }

        public Guid? AnswerId { get; set; }
        public Answer Answer { get; set; }

        public Guid? CommentId { get; set; }
        public Comment Comment { get; set; }

        public int VoteType { get; set; }
    }
}
