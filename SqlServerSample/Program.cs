using System.Data.SqlClient;
using System;

namespace SqlServerSample
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection sqlConnection;
            string connectionString = @"Data Source=DESKTOP-7TNJUDQ\SQLSERVER2019;Initial Catalog=LandonHotel;Integrated Security=True";
            while (true)
            {
                try
                {//MENU
                    sqlConnection = new SqlConnection(connectionString);
                    sqlConnection.Open();
                    Console.WriteLine("\nConnection to Landon Hotel Employee Database Established \n");
                    Console.WriteLine("Select The Letter Of Operation You Wish To Perform");
                    Console.WriteLine("A) Create New Entry");
                    Console.WriteLine("B) Retrieve Table");
                    Console.WriteLine("C) Update Entry");
                    Console.WriteLine("D) Delete Entry");
                    Console.WriteLine("X) Exit Application");
                    string crudeOption = Console.ReadLine().ToUpper();
                    //CREATE STARTS HERE
                    switch (crudeOption)
                    {
                        case "A":
                            Person hotelEmployee = new Person();
                            Console.WriteLine("Enter A New Employee ID");// find a solution to auto increment
                            hotelEmployee.employeeId = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter Name:");
                            hotelEmployee.name = Console.ReadLine();
                            Console.WriteLine("Enter Title:");
                            hotelEmployee.title = Console.ReadLine();
                            Console.WriteLine("Enter Country:");
                            hotelEmployee.country = Console.ReadLine();
                            Console.WriteLine("Enter Email:");
                            hotelEmployee.email = Console.ReadLine();
                            string createQuery = "INSERT INTO Employees(EmployeeID, Name, Title, Country, Email) VALUES(" + hotelEmployee.employeeId + ",'" + hotelEmployee.name + "', '" + hotelEmployee.title + "', ' " + hotelEmployee.country + "','" + hotelEmployee.email + "')";

                            SqlCommand createCommand = new SqlCommand(createQuery, sqlConnection);
                            createCommand.ExecuteNonQuery();

                            Console.WriteLine($"\nAdded New Entry To Database:\nName: {hotelEmployee.name}\nTitle: {hotelEmployee.title}\nCountry: {hotelEmployee.country}\nEmail: {hotelEmployee.email}");
                            break;
                        case "C":
                            Person hotelEmployee1 = new Person();
                            Console.WriteLine("Update Existing Employee Field(s)");
                            while (true)// UPDATE STARTS HERE
                            {
                                Console.WriteLine("Press Any Key to Continue or X to Exit");
                                string fieldOptionA = Console.ReadLine().ToUpper();
                                if (fieldOptionA == "X") { break; }
                                Console.WriteLine("Enter Employee ID");
                                hotelEmployee1.employeeId = int.Parse(Console.ReadLine());
                                string updateQueryDisplay = ($"SELECT EmployeeID, Name FROM Employees WHERE EmployeeID = {hotelEmployee1.employeeId}");
                                SqlCommand updateDisplayCommand = new SqlCommand(updateQueryDisplay, sqlConnection);
                                SqlDataReader dataReader1 = updateDisplayCommand.ExecuteReader();
                                Console.WriteLine($"Employee User ID: {hotelEmployee1.employeeId} will be updated");
                                while (dataReader1.Read())
                                {
                                    Console.WriteLine($"Employee Name: {dataReader1.GetValue(1).ToString()}");//display name
                                }
                                dataReader1.Close();
                                Console.WriteLine("Please Select a Field To Update");
                                Console.WriteLine("A) Name");
                                Console.WriteLine("B) Title");
                                Console.WriteLine("C) Country");
                                Console.WriteLine("D) Email");
                                Console.WriteLine("X) Exit");
                                string fieldOption = Console.ReadLine().ToUpper();

                                if (fieldOption == "A")
                                {
                                    Console.WriteLine("Enter Employee ID to Confirm");
                                    int employeeId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Please Enter a new Name");
                                    string employeeName = Console.ReadLine();
                                    string updateEmployeeNameField = ($"UPDATE Employees SET Name = '{employeeName}' WHERE EmployeeID = {hotelEmployee1.employeeId}");
                                    SqlCommand updateEmployeeName = new SqlCommand(updateEmployeeNameField, sqlConnection);
                                    updateEmployeeName.ExecuteNonQuery();
                                    Console.WriteLine($"Employee Name has been Updated to {employeeName}");
                                }
                                else if (fieldOption == "B")
                                {
                                    Console.WriteLine("Enter Employee ID to Confirm");
                                    int employeeId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Please Enter a new Title");
                                    string employeeTitle = Console.ReadLine();
                                    string updateEmployeeTitleField = ($"UPDATE Employees SET Title = '{employeeTitle}' WHERE EmployeeID = {hotelEmployee1.employeeId}");
                                    SqlCommand updateEmployeeTitle = new SqlCommand(updateEmployeeTitleField, sqlConnection);
                                    updateEmployeeTitle.ExecuteNonQuery();
                                }
                                else if (fieldOption == "C")
                                {
                                    Console.WriteLine("Enter Employee ID to Confirm");
                                    int employeeId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Please Enter a new Country");
                                    string employeeCountry = Console.ReadLine();
                                    string updateEmployeeCountryField = ($"UPDATE Employees SET Country = '{employeeCountry}' WHERE EmployeeID = {hotelEmployee1.employeeId}");
                                    SqlCommand updateEmployeeCountry = new SqlCommand(updateEmployeeCountryField, sqlConnection);
                                    updateEmployeeCountry.ExecuteNonQuery();
                                }
                                else if (fieldOption == "D")
                                {
                                    Console.WriteLine("Enter Employee ID to Confirm");
                                    int employeeId = int.Parse(Console.ReadLine());
                                    Console.WriteLine("Please Enter new Email");
                                    string employeeEmail = Console.ReadLine();
                                    string updateEmployeeEmailField = ($"UPDATE Employees SET Email = '{employeeEmail}' WHERE EmployeeID = {hotelEmployee1.employeeId}");
                                    SqlCommand updateEmployeeEmail = new SqlCommand(updateEmployeeEmailField, sqlConnection);
                                    updateEmployeeEmail.ExecuteNonQuery();
                                }
                                else if (fieldOption == "X")
                                {
                                    sqlConnection.Close();
                                }
                                else throw new Exception();
                            }
                            break;
                        //RETRIEVE STARTS HERE
                        case "B":
                            Console.WriteLine("Retrieve All Employee Data");
                            Console.WriteLine("Press Any Key To Proceed");
                            Console.ReadLine();
                            string retrieveQuery = "SELECT * FROM Employees";
                            SqlCommand retrieveQueryEmployee = new SqlCommand(retrieveQuery, sqlConnection);
                            SqlDataReader dataReader = retrieveQueryEmployee.ExecuteReader();
                            while (dataReader.Read())
                            {
                                Console.WriteLine(dataReader.GetValue(0).ToString());
                                Console.WriteLine(dataReader.GetValue(1).ToString());
                                Console.WriteLine(dataReader.GetValue(2).ToString());
                                Console.WriteLine(dataReader.GetValue(3).ToString());
                                Console.WriteLine(dataReader.GetValue(4).ToString());
                            }
                            Console.WriteLine("\n-- END --");
                            Console.WriteLine("Press Any Key To Proceed");
                            Console.ReadLine();
                            sqlConnection.Close();
                            break;
                        //RETRIEVE ENDS HERE

                        //DELETE STARTS HERE
                        case "D":
                            while (true)
                            {
                                Console.WriteLine("\nDelete Field(s) From Employee Data");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nWARNING: This Process Is Destructive And Irreversible");
                                Console.ResetColor();
                                Console.WriteLine("Press Any Key to Proceed, X to Abort");
                                string fieldOptionB = Console.ReadLine().ToUpper();
                                if (fieldOptionB == "X") { break; }
                                Console.WriteLine("Enter Employee ID for record you wish to delete");
                                int deleteEmployeeID = int.Parse(Console.ReadLine());
                                string deleteEmployeeIdQuery = ($"DELETE FROM Employees WHERE EmployeeID = {deleteEmployeeID}");
                                SqlCommand deleteEmployeeIdCommand = new SqlCommand(deleteEmployeeIdQuery, sqlConnection);
                                deleteEmployeeIdCommand.ExecuteNonQuery();
                                Console.WriteLine($"Employee {deleteEmployeeID} has been deleted");
                            }
                            break;
                        case "X":
                            {
                                Console.WriteLine("Thank You For Using The CRUDE App");
                                Console.ReadLine();
                                sqlConnection.Close();
                                Environment.Exit(0);
                                break;
                            }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Please Enter A Valid Entry");
                }
            }
        }
    }
}