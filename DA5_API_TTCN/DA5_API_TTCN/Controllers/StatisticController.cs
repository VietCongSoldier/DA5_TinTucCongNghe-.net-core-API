using System;
using System.Linq;
using DA5_API_TTCN.Entities;
using DA5_API_TTCN.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace DA5_API_TTCN.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticController : Controller
    {
        private TinTucCongNgheDA5Context db = new TinTucCongNgheDA5Context();
        [Route("search")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var kq = db.Statistics.Where(x => x.Isdeleted != true).OrderByDescending(x => x.Statisticid).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                var cnt = kq.ToList().Count();
                return Ok(
                         new ResponseListMessage
                         {
                             page = page,
                             totalItem = cnt,
                             pageSize = pageSize,
                             data = kq
                         });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Route("getall")]
        [HttpPost]
        public IActionResult GetAll([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var kq = db.Statistics.Where(x => x.Isdeleted != true).OrderByDescending(x => x.Statisticid).ToList();
                return Ok(kq);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Route("getbyid/{id}")]
        [HttpGet]
        public IActionResult GetById(int? id)
        {
            var kq = db.Statistics.SingleOrDefault(x => x.Statisticid == id);
            return Ok(new { kq });
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Create([FromBody] StatisticModel model)
        {
            db.Statistics.Add(model.statistic);
            db.SaveChanges();
            return Ok(new { data = "Thêm thành công!" });
        }


        [Route("update")]
        [HttpPost]
        public IActionResult Update([FromBody] StatisticModel model)
        {
            var obj = db.Statistics.SingleOrDefault(x => x.Statisticid == model.statistic.Statisticid);
            obj.Statisticid = model.statistic.Statisticid;
            obj.Posttimenews = model.statistic.Posttimenews;
            obj.Postmostread = model.statistic.Postmostread;
            obj.Visitnumer = model.statistic.Visitnumer;
            db.SaveChanges();
            return Ok(new { data = "Sửa thành công!" });
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var kq = db.Statistics.SingleOrDefault(x => x.Statisticid == id);
            kq.Isdeleted = true;
            db.SaveChanges();
            return Ok(new { data = "Xóa thành công!" });
        }
    }
}
