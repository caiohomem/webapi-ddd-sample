using System.ComponentModel.DataAnnotations;

namespace Hexis.DomainModelLayer
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}