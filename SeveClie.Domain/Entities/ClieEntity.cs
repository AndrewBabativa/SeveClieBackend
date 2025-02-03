using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SeveClie.Domain.Entities
{
    public class ClieEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdClie { get; private set; }

        [Required]
        [StringLength(20, ErrorMessage = "La cédula no puede superar los 20 caracteres.")]
        public string Cedula { get; private set; }

        [Required]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; private set; }

        [Required]
        [StringLength(10, ErrorMessage = "El género no puede superar los 10 caracteres.")]
        public string Genero { get; private set; }

        [Required]
        public DateTime FechaNac { get; private set; }

        [Required]
        [StringLength(50, ErrorMessage = "El estado civil no puede superar los 50 caracteres.")]
        public string EstadoCivil { get; private set; }

        private ClieEntity() { }

        public static ClieEntity Create(string cedula, string nombre, string genero, DateTime fechaNac, string estadoCivil)
        {
            if (string.IsNullOrWhiteSpace(cedula)) throw new ArgumentException("La cédula no puede estar vacía.", nameof(cedula));
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
            if (string.IsNullOrWhiteSpace(genero)) throw new ArgumentException("El género no puede estar vacío.", nameof(genero));
            if (string.IsNullOrWhiteSpace(estadoCivil)) throw new ArgumentException("El estado civil no puede estar vacío.", nameof(estadoCivil));

            return new ClieEntity
            {
                Cedula = cedula,
                Nombre = nombre,
                Genero = genero,
                FechaNac = fechaNac,
                EstadoCivil = estadoCivil
            };
        }

        public void UpdateCedula(string cedula)
        {
            if (string.IsNullOrWhiteSpace(cedula)) throw new ArgumentException("La cédula no puede estar vacía.", nameof(cedula));
            Cedula = cedula;
        }

        public void UpdateNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentException("El nombre no puede estar vacío.", nameof(nombre));
            Nombre = nombre;
        }

        public void UpdateGenero(string genero)
        {
            if (string.IsNullOrWhiteSpace(genero)) throw new ArgumentException("El género no puede estar vacío.", nameof(genero));
            Genero = genero;
        }

        public void UpdateFechaNac(DateTime fechaNac)
        {
            FechaNac = fechaNac;
        }

        public void UpdateEstadoCivil(string estadoCivil)
        {
            if (string.IsNullOrWhiteSpace(estadoCivil)) throw new ArgumentException("El estado civil no puede estar vacío.", nameof(estadoCivil));
            EstadoCivil = estadoCivil;
        }
    }
}
