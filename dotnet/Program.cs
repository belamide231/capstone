// dotnet add package dotenv.net
// dotnet add package MongoDB.Driver
// dotnet add package AspNetCore.Identity.Mongo
// dotnet add package StackExchange.Redis
// dotnet add package Newtonsoft.Json
// dotnet add package MailKit
// dotnet add package BCrypt.Net-Next


using AspNetCore.Identity.Mongo;
using MongoDB.Bson;


new EnvHelper();
new MailHelper();


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls(EnvHelper._ServerUrl!);


builder.Services.AddSingleton<Mongo>();
builder.Services.AddSingleton<Redis>();
builder.Services.AddTransient<UserServices>();
builder.Services.AddCors(option => {
    option.AddPolicy("*", policy => {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});
builder.Services.AddAuthorizationBuilder();
builder.Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, ObjectId>(option => {
                    option.ConnectionString = EnvHelper._MongoUrl;
                    option.UsersCollection = Mongo._applicationUsers;
                    option.RolesCollection = Mongo._applicationRoles;
                });
builder.Services.AddControllersWithViews();


var app = builder.Build();


if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseCors("*");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
