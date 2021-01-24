using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace BlockedPowerFULL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactCalcController : Controller
    {
        //TODO: Переписать в относительный путь!!!
        private string _path = @"E:\Programms\Diploma\params.txt";

        [HttpGet]
        public async Task<JsonResult> GetData()
        {
            var str = "";
            using (StreamReader sr = new StreamReader(_path))
            {
                str = await sr.ReadToEndAsync();
            }
            return Json(str);
        }

        [HttpPost]
        public async Task Post([FromBody] string value)
        {
            using (StreamWriter sw = new StreamWriter(_path, false, System.Text.Encoding.Default))
            {
                await sw.WriteLineAsync(value);
            }
        }
    }
}
