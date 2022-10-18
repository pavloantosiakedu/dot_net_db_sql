using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDBBasics.Models
{
    public class StudentsRepository : IRepository<Student>
    {
        private SqlConnection connection { get; set; }
 
        public StudentsRepository(SqlConnection connection)
        {
            this.connection = connection;
        }

        // TO DO: CRUD
        public int Count()
        {
            using (SqlCommand command = new SqlCommand(
                "SELECT COUNT(*) FROM Students",
                connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

        public Student Create(Student item)
        {
            using (SqlCommand command = new SqlCommand(
                String.Format("INSERT INTO Students (FullName, Course, Rating) OUTPUT INSERTED.ID VALUES ('{0}',{1},{2});", item.FullName, item.Course, item.Rating),
                connection))
            {
                item.Id = (int)command.ExecuteScalar();
            }

            return item;
        }

        public bool Delete(int id)
        {
            using (SqlCommand command = new SqlCommand(
                String.Format("DELETE FROM Students WHERE Id={0}", id),
                connection))
            {
                return command.ExecuteNonQuery() > 0;
            }
        }

        public Student Read(int id)
        {
            using (SqlCommand command = new SqlCommand(String.Format("SELECT * FROM Students WHERE Id={0}", id), connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    return new Student()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        FullName = Convert.ToString(reader["FullName"]),
                        Course = Convert.ToInt32(reader["Course"]),
                        Rating = Convert.ToDouble(reader["Rating"])
                    };
                }
            }
        }

        public PaginationList<Student> Read(int page = 1, int perPage = 5, string sortByColumn = Columns.Id, string sortByOrder = SortOrder.ASC)
        {
            var skip = (page - 1) * perPage;
            var take = perPage;

            var items = new List<Student>();
            using (SqlCommand command = new SqlCommand(String.Format("SELECT * FROM Students ORDER BY {2} {3} OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", skip, take, sortByColumn, sortByOrder), connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        items.Add(new Student()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            FullName = Convert.ToString(reader["FullName"]),
                            Course = Convert.ToInt32(reader["Course"]),
                            Rating = Convert.ToDouble(reader["Rating"])
                        });
                    }
                }
            }

            return new PaginationList<Student>()
            {
                Items = items,
                Count = Count()
            };
        }

        public bool Update(Student item)
        {
            using (SqlCommand command = new SqlCommand(
               String.Format("UPDATE Students SET FullName='{0}', Course={1}, Rating={2} WHERE Id={3};", item.FullName, item.Course, item.Rating, item.Id),
               connection))
            {
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
