using LibraryApp.Data.Common;
using Microsoft.AspNetCore.Identity;
using System;

namespace LibraryApp.Data.Entities
{
    public class Role : IdentityRole<int>, IBaseEntity
    {
        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? CreatedRoleId { get; set; }
        public long? UpdatedRoleId { get; set; }
        public bool IsDeleted { get; set; }
    }

}
