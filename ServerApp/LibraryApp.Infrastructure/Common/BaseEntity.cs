using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Data.Common
{
    public abstract class BaseEntity : IBaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
