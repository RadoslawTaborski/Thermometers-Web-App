using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCexp.Models;

namespace MVCexp.Controllers
{
    public class ExpController : Controller
    {
        public static StudentModel objstudentmodel = new StudentModel();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            objstudentmodel.DetailModel = new List<Detail>();
            objstudentmodel.DetailModel.Add(new Detail { RollNo = 1, Name = "abc", Marks = 1, Address = "222" });
            objstudentmodel.DetailModel.Add(new Detail { RollNo = 2, Name = "bcd", Marks = 1, Address = "333" });
            objstudentmodel.DetailModel.Add(new Detail { RollNo = 3, Name = "abc", Marks = 1, Address = "222" });
            objstudentmodel.DetailModel.Add(new Detail { RollNo = 4, Name = "bcd", Marks = 1, Address = "444" });
            return View(objstudentmodel);
        }
        [HttpPost]
        public ActionResult Index(bool aaa=true)
        {
            objstudentmodel.DetailModel.Add(new Detail { RollNo = 5, Name = "jh", Marks = 8, Address = "fgd" });
            return View(objstudentmodel);
        }

    }
}