using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql.Data.MySqlClient;
using ProjectWPF.Extensions;

namespace ProjectWPF.Model
{
    class WindkrachtDataService
    {
        private static string connectionString =
            ConfigurationManager.ConnectionStrings["azure"].ConnectionString;

        //private static IDbConnection db = new SqlConnection(connectionString);

        private static MySqlConnection db = new MySqlConnection(connectionString);

        public ObservableCollection<Windkracht> GetWindkrachten()
        {
            string sql = "Select * from Windkracht";
            return (ObservableCollection<Windkracht>)db.Query<Windkracht>(sql).ToObservableCollection();
        }

        public ObservableCollection<Windkracht> GetWindkracht(Windkracht windkracht)
        {
            string sql = "Select * from Windkracht Where Id = @Id";
            ObservableCollection<Windkracht> windkrachten = db.Query<Windkracht>(sql, new {windkracht.Id}).ToObservableCollection();

            return windkrachten;
        }

        public ObservableCollection<Windkracht> GetWindkrachtWhereKracht(Windkracht windkracht)
        {
            string sql = "Select * from Windkracht Where Kracht = @Kracht";
            ObservableCollection<Windkracht> windkrachten = db.Query<Windkracht>(sql, new {windkracht.Kracht}).ToObservableCollection();

            return windkrachten;
        }

        public void ToevoegWindkracht(Windkracht windkracht)
        {
            string sql = "INSERT INTO Windkracht (Kracht,Benaming,Van,Tot,Uitwerking) VALUES (@Kracht, @Benaming, @Van, @Tot, @Uitwerking)";
            db.Execute(sql, new { windkracht.Kracht, windkracht.Benaming, windkracht.Van, windkracht.Tot, windkracht.Uitwerking});
        }

        public void VerwijderTemperatuur(Windkracht windkracht)
        {
            string sql = "DELETE FROM Windkracht WHERE Id= @Id";
            db.Execute(sql, new { windkracht.Id});
        }

        public void UpdateWindkracht(Windkracht windkracht)
        {
            string sql = "Update Windkracht set Kracht= @Kracht, Benaming= @Benaming, Van= @Van, Tot= @Tot, Uitwerking= @Uitwerking Where id = @Id";
            db.Execute(sql, new { windkracht.Kracht, windkracht.Benaming, windkracht.Van, windkracht.Tot, windkracht.Uitwerking, windkracht.Id });
        }
    }
}
