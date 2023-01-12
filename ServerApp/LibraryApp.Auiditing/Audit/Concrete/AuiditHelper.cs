using LibraryApp.Auiditing.Auiditing.Abstract;
using LibraryApp.Data.Common;
using LibraryApp.Infrastructure.Entities;
using LibraryApp.Infrastructure.Enums;
using LibraryApp.WebToken.Jwt.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryApp.Auditing.Auditing.Concrete
{
    public class AuditHelper : IAuditHelper
    {
        private readonly IJwtHelper jwtHelper;

        public AuditHelper(IJwtHelper jwtHelper)
        {
            this.jwtHelper = jwtHelper;
        }

        public List<Audit> AddAuditLog(ChangeTracker changeTracker)
        {
            long currentUserId = long.Parse(jwtHelper.GetValueFromToken("userId"));
            int currentKurumKodu = int.Parse(jwtHelper.GetValueFromToken("kurumKodu"));
            string currentSessionId = jwtHelper.GetValueFromToken("sessionGuid");
            long currentRoleId = long.Parse(jwtHelper.GetValueFromToken("roleId"));

            List<Audit> auditList = new List<Audit>();

            foreach (var change in changeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified)))
            {
                Audit audit = new Audit
                {
                    TableName = change.Entity.GetType().Name,
                    UserId = currentUserId,
                    CorporationCode = currentKurumKodu,
                    CreatedDate = DateTime.Now,
                    SessionId = currentSessionId,
                    RoleId = currentRoleId
                };

                var oldValues = new StringBuilder();
                var newValues = new StringBuilder();

                if (change.State == EntityState.Modified)
                {
                    var auditableEntity = ((AuditableEntity)change.Entity);
                    auditableEntity.CreatedDate = change.OriginalValues["CreatedDate"] != null ? (DateTime)change.OriginalValues["CreatedDate"] : (DateTime?)null;
                    auditableEntity.CreatedBy = change.OriginalValues["CreatedBy"] != null ? (long)change.OriginalValues["CreatedBy"] : (long?)null;
                    auditableEntity.CreatedRoleId = change.OriginalValues["CreatedRoleId"] != null ? (long)change.OriginalValues["CreatedRoleId"] : (long?)null;

                    auditableEntity.UpdatedDate = DateTime.Now;
                    auditableEntity.UpdatedBy = currentUserId;
                    auditableEntity.UpdatedRoleId = currentRoleId;

                    audit.TableRowId = (long)change.Metadata.FindPrimaryKey().Properties.ToDictionary(x => x.Name, x => x.PropertyInfo.GetValue(change.Entity)).FirstOrDefault().Value;
                    audit.RowGuid = change.OriginalValues["RowGuid"]?.ToString();
                    audit.AuditType = AuditType.Update;
                    foreach (var propertyName in change.OriginalValues.Properties.Select(p => p.Name))
                    {
                        var oldVal = change.OriginalValues[propertyName];
                        var newVal = change.CurrentValues[propertyName];

                        if (oldVal != null && newVal != null && !Equals(oldVal, newVal))
                        {
                            if (oldValues.Length > 2500 || newValues.Length > 2500)
                            {
                                audit.OldValues = oldValues.ToString();
                                audit.NewValues = newValues.ToString();
                                auditList.Add(audit);

                                newValues = new StringBuilder();
                                audit.NewValues = string.Empty;

                                oldValues = new StringBuilder();
                                audit.OldValues = string.Empty;
                            }
                            if (oldValues.Length > 0)
                            {
                                oldValues.AppendFormat("{0}", "||");
                            }

                            if (newValues.Length > 0)
                            {
                                newValues.AppendFormat("{0}", "||");
                            }

                            newValues.AppendFormat("{0}={1}", propertyName, newVal?.ToString());
                            oldValues.AppendFormat("{0}={1}", propertyName, oldVal?.ToString());
                        }
                    }

                    if (!String.IsNullOrWhiteSpace(oldValues.ToString()) || !String.IsNullOrWhiteSpace(newValues.ToString()))
                    {
                        audit.OldValues = oldValues.ToString();
                        audit.NewValues = newValues.ToString();
                        auditList.Add(audit);
                    }
                }
                else if (change.State == EntityState.Added)
                {
                    var auditableEntity = ((AuditableEntity)change.Entity);

                    auditableEntity.CreatedDate = DateTime.Now;
                    auditableEntity.CreatedBy = currentUserId;
                    auditableEntity.CreatedRoleId = currentRoleId;

                    audit.TableRowId = 0;
                    audit.AuditType = AuditType.Insert;
                    audit.OldValues = "";

                    foreach (var propertyName in change.OriginalValues.Properties.Select(p => p.Name))
                    {
                        var newVal = propertyName != "ID" ? change.CurrentValues[propertyName] : 0;

                        if (newValues.Length > 2500)
                        {
                            audit.NewValues = newValues.ToString();
                            auditList.Add(audit);
                            newValues = new StringBuilder();
                            audit.NewValues = string.Empty;
                        }

                        if (newValues.Length > 0)
                        {
                            newValues.AppendFormat("{0}", "||");
                        }

                        newValues.AppendFormat("{0}={1}", propertyName, newVal?.ToString());
                    }
                    if (!String.IsNullOrWhiteSpace(newValues.ToString()))
                    {
                        audit.NewValues = newValues.ToString();
                        auditList.Add(audit);
                    }
                }
            }
            return auditList;
        }
    }
}
