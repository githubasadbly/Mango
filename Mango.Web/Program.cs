using Mango.Web.Service;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();
//----------change------------------
builder.Services.AddHttpContextAccessor();   //this is for cookies service store and retrive cookie
builder.Services.AddHttpClient();          //this is register Http Client service
builder.Services.AddHttpClient<ICouponService, CouponService>();  //this also register Httpclient with coupon service
builder.Services.AddHttpClient<IAuthService, AuthService>();      //this also register Httpclient with Auth service
builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddHttpClient<ICartService, CartService>();
builder.Services.AddHttpClient<IOrderService, OrderService>();

// appsettings se url aa rhe hai 
SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];
SD.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];
SD.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
SD.ShoppingCartAPIBase = builder.Configuration["ServiceUrls:ShoppingCartAPI"];
SD.OrderAPIBase = builder.Configuration["ServiceUrls:OrderAPI"];

builder.Services.AddScoped<IBaseService, BaseService>();    
builder.Services.AddScoped<ICouponService,CouponService>();
builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<ITokenProvider,TokenProvider>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan=TimeSpan.FromHours(10);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });
// ------------- -------------------



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
