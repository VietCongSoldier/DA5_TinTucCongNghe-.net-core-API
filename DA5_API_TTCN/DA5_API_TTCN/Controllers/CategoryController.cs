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
    public class CategoryController : Controller
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
                var result = db.Categories.Where(x => x.Isdeleted != true).OrderByDescending(x => x.Visiblemenu).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                var cnt = result.ToList().Count();
                dynamic kq = null;
                if (!string.IsNullOrEmpty(search))
                {
                    kq = result.Where(x => x.Isdeleted != true && x.Categoryname.ToLower().Contains(search.ToLower()));
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
        //        var kq = db.Categories.Where(x => x.Isdeleted != true).OrderByDescending(x => x.Visiblemenu).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
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
                var kq = db.Categories.Where(x => x.Isdeleted != true).OrderByDescending(x => x.Visiblemenu).ToList();
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
            var kq = db.Categories.SingleOrDefault(x => x.Categoryid == id);
            return Ok(new { kq });
        }

        [Route("add")]
        [HttpPost]
        public IActionResult Create([FromBody] CategoryModel model)
        {
            db.Categories.Add(model.category);
            db.SaveChanges();
            return Ok(new { data = "Thêm thành công!" });
        }


        [Route("update")]
        [HttpPost]
        public IActionResult Update([FromBody] CategoryModel model)
        {
            var obj = db.Categories.SingleOrDefault(x => x.Categoryid == model.category.Categoryid);
            obj.Categoryid = model.category.Categoryid;
            obj.Categoryname = model.category.Categoryname;
            obj.Content = model.category.Content;
            obj.Isdeleted = model.category.Isdeleted;
            obj.Sort = model.category.Sort;
            obj.Visiblemenu = model.category.Visiblemenu;
            obj.Url = model.category.Url;
            db.SaveChanges();
            return Ok(new { data = "Sửa thành công!" });
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var kq = db.Categories.SingleOrDefault(x => x.Categoryid == id);
            kq.Isdeleted = true;
            db.SaveChanges();
            return Ok(new { data = "Xóa thành công!" });
        }
    }
}
