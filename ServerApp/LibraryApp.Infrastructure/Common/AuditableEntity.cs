using System;

namespace LibraryApp.Data.Common
{
    public class AuditableEntity: BaseEntity
    {
        public long? CreatedBy { get; set; }

        public long? UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public long? CreatedRoleId { get; set; }

        public long? UpdatedRoleId { get; set; }
    }
}
