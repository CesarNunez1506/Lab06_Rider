using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAB06_Cesar.Models;

[Table("usuarios")]
public partial class Usuario
{
    [Key]
    [Column("id_user")]
    public ulong IdUser { get; set; }

    [Required]
    [Column("user")]
    [StringLength(50)]
    public string User { get; set; } = null!;

    [Required]
    [Column("password_hash")]
    public string Password { get; set; } = null!;

    [Column("password_salt")]
    public string? PasswordSalt { get; set; }

    [Column("role")]
    [StringLength(20)]
    public string Role { get; set; } = "User";
}
