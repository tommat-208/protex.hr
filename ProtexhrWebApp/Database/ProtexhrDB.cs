using System.Diagnostics;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Repository;


namespace ProtexhrWebApp.Database
{
    public static class ProtexhrDB
    {
        private static WebApplicationBuilder? builder = WebApplication.CreateBuilder();
        private static string connStr = builder.Configuration.GetConnectionString("ProtexhrDBConnectionString");
        //private static string connStr = "Server=10.99.99.167,1433;Database=protex.hr;User Id=sa;Password=e65e2709-9b47-44dc-a0f8-780830ff000f;TrustServerCertificate=True";


        /*
         // Modello generale
        using (SqlConnection conn = new SqlConnection(connStr))
        {
            conn.Open();

            var txt = $@"";
            var cmd = new SqlCommand(txt, conn);
            var rdr = cmd.ExecuteReader();

            while (rdr.Read()) 
            {
                    
            }
    
        }
         */


        /* Employees */
        public static Employee GetEmployee(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $"SELECT * FROM employees WHERE id={id};";
                var cmd = new SqlCommand(txt, conn);
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var e = new Employee()
                    {
                        Id = rdr.GetInt32(0),
                        FirstName = rdr.GetString(1),
                        LastName = rdr.GetString(2),
                        Role = GetRole(rdr.GetInt32(3)),
                        HireDate = rdr.GetDateTime(4),
                        Email = rdr.GetString(5)
                    };

                    return e;
                }

                return null;
            }


        }

        public static List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = "SELECT id FROM employees;";
                var cmd = new SqlCommand(txt, conn);
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    employees.Add(GetEmployee(rdr.GetInt32(0)));
                }

            }

            return employees;
        }

        public static void InsertEmployee(Employee e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"INSERT INTO employees(firstName, lastName, roleId, hireDate, email) VALUES
                        ('{e.FirstName}', '{e.LastName}', {e.Role.Id}, '{e.HireDate.ToString("yyyy-MM-dd")}', '{e.Email}');";
                var cmd = new SqlCommand(txt, conn);

                cmd.ExecuteNonQuery();
            }
        }

        public static bool UpdateEmployee(Employee e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"UPDATE employees SET firstName='{e.FirstName}', lastName='{e.LastName}', roleId={e.Role.Id}, 
                        hireDate='{e.HireDate.ToString("yyyy-MM-dd")}', email='{e.Email}' WHERE id={e.Id};";
                var cmd = new SqlCommand(txt, conn);
                var result = cmd.ExecuteNonQuery();

                return result > 0;
            }
        }

        public static bool DeleteEmployee(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"DELETE FROM employees WHERE id={id}";
                var cmd = new SqlCommand(txt, conn);
                var result = cmd.ExecuteNonQuery();

                return result > 0;
            }
        }

        /* Roles */
        public static Role GetRole(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $"SELECT * FROM roles WHERE id={id};";
                var cmd = new SqlCommand(txt, conn);
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var r = new Role()
                    {
                        Id = rdr.GetInt32(0),
                        Name = rdr.GetString(1),
                        Description = rdr.GetString(2)
                    };
                    return r;
                }

                return null;
            }
        }

        public static List<Role> GetAllRoles()
        {
            List<Role> roles = new List<Role>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = "SELECT id FROM roles";
                var cmd = new SqlCommand(txt, conn);
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    roles.Add(GetRole(rdr.GetInt32(0)));
                }
            }

            return roles;
        }

        public static void InsertRole(Role r)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"INSERT INTO roles(name, description) VALUES ('{r.Name}', '{r.Description}');";

                var cmd = new SqlCommand(txt, conn);

                cmd.ExecuteNonQuery();
            }
        }

        public static bool UpdateRole(Role r)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"UPDATE roles SET name='{r.Name}', description='{r.Description}' WHERE id={r.Id};";
                var cmd = new SqlCommand(txt, conn);
                var result = cmd.ExecuteNonQuery();

                return result > 0;
            }
        }

        public static bool DeleteRole(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"DELETE FROM roles WHERE id={id}";
                var cmd = new SqlCommand(txt, conn);
                var result = cmd.ExecuteNonQuery();

                return result > 0;
            }
        }


        /* Attendance records */
        public static void InsertAttendanceRecord(AttendanceRecord a)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"INSERT INTO attendanceRecords(employeeId, date, checkInTime, checkOutTime) VALUES 
                            ({a.Employee.Id}, '{a.Date.ToString("yyyy-MM-dd")}', '{a.CheckInTime.ToString()}', 
                            '{a.CheckOutTime.ToString()}');";

                var cmd = new SqlCommand(txt, conn);
                cmd.ExecuteNonQuery();
            }

        }

        public static AttendanceRecord GetAttendanceRecord(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"SELECT * FROM attendanceRecords WHERE id={id};";
                var cmd = new SqlCommand(txt, conn);
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    AttendanceRecord a = new()
                    {
                        Id = rdr.GetInt32(0),
                        Employee = GetEmployee(rdr.GetInt32(1)),
                        Date = rdr.GetDateTime(2),
                        CheckInTime = rdr.GetTimeSpan(3),
                        CheckOutTime = rdr.GetTimeSpan(4)
                    };

                    return a;
                }
                return null;
            }
        }

        public static List<AttendanceRecord> GetAttendanceRecordsByEmployeeId(int id)
        {
            var l = new List<AttendanceRecord>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"SELECT id FROM attendanceRecords WHERE employeeId={id};";
                var cmd = new SqlCommand(txt, conn);
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    l.Add(GetAttendanceRecord(rdr.GetInt32(0)));
                }

            }

            return l;
        }

        /* Leave requests */
        public static LeaveRequest GetLeaveRequest(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"SELECT * FROM leaveRequests WHERE id={id};";
                var cmd = new SqlCommand(txt, conn);
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var l = new LeaveRequest
                    {
                        Id = rdr.GetInt32(0),
                        Employee = GetEmployee(rdr.GetInt32(1)),
                        StartDate = rdr.GetDateTime(2),
                        EndDate = rdr.GetDateTime(3)
                    };

                    return l;
                }

                return null;
            }
        }


        public static List<LeaveRequest> GetPendingLeaveRequests()
        {
            var l = new List<LeaveRequest>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"SELECT id FROM leaveRequests WHERE status='{LeaveRequestStatus.Pending}'";
                var cmd = new SqlCommand(txt, conn);
                var rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    l.Add(GetLeaveRequest(rdr.GetInt32(0)));
                }

            }

            return l;
        }

        public static void InsertLeaveRequest(LeaveRequest l)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"INSERT INTO leaveRequests(employeeId, startDate, endDate, status) VALUES 
                            ({l.Employee.Id}, '{l.StartDate.ToString("yyyy-MM-dd")}', '{l.EndDate.ToString("yyyy-MM-dd")}', '{l.Status}')";
                var cmd = new SqlCommand(txt, conn);
                cmd.ExecuteNonQuery();
            }
        }

        public static bool UpdateLeaveRequest(LeaveRequest l)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                var txt = $@"UPDATE leaveRequests SET employeeId={l.Employee.Id}, startDate='{l.StartDate.ToString("yyyy-MM-dd")}',
                            endDate='{l.EndDate.ToString("yyyy-MM-dd")}', status='{l.Status}' WHERE id={l.Id}";
                var cmd = new SqlCommand(txt, conn);
                var result = cmd.ExecuteNonQuery();

                return result > 0;
            }


        }

    }
}
