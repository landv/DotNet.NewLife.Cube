using DotNet.NewLife.Cube.Areas.School.Entity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using NewLife.Cube;
using NewLife.Log;

using NewLife.Web;

using System.ComponentModel;
using System.Reflection;
using System.Security.Claims;

using XCode.Membership;

namespace DotNet.NewLife.Cube.Areas.School.Controllers
{
    [SchoolArea]
    [DisplayName("班级")]
    public class ClassController : EntityController<Class, ClassModel>
    {
        private readonly ITracer _tracer;

        public ClassController(IServiceProvider provider)
        {

            PageSetting.EnableTableDoubleClick = true;

            _tracer = provider?.GetService<ITracer>();
        }

        protected override IEnumerable<Class> Search(Pager p)
        {
            using var span = _tracer?.NewSpan(nameof(Search), p);

            var id = p["Id"].ToInt(-1);
            if (id > 0)
            {
                var entity = Class.FindById(id);
                return entity == null ? new List<Class>() : new List<Class> { entity };
            }

            var start = p["dtStart"].ToDateTime();
            var end = p["dtEnd"].ToDateTime();

            return Class.Search(start, end, p["Q"], p);
        }
    }
}