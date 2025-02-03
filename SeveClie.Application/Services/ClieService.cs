using SeveClie.Application.Commands;
using SeveClie.Application.DTOs;
using SeveClie.Domain.Entities;
using SeveClie.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;

namespace SeveClie.Application.Services
{
    public class ClieService
    {
        private readonly IClieRepository _clieRepository;

        public ClieService(IClieRepository ClieRepository)
        {
            _clieRepository = ClieRepository;
        }

        public async Task<ClieDto> CreateClieAsync(CreateClieCommand command)
        {
            try
            {
                var clieEntity = ClieEntity.Create(command.Cedula, command.Nombre, command.Genero, command.FechaNac, command.EstadoCivil);
                await _clieRepository.AddAsync(clieEntity);


                return new ClieDto
                {
                    IdClie = clieEntity.IdClie,
                    Cedula = clieEntity.Cedula,
                    Nombre = clieEntity.Nombre,
                    Genero = clieEntity.Genero,
                    FechaNac = clieEntity.FechaNac,
                    EstadoCivil = clieEntity.EstadoCivil
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el cliente.", ex);
            }
        }

        public async Task<ClieDto> UpdateClieAsync(UpdateClieCommand command)
        {
            try
            {
                var clie = await _clieRepository.GetByIdAsync(command.IdClie);

                if (clie == null)
                    throw new KeyNotFoundException($"Client with ID {command.IdClie} not found");

                clie.UpdateNombre(command.Nombre);
                clie.UpdateEstadoCivil(command.EstadoCivil);

                if (!string.IsNullOrWhiteSpace(command.Cedula))
                    clie.UpdateCedula(command.Cedula);

                if (command.FechaNac != default)
                    clie.UpdateFechaNac(command.FechaNac);

                if (!string.IsNullOrWhiteSpace(command.Genero))
                    clie.UpdateGenero(command.Genero);

                await _clieRepository.UpdateAsync(clie);

                return new ClieDto
                {
                    IdClie = clie.IdClie,
                    Cedula = clie.Cedula,
                    Nombre = clie.Nombre,
                    Genero = clie.Genero,
                    FechaNac = clie.FechaNac,
                    EstadoCivil = clie.EstadoCivil
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the client.", ex);
            }
        }

        public async Task DeleteClieAsync(DeleteClieCommand command)
        {
            try
            {
                var clieEntity = await _clieRepository.GetByIdAsync(command.IdClie);

                if (clieEntity == null)
                    throw new KeyNotFoundException($"Cliente con ID {command.IdClie} no encontrado.");

                await _clieRepository.DeleteAsync(command.IdClie);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el cliente.", ex);
            }
        }

        public async Task<IEnumerable<ClieDto>> GetAllClieAsync()
        {
            var clieEntities = await _clieRepository.GetAllAsync();

            var clieDtos = clieEntities.Select(c => new ClieDto
            {
                IdClie = c.IdClie,
                Cedula = c.Cedula,
                Nombre = c.Nombre,
                Genero = c.Genero,
                FechaNac = c.FechaNac,
                EstadoCivil = c.EstadoCivil
            });

            return clieDtos;
        }
    }
}
