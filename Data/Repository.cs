using EmployeeApplication.Models; //Access EmployeesEntity Model
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data; //using for handling DataTable, DataRow, and related database objects
using System.Data.SqlClient; //using for SQL Server database connections and commands

namespace EmployeeApplication.Data
{
    public class Repository
    {
        private SqlConnection _connection;// Private field for managing SQL Server connection

        public Repository() //constructor for db initialization
        {
            string connStr = "server=Dumindu-PC;database=EmployeeDB;Integrated Security=true;TrustServerCertificate=true;"; //Connection String
            _connection = new SqlConnection(connStr); //Assign connection string into _cooection variable.
        }

        public List<EmployeesEntity> GetAllEmployees() // Fetch All the Records from the DB
        {
            List<EmployeesEntity> employeeListEntity = new List<EmployeesEntity>(); //Create a list for store Employees Data
            SqlCommand cmd = new SqlCommand("GetAllEmployees", _connection); //create sql command
            cmd.CommandType = System.Data.CommandType.StoredProcedure; //command is calling via stored procedure

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);// Adapter to fill data from SQL query
            DataTable dt=new DataTable(); //DataTable for holding results
            dataAdapter.Fill(dt); //Datatable fill with Quiried Data

            // Map the first row to EmployeesEntity
            foreach (DataRow dr in dt.Rows)
            {
                employeeListEntity.Add(
                    new EmployeesEntity
                    {
                        Id=Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
                        Email= dr["Email"].ToString(),
                        JobPosition = dr["JobPosition"].ToString(),

                    }
                    );
            }

            return employeeListEntity; //Return EmployeesDetails
        }

        public EmployeesEntity GetEmployeeById(int Id) // Fetch All the Records from the DB (employee details by ID)
        {
            EmployeesEntity employeeListEntity = new EmployeesEntity(); // Object to hold employee details
            SqlCommand cmd = new SqlCommand("GetEmployeeDetailsById", _connection);// SQL command to execute stored procedure (GetEmployeeDetailsById)
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter param;
            cmd.Parameters.Add(new SqlParameter("@Id", Id));// Add employee ID as a parameter


            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            // Map the first row to EmployeesEntity
            foreach (DataRow dr in dt.Rows)
            {
                employeeListEntity = new EmployeesEntity
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Name = dr["Name"].ToString(),
                    Email = dr["Email"].ToString(),
                    JobPosition = dr["JobPosition"].ToString(),

                };
                   
            }

            return employeeListEntity;
        }

        public bool AddEmployee(EmployeesEntity employee) //Adding Employees to db
        {
            SqlCommand cmd = new SqlCommand("AddEmployee", _connection); // SQL command to execute stored procedure
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // Add parameters to the command
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@JobPosition", employee.JobPosition);

            _connection.Open(); // Open database connection

            int i = cmd.ExecuteNonQuery(); // Execute the command and get the number of rows affected
            _connection.Close(); // Close the connection

            if (i >= 1)
            {
                return true; // Return true if at least one row was affected
            }
            else
            {
                return false;
            }

        }

        public bool EditEmployeeDetails(int Id, EmployeesEntity employee) //Edit Employee Details
        {
            SqlCommand cmd = new SqlCommand("EditEmployee", _connection);// SQL command to execute stored procedure (EditEmployee)
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            // Add parameters to the command
            cmd.Parameters.AddWithValue("@Id", employee.Id);
            cmd.Parameters.AddWithValue("@Name", employee.Name);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@JobPosition", employee.JobPosition);

            _connection.Open();

            int i = cmd.ExecuteNonQuery();
            _connection.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool DeleteEmployeeDetails(int Id, EmployeesEntity employee) //Delete Employee Details
        {
            SqlCommand cmd = new SqlCommand("DeleteEmployee", _connection); // SQL command to execute stored procedure(DeleteEmployee)
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", employee.Id); // Add employee ID as a parameter

            _connection.Open();

            int i = cmd.ExecuteNonQuery();
            _connection.Close();

            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        // Calculate Working Days
        public int GetWorkingDays(DateTime startDate, DateTime endDate)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("CalculateWorkingDays", _connection)// SQL command to execute stored procedure(CalculateWorkingDays)
                {
                    CommandType = CommandType.StoredProcedure
                };

                // Add start and end dates as parameters
                cmd.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date) { Value = startDate });
                cmd.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date) { Value = endDate });

                _connection.Open();
                int workingDays = (int)cmd.ExecuteScalar();// Execute the command and get the result
                _connection.Close();

                return workingDays;// Return the calculated working days
            }
            catch (Exception ex)// Handle exceptions
            {
                Console.WriteLine($"Error calculating working days: {ex.Message}");  // Log the error
                throw; //throw the Exeption
            }
        }

    }
}
