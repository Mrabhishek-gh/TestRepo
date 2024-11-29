using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplicationTest.Models
{
    public class Student
    {
        public int customer_id { get; set; }

        [Required(ErrorMessage = "First is required")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 Characters")]
        public string first_name { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 Characters")]
        public string last_name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email address Format")]
        public string email { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(2, ErrorMessage = "State code cannot exceed 2 Characters")]
        public string state { get; set; }

    }

    public class StudentAdd
    {
        public string street { get; set; }
        public string city { get; set; }
    }

    public class Login
    {
        public string username { get; set; }
        public string password { get; set; }

    }



    public class StudentBuisness
    {
        public List<Student> GetAllCustomers()
        {

            string connstr = "data source = localhost; database = BikeStores; user id = sa; password = mcc#1234";

            SqlConnection sqlConnection = new SqlConnection(connstr);

            SqlCommand sqlCommand = new SqlCommand("select [customer_id], [first_name], [last_name],[email],[state] from [sales].[customers] where [customer_id] >= 1440", sqlConnection);

            List<Student> studentList = new List<Student>();

            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Student obj = new Student();

                    obj.customer_id = Convert.ToInt32(reader["customer_id"]);
                    obj.first_name = reader["first_name"].ToString();
                    obj.last_name = reader["last_name"].ToString();
                    obj.email = reader["email"].ToString();
                    obj.state = reader["state"].ToString();

                    studentList.Add(obj);
                }
            }

            sqlConnection.Close();

            return studentList;
        }

        public List<StudentAdd> GetAllCustomersAddress()
        {

            string connstr = "data source = localhost; database = BikeStores; user id = sa; password = mcc#1234";

            SqlConnection sqlConnection = new SqlConnection(connstr);

            SqlCommand sqlCommand = new SqlCommand("select [street], [city] from [sales].[customers] where [customer_id] >= 1440", sqlConnection);

            List<StudentAdd> studentListAdd = new List<StudentAdd>();

            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    StudentAdd obj = new StudentAdd();

                    obj.street = reader["street"].ToString();
                    obj.city = reader["city"].ToString();

                    studentListAdd.Add(obj);
                }
            }

            sqlConnection.Close();

            return studentListAdd;
        }



        public Student GetCustomer(int id)
        {

            string connstr = "data source = localhost; database = BikeStores; user id = sa; password = mcc#1234";

            SqlConnection sqlConnection = new SqlConnection(connstr);

            SqlCommand sqlCommand = new SqlCommand("select[customer_id], [first_name], [last_name],[email],[state] from[sales].[customers]  where [customer_id] = @id", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@id", id);

            Student obj = new Student();
            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();

                obj.customer_id = Convert.ToInt32(reader["customer_id"]);
                obj.first_name = reader["first_name"].ToString();
                obj.last_name = reader["last_name"].ToString();
                obj.email = reader["email"].ToString();
                obj.state = reader["state"].ToString();
            }
            else
            {
                obj = null;
            }

            sqlConnection.Close();

            return obj;
        }

        public string InsertCustomer(Student student)
        {
            string msg = string.Empty;
            try
            {

                string connstr = "data source = localhost; database = BikeStores; user id = sa; password = mcc#1234";

                DataSet dataSet = new DataSet();
                SqlDataAdapter dataAdapter = new SqlDataAdapter("insert into  [sales].[customers] (first_name, last_name,email,state) values(@first_name, @last_name,@email,@state)", connstr);

                dataAdapter.SelectCommand.Parameters.AddWithValue("@first_name", student.first_name);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@last_name", student.last_name);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@email", student.email);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@state", student.state);

                dataAdapter.Fill(dataSet);

                msg = "Succesfully Inserted";
            }
            catch (Exception)
            {

                msg = "Something went wrong";
            }

            return msg;

        }

        public void DeleteCustomer(int id)
        {

            string connstr = "data source = localhost; database = BikeStores; user id = sa; password = mcc#1234";

            SqlConnection sqlConnection = new SqlConnection(connstr);

            SqlCommand sqlCommand = new SqlCommand("delete from [sales].[customers] where [customer_id] = @cust_id", sqlConnection);

            sqlCommand.Parameters.AddWithValue("@cust_id", id);
            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public string UpdateCustomer(int id, Student obj)
        {
            string msg = string.Empty;
            try
            {

                string connstr = "data source = localhost; database = BikeStores; user id = sa; password = mcc#1234";

                SqlConnection sqlConnection = new SqlConnection(connstr);

                SqlCommand sqlCommand = new SqlCommand("update [sales].[customers] set [first_name] = @f_name, [last_name] = @l_name, [email] = @email, [state] = @state where [customer_id] = @cust_id", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@cust_id", id);
                sqlCommand.Parameters.AddWithValue("@f_name", obj.first_name);
                sqlCommand.Parameters.AddWithValue("@l_name", obj.last_name);
                sqlCommand.Parameters.AddWithValue("@email", obj.email);
                sqlCommand.Parameters.AddWithValue("@state", obj.state);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                msg = "Successfully Updated";
            }
            catch (SqlException)
            {

                msg = "Something went wrong";
            }

            return msg;


        }

        public int CountCustomer()
        {
            int count = 0;


            string connstr = "data source = localhost; database = BikeStores; user id = sa; password = mcc#1234";

            SqlConnection sqlConnection = new SqlConnection(connstr);

            SqlCommand sqlCommand = new SqlCommand("select count(*) from [sales].[customers] where [customer_id] >= 1440 ", sqlConnection);

            sqlConnection.Open();
            count = Convert.ToInt32(sqlCommand.ExecuteScalar());
            sqlConnection.Close();

            return count;

        }

        public int Auth(Login obj)
        {
            string connstr = "data source = localhost; database = BikeStores; user id = sa; password = mcc#1234";

            SqlConnection sqlConnection = new SqlConnection(connstr);

            SqlCommand sqlCommand = new SqlCommand("sp_Authentication", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@username",obj.username.ToString()),
                new SqlParameter("@password",obj.password.ToString())
            };

            SqlParameter message = new SqlParameter()
            {
                ParameterName = "@output",
                Size = 1,
                Direction = ParameterDirection.Output
            };

            sqlCommand.Parameters.AddRange(sqlParameter);
            sqlCommand.Parameters.Add(message);

            sqlConnection.Open();
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close() ;

            int outputValue = Convert.ToInt32(sqlCommand.Parameters["@output"].Value);

            return outputValue;

        }
    }
}