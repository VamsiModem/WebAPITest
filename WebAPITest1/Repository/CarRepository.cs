using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebAPITest1.Models;

namespace WebAPITest1.Repository
{
    public class CarRepository : IRepository
    {
        string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;

        /// <summary>
        /// Gets list of cars from database
        /// </summary>
        /// <returns>List of cars</returns>
        public IEnumerable<Car> GetCars()
        {
            List<Car> cars = new List<Car>();
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string query = "SELECT Id, Color, Model, Make, Year FROM [Test].[dbo].[Cars]";
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Car car = new Car()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal(("Id"))),
                            Model = reader.GetString(reader.GetOrdinal(("Model"))),
                            Make = reader.GetString(reader.GetOrdinal(("Make"))),
                            Color = reader.GetString(reader.GetOrdinal(("Color"))),
                            Year = reader.GetInt32(reader.GetOrdinal(("Year"))),
                        };
                        cars.Add(car);
                    }
                }
            }
            return cars;
        }

        /// <summary>
        /// Inserts a new car into the database
        /// </summary>
        /// <param name="car"></param>
        /// <returns>Returns number of rows affected, returns -1 on exception</returns>
        public int InsertCar(Car car)
        {
            int rowsAffected = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = @"INSERT INTO [dbo].[Cars] (Color, Model, Make, Year) VALUES (@Color, @Model ,@Make, @Year)";
                        sqlCommand.Parameters.AddWithValue("Make", car.Make);
                        sqlCommand.Parameters.AddWithValue("Color", car.Color);
                        sqlCommand.Parameters.AddWithValue("Model", car.Model);
                        sqlCommand.Parameters.AddWithValue("Year", car.Year);
                        rowsAffected = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                return rowsAffected;
            }
            return rowsAffected;
        }

        /// <summary>
        /// Updates the car with new car by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="car"></param>
        /// <returns>Returns number of rows affected, returns -1 on exception</returns>
        public int UpdateCar(int id, Car car)
        {
            int rowsAffected = -1;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = @"UPDATE [dbo].[Cars] SET Color = @Color ,Model = @Model ,Make = @Make ,Year = @Year WHERE Id = @Id";
                        sqlCommand.Parameters.AddWithValue("Make", car.Make);
                        sqlCommand.Parameters.AddWithValue("Color", car.Color);
                        sqlCommand.Parameters.AddWithValue("Model", car.Model);
                        sqlCommand.Parameters.AddWithValue("Year", car.Year);
                        sqlCommand.Parameters.AddWithValue("Id", id);
                        rowsAffected = sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                return rowsAffected;
            }
            return rowsAffected;
        }
    }
}