using Microsoft.AspNetCore.Mvc;
using NewLife.Cube.ViewModels;
using NewLife.Cube;
using System.ComponentModel;
using System.Reflection;
using XCode.Membership;
using NewLife.Web;
using DotNet.NewLife.Cube.Areas.School.Entity;

namespace DotNet.NewLife.Cube.Areas.School.Controllers
{
    [SchoolArea]
    [DisplayName("学生")]
    public class StudentController : EntityController<Student, StudentModel>
    {
        static StudentController()
        {
            ListFields.RemoveField("CreateUserID");
            ListFields.RemoveField("UpdateUserID");
            //FormFields
        }

        protected override Student Find(Object key)
        {
            return base.Find(key);
        }

        protected override IEnumerable<Student> Search(Pager p)
        {
            return base.Search(p);
            //var classid = p["classid"].ToInt();
            //return Student.Search(null,p);
        }

        public override ActionResult Index(Pager p = null)
        {
            return base.Index(p);
        }
    }
}