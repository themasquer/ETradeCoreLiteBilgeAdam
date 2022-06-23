using DataAccess.Contexts;
using DataAccess.Services.CRUD;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

#region Localization
List<CultureInfo> cultures = new List<CultureInfo>()
{
    new CultureInfo("en-US")
};

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
#endregion

// Add services to the container.
builder.Services.AddDbContext<Db>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Db")));

builder.Services.AddScoped<CategoryServiceBase, CategoryService>();
builder.Services.AddScoped<ProductServiceBase, ProductService>();
builder.Services.AddScoped<StoreServiceBase, StoreService>();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region CORS (Cross Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => builder
            .AllowAnyOrigin() // Her kaynaktan gelen isteklere yanýt ver, kaynaklara örnek https://sahibinden.com, https://hepsiburada.com, vb. Mesela origin için baþka methodlar ile sadece sahibinden.com üzerinden gelen isteklere yanýt verilmesi ayarlanabilir.
            .AllowAnyHeader() // Ýsteklerin (request) gövdesi (body) dýþýnda baþlýk (head) içerisinde gönderilen ekstra veriler, örneðin Authorize, Content-Type, vb. Mesela header'lar için baþka methodlar ile sadece Content-Type header'ýna izin verilebilir. 
            .AllowAnyMethod() // Method'lar: get, post, put, delete, vb. Ýsteklerdeki bütün method'lara izin ver. Mesela method'lar için builder üzerinden baþka methodlar ile sadece get header'ýna yanýt verilmesi saðlanabilir.
    );
});
#endregion

var app = builder.Build();

#region Localization
app.UseRequestLocalization(new RequestLocalizationOptions()
{
    DefaultRequestCulture = new RequestCulture(cultures.FirstOrDefault().Name),
    SupportedCultures = cultures,
    SupportedUICultures = cultures,
});
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region CORS(Cross Origin Resource Sharing)
app.UseCors();
#endregion

app.UseAuthorization();

app.MapControllers();

app.Run();
