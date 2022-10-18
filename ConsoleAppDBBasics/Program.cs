using ConsoleAppDBBasics.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SortOrder = ConsoleAppDBBasics.Models.SortOrder;

namespace ConsoleAppDBBasics
{
    class Program
    {
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\antosp\source\repos\ConsoleAppDBBasics\ConsoleAppDBBasics\StudentsDB.mdf;Integrated Security=True";

        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var studentsRepository = new StudentsRepository(connection);

                // приклад зчитування даних по ID (Read)
                var id = 15;
                var student = studentsRepository.Read(id);

                if (student != null)
                {
                    Console.WriteLine("{0} {1} {2} {3}", student.Id, student.FullName, student.Course, student.Rating);
                }
                else
                {
                    Console.WriteLine("Can't find student with Id={0}", id);
                }

                // Приклад створення нового запису (Create)
                var newStudent = studentsRepository.Create(new Student()
                {
                    FullName = "New Student with return Id !!!!",
                    Course = 2,
                    Rating = 60
                });
                Console.WriteLine("Newly created student Id: {0}", newStudent.Id);

                // Приклад видалення даних студента по ID (Delete)
                studentsRepository.Delete(1004);

                // Приклад модифікації даних про студента по ID (Update)
                studentsRepository.Update(new Student()
                {
                    Id = 1005,
                    FullName = "New Student !!!!!!!!!",
                    Course = 2,
                    Rating = 60
                });

                // Приклад посторінкового зчитування даних з можливістю сортування
                var students = studentsRepository.Read(page: 1, perPage: 5, sortByColumn: Columns.Rating, sortByOrder: SortOrder.DESC);
                foreach(var s in students.Items)
                {
                    Console.WriteLine("{0} {1} {2} {3}", s.Id, s.FullName, s.Course, s.Rating);
                }

                Console.WriteLine("Total count: {0}", students.Count);
            }

            Console.ReadKey();
        }
    }
}
