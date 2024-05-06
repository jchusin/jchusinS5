using jchusinS5.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jchusinS5
{
    public class PersonRepository
    {
        string _dbPath;
        private SQLiteConnection conn;
        public string StatusMessage  { get; set; }
        private void Init()
        {
            if (conn is not null)
                return;
                    conn = new(_dbPath);
            conn.CreateTable<Persona>();
        }
        public PersonRepository(string dbPath)
        {
            _dbPath = dbPath;
        }


        public void AddNewPerson(string name)
        {
            try
            {
                Init();
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Nombre es requerido");

                Persona person = new() { Name = name };
                conn.Insert(person);
                StatusMessage = string.Format("Se insertó una persona: {0}", name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error no se insertó: {0}", ex.Message);
            }
        }

        public List<Persona> GetAllPeople()
        {
            try
            {
                Init();
                return conn.Table<Persona>().ToList();
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al listar: {0}", ex.Message);
            }
            return new List<Persona>();
        }

        public void UpdatePerson(Persona person)
        {
            try
            {
                Init();
                conn.Update(person);
                StatusMessage = "Persona actualizada correctamente";
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al actualizar persona: {0}", ex.Message);
            }
        }

        public void DeletePerson(int id)
        {
            try
            {
                Init();
                conn.Delete<Persona>(id);
                StatusMessage = "Persona eliminada correctamente";
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Error al eliminar persona: {0}", ex.Message);
            }
        }



    }
}
