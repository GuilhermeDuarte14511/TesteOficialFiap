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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
