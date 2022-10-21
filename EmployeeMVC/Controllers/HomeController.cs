using EmployeeMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace EmployeeMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        [HttpGet]
        public IActionResult EmpView()
        {
        
        var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44331");
            List<EmployeeModel>? employee = new List<EmployeeModel>();
            HttpResponseMessage res = client.GetAsync("api/Emp/Get").Result;
            if(res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                employee = JsonConvert.DeserializeObject<List<EmployeeModel>>(result);

            }
            return View(employee);

        }

        public IActionResult Post()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Post(EmployeeModel empreg)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44331");
            var postTask = client.PostAsJsonAsync<EmployeeModel>("api/Emp/Post", empreg);
            postTask.Wait();
            var Result = postTask.Result;
            if (Result.IsSuccessStatusCode)
            {
                return RedirectToAction("EmpView");
            }
            return View();
        }
        public async Task<IActionResult> Delete(string UserName)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44331");
            await client.DeleteAsync($"api/Emp/delete/{UserName}");
            return RedirectToAction("EmpView");

        }
    }
}