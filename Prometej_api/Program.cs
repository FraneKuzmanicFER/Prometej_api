using Microsoft.EntityFrameworkCore;
using Prometej_core.DataAccessLayer;
using Prometej_core.Models.efModels;
using Prometej_core.Services.Contracts;
using Prometej_core.Services.Implementations;
using Prometej_persistance;
using Prometej_persistance.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("client-app",
           policyBuilder =>
           {
               policyBuilder.WithOrigins("http://localhost:5173")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
           }
             );
});

builder.Services.AddAutoMapper(typeof(UserService).Assembly);

#region Repo DI

builder.Services.AddTransient<IRepository<User>, Repository<User>>();

#endregion Repo DI

#region Service DI

builder.Services.AddScoped<IUserService, UserService>();

#endregion Service DI

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseCors("client-app");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
