using System;

namespace NBlog.Domain.Entities
{
    /// <summary>
    /// 用户角色
    /// </summary>
    public class UserRole
    {
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}