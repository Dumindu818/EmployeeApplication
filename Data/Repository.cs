using EmployeeApplication.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeApplication.Data
{
    public class Repository
    {
        private SqlConnection _connection;

        public Repository()
        {
            string connStr = "server=Dumindu-PC;database=EmployeeDB;Integrated Security=true;TrustServerCertificate=true;";
            _connection = new SqlConnection(connStr);
        }

        public List<EmployeesEntity> GetAllEmployees() // Fetch All the Records from the DB
        {
            List<EmployeesEntity> employeeListEntity = new List<EmployeesEntity>();
            SqlCommand cmd = new SqlCommand("GetAllEmployees", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt=new DataTable();
            dataAdapter.Fill(dt);

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

            return employeeListEntity;
        }

        public EmployeesEntity GetEmployeeById(int Id) // Fetch All the Records from the DB
        {
            EmployeesEntity employeeListEntity = new EmployeesEntity();
            SqlCommand cmd = new SqlCommand("GetEmployeeDetailsById", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlParameter param;
            cmd.Parameters.Add(new SqlParameter("@Id", Id));


            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

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

        public bool AddEmployee(EmployeesEntity employee) //Adding Employees
        {
            SqlCommand cmd = new SqlCommand("AddEmployee", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

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

        public bool EditEmployeeDetails(int Id, EmployeesEntity employee) //Edit Employee Details
        {
            SqlCommand cmd = new SqlCommand("EditEmployee", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

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
            SqlCommand cmd = new SqlCommand("DeleteEmployee", _connection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Id", employee.Id);

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
    }
}
