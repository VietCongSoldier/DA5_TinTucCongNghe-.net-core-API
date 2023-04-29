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
    public class CategorynewsController : Controller
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
                string search = "";
                if (formData.Keys.Contains("search") && !string.IsNullOrEmpty(Convert.ToString(formData["search"]))) { search = formData["search"].ToString(); }
                var result = db.Categorynews.Where(x => x.Isdeleted != true).OrderByDescending(x => x.Visiblemenu).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                var cnt = result.ToList().Count();
                dynamic kq = null;
                if (!string.IsNullOrEmpty(search))
                {
                    kq = result.Where(x => x.Isdeleted != true && x.Categorynewsname.ToLower().Contains(search.ToLower()));
                }
                else
                {
                    kq = result;
                }
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
        //[Route("search")]
        //[HttpPost]
        //public IActionResult Search([FromBody] Dictionary<string, object> formData)
        //{
        //    try
        //    {
        //        var page = int.Parse(formData["page"].ToString());
        //        var pageSize = int.Parse(formData["pageSize"].ToString());
        //        var kq = db.Categorynews.Where(x => x.Isdeleted != true).OrderByDescending(x => x.Visiblemenu).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
        //        var cnt = kq.ToList().Count();
        //        return Ok(
        //                 new ResponseListMessage
        //                 {
        //                     page = page,
        //                     totalItem = cnt,
        //                     pageSize = pageSize,
        //                     data = kq
        //                 });

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        [Route("getall")]
        [HttpPost]
        public IActionResult GetAll([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var kq = db.Categorynews.Where(x => x.Isdeleted != true).OrderByDescending(x => x.Visiblemenu).ToList();
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
            var kq = db.Categorynews.SingleOrDefault(x => x.CategorynewsId == id);
            return Ok(new { kq });
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Create([FromBody] CategorynewsModel model)
        {
            db.Categorynews.Add(model.categorynews);
            db.SaveChanges();
            return Ok(new { data = "Thêm thành công!" });
        }


        [Route("update")]
        [HttpPost]
        public IActionResult Update([FromBody] CategorynewsModel model)
        {
            var obj = db.Categorynews.SingleOrDefault(x => x.CategorynewsId == model.categorynews.CategorynewsId);
            obj.CategorynewsId = model.categorynews.CategorynewsId;
            obj.Categorynewsname = model.categorynews.Categorynewsname;
            obj.Noidung = model.categorynews.Noidung;
            obj.Sapxep = model.categorynews.Sapxep;
            obj.Visiblemenu = model.categorynews.Visiblemenu;
            obj.Url = model.categorynews.Url;
            obj.Linkngoai = model.categorynews.Linkngoai;
            db.SaveChanges();
            return Ok(new { data = "Sửa thành công!" });
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var kq = db.Categorynews.SingleOrDefault(x => x.CategorynewsId == id);
            kq.Isdeleted = true;
            db.SaveChanges();
            return Ok(new { data = "Xóa thành công!" });
        }
    }
}
