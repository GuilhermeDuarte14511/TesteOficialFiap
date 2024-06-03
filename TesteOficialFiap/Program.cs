using Microsoft.OpenApi.Models;
using Business;
using Newtonsoft.Json.Serialization;
using TesteTecnicoFIAP.Interface;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner
builder.Services.AddControllersWithViews()
      .AddNewtonsoftJson(options =>
      {
          options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      });

// Registrar as BLLs com Dapper
builder.Services.AddScoped<IAlunoBLL>(provider => new AlunoBLL(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITurmaBLL>(provider => new TurmaBLL(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAlunoTurmaBLL>(provider => new AlunoTurmaBLL(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar serviços Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TesteTecnicoFIAP API", Version = "v1" });
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configurar o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TesteTecnicoFIAP API v1");
    c.RoutePrefix = "swagger";
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
