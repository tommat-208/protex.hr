using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Repository;

namespace protex.hr.Services
{
    public static class APIService
    {
        private static string url = "https://10.99.99.46:7062";
        private static HttpClient _httpClient { get { return GetHttpClient(); } }

        /* Employee */
        public static async Task<List<Employee>> GetAllEmployees()
        {
            var res = await _httpClient.GetAsync($"{url}/Employees/all");

            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                var l = JsonConvert.DeserializeObject<List<Employee>>(json);
                return l;
            }

            return null;
        }

        public static async Task<Employee> GetEmployee(int id)
        {
            var res = await _httpClient.GetAsync($"{url}/Employees?id="+id);

            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                var e = JsonConvert.DeserializeObject<Employee>(json);
                return e;
            }

            return null;
        }

        public static async Task<bool> InsertEmployee(Employee e)
        {
            var v = new ModelValidator();
            v.Validate(e);

            string json = JsonConvert.SerializeObject(e);
            var cont = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _httpClient.PostAsync(url+"/Employees", cont);

            return res.IsSuccessStatusCode;
        }

        public static async Task<bool> DeleteEmployee(int id)
        {
            var res = await _httpClient.DeleteAsync(url + "/Employees?id=" + id);

            return res.IsSuccessStatusCode;
        }

        public static async Task<bool> UpdateEmployee(Employee e)
        {
            var v = new ModelValidator();
            v.Validate(e);

            var json = JsonConvert.SerializeObject(e);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _httpClient.PutAsync(url + "/Employees", content);

            return res.IsSuccessStatusCode;
        }

        /* Role */
        public static async Task<List<Role>> GetAllRoles()
        {
            var res = await _httpClient.GetAsync(url + "/Roles/all");

            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                var l = JsonConvert.DeserializeObject<List<Role>>(json);

                return l;
            }

            return null;
        }

        public static async Task<bool> InsertRole(Role r)
        {
            var v = new ModelValidator();
            v.Validate(r);

            string json = JsonConvert.SerializeObject(r);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _httpClient.PostAsync(url + "/Roles", content);

            return res.IsSuccessStatusCode;
        }

        public static async Task<bool> DeleteRole(int id)
        {
            var res = await _httpClient.DeleteAsync(url + "/Roles?id=" + id);

            return res.IsSuccessStatusCode;
        }

        public static async Task<Role> GetRole(int id)
        {
            var res = await _httpClient.GetAsync(url + "/Roles?id=" + id);

            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                var l = JsonConvert.DeserializeObject<Role>(json);

                return l;
            }

            return null;

        }

        public static async Task<bool> UpdateRole(Role r)
        {
            var v = new ModelValidator();
            v.Validate(r);

            var json = JsonConvert.SerializeObject(r);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _httpClient.PutAsync(url + "/Roles", content);

            return res.IsSuccessStatusCode;
        }

        /* AttendanceRecords */
        public static async Task<bool> InsertAttendanceRecord(AttendanceRecord a)
        {
            var v = new ModelValidator();
            v.Validate(a);

            Console.WriteLine("APIService.InsertAttendanceRecord: passata la validazione");

            var json = JsonConvert.SerializeObject(a);

            Console.Write("json: " + json);
            var content = new StringContent(json, Encoding.UTF8 , "application/json");
            var res = await _httpClient.PostAsync(url + "/Attendances", content);

            return res.IsSuccessStatusCode;
        }

        public static async Task<List<AttendanceRecord>> GetAttendaceByEmployeeId(int employee_id)
        {
            var res = await _httpClient.GetAsync(url + "/Attendances?employee_id="+employee_id);

            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var l = JsonConvert.DeserializeObject<List<AttendanceRecord>>(json);

                return l;
            }

            return null;
        }


        /* LeaveRequests */
        public static async Task<bool> InsertLeaveRequest(LeaveRequest l)
        {
            var v = new ModelValidator();
            v.Validate(l);

            var json = JsonConvert.SerializeObject(l);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _httpClient.PostAsync(url + "/Leaves", content);

            return res.IsSuccessStatusCode;
        }

        public static async Task<List<LeaveRequest>> GetPendingLeaveRequests()
        {
            var res = await _httpClient.GetAsync($"{url}/Leaves/pending");

            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                var l = JsonConvert.DeserializeObject<List<LeaveRequest>>(json);
                return l;
            }

            return null;
        }

        public static async Task<bool> ApproveLeaveRequest(LeaveRequest l)
        {
            var v = new ModelValidator();
            v.Validate(l);

            var json = JsonConvert.SerializeObject(l);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _httpClient.PutAsync(url + "/Leaves/approve", content);

            return res.IsSuccessStatusCode;
        }

        public static async Task<bool> RejectLeaveRequest(LeaveRequest l)
        {
            var v = new ModelValidator();
            v.Validate(l);

            var json = JsonConvert.SerializeObject(l);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _httpClient.PutAsync(url + "/Leaves/reject", content);

            return res.IsSuccessStatusCode;
        }



        private static HttpClient GetHttpClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(url)
            };

            return httpClient;
        }

        
    }
}
