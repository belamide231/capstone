// dotnet add package dotenv.net
// dotnet add package MongoDB.Driver
// dotnet add package AspNetCore.Identity.Mongo
// dotnet add package StackExchange.Redis
// dotnet add package Newtonsoft.Json
// dotnet add package MailKit
// dotnet add package BCrypt.Net-Next
// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer


using System.Text;
using AspNetCore.Identity.Mongo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;


new EnvHelper();
new MailHelper();


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls(EnvHelper._ServerUrl!);


builder.Services.AddScoped<CreateAdmin>();
builder.Services.AddSingleton<Mongo>();
builder.Services.AddSingleton<Redis>();
builder.Services.AddTransient<UserServices>();
builder.Services.AddTransient<UsersServices>();
builder.Services.AddTransient<DepartmentServices>();
builder.Services.AddTransient<PostServices>();
builder.Services.AddTransient<IAuthorizationHandler, AdminHandler>();
builder.Services.AddTransient<IAuthorizationHandler, AdminOrDeanHandler>();
builder.Services.AddTransient<IAuthorizationHandler, AdminOrDeanOrTeacherHandler>();
builder.Services.AddTransient<IAuthorizationHandler, DeanHandler>();
builder.Services.AddTransient<IAuthorizationHandler, TeacherHandler>();
builder.Services.AddTransient<IAuthorizationHandler, StudentHandler>();
builder.Services.AddTransient<IAuthorizationHandler, UserHandler>();
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
                    option.UsersCollection = Mongo._ApplicationUsers;
                    option.RolesCollection = Mongo._ApplicationRoles;
                })
                .AddApiEndpoints()
                .AddDefaultTokenProviders();
builder.Services.AddScoped<UserManager<ApplicationUser>>();
builder.Services.AddAuthentication(option => {
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option => {
    option.TokenValidationParameters = new TokenValidationParameters {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EnvHelper._JwtKey!)),
        ClockSkew = TimeSpan.Zero
    };
}).AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorization(option => {
    option.AddPolicy(AdminHandler._Policy, policy => policy.AddRequirements(new AdminRequirement()));
    option.AddPolicy(AdminOrDeanHandler._Policy, policy => policy.AddRequirements(new AdminOrDeanRequirement()));
    option.AddPolicy(AdminOrDeanOrTeacherHandler._Policy, policy => policy.AddRequirements(new AdminOrDeanOrTeacherRequirement()));
    option.AddPolicy(DeanHandler._Policy, policy => policy.AddRequirements(new DeanRequirement()));
    option.AddPolicy(TeacherHandler._Policy, policy => policy.AddRequirements(new TeacherRequirement()));
    option.AddPolicy(StudentHandler._Policy, policy => policy.AddRequirements(new StudentRequirement()));
    option.AddPolicy(UserHandler._Policy, policy => policy.AddRequirements(new UserRequirement()));
});
builder.Services.AddControllersWithViews();


var app = builder.Build();


using (var scope = app.Services.CreateScope()) {
    var admin = scope.ServiceProvider.GetRequiredService<CreateAdmin>();
    await admin.Create();
}


if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseCors("*");
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions {
    FileProvider = new PhysicalFileProvider(UserInterfaceController._WebDirectory),
    RequestPath = ""
});
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserInterface}/{action=Render}/{WebPath?}");
app.UseWebSockets();
app.UseMiddleware<Messenger>();


app.Run();