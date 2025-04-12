string[] AMPLFiles = new string[]{
    @"C:\Users\User\Desktop\DemetreBadzgaradze\effective-giggle-Backend\AMPLFiles\Eq.dat",
    @"C:\Users\User\Desktop\DemetreBadzgaradze\effective-giggle-Backend\AMPLFiles\Eq.mod"
};

foreach (var filepath in AMPLFiles)
{
    File.WriteAllText(filepath, string.Empty);
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(op =>
    op.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:8000/",
                               "http://localhost:8000",
                               "http://127.0.0.1:5500")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader();
        })
    );

builder.Services.AddControllers();
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

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
