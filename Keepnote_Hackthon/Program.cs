using System.Data;
using System.Data.SqlClient;

namespace Keepnote_Hackthon
{
    class KeepNote
    {
        
        public void CreateNote(SqlConnection con)
        {
            Console.WriteLine("Enter Title");
            string title = Console.ReadLine();
            Console.WriteLine("Enter Description");
            string description = Console.ReadLine();
            Console.Write("Enter Date (dd-MM-yyyy): ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());

            SqlDataAdapter adp = new SqlDataAdapter("Select * from KeepNote", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            var row = ds.Tables[0].NewRow();
            row["Title"] = title;
            row["Description"] = description;
            row["Date"] = date;

            ds.Tables[0].Rows.Add(row);

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);

            Console.WriteLine("Note added successfully");
        }

        public void ViewNote(SqlConnection con)
        {
            Console.WriteLine("Enter Note ID");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from KeepNote where Id ={id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            if (ds == null)
            {
                Console.WriteLine("Note with specified id does not exists");
            }
            else
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        Console.WriteLine($"{column.ColumnName}: {row[column]}");
                    }
                    
                }
            }
        }

        public void ViewAllNotes(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * from KeepNote", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            if (ds == null)
            {
                Console.WriteLine("Note does not exists");
            }
            else
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    foreach (DataColumn column in ds.Tables[0].Columns)
                    {
                        Console.WriteLine($"{column.ColumnName}: {row[column]}");
                    }
                    
                }
            }
        }

        public void UpdateNote(SqlConnection con)
        {
            Console.WriteLine("Enter the Id of the row to update:");
            int id = Convert.ToInt32(Console.ReadLine());

            string selectQuery = $"SELECT * FROM KeepNote WHERE Id = {id}";

            SqlDataAdapter adp = new SqlDataAdapter(selectQuery, con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {

                Console.WriteLine("Enter the new Title:");
                string newTitle = Console.ReadLine();

                Console.WriteLine("Enter the new Description:");
                string newDescription = Console.ReadLine();

                Console.WriteLine("Enter the new Date (dd-MM-yyyy):");
                DateTime newDate = DateTime.Parse(Console.ReadLine());

                ds.Tables[0].Rows[0]["Title"] = newTitle;
                ds.Tables[0].Rows[0]["Description"] = newDescription;
                ds.Tables[0].Rows[0]["Date"] = newDate;

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adp);
                adp.Update(ds);

                Console.WriteLine("Row updated successfully");
            }
            else
            {
                Console.WriteLine($"No row found with Id = {id}");
            }

        }


        public void DeleteNote(SqlConnection con)
        {
            Console.WriteLine("Enter Note Id you want to delete");
            int id = Convert.ToInt16(Console.ReadLine());

            string selectQuery = $"select * FROM KeepNote WHERE Id = {id}";

            SqlDataAdapter adp = new SqlDataAdapter(selectQuery, con);
            DataSet ds = new DataSet();
            adp.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Rows[0].Delete();

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adp);
                adp.Update(ds);

                Console.WriteLine("Row Deleted successfully");
            }
            else
            {
                Console.WriteLine($"No row found with Id = {id}");
            }

        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Data source=IN-DQ3K9S3; Initial Catalog=KeepNotes; Integrated Security=true");
            KeepNote app = new KeepNote();

            while (true)
            {
                Console.WriteLine("Welcome To KeepNote App");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Create Note");
                Console.WriteLine("2. View Note");
                Console.WriteLine("3. View All Notes");
                Console.WriteLine("4. Update Note");
                Console.WriteLine("5. Delete Note");

                int Choice = 0;

                try
                {
                    Console.WriteLine("Enter Your choice: ");
                    Choice = Convert.ToInt32(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Enter only Numbers");
                }

                switch (Choice)
                {
                    case 1:
                        {

                            app.CreateNote(con);
                            break;

                        }
                    case 2:
                        {
                            app.ViewNote(con);
                            break;
                        }
                    case 3:
                        {
                            app.ViewAllNotes(con);
                            break;
                        }
                    case 4:
                        {
                            app.UpdateNote(con);
                            break;
                        }
                    case 5:
                        {
                            app.DeleteNote(con);
                            break;
                        }
                    default:
                        Console.WriteLine("Wrong Choice Entered");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
}