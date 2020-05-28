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
    class TemperatuurDataService
    {
        private static string connectionString =
            ConfigurationManager.ConnectionStrings["azure"].ConnectionString;

        //private static IDbConnection db = new SqlConnection(connectionString);

        private static MySqlConnection db = new MySqlConnection(connectionString);

        public ObservableCollection<Temperatuur> GetTemperaturen()
        {
            string sql = "Select * from Temperatuur";
            return (ObservableCollection<Temperatuur>)db.Query<Temperatuur>(sql).ToObservableCollection();
        }

        public ObservableCollection<Temperatuur> GetJaren()
        {
            string sql = "Select Distinct Jaar from Temperatuur";
            return (ObservableCollection<Temperatuur>)db.Query<Temperatuur>(sql).ToObservableCollection();
        }

        public ObservableCollection<Temperatuur> GetTemperaturenPerJaar(Temperatuur jaar)
        {
            string sql = "Select * from Temperatuur Where Jaar = @Jaar";
            return (ObservableCollection<Temperatuur>)db.Query<Temperatuur>(sql, new { jaar.Jaar }).ToObservableCollection();
        }

        public ObservableCollection<Temperatuur> GetTemperatuur(Temperatuur jaar)
        {
            string sql = "Select * from Temperatuur Where MaandId = @MaandId And Jaar = @jaar";
            return (ObservableCollection<Temperatuur>)db.Query<Temperatuur>(sql, new { jaar.MaandId, jaar.Jaar }).ToObservableCollection();
        }

        public ObservableCollection<Temperatuur> GetTemperaturenPerMaand(Maand maand)
        {
            string sql = "Select * from Temperatuur Where MaandId = @Id";
            return (ObservableCollection<Temperatuur>)db.Query<Temperatuur>(sql, new { maand.Id}).ToObservableCollection();
        }

        public ObservableCollection<Temperatuur> GetTemperaturenPerWindkracht(Windkracht windkracht)
        {
            string sql = "Select * from Temperatuur Where WindkrachtId = @Id";
            return (ObservableCollection<Temperatuur>)db.Query<Temperatuur>(sql, new { windkracht.Id }).ToObservableCollection();
        }

        public ObservableCollection<Temperatuur> GetTemperaturenWhereJaarMaand(Temperatuur temperatuur)
        {
            string sql = "Select * from Temperatuur Where Jaar = @Jaar And MaandId = @MaandId";
            return (ObservableCollection<Temperatuur>)db.Query<Temperatuur>(sql, new { temperatuur.Jaar, temperatuur.MaandId }).ToObservableCollection();
        }

        public ObservableCollection<Temperatuur> GetTemperaturenWhereId(Temperatuur temperatuur)
        {
            string sql = "Select * from Temperatuur Where Id = @Id";
            return (ObservableCollection<Temperatuur>)db.Query<Temperatuur>(sql, new { temperatuur.Id }).ToObservableCollection();
        }



        public void ToevoegTemperatuur(Temperatuur temperatuur)
        {
            string sql = "INSERT INTO Temperatuur (Minimum, Gemiddelde, Maximum, MaandId, Jaar, maxWind, WindkrachtId) " +
                "VALUES (@Minimum, @Gemiddelde, @Maximum, @MaandId, @Jaar, @MaxWind, @WindkrachtId)";
            db.Execute(sql, new { temperatuur.Minimum, temperatuur.Gemiddelde, temperatuur.Maximum, temperatuur.MaandId, temperatuur.Jaar, temperatuur.MaxWind, temperatuur.WindkrachtId});
        }

        public void VerwijderTemperatuur(Temperatuur temperatuur)
        {
            string sql = "DELETE FROM Temperatuur Where Id= @Id";
            db.Execute(sql, new { temperatuur.Id});
        }

        public void UpdateTemperatuur(Temperatuur temperatuur)
        {
            string sql = "Update Temperatuur set Minimum= @Minimum, Gemiddelde = @Gemiddelde, Maximum = @Maximum,  MaandId = @MaandId, Jaar = @Jaar, MaxWind = @MaxWind, WindkrachtId = @WindkrachtId Where id = @Id";
            db.Execute(sql, new { temperatuur.Minimum, temperatuur.Gemiddelde, temperatuur.Maximum, temperatuur.MaandId, temperatuur.Jaar, temperatuur.MaxWind, temperatuur.WindkrachtId, temperatuur.Id });
        }
    }
}
