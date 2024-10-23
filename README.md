# 魔方从零开始VS2022+NET8+MVC

* [魔方从零开始VS2022+NET8+MVC[202401]](https://newlifex.com/cube/cube_zero_start_vs2022_net8_mvc)
* [魔方从零开始VS2022+NET8+MVC[202402]](https://newlifex.com/cube/cube_zero_start_vs2022_net8_mvc_02)

新生命官网教程地址：[https://newlifex.com/cube/cube_zero_start_vs2022_net8_mvc](https://newlifex.com/cube/cube_zero_start_vs2022_net8_mvc)

本教程中的代码（拉取后直接运行即可）：[https://github.com/landv/DotNet.NewLife.Cube](https://github.com/landv/DotNet.NewLife.Cube)

## 初始化

+ 魔方源码库地址：[https://github.com/NewLifeX/NewLife.Cube](https://github.com/NewLifeX/NewLife.Cube)
+ XCode源码库地址： [https://github.com/NewLifeX/NewLife.XCode](https://github.com/NewLifeX/NewLife.XCode)  （需要使用xcodetool.exe 这个工具来动态生成代码以及自动建表）
+ IDE：VS 2022社区版（中文版） DotNet Core 版本为NET8

## 创建项目

+ <font style="color:rgb(64, 64, 64);">打开VS，依次点击工具栏文件按钮->新建->项目，选择ASP.NET Core Web 应用(模型-视图-控制器） </font>

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729581477968-4dd81864-dfe5-42c1-a918-c7750f35dbbe.png)

+ <font style="color:rgb(64, 64, 64);">填写自己的项目名称，如：DotNet.NewLife.Cube，点击下一步</font>

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729581583519-4a2e8b31-d0da-4c8c-b4b4-07f3987291aa.png)

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729581691576-846c618b-190c-4642-beaa-933156f9935e.png)

+ 项目结构如下

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729581778079-f7be6e24-4525-4ee6-b48f-810065d7d6ad.png)

## <font style="color:rgb(64, 64, 64);">安装并应用NewLife.Cube.Core</font>

+ <font style="color:rgb(64, 64, 64);">右键点击项目中依赖项->点击管理NuGet程序包->搜索框输入"</font>**<font style="color:rgb(64, 64, 64);">NewLife.Cube.Core</font>**<font style="color:rgb(64, 64, 64);">"，选中搜索结果，点击安装。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729584683639-2f7b430a-263f-4c4f-9b57-e2a248d9f08a.png)

###### 修改Program.cs

**注：**_使用皮肤需要在nuget下载安装对应的皮肤后，在后台系__<font style="background-color:#FBDE28;">统设置》魔方设置》界面设置》主体样式</font>__里面可以进行切换。_

_这里会默认进入Admin路由，默认创建的MVC框架的内容不会变，路由没有进行禁止。_

```csharp
using NewLife.Cube;
//using NewLife.Cube.WebMiddleware;
//using NewLife.Cube.AdminLTE;
//using NewLife.Cube.ElementUI;
//using NewLife.Cube.LayuiAdmin;
//using NewLife.Cube.Metronic;
//using NewLife.Cube.Tabler;
//using NewLife.Cube.ElementUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// 引入星尘，设置监控中间件
//var star = builder.Services.AddStardust(null);
//TracerMiddleware.Tracer = star?.Tracer;

// 启用接口响应压缩
builder.Services.AddResponseCompression();
builder.Services.AddControllersWithViews();
// 启用魔方
builder.Services.AddCube();

var app = builder.Build();

// 加载魔方UI
//app.UseAdminLTE(app.Environment);
//app.UseTabler(app.Environment);
//app.UseMetronic(app.Environment);
//app.UseElementUI(app.Environment);
//app.UseMetronic8(app.Environment);
//app.UseLayuiAdmin(app.Environment);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// 使用魔方
app.UseCube(app.Environment);
// 配置魔方为首页，也就是默认打开魔方登录后台界面
app.UseCubeHome();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
```

## <font style="color:rgb(64, 64, 64);">添加自己的页面</font>

### <font style="color:rgb(64, 64, 64);">新建区域</font>

+ <font style="color:rgb(64, 64, 64);">首先新建区域</font><font style="color:rgb(130, 0, 20);background-color:rgb(255, 232, 230);">School</font><font style="color:rgb(64, 64, 64);">，手动创建文件夹</font><font style="color:rgb(130, 0, 20);background-color:rgb(255, 232, 230);">Areas/School</font><font style="color:rgb(64, 64, 64);">，在此文件夹创建区域类</font><font style="color:rgb(130, 0, 20);background-color:rgb(255, 232, 230);">SchoolArea</font><font style="color:rgb(64, 64, 64);">，填入以下内容。</font>

```csharp
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
```

### <font style="color:rgb(64, 64, 64);">新建实体</font>

<font style="color:rgb(64, 64, 64);">新建数据库实体类。新建文件夹</font><font style="background-color:#FBDE28;">Areas/School/Models</font><font style="color:rgb(64, 64, 64);">，将实体类放在此文件夹。</font>

<font style="color:rgb(64, 64, 64);">使用</font><font style="color:rgb(64, 64, 64);background-color:#FBDE28;">XCode</font>

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729583732849-b3d02572-a25f-43d3-b2ae-e135fbb5a100.png)

在终端中打开

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729583766548-b77b24f3-3ef2-4662-9cd6-74e18f08f995.png)

运行<font style="background-color:#FBDE28;">xcode</font> 会生成一个<font style="background-color:#FBDE28;">Model.xml</font>示例

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729583931290-95e98af4-f0c0-424c-b4b5-610733183aa5.png)

修改Model.xml 完毕后再次运行<font style="background-color:#FBDE28;">xcode</font>就可以自动生成代码

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729584224037-87ac4e40-bcc8-4a57-b5c7-d4ab13b5abad.png)

Model.xml 修改后的内容

```xml
<?xml version="1.0" encoding="utf-8"?>
<EntityModel xmlns:xs="http://www.w3.org/2001/XMLSchema-instance" xs:schemaLocation="https://newlifex.com https://newlifex.com/Model202407.xsd" Document="https://newlifex.com/xcode/model" xmlns="https://newlifex.com/Model202407.xsd">
  <Option>
    <!--类名模板。其中{name}替换为Table.Name，如{name}Model/I{name}Dto等-->
    <ClassNameTemplate />
    <!--显示名模板。其中{displayName}替换为Table.DisplayName-->
    <DisplayNameTemplate />
    <!--基类。可能包含基类和接口，其中{name}替换为Table.Name-->
    <BaseClass>Entity</BaseClass>
    <!--命名空间-->
    <Namespace>peipei.Areas.School.Entity</Namespace>
    <!--输出目录-->
    <Output>Entity</Output>
    <!--是否使用中文文件名。默认false-->
    <ChineseFileName>True</ChineseFileName>
    <!--用于生成Copy函数的参数类型。例如{name}或I{name}-->
    <ModelNameForCopy />
    <!--带有索引器。实现IModel接口-->
    <HasIModel>True</HasIModel>
    <!--可为null上下文。生成String?等-->
    <Nullable>True</Nullable>
    <!--数据库连接名-->
    <ConnName>School</ConnName>
    <!--模型类模版。设置后生成模型类，用于接口数据传输，例如{name}Model-->
    <ModelClass>{name}Model</ModelClass>
    <!--模型类输出目录。默认当前目录的Models子目录-->
    <ModelsOutput>.\Models\</ModelsOutput>
    <!--模型接口模版。设置后生成模型接口，用于约束模型类和实体类，例如I{name}-->
    <ModelInterface>I{name}</ModelInterface>
    <!--模型接口输出目录。默认当前目录的Interfaces子目录-->
    <InterfacesOutput>.\Interfaces\</InterfacesOutput>
    <!--用户实体转为模型类的模型类。例如{name}或{name}DTO-->
    <ModelNameForToModel />
    <!--命名格式。Default/Upper/Lower/Underline-->
    <NameFormat>Default</NameFormat>
    <!--魔方区域显示名-->
    <DisplayName />
    <!--魔方控制器输出目录-->
    <CubeOutput />
  </Option>
  <Tables>
    <Table Name="Class" Description="班级" DbType="SqlServer">
      <Columns>
        <Column Name="Id" DataType="Int32" Identity="True" PrimaryKey="True" Description="编号" />
        <Column Name="TenantId" DataType="Int32" Map="XCode.Membership.Tenant@Id@Name@TenantName" Description="租户" />
        <Column Name="Name" DataType="String" Master="True" Description="名称" />
        <Column Name="Enable" DataType="Boolean" Description="启用" />
        <Column Name="GraduationDate" DataType="DateTime" Description="毕业时间" />
        <Column Name="Model" DataType="String" Length="20" Description="设备型号" />
        <Column Name="CreateUserID" DataType="Int32" Description="创建者" />
        <Column Name="CreateTime" DataType="DateTime" Description="创建时间" />
        <Column Name="CreateIP" DataType="String" Description="创建地址" />
        <Column Name="UpdateUserID" DataType="Int32" Description="更新者" />
        <Column Name="UpdateTime" DataType="DateTime" Description="更新时间" />
        <Column Name="UpdateIP" DataType="String" Description="更新地址" />
        <Column Name="Remark" DataType="String" Length="200" Description="备注" />
      </Columns>
    </Table>
    <Table Name="Student" Description="学生" DbType="SqlServer">
      <Columns>
        <Column Name="Id" DataType="Int32" Identity="True" PrimaryKey="True" Description="编号" />
        <Column Name="TenantId" DataType="Int32" Map="XCode.Membership.Tenant@Id@Name@TenantName" Description="租户" />
        <Column Name="ClassId" DataType="Int32" Map="Class@Id@$" Description="班级" Category="基本信息" />
        <Column Name="Name" DataType="String" Master="True" Description="名称" Category="基本信息" />
        <Column Name="Sex" DataType="Int32" Description="性别" Type="XCode.Membership.SexKinds" Category="基本信息" />
        <Column Name="Age" DataType="Int32" Description="年龄" Category="基本信息" />
        <Column Name="Mobile" DataType="String" Description="手机" Category="基本信息" />
        <Column Name="Address" DataType="String" Description="地址" Category="基本信息" />
        <Column Name="Enable" DataType="Boolean" Description="启用" Category="基本信息" />
        <Column Name="Avatar" DataType="String" ItemType="image" Description="头像" Category="基本信息" />
        <Column Name="Weight" DataType="Double" Scale="2" Description="体重。小数" />
        <Column Name="Amount" DataType="Decimal" Scale="3" Description="存款。小数" />
        <Column Name="CreateUserID" DataType="Int32" Description="创建者" Category="扩展信息" />
        <Column Name="CreateTime" DataType="DateTime" Description="创建时间" Category="扩展信息" />
        <Column Name="CreateIP" DataType="String" Description="创建地址" Category="扩展信息" />
        <Column Name="UpdateUserID" DataType="Int32" Description="更新者" Category="扩展信息" />
        <Column Name="UpdateTime" DataType="DateTime" Description="更新时间" Category="扩展信息" />
        <Column Name="UpdateIP" DataType="String" Description="更新地址" Category="扩展信息" />
        <Column Name="Remark" DataType="String" Length="200" Description="备注" Category="扩展信息" />
      </Columns>
      <Indexes>
        <Index Columns="TenantId,ClassId" />
        <Index Columns="ClassId" />
      </Indexes>
    </Table>
    <Table Name="Trade" Description="交易" ConnName="Bill" DbType="SqlServer">
      <Columns>
        <Column Name="Id" DataType="Int64" Identity="True" PrimaryKey="True" Description="订单编号" />
        <Column Name="TenantId" DataType="Int32" Map="XCode.Membership.Tenant@Id@Name@TenantName" Description="租户" />
        <Column Name="NodeId" DataType="Int32" Description="节点号" />
        <Column Name="Tid" DataType="String" Description="订单号" />
        <Column Name="Status" DataType="Int32" Description="状态" />
        <Column Name="PayStatus" DataType="Int32" Description="是否支付" />
        <Column Name="ShipStatus" DataType="Int32" Description="是否发货" />
        <Column Name="CreateIPReceiverPhone" DataType="String" Description="收货人电话" />
        <Column Name="ReceiverMobile" DataType="String" Description="收货人手机号" />
        <Column Name="ReceiverState" DataType="String" Description="收货省" />
        <Column Name="ReceiverCity" DataType="String" Description="收货人区" />
        <Column Name="ReceiverDistrict" ColumnName="Receiver_District" DataType="String" Description="收货区" />
        <Column Name="ReceiverAddress" DataType="String" Description="收货地址" />
        <Column Name="BuyerName" DataType="String" Description="买家昵称" />
        <Column Name="Created" DataType="Int32" Description="创建时间" />
        <Column Name="Modified" DataType="Int32" Description="是否发送过" />
        <Column Name="IsSend" DataType="Int32" Description="更新者" />
        <Column Name="ErrorMsg" DataType="String" Length="200" Description="错误原因" />
      </Columns>
      <Indexes>
        <Index Columns="TenantId,NodeId" />
        <Index Columns="NodeId" />
      </Indexes>
    </Table>
  </Tables>
</EntityModel>
```

### <font style="color:rgb(64, 64, 64);">新建控制器</font>

+ <font style="color:rgb(64, 64, 64);">新建文件夹</font><font style="color:rgb(130, 0, 20);background-color:rgb(255, 232, 230);">Areas/School/Controllers</font><font style="color:rgb(64, 64, 64);">，新建ClassController、StudentController两个控制器，分别填入以下内容。</font>

```csharp
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
```

```csharp
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
```

### <font style="color:rgb(64, 64, 64);">页面修改</font>

+ <font style="color:rgb(64, 64, 64);">此时运行，一切正常，可看到如下页面。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729586376895-bd18e6f6-e533-44de-99db-b4dc975ba17a.png)

+ <font style="color:rgb(64, 64, 64);">比如修改学生主页，则新建文件夹</font><font style="color:rgb(130, 0, 20);background-color:rgb(255, 232, 230);">Areas/School/Views/Student/</font><font style="color:rgb(64, 64, 64);">，新建文件</font><font style="color:rgb(130, 0, 20);background-color:rgb(255, 232, 230);">_List_Data.cshtml</font><font style="color:rgb(64, 64, 64);">。填入以下内容。</font>

```csharp
@using NewLife;
@using NewLife.Cube
@using NewLife.Web;
@using XCode;
@using XCode.Configuration;
@using XCode.Membership
@{
    var fact = ViewBag.Factory as IEntityFactory;
    var page = ViewBag.Page as Pager;
    var fields = ViewBag.Fields as List<FieldItem>;
}
<table class="table table-bordered table-hover table-striped table-condensed">
    <thead>
        <tr>
            <th class="text-center hidden-md hidden-sm hidden-xs"><a href="@Html.Raw(page.GetSortUrl("ID"))">编号</a></th>
            <th class="text-center"><a href="@Html.Raw(page.GetSortUrl("ClassID"))">班级2</a></th>
            <th class="text-center"><a href="@Html.Raw(page.GetSortUrl("Name"))">名称3</a></th>
            <th class="text-center"><a href="@Html.Raw(page.GetSortUrl("Sex"))">性别4</a></th>
            <th class="text-center"><a href="@Html.Raw(page.GetSortUrl("Age"))">年龄5</a></th>
            <th class="text-center"><a href="@Html.Raw(page.GetSortUrl("Mobile"))">手机</a></th>
            <th class="text-center"><a href="@Html.Raw(page.GetSortUrl("Address"))">地址</a></th>
            <th class="text-center"><a href="@Html.Raw(page.GetSortUrl("CreateTime"))">创建时间</a></th>
            <th class="text-center"><a href="@Html.Raw(page.GetSortUrl("CreateIP"))">创建地址</a></th>
            <th class="text-center"><a href="@Html.Raw(page.GetSortUrl("UpdateTime"))">更新时间</a></th>
            <th class="text-center"><a href="@Html.Raw(page.GetSortUrl("UpdateIP"))">更新地址</a></th>
            <th class="text-center"><a href="@Html.Raw(page.GetSortUrl("Remark"))">备注</a></th>
            @if (this.Has(PermissionFlags.Detail, PermissionFlags.Update, PermissionFlags.Delete))
            {
                <th class="text-center">操作</th>
            }
        </tr>
    </thead>
    <tbody>
        @foreach (var entity in Model)
        {
            <tr>
                <td class="text-center hidden-md hidden-sm hidden-xs">@entity.ID</td>
                <td>@entity.ClassName</td>
                <td>@entity.Name</td>
                <td class="text-center">@entity.Sex</td>
                <td class="text-right">@entity.Age.ToString("n0")</td>
                <td>@entity.Mobile</td>
                <td>@entity.Address</td>
                <td>@Utility.ToFullString(entity.CreateTime, "")</td>
                <td>@entity.CreateIP</td>
                <td>@Utility.ToFullString(entity.UpdateTime, "")</td>
                <td>@entity.UpdateIP</td>
                <td>@entity.Remark</td>
                @if (this.Has(PermissionFlags.Detail, PermissionFlags.Update, PermissionFlags.Delete))
                {
                    <td class="text-center">
                        @await Html.PartialAsync("_List_Data_Action", (Object)entity)
                    </td>
                }
            </tr>
        }
    </tbody>
</table>
```

## XCode工具编译和使用

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729580927549-cf7d221a-c4ba-4553-a7aa-65ddef0611d6.png)

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729580952882-b7749255-9aea-46ca-9798-4ed90ec108b7.png)

双击xcodetool.exe 理论上会自动注册到dot net tool里面

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729581031713-e983688b-8b08-4a5a-a47d-486a375e69cd.png)

```bash
Execute(dotnet tool list -g)
Execute(dotnet tool install xcodetool -g)
```

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729581055757-abf25228-2d09-4de7-9acd-f99cfdc8eee7.png)

使用xcode命令即可调用。第一次执行会下载一个model.xml示例，修改完毕后。再次执行会生成代码。

**注意：**_有的时候电脑会间歇性抽风，emm这是一个很蛋疼的问题，虽然重启会好。这里再提供一个解决方法将XCodeTool.exe 所在目录添加到系统path环境里面即可。_

## 关于数据库相关

### 关于数据库连接库

如果框架没有自动拉取到数据库依赖DLL可以使用nuget的方式：

会出现这样的情况

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729580695566-465e9364-e8d2-4b45-9ade-38b06f7cda4f.png)

在<font style="background-color:#FBDE28;">nuget </font>上搜索 <font style="background-color:#FBDE28;">Xcode.   </font> 进行手动安装

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729580616452-e484c406-087c-42d0-a653-3f591dd035e2.png)

### 关于数据库字段

###### appsettings.json相关

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Urls": "http://*:8080;https://*:8081",
  "StarServer": "http://star.newlifex.com:6600",
  "ConnectionStrings": {
    "Membership": "Data Source=192.168.1.106;Initial Catalog=peipei;user=sa;password=landv;provider=mssql",
    "School": "MapTo=Membership",
    "Bill": "MapTo=Membership",
    "Log": "MapTo=Membership"
  }
}
```

其中<font style="background-color:#FBDE28;">MapTo=Membership</font>  为等同于Membership 的配置。

Membership和Log 是框架需要的，如果这里不进行配置数据库连接，就会自动创建sqlite。

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729580156368-ac6be6f3-61a6-45e1-b779-17abf0cae973.png)

这里有多少个，就需要配置多个连接，如果不配置就自动创建为sqlite。

下面是默认sqlite配置字符串

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729587607691-0ed4fd3a-9a9f-4b78-a051-3e59a3071a08.png)

###### 数据库连接字符串示例

```json
"ConnectionStrings": {
    "mssql": "Data Source=192.168.1.106;Initial Catalog=peipei;user=sa;password=landv;provider=mssql",
    "mssql2": "Data Source=.;Initial Catalog=master;Integrated Security=True;provider=mssql",
    "Oracle": "Data Source=Tcp://127.0.0.1/ORCL;User Id=scott;Password=tiger;provider=oracle",
    "Oracle2": "Data Source=orc;User ID=admin;Password=admin;provider=oracle",
    "sqlite": "Data Source=test.db;provider=sqlite",
    "MySql": "Server=127.0.0.1;Port=3306;Database=mysql;Uid=root;Pwd=root;provider=mysql",
    "PostgreSQL": "Server=.;Database=master;Uid=root;Pwd=root;provider=PostgreSQL",
    "Membership": "Data Source=Membership.db;provider=sqlite"
  }
```

+ [魔方从零开始VS2022+NET8+MVC[202401]](https://newlifex.com/cube/cube_zero_start_vs2022_net8_mvc)

# 魔方从零开始VS2022+NET8+MVC[202402]

## 自定义页面

### 替换框架页面

#### **<font style="color:rgb(38, 38, 38);">自定义启动页</font>**

魔方启动打开的第一个页面，这个页面会一闪而过。

+ <font style="color:rgb(38, 38, 38);">可能有人看着不爽，那么自定义有两种方法。第一是视图覆盖，第二是默认路由替换。</font>

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729643755457-f99d1035-ae52-44eb-811e-8cb7ad71b83a.png)

##### <font style="color:rgb(51, 51, 51);">视图覆盖</font>

+ <font style="color:rgb(38, 38, 38);">视图位于</font><font style="color:rgb(130, 0, 20);background-color:rgb(255, 232, 230);">Views/CubeHome/Index.cshtml</font><font style="color:rgb(38, 38, 38);">，换成自己的页面即可。</font>

修改后的效果如下：

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729644150445-f622f929-e0ef-4419-a281-537220fa1a82.png)

框架页面源码

[https://github.com/NewLifeX/NewLife.Cube/tree/master/NewLife.CubeNC/Views](https://github.com/NewLifeX/NewLife.Cube/tree/master/NewLife.CubeNC/Views)

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729644427630-e05096ca-9475-4c90-acf1-4e3cea4d2e13.png)

比如现在要修改你们讨厌的启动页面

下面是源文件

[https://raw.githubusercontent.com/NewLifeX/NewLife.Cube/refs/heads/master/NewLife.CubeNC/Views/CubeHome/Index.cshtml](https://raw.githubusercontent.com/NewLifeX/NewLife.Cube/refs/heads/master/NewLife.CubeNC/Views/CubeHome/Index.cshtml)

[https://github.com/NewLifeX/NewLife.Cube/blob/master/NewLife.CubeNC/Views/CubeHome/Index.cshtml](https://github.com/NewLifeX/NewLife.Cube/blob/master/NewLife.CubeNC/Views/CubeHome/Index.cshtml)

这个路径是关键点。

<font style="background-color:#FBDE28;">Views/CubeHome/Index.cshtml</font>

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729644610840-caebe89b-2bac-4c86-8f5d-3c03a22ca7d8.png)

按照这个路径创建文件,并在<font style="background-color:#FBDE28;">CubeHome</font>文件夹上右键，添加视图。

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729644702356-db8d4d60-cb7d-47d1-ad13-cf6c36021b9f.png)

添加Razor视图-空

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729644785452-75587f89-41fb-46f7-9428-5d3ce3b3806a.png)

默认Index.cshtml名称即可

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729644804828-38476220-c132-44da-a9fb-f55e44d79c86.png)

然后从原始文件位置复制粘贴到此处。[https://github.com/NewLifeX/NewLife.Cube/blob/master/NewLife.CubeNC/Views/CubeHome/Index.cshtml](https://github.com/NewLifeX/NewLife.Cube/blob/master/NewLife.CubeNC/Views/CubeHome/Index.cshtml)

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729644875728-98250952-3d5b-48c4-bba4-2ec7eb4d5b7c.png)

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729644914222-24d7e614-4211-4a3c-9379-035204ce9ca4.png)

```csharp
@using NewLife;
@using NewLife.Common;
@using NewLife.Web
@{
    Layout = null;
    var page = "/";
    page = page.EnsureEnd("/") + "Admin";
}

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>@SysConfig.Current.DisplayName</title>
    <meta http-equiv="refresh" content="0;url=@(page)">
</head>
<body>
    <div>
        <h1>@SysConfig.Current.DisplayName</h1>
        <h3>xxxxxxxxxxxxxxxxxxxxxx正在启动系统……</h3>
        @SysConfig.Current.Company
    </div>
</body>
@await Html.PartialAsync("_Footer")
    </html>
```

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729644935699-4946aa3a-ba43-4ac7-a2c8-3edfd585ca91.png)

需要清理解决方案，重新运行即可看到（会一闪而过）

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729644150445-f622f929-e0ef-4419-a281-537220fa1a82.png?x-oss-process=image%2Fformat%2Cwebp)

**注：**_其他页面修改方法雷同_

#### 自定义登录页面

与自定义启动页面类似

[https://github.com/NewLifeX/NewLife.Cube/blob/master/NewLife.CubeNC/Areas/Admin/Views/User/Login.cshtml](https://github.com/NewLifeX/NewLife.Cube/blob/master/NewLife.CubeNC/Areas/Admin/Views/User/Login.cshtml)

[https://github.com/NewLifeX/NewLife.Cube/blob/master/NewLife.CubeNC/Areas/Admin/Views/User/Login.cshtml](https://github.com/NewLifeX/NewLife.Cube/blob/master/NewLife.CubeNC/Areas/Admin/Views/User/Login.cshtml)

看上面的路径<font style="background-color:#FBDE28;">/Areas/Admin/Views/User/Login.cshtml </font> 一样在咱们自己的项目里面创建这个路径和文件。

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729645659299-5fadcf73-d634-457f-acaa-4649eda73ce1.png)

注意差异点

![](https://cdn.nlark.com/yuque/0/2024/png/638116/1729645807448-7577fc26-0222-4f01-8e8e-9da8989db39e.png)

```csharp
@using NewLife.Common;
@{
    Layout = null;
    ViewBag.Title = "登录";
}
<!DOCTYPE html>
<html lang="zh-CN">
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- 上述3个meta标签*必须*放在最前面，任何其他内容都*必须*跟随其后！ -->

    <title>@ViewBag.Title - @SysConfig.Current.DisplayName</title>
    <link href="https://cdn.bootcdn.net/ajax/libs/element-ui/2.13.2/theme-chalk/index.css" rel="stylesheet">
    <!-- Login -->
    <style scoped>
        .login-logo {
            text-align: center;
            font-size: 130px;
            color: #4ca6ff;
            margin-top: 50px;
        }

        .cube-login {
            background: #fff;
            padding-bottom: 0;
            border-radius: 15px;
            text-align: center;
        }

            .cube-login .heading {
                display: block;
                font-size: 24px;
                font-weight: 700;
                padding: 5px 0;
                margin-bottom: 20px;
                text-align: center;
            }

            .cube-login input {
                border-radius: 20px;
                box-shadow: none;
                padding: 0 20px 0 45px;
                height: 40px;
                transition: all 0.3s ease 0s;
            }

            .cube-login .text {
                float: left;
                margin-left: 7px;
                line-height: 20px;
                padding-top: 5px;
                text-transform: capitalize;
            }

            .cube-login a {
                position: absolute;
                top: 12px;
                right: 0px;
                font-size: 17px;
                color: #c8c8c8;
                transition: all 0.5s ease 0s;
                color: #4ca6ff;
            }

        .btn {
            float: right;
            font-size: 14px;
            color: #fff;
            background: #00b4ef;
            /* border-radius: 30px; */
            border-radius: 4px;
            padding: 8px 50px;
            border: none;
            text-transform: capitalize;
            transition: all 0.5s ease 0s;
            margin: -25px 0 15px 0;
            width: 100%;
        }

        .text-primary {
            color: #337ab7;
        }

        label {
            display: inline-block;
            max-width: 100%;
            margin-bottom: 5px;
            font-weight: 700;
        }
    </style>
    <!-- Login3 -->
    <style scoped>
        .text-center {
            text-align: center;
        }

        p.login3 {
            font-size: 22px;
            position: relative;
            width: 100%;
            color: #333;
        }

            p.login3 span {
                height: 1px;
                position: absolute;
                background-color: #928f8f;
                width: 35%;
                top: 50%;
            }

                p.login3 span.right {
                    right: 65%;
                }

                p.login3 span.left {
                    left: 65%;
                }
    </style>
</head>
<body>
    <div id="app">
        <el-row type="flex"
                justify="center">
            <el-col :span="6">
                <!-- Login -->
                <div>
                    <!-- Logo-->
                    <el-row>
                        <el-col :span="24"
                                class="login-logo">
                            <i class="el-icon-cloudy"></i>
                        </el-col>
                    </el-row>
                    <template v-if="setting.AllowLogin">
                        <el-form :model="loginForm"
                                 class="cube-login">
                            <!-- 登录-->
                            <span class="heading text-primary">{{ sysConfig.DisplayName }} 登录</span>
                            <el-form-item label="">
                                <el-input v-model="loginForm.username"
                                          placeholder="用户名 / 邮箱"
                                          prefix-icon="el-icon-user"
                                          class="form-control">
                                </el-input>
                            </el-form-item>

                            <el-form-item label="">
                                <el-input v-model="loginForm.password"
                                          placeholder="密码"
                                          prefix-icon="el-icon-lock"
                                          class="form-control"
                                          show-password>
                                </el-input>
                            </el-form-item>
                            <el-form-item label="">
                                <el-checkbox class="text text-primary"
                                             v-model="loginForm.remember">记住我</el-checkbox>

                                <template v-if="setting.AllowRegister">
                                    <div style="display: inline-block; margin-top: 5px; float: right;">
                                        <a href="#Register"
                                           data-toggle="tab"
                                           style="margin-left: auto; margin-right: auto; position: static; font-size: 15px; margin-top: 5px;">
                                            <span>我要注册</span>
                                        </a>
                                    </div>
                                </template>
                            </el-form-item>
                        </el-form>

                        <button class="btn"
                                @@click="login">
                            登录
                        </button>
                    </template>
                </div>
                <!-- Login3 -->
                <div v-if="setting.AutoRegister && ms.length > 0">
                    <el-row>
                        <el-col :span="24" class="text-center">
                            <p class="login3">
                                <span class="left"></span>
                                第三方登录
                                <span class="right"></span>
                            </p>
                            <el-row>
                                <el-col :sm="24">
                                    <template v-for="(mi, i) in ms">
                                        <a :key="i" :href="getUrl(mi)">
                                            {{ mi.Name }}
                                        </a>
                                    </template>
                                </el-col>
                            </el-row>
                        </el-col>
                    </el-row>
                </div>
            </el-col>
        </el-row>
    </div>
    <script src="https://cdn.bootcdn.net/ajax/libs/vue/2.6.11/vue.min.js"></script>
    <script src="https://cdn.bootcdn.net/ajax/libs/element-ui/2.13.2/index.js"></script>
    <script src="https://cdn.bootcdn.net/ajax/libs/axios/0.19.2/axios.min.js"></script>
    <script>
        new Vue({
            el: '#app',
            data() {
                return {
                    loginForm: {
                        username: null,
                        password: null,
                        remember: true
                    },
                    sysConfig: {
                        DisplayName: "魔方xxxxx平台"
                    },
                    setting: {
                        AllowLogin: true,
                        AllowRegister: true,
                        AutoRegister: true
                    },
                    ms: [
                        {
                            Name: "NewLife"
                        }
                    ],
                    dic: {
                        NewLife: "新生命",
                        Baidu: "百度",
                        Weixin: "微信",
                        Taobao: "淘宝",
                        Ding: "钉钉"
                    },
                    returnUrl: null
                };
            },
            computed: {
                request() {
                    const service = axios.create({
                        timeout: 50000
                    });

                    // 响应拦截
                    service.interceptors.response.use(
                        response => {
                            const data = response.data;
                            if (data.code === 500) {
                                alert(data.message);
                                return Promise.reject(data.message);
                            }
                            return data;
                        },
                        error => {
                            console.log('err' + error)
                            return Promise.reject(error);
                        }
                    );

                    return service;
                }
            },
            methods: {
                login() {
                    let vm = this;
                    vm.loginByUsernameAsync(vm.loginForm)
                        .then(() => {
                            let href = "/Admin";
                            let r = vm.getQueryString("r");
                            if (r != null) {
                                href = r;
                            }
                            location.href = href;
                        })
                        .catch(() => { });
                },
                getUrl(mi) {
                    let vm = this;
                    var url = "/Sso/Login?name=" + mi.Name;
                    if (vm.returnUrl != null) {
                        url += "&r=" + vm.returnUrl;
                    }
                    return url;
                },
                getName(mi) {
                    let vm = this;
                    let nickName = vm.dic[mi.Name];
                    if (nickName == null) {
                        nickName = mi.Name;
                    }
                    return nickName;
                },

                loginByUsernameAsync(userInfo) {
                    let vm = this;
                    const data = {
                        username: userInfo.username,
                        password: userInfo.password,
                        remember: userInfo.remember
                    };

                    return vm.request({
                        url: "/Admin/User/Login",
                        method: "post",
                        params: data
                    });
                },
                getQueryString(name) {
                    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                    var r = window.location.search.substr(1).match(reg);
                    if (r != null) return unescape(r[2]); return null;
                }
            }
        })
    </script>
</body>
</html>
```



