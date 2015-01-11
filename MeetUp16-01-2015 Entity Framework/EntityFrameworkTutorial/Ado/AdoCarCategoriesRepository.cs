using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EntityFrameworkTutorial.Ado
{
    public class AdoCarCategoriesRepository : ICarCategoriesRepository
    {
        private static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; }
        }

        public IEnumerable<CarCategory> GetAllCategories()
        {
            var result = new List<CarCategory>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var getAllCommand = new SqlCommand("select * from CarCategories", connection);
                
                    connection.Open();
                using (var reader = getAllCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new CarCategory
                        {
                            Id = (int)reader["Id"],
                            Code = (string)reader["Code"],
                            Description = (string)reader["Description"]
                        });
                    }
                }
            }
            return result;
        }

        public CarCategory GetCategoryById(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var getByIdCommand = new SqlCommand("select * from CarCategories where Id = @Id", connection);
                getByIdCommand.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (var reader = getByIdCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new CarCategory
                        {
                            Id = (int)reader["Id"],
                            Code = (string)reader["Code"],
                            Description = (string)reader["Description"]
                        };
                    }
                }
            }
            return null;
        }

        public int InsertCarCategory(CarCategory category)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var insertCommand = new SqlCommand(@"INSERT INTO CarCategories 
                        ([Code], [Description]) VALUES 
                        (@Code, @Description)
                        SET @Id = SCOPE_IDENTITY()", connection);
                var codeSqlParam = new SqlParameter
                {
                    ParameterName = "@code",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = category.Code,
                };
                var descriptionSqlParam = new SqlParameter
                {
                    ParameterName = "@Description",
                    SqlDbType = SqlDbType.NVarChar,
                    Value = category.Description
                };
                var idSqlParam = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output
                };

                insertCommand.Parameters.Add(codeSqlParam);
                insertCommand.Parameters.Add(descriptionSqlParam);
                insertCommand.Parameters.Add(idSqlParam);

                connection.Open();
                insertCommand.ExecuteScalar();
                return ((int)insertCommand.Parameters["@Id"].Value);
            }
        }

        public void UpdateCarCategory(CarCategory category)
        {
            throw new NotImplementedException();
        }

        public void DeleteCarCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}