using EduHub.Application.Clubs;
using EduHub.Domain.Interfaces;
using EduHub.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ── Đăng ký services (Dependency Injection) ──────────────────────────────
builder.Services.AddScoped<IClubRepository, InMemoryClubRepository>();
builder.Services.AddScoped<IMemberRepository, InMemoryMemberRepository>();

builder.Services.AddScoped<GetClubByIdQueryHandler>();
builder.Services.AddScoped<GetClubByIdQueryHandler>();
builder.Services.AddScoped<CreateClubCommandHandler>();
builder.Services.AddScoped<DeleteClubCommandHandler>();
//          CreateClubCommandHandler, DeleteClubCommandHandler (AddScoped)

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "EduHub API", Version = "v1" });
});

var app = builder.Build();

// ── Middleware pipeline ───────────────────────────────────────────────────
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
