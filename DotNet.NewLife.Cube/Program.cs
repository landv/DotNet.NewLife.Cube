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
// �����ǳ������ü���м��
//var star = builder.Services.AddStardust(null);
//TracerMiddleware.Tracer = star?.Tracer;

// ���ýӿ���Ӧѹ��
builder.Services.AddResponseCompression();
builder.Services.AddControllersWithViews();
// ����ħ��
builder.Services.AddCube();

var app = builder.Build();

// ����ħ��UI
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

// ʹ��ħ��
app.UseCube(app.Environment);
//// ʹ��ħ����ҳ
app.UseCubeHome();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();