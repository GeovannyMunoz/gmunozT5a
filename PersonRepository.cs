using gmunozT5a.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gmunozT5a
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection conn;
        public string StatusMessage { get; set; }

        public void Init()
        {
            if (conn is not null)
                return;
            conn = new(_dbPath);
            conn.CreateTable<Persona>();
        }

        public PersonRepository(string bdPath)
        {
            _dbPath = bdPath;
        }

        public void AddNewPerson(string nombre)
        {
            int result = 0;
            try
            {
                Init();
                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Nombre requerido");

                Persona persona = new Persona() { Name= nombre};
                result = conn.Insert(persona);

                StatusMessage = string.Format("Persona insertada", result, nombre);
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error al insertar", nombre, ex.Message);
            }
        }

        public List<Persona> GetAllPepople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error al mostar los datos", ex.Message);
            }

            return new List<Persona>();
        }

        public void UpdatePerson(Persona persona)
        {
            int result = 0;
            try
            {

                result = conn.Update(persona);

                StatusMessage = string.Format("Persona Actualizada", result, persona);
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error al actualizar", persona, ex.Message);
            }
        }

        public void DeletePerson(Persona persona)
        {
            int result = 0;
            try
            {

                result = conn.Delete(persona);

                StatusMessage = string.Format("Persona eliminada", result, persona);
            }
            catch (Exception ex)
            {

                StatusMessage = string.Format("Error al eliminar", persona, ex.Message);
            }
        }
    }
}
