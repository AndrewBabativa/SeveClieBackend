using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ClieDto
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdClie { get; set; }

    [Required(ErrorMessage = "La cédula es obligatoria")]
    [StringLength(20, ErrorMessage = "La cédula no puede exceder los 20 caracteres")]
    public string Cedula { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "El género es obligatorio")]
    [StringLength(10, ErrorMessage = "El género no puede exceder los 10 caracteres")]
    public string Genero { get; set; }

    [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
    [DataType(DataType.Date)]
    public DateTime FechaNac { get; set; }

    [Required(ErrorMessage = "El estado civil es obligatorio")]
    [StringLength(50, ErrorMessage = "El estado civil no puede exceder los 50 caracteres")]
    public string EstadoCivil { get; set; }
}
