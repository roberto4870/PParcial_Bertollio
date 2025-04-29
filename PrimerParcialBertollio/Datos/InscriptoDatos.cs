using System.Data.SqlClient;
using PrimerParcialBertollio.Models;

namespace PrimerParcialBertollio.Datos
{
    public class InscriptoDatos
    {
        string connectionString = @"Data Source=DESKTOP-8R4UUDT\SQLEXPRESS;Initial Catalog=TorneoDeportivo;Integrated Security=True";
        public List<Inscripto> ListaInscripto()
        {
            List<Inscripto> lista = new List<Inscripto>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Inscripto.Id as IdInscripto, " +
                    "Inscripto.Nombre as NombreInscripto, " +
                    "Inscripto.IdDisciplina, Inscripto.Edad, " +
                    "Inscripto.CiudadResidencia, " +
                    "Disciplina.NombreDisciplina " +
                    "FROM Inscripto " +
                    "INNER JOIN Disciplina ON Inscripto.IdDisciplina = Disciplina.Id ORDER BY Disciplina.NombreDisciplina";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Inscripto()
                    {
                        Id = (int)reader["IdInscripto"],
                        Nombre = reader["NombreInscripto"].ToString(),
                        IdDisciplina = (int)reader["IdDisciplina"],
                        Edad = (int)reader["Edad"],
                        CiudadResidencia = reader["CiudadResidencia"].ToString(),

                        Disciplina = new Disciplina()
                        {
                            Id = (int)reader["IdDisciplina"],
                            NombreDisciplina = reader["NombreDisciplina"].ToString()
                        }

                    });
                }
                return lista;
            }
        }

        public string CrearInscripto(Inscripto inscripto)
        {
            string query = $"INSERT INTO Inscripto (Nombre, IdDisciplina, Edad, CiudadResidencia) VALUES " +
                $"('{inscripto.Nombre}',{inscripto.IdDisciplina},{inscripto.Edad},'{inscripto.CiudadResidencia}')";
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    return "";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public List<Disciplina> ListarDisciplina()
        {
            List<Disciplina> lista = new List<Disciplina>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Disciplina ORDER BY NombreDisciplina";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Disciplina()
                    {
                        Id = (int)reader["Id"],
                        NombreDisciplina = reader["NombreDisciplina"].ToString()
                    });
                }
                return lista;
            }

        }

        public List<(string NombreDisciplina, int Cantidad)> ObtenerCantidadPorDisciplina()
        {
            List<(string, int)> lista = new List<(string, int)>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT D.NombreDisciplina, COUNT(I.Id) AS Cantidad " +
                               "FROM Inscripto I " +
                               "INNER JOIN Disciplina D ON I.IdDisciplina = D.Id " +
                               "GROUP BY D.NombreDisciplina " +
                               "ORDER BY D.NombreDisciplina";
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add((reader["NombreDisciplina"].ToString(), (int)reader["Cantidad"]));
                }
            }
            return lista;
        }

    }
}
