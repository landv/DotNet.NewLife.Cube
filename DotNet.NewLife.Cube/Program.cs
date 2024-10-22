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
//// 使用魔方首页
app.UseCubeHome();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();