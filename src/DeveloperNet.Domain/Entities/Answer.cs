using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeveloperNet.Domain.Entities.Common;

namespace DeveloperNet.Domain.Entities
{
    public class Answer : IEntity<Guid>
    {
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int Rating { get; set; } 
    }
}
