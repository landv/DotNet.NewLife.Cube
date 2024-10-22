using NewLife.Cube;
using NewLife;
using System.ComponentModel;

namespace DotNet.NewLife.Cube.Areas.School
{
    [DisplayName("教务系统")]
    public class SchoolArea : AreaBase
    {
        public SchoolArea() : base(nameof(SchoolArea).TrimEnd("Area")) { }

        static SchoolArea() => RegisterArea<SchoolArea>();
    }
}