using LibraryApp.Data.Common;
using LibraryApp.Infrastructure.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Infrastructure.Entities
{
    public class Audit : BaseEntity
    {
        public string TableName { get; set; }

        public long TableRowId { get; set; }

        public long UserId { get; set; }

        public long RoleId { get; set; }
 
        public int? CorporationCode { get; set; }

        public AuditType AuditType { get; set; }

        [MaxLength(3000)]
        public string OldValues { get; set; }

        [MaxLength(3000)]
        public string NewValues { get; set; }

        public DateTime CreatedDate { get; set; }

        [MaxLength(36)]
        public string SessionId { get; set; }

        [MaxLength(36)]
        public string RowGuid { get; set; }
    }
}
