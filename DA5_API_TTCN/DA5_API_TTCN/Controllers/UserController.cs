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
    public class UserController : Controller
    {
        private IUserService _userService;
        private TinTucCongNgheDA5Context db = new TinTucCongNgheDA5Context();
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu sai!" });

            return Ok(user);
        }
        [Route("search")]
        [HttpPost]
        public IActionResult Search([FromBody] Dictionary<string, object> formData)
        {
            try
            {
                var page = int.Parse(formData["page"].ToString());
                var pageSize = int.Parse(formData["pageSize"].ToString());
                var fullname = formData.Keys.Contains("fullname") ? (formData["fullname"]).ToString().Trim() : "";
                var result = from t in db.Accounts
                             join n in db.Members on t.Memberid equals n.Memberid
                             select new User
                             {
                                 Role = t.Decentralization,
                                 MaNguoiDung = t.Memberid,
                                 TaiKhoan = t.Username,
                                 HoTen = n.Fullname,
                                 MatKhau = t.Password,
                                 DiaChi = n.Address,
                                 DienThoai = n.Phonenumber,
                                 Email = n.Email,
                                 Anh = n.Img,
                             };
                var kq = result.Where(x => x.HoTen.Contains(fullname)).OrderByDescending(x => x.MaNguoiDung).Skip(pageSize * (page - 1)).Take(pageSize).ToList();
                return Ok(
                         new ResponseListMessage
                         {
                             page = page,
                             totalItem = kq.Count,
                             pageSize = pageSize,
                             data = kq
                         });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }




        [Route("get-by-id/{id}")]
        [HttpGet]
        public IActionResult GetById(int? id)
        {
            var result = from t in db.Accounts
                         join n in db.Members on t.Memberid equals n.Memberid
                         select new User
                         {
                             Role = t.Decentralization,
                             MaNguoiDung = t.Memberid,
                             TaiKhoan = t.Username,
                             HoTen = n.Fullname,
                             MatKhau = t.Password,
                             DiaChi = n.Address,
                             DienThoai = n.Phonenumber,
                             Email = n.Email,
                             Anh = n.Img,
                         };
            var user = result.SingleOrDefault(x => x.MaNguoiDung == id);
            return Ok(new { user });
        }

        [Route("create-user")]
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserModel model)
        {
            db.Members.Add(model.member);
            db.SaveChanges();

            int Memberid = model.member.Memberid;
            model.account.Memberid = Memberid;
            db.Members.Add(model.member);
            db.Accounts.Add(model.account);
            db.SaveChanges();
            return Ok(new { data = "Thêm thành công!" });
        }


        [Route("update-user")]
        [HttpPost]
        public IActionResult UpdateUser([FromBody] UserModel model)
        {
            var obj_member = db.Members.SingleOrDefault(x => x.Memberid == model.member.Memberid);
            obj_member.Fullname = model.member.Fullname;
            obj_member.Address = model.member.Address;
            obj_member.Email = model.member.Email;
            obj_member.Birthday = model.member.Birthday;
            obj_member.Gender = model.member.Gender;
            obj_member.Phonenumber = model.member.Phonenumber;
            obj_member.Img = model.member.Img;
            obj_member.Isdeleted = model.member.Isdeleted;
            //....
            db.SaveChanges();

            var obj_account = db.Accounts.SingleOrDefault(x => x.Accountid == model.account.Accountid);
            obj_account.Username = model.account.Username;
            obj_account.Password = model.account.Password;
            //....
            db.SaveChanges();
            return Ok(new { data = "OK" });
        }

        [Route("delete-user/{id}")]
        [HttpDelete]
        public IActionResult DeleteUser(int? id)
        {
            var obj1 = db.Accounts.SingleOrDefault(s => s.Memberid == id);
            db.Accounts.Remove(obj1);
            db.SaveChanges();
            var obj2 = db.Members.SingleOrDefault(s => s.Memberid == id);
            db.Members.Remove(obj2);
            db.SaveChanges();
            return Ok(new { data = "Xóa thành công!" });
        }
    }
}
