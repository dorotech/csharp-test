using System.ComponentModel.DataAnnotations;

namespace Book.Domain.Models
{
    public class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
    }
}