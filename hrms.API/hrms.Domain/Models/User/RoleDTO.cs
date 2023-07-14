﻿namespace hrms.Domain.Models.User
{
    public class RoleDTO
    {
        public int? Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool? IsActive { get; set; }
    }
}
