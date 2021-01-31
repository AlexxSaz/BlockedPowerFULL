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
using System.Diagnostics;
using System.Xml;
using System.Data;
using SpreadsheetGear;
using LockedPowerLibrary;

namespace BlockedPowerFULL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FactCalcController : Controller
    {
        //TODO: Переписать в относительный путь!!!
        private string _path =
            @"E:\Programms\Diploma\BlockedPowerFULL\calcParams.txt";

        [HttpGet]
        public async Task<JsonResult> GetData()
        {
            int hour = 0;

            using (var sw = new StreamReader(_path,
                System.Text.Encoding.Default, false))
            {
                hour = Convert.ToInt32(await sw.ReadLineAsync());
            }

            var systems =
                ESystem.GetParameters("balance10-04-03-2020.xlsx");
            var sections =
                ESection.GetParameters("ppbr(a)_22012020_1.xls", hour);

            double sumGen = 0;
            double sumLoad = 0;
            double sumReserve = 0;

            int n = 0;
            int nm = 0;

            foreach (var section in sections)
            {
                sumGen = 0;
                sumLoad = 0;
                sumReserve = 0;

                if (section.id == 1)
                {
                    n = 0;
                    nm = 2;
                }
                else if (section.id == 2)
                {
                    n = 2;
                    nm = 4;
                }
                else if (section.id == 3)
                {
                    n = 4;
                    nm = 5;
                }
                else if (section.id == 4)
                {
                    n = 5;
                    nm = 7;
                }
                else if (section.id == 5)
                {
                    n = 7;
                    nm = 11;
                }

                for (int i = n; i < nm; i++)
                {
                    sumGen = sumGen + systems[i].valueOfGenPower;
                    sumLoad = sumLoad + systems[i].valueOfLoadPower;
                    sumReserve = sumReserve + systems[i].getValueOfPowerReserve;
                }
                section.valueOfPowerFlow = sumGen - sumLoad +
                    (sections.IndexOf(section) != 0 ?
                    sections[sections.IndexOf(section) - 1].valueOfPowerFlow : 0);

                section.valueOfBlockedPower = sumReserve - section.valueMDP +
                    section.valueOfPowerFlow +
                    (sections.IndexOf(section) != 0 ?
                    (sections[sections.IndexOf(section) - 1].valueMDP -
                    sections[sections.IndexOf(section) - 1].valueOfPowerFlow) : 0);
            }

            var sumBlockedPower =
                Math.Round(sections.Sum(x => x.valueOfBlockedPower), 2);

            return Json(sumBlockedPower.ToString());
        }

        [HttpGet]
        public async Task GetReport()
        {
            using (StreamWriter sw = new StreamWriter(_path,
                false, System.Text.Encoding.Default))
            {
                await sw.WriteLineAsync(value);
            }
        }

        [HttpPost]
        public async Task Post([FromBody] string value)
        {
            using (StreamWriter sw = new StreamWriter(_path,
                false, System.Text.Encoding.Default))
            {
                await sw.WriteLineAsync(value);
            }
        }
    }
}
