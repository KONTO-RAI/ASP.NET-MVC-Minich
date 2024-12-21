using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeveloperNet.Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace DeveloperNet.Domain.Entities
{
    public class User : IdentityUser<Guid>, IEntity<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public int Rating { get; set; }
    }
}
