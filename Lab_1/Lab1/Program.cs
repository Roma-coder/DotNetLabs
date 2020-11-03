using System;
using System.IO;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Lab1
{
    class Program
    {
        private delegate void _methodToExecute(string table, MySqlCommand query);

        private const string Host = "localhost";
        private const string Database = "student_performance";
        private const string User = "root";
        private const string Password = "";

        private MySqlConnection _connection;

        private readonly static string[] _tables = { "students", "subjects", "teachers", "exams" };


        public void ReadAll()
        {
            MakeForAllTables(ReadTable);
        }

        public void UpAll()
        {
            MakeForAllTables(UpTable);
        }

        public void DownAll()
        {
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            using (MySqlCommand query = _connection.CreateCommand())
            {
                var list = new List<string>(_tables);
                list.Reverse();

                foreach (var table in list)
                {
                    DownTable(table, query);
                }
            }

            _connection.Close();
        }

        private void MakeConnection()
        {
            string connection_string = $"Database={Database};Datasource={Host};User={User};Pasword={Password}";
            _connection = new MySqlConnection(connection_string);
        }

        private void MakeForAllTables(_methodToExecute method)
        {
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            using (MySqlCommand query = _connection.CreateCommand())
            {
                foreach (var table in _tables)
                {
                    method(table, query);
                }
            }

            _connection.Close();
        }

        private void ReadTable(string table, MySqlCommand query)
        {
            MySqlDataReader reader = default;
            try
            {
                query.CommandText = $"SELECT * FROM {table}";
                reader = query.ExecuteReader();
                Console.WriteLine($"======================================={table}=======================================");

                while (reader.Read())
                {
                    object[] objects = new object[reader.FieldCount];
                    reader.GetValues(objects);

                    foreach (object ob in objects)
                    {

                        if (ob is System.Byte[])
                        {
                            Console.WriteLine(System.Text.Encoding.UTF8.GetString(ob as System.Byte[]));
                        }
                        else
                        {
                            Console.WriteLine(ob.ToString());
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                reader?.Close();
            }
        }

        private void ShowTable()
        {
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }

            using (MySqlCommand query = _connection.CreateCommand())
            {
                query.CommandText = 
                    $"SELECT st.student_surname, st.student_name, sb.subject_name, sb.subject_date, t.teacher_surname, t.teacher_name " +
                    $"FROM exams e " +
                    $"JOIN students st ON e.student_id = st.student_id " +
                    $"JOIN teachers t ON e.teacher_id = t.teacher_id " +
                    $"JOIN subjects sb ON e.subject_id = sb.subject_id";

                var reader = query.ExecuteReader();
                while (reader.Read())
                {
                    object[] objects = new object[reader.FieldCount];
                    reader.GetValues(objects);

                    string result = "| ";
                    foreach (object ob in objects)
                    {
                        result += ob.ToString() +" | ";
                    }
                    Console.WriteLine(result);
                }

            }
        }

        private void UpTable(string table, MySqlCommand query)
        {
            MySqlDataReader reader = default;
            foreach (var line in ReadFromFile($@"{table}.txt"))
            {
                try
                {

                    string row = default;
                    if (table == "orders")
                    {
                        string guid = Guid.NewGuid().ToString();
                        row = $"'{guid}',{string.Join(",", line)}";
                    }
                    else
                    {
                        row = $"{string.Join(",", line)}";
                    }

                    query.CommandText = $"INSERT INTO {table} VALUES({row})";
                    reader = query.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    reader?.Close();
                }
            }
        }

        private void DownTable(string table, MySqlCommand query)
        {
            MySqlDataReader reader = default;
            try
            {
                query.CommandText = $"DELETE FROM {table}";
                reader = query.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                reader?.Close();
            }
        }

        private IEnumerable<string[]> ReadFromFile(string filepath)
        {

            StreamReader reader = default;
            try
            {
                reader = new StreamReader(filepath);
            }
            catch
            {
                Console.WriteLine($"Can not open {filepath}");
                yield break;
            }

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line.Length > 0) yield return line.Split(';');
            }

            reader.Close();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Program program = new Program();
            program.MakeConnection();
            program.DownAll();
            program.UpAll();
            program.ReadAll();
            program.ShowTable();
            Console.ReadLine();
        }
    }
}