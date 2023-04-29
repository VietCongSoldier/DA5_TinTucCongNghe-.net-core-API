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
    [Route("api/home")]
    public class HomeController : Controller
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
                dynamic kq = null;
                if (!string.IsNullOrEmpty(search))
                {
                    kq = result.Where(x => x.Isdeleted != true && x.Link != "unapproved" && x.Title.ToLower().Contains(search.ToLower()) || x.CategoryName.ToLower().Contains(search.ToLower()));
                }
                else
                {
                    kq = result.Where(x => x.Isdeleted != true && x.Link != "unapproved").OrderByDescending(x => x.Newsid).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
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
        //                         Categoryid = t.Categoryid,
        //                         Title = t.Title,
        //                         Description = t.Description,
        //                         Content = t.Content,
        //                         Authorid = t.Authorid,
        //                         Fullname = m.Fullname,
        //                         Posttime = t.Posttime,
        //                         Keyword = t.Keyword,
        //                         Image = t.Image,
        //                         Link = t.Link,
        //                         Numread = t.Numread,
        //                         Isdeleted = t.Isdeleted,
        //                     };
        //        var cnt = result.ToList().Count();
        //        var kq = result.Where(x => x.Isdeleted != true && x.Link != "unapproved").OrderByDescending(x => x.Posttime).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
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
        [Route("getslide")]
        [HttpGet]
        public IActionResult GetSlide()
        {
            try
            {
                var result = from t in db.News
                             join c in db.Categories on t.Categoryid equals c.Categoryid
                             join m in db.Members on t.Authorid equals m.Memberid
                             select new eNews
                             {
                                 Newsid = t.Newsid,
                                 CategoryName = c.Categoryname,
                                 Categoryid = t.Categoryid,
                                 Title = t.Title,
                                 Description = t.Description,
                                 Content = t.Content,
                                 Authorid = t.Authorid,
                                 Fullname = m.Fullname,
                                 Posttime = t.Posttime,
                                 Keyword = t.Keyword,
                                 Image = t.Image,
                                 Link = t.Link,
                                 Numread = t.Numread,
                                 Isdeleted = t.Isdeleted,
                             };
                var kq = result.Where(x => x.Isdeleted != true && x.Link != "unapproved").Take(20).ToList();
                return Ok(kq);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Route("getthuthuat")]
        [HttpGet]
        public IActionResult GetNewsCategory()
        {
            try
            {
                var result = from t in db.News
                             join c in db.Categories on t.Categoryid equals c.Categoryid
                             join m in db.Members on t.Authorid equals m.Memberid
                             select new eNews
                             {
                                 Newsid = t.Newsid,
                                 CategoryName = c.Categoryname,
                                 Categoryid = t.Categoryid,
                                 Title = t.Title,
                                 Description = t.Description,
                                 Content = t.Content,
                                 Authorid = t.Authorid,
                                 Fullname = m.Fullname,
                                 Posttime = t.Posttime,
                                 Keyword = t.Keyword,
                                 Image = t.Image,
                                 Link = t.Link,
                                 Numread = t.Numread,
                                 Isdeleted = t.Isdeleted,
                             };
                var kq = result.Where(x => x.Isdeleted != true && x.Categoryid==6 && x.Link != "unapproved").Take(10).ToList();
                return Ok(kq);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Route("getnewsbycategoryid/{id}")]
        [HttpPost]
        public IActionResult Getnewsbycateid([FromBody] Dictionary<string, object> formData, int? id)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var result = from t in db.News
                             join c in db.Categories on t.Categoryid equals c.Categoryid
                             join m in db.Members on t.Authorid equals m.Memberid
                             select new eNews
                             {
                                 Newsid = t.Newsid,
                                 CategoryName = c.Categoryname,
                                 Categoryid = t.Categoryid,
                                 Title = t.Title,
                                 Description = t.Description,
                                 Content = t.Content,
                                 Authorid = t.Authorid,
                                 Fullname = m.Fullname,
                                 Posttime = t.Posttime,
                                 Keyword = t.Keyword,
                                 Image = t.Image,
                                 Link = t.Link,
                                 Numread = t.Numread,
                                 Isdeleted = t.Isdeleted,
                             };
                var cnt = result.ToList().Count();
                var kq = result.Where(x => x.Isdeleted != true && x.Categoryid == id && x.Link != "unapproved").OrderByDescending(x => x.Posttime).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
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
        [Route("getnewsbyauthorid/{id}")]
        [HttpPost]
        public IActionResult Getnewsbyauthorid([FromBody] Dictionary<string, object> formData, int? id)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var result = from t in db.News
                             join c in db.Categories on t.Categoryid equals c.Categoryid
                             join m in db.Members on t.Authorid equals m.Memberid
                             select new eNews
                             {
                                 Newsid = t.Newsid,
                                 CategoryName = c.Categoryname,
                                 Categoryid = t.Categoryid,
                                 Title = t.Title,
                                 Description = t.Description,
                                 Content = t.Content,
                                 Authorid = t.Authorid,
                                 Fullname = m.Fullname,
                                 Posttime = t.Posttime,
                                 Keyword = t.Keyword,
                                 Image = t.Image,
                                 Link = t.Link,
                                 Numread = t.Numread,
                                 Isdeleted = t.Isdeleted,
                             };
                var cnt = result.ToList().Count();
                var kq = result.Where(x => x.Isdeleted != true && x.Authorid == id && x.Link != "unapproved").OrderByDescending(x => x.Posttime).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
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
        [Route("getnewsbycate/{id}")]
        [HttpGet]
        public IActionResult Getnewsbycateid1( int? id)
        {
            try
            {
                var result = from t in db.News
                             select new 
                             {
                                 Newsid = t.Newsid,
                                 Categoryid = t.Categoryid,
                                 Title = t.Title,
                                 Description = t.Description,
                                 Content = t.Content,
                                 Authorid = t.Authorid,
                                 Posttime = t.Posttime,
                                 Keyword = t.Keyword,
                                 Image = t.Image,
                                 Link = t.Link,
                                 Numread = t.Numread,
                                 Isdeleted = t.Isdeleted,
                             };
                var cnt = result.ToList().Count();
                var kq = result.Where(x => x.Isdeleted != true && x.Categoryid == id && x.Link != "unapproved").OrderByDescending(x => x.Posttime).Take(7).ToList();
                return Ok(kq);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Route("getnewsbyid/{id}")]
        [HttpGet]
        public IActionResult GetNewsById(int? id)
        {
            var result = from t in db.News
                         join c in db.Categories on t.Categoryid equals c.Categoryid
                         join m in db.Members on t.Authorid equals m.Memberid
                         select new eNews
                         {
                             Newsid = t.Newsid,
                             CategoryName = c.Categoryname,
                             Categoryid = t.Categoryid,
                             Title = t.Title,
                             Description = t.Description,
                             Content = t.Content,
                             Authorid = t.Authorid,
                             Fullname = m.Fullname,
                             Posttime = t.Posttime,
                             Keyword = t.Keyword,
                             Image = t.Image,
                             Link = t.Link,
                             Numread = t.Numread,
                             Isdeleted = t.Isdeleted,
                         };
            var kq = result.Where(x => x.Newsid == id && x.Link != "unapproved");
            return Ok(new JsonResult(kq)); 
        }
        //CATEGORY
        [Route("getallCategory")]
        [HttpGet]
        public IActionResult GetAllCate()
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
        [Route("getcategorybyid/{id}")]
        [HttpGet]
        public IActionResult GetCateById(int? id)
        {
            var kq = db.Categorynews.SingleOrDefault(x => x.CategorynewsId == id);
            return Ok(kq);
        }
        [Route("getauthorbyid/{id}")]
        [HttpGet]
        public IActionResult GetAuthorById(int? id)
        {
            var kq = db.Members.SingleOrDefault(x => x.Memberid == id);
            return Ok(kq);
        }
        //COMMENT
        [Route("getcomment")]
        [HttpGet]
        public IActionResult GetCmt()
        {
            try
            {
                var result = from t in db.Feedbacks
                             select new
                             {
                                 t.Feedbackid,
                                 t.Namereader,
                                 t.Contents,
                                 t.Email,
                                 t.Datecomment,
                                 t.Status,
                                 t.Img,
                                 t.Newsid,
                                 t.Isdeleted
                             };
                var kq = result.Where(x => x.Isdeleted != true).OrderByDescending(x => x.Datecomment).Take(10).ToList();
                return Ok(kq);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Route("getcommentbynewsid/{id}")]
        [HttpGet]
        public IActionResult GetCmtByNewsId(int? id)
        {
            var kq = db.Feedbacks.Where(x => x.Isdeleted != true && x.Newsid == id ).OrderByDescending(x => x.Datecomment).ToList();
            return Ok(new JsonResult(kq));
        }
        //Member
        [Route("getmember")]
        [HttpGet]
        public IActionResult GetMem()
        {
            try
            {
                var result = from t in db.Members
                             select new
                             {
                                 t.Memberid,t.Fullname,t.Gender,t.Birthday,t.Address,t.Img,t.Isdeleted
                             };
                var kq = result.Where(x => x.Isdeleted != true).Take(10).ToList();
                return Ok(kq);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Route("getmemberbyid/{id}")]
        [HttpGet]
        public IActionResult GetMemById(int? id)
        {
            var kq = db.Feedbacks.SingleOrDefault(x => x.Feedbackid == id);
            return Ok(new { kq });
        }

        //Update View
        [Route("updateview/{id}")]
        [HttpGet]
        public IActionResult UpdateView(int? id)
        {
            var obj = db.News.SingleOrDefault(s => s.Newsid == id);
            obj.Numread += 1;
            db.SaveChanges();
            return Ok(new { data = "Thành công!" });
        }

        //Add Cmt
        [Route("addcomment")]
        [HttpPost]
        public IActionResult Addcmt([FromBody] CommentModel cmt)
        {
            db.Feedbacks.Add(cmt.feedback);
            db.SaveChanges();
            return Ok(new { data = "Thêm thành công!" });
        }
    }
}
