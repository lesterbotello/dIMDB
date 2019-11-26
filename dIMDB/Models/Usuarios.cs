namespace dIMDB.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Usuarios
    {
        [Key]
        [Display(Name = "Código")]
        public int IDUsuario { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre de Usuario")]
        public string UserName { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Es Administrador?")]
        public bool IsAdmin { get; set; }
    }
}
