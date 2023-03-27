using Microsoft.EntityFrameworkCore;
using TextEditor.Domain;
using TextEditor.Domain.Accounts.DomainServices;
using TextEditor.Domain.Accounts.DomainServices.Interfaces;
using TextEditor.Domain.Accounts.Repositories;
using TextEditor.Infrastructure.Datas;
using TextEditor.Infrastructure.Datas.Accounts;
using TextEditor.Infrastructure.Hash;
using TextEditor.SharedKernel.MD5;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TextEditorContext>(options => options.UseSqlServer(builder.Configuration
    .GetConnectionString("WebApiDatabase")));


builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IHash, MD5Util>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
