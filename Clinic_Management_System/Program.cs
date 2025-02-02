using Clinic_Management_System.AppDbContext;
using Clinic_Management_System.Services.AppointmentRepositorys;
using Clinic_Management_System.Services.DoctorRepositorys;
using Clinic_Management_System.Services.MedicineRepositorys;
using Clinic_Management_System.Services.PatientController;
using Clinic_Management_System.Services.PrescriptionRepositorys;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connect = builder.Configuration.GetConnectionString("myconnection");
builder.Services.AddDbContext<dbcontext>(options => options.UseSqlServer(connect));

builder.Services.AddControllers();

builder.Services.AddScoped<IDoctorRepo,DoctorRepo>();
builder.Services.AddScoped<IPatientRepo, PatientRepo>();
builder.Services.AddScoped<IMedicineRepo, MedicineRepo>();
builder.Services.AddScoped<IAppointmentRepo, AppointmentRepo>();
builder.Services.AddScoped<IPrescriptionRepo, PrescriptionRepo>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
