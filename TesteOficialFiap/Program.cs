using Microsoft.OpenApi.Models;
using Business;
using Newtonsoft.Json.Serialization;
using TesteTecnicoFIAP.Interface;

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao cont�iner
builder.Services.AddControllersWithViews()
      .AddNewtonsoftJson(options =>
      {
          options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      });

// Registrar as BLLs com Dapper
builder.Services.AddScoped<IAlunoBLL>(provider => new AlunoBLL(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITurmaBLL>(provider => new TurmaBLL(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAlunoTurmaBLL>(provider => new AlunoTurmaBLL(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar servi�os Swagger e ReDoc
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TesteTecnicoFIAP API", Version = "v1" });
    // Adicionar documenta��o XML se necess�rio
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configurar o pipeline de requisi��es HTTP
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

// Configurar Swagger e ReDoc
app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TesteTecnicoFIAP API v1");
    c.RoutePrefix = "swagger";
});

app.UseReDoc(c =>
{
    c.RoutePrefix = "docs"; // Serve o ReDoc em /redoc
    c.SpecUrl = "/swagger/v1/swagger.json";
    c.DocumentTitle = "TesteTecnicoFIAP API Documentation";
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
