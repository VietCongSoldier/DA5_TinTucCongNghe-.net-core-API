
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
    public class NewsController : Controller
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
                var result = from t in db.News
                             join c in db.Categories on t.Categoryid equals c.Categoryid
                             join m in db.Members on t.Authorid equals m.Memberid
                             select new eNews
                             {
                                 Newsid = t.Newsid,
                                 CategoryName = c.Categoryname,
                                 Title = t.Title, 
                                 Description = t.Description,
                                 Content = t.Content,
                                 Fullname = m.Fullname,
                                 Posttime = t.Posttime,
                                 Keyword = t.Keyword,
                                 Image = t.Image,
                                 Link = t.Link,
                                 Numread = t.Numread,
                                 Isdeleted = t.Isdeleted,
                             };
                var cnt = result.ToList().Count();
                //var kq = result.Where(x => x.Isdeleted != true).OrderByDescending(x => x.Newsid).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                dynamic kq =null;
                if (!string.IsNullOrEmpty(search))
                {
                    kq = result.Where(x => x.Isdeleted != true  && x.Title.ToLower().Contains(search.ToLower()) || x.CategoryName.ToLower().Contains(search.ToLower()));
                }
                else
                {
                    kq = result.Where(x => x.Isdeleted != true ).OrderByDescending(x => x.Newsid).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
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
        //        var result = from t in db.News
        //                     join c in db.Categories on t.Categoryid equals c.Categoryid
        //                     join m in db.Members on t.Authorid equals m.Memberid
        //                     select new eNews
        //                     {
        //                         Newsid = t.Newsid,
        //                         CategoryName = c.Categoryname,
        //                         Title = t.Title,
        //                         Description = t.Description,
        //                         Content = t.Content,
        //                         Fullname = m.Fullname,
        //                         Posttime = t.Posttime,
        //                         Keyword = t.Keyword,
        //                         Image = t.Image,
        //                         Link = t.Link,
        //                         Numread = t.Numread,
        //                         Isdeleted = t.Isdeleted,
        //                     };
        //        var cnt = result.ToList().Count();
        //        var kq = result.Where(x => x.Isdeleted != true).OrderByDescending(x => x.Newsid).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
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



        [Route("getbyid/{id}")]
        [HttpGet]
        public IActionResult GetById(int? id)
        {
            var result = from t in db.News
                         join c in db.Categories on t.Categoryid equals c.Categoryid
                         join m in db.Members on t.Authorid equals m.Memberid
                         select new 
                         {
                             Newsid = t.Newsid,
                             CategoryName = c.Categoryname,
                             Categoryid=t.Categoryid,
                             Title = t.Title,
                             Description = t.Description,
                             Content = t.Content,
                             Fullname = m.Fullname,
                             Authorid=t.Authorid,
                             Posttime = t.Posttime,
                             Keyword = t.Keyword,
                             Image = t.Image,
                             Link = t.Link,
                             Numread = t.Numread,
                             Isdeleted = t.Isdeleted,
                         };
            var kq = result.SingleOrDefault(x => x.Newsid == id);
            return Ok(new { kq });
        }

        [Route("create")]
        [HttpPost]
        public IActionResult Create([FromBody] NewsModel model)
        {
            db.News.Add(model.news);
            db.SaveChanges();
            return Ok(new { data = "Thêm thành công!" });
        }
        [Route("update")]
        [HttpPost]
        public IActionResult Update([FromBody] NewsModel model)
        {
            var obj = db.News.SingleOrDefault(x => x.Newsid == model.news.Newsid);
            obj.Categoryid = model.news.Categoryid;
            obj.Title = model.news.Title;
            obj.Description = model.news.Description;
            obj.Content = model.news.Content;
            obj.Authorid = model.news.Authorid;
            obj.Posttime = model.news.Posttime;
            obj.Keyword = model.news.Keyword;
            obj.Image = model.news.Image;
            db.SaveChanges();
            return Ok(new { data = "Sửa thành công!" });
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj1 = db.News.SingleOrDefault(s => s.Newsid == id);
            obj1.Isdeleted = true;
            db.SaveChanges();
            return Ok(new { data = "Xóa thành công!" });
        }

        [Route("approved/{id}")]
        [HttpDelete]
        public IActionResult Approved(int? id)
        {
            var obj = db.News.SingleOrDefault(s => s.Newsid == id);
            obj.Link = "approved";
            db.SaveChanges();
            return Ok(new { data = "Xóa thành công!" });
        }
        [Route("unapproved/{id}")]
        [HttpDelete]
        public IActionResult Unapproved(int? id)
        {
            var obj = db.News.SingleOrDefault(s => s.Newsid == id);
            obj.Link = "unapproved";
            db.SaveChanges();
            return Ok(new { data = "Xóa thành công!" });
        }
    }
}
