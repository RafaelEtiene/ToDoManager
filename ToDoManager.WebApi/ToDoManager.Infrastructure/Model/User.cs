using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoManager.Infrastructure.Model
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O nome de usuário deve ter no máximo 100 caracteres.")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(256, ErrorMessage = "O hash da senha deve ter no máximo 256 caracteres.")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
