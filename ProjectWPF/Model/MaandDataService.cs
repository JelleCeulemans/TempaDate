using Dapper;
using MySql.Data.MySqlClient;
using ProjectWPF.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWPF.Model
{
    class MaandDataService
    {
        private static string connectionString =
            ConfigurationManager.ConnectionStrings["azure"].ConnectionString;

        //private static IDbConnection db = new SqlConnection(connectionString);

        private static MySqlConnection db = new MySqlConnection(connectionString);

        public ObservableCollection<Maand> GetMaanden()
        {
            string sql = "Select * from Maand";
            return (ObservableCollection<Maand>)db.Query<Maand>(sql).ToObservableCollection();
        }

        public ObservableCollection<Maand> GetMaandenWhere(Temperatuur temp)
        {
            string sql = "Select * from Maand Where Id = @MaandId";
            return (ObservableCollection<Maand>)db.Query<Maand>(sql, new { temp.MaandId }).ToObservableCollection();
        }

        public ObservableCollection<Maand> GetMaandWhereId(Maand maand)
        {
            string sql = "Select * from Maand Where Id = @Id";
            return (ObservableCollection<Maand>)db.Query<Maand>(sql, new { maand.Id }).ToObservableCollection();
        }

        public ObservableCollection<Maand> GetMaandenWhereNaam(Maand maand)
        {
            string sql = "Select * from Maand Where Naam = @Naam";
            return (ObservableCollection<Maand>)db.Query<Maand>(sql, new { maand.Naam }).ToObservableCollection();
        }

        public void UpdateMaand(Maand maand)
        { 
            string sql = "Update Maand set Naam = @Naam Where id = @Id";
            db.Execute(sql, new { maand.Naam, maand.Id });
        }

        public void ToevoegMaand(Maand maand)
        {   
            string sql = "INSERT INTO Maand (Naam) VALUES (@Naam)";
            db.Execute(sql, new { maand.Naam});
        }

        public void VerwijderMaand(Maand maand)
        {
            string sql = "DELETE FROM Maand WHERE Id= @Id";
            db.Execute(sql, new { maand.Id});
        }
    }
}
