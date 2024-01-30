using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Terramours_Gpt_Vector.Commons;
using Terramours_Gpt_Vector.Entities;
using Terramours_Gpt_Vector.IService;
using Terramours_Gpt_Vector.Req;
using Terramours_Gpt_Vector.Res;
using Terramours_Gpt_Vector.Service;

var builder = WebApplication.CreateBuilder(args);



var isDev = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;
IConfiguration configuration = builder.Configuration;
var connectionString = configuration.GetValue<string>("DbConnectionString");
//automapper
MapperConfiguration mapperConfig = new(cfg => {
    cfg.CreateMap<IndexItem, IndexQueryRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<ApiKey, ApikeyQueryRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<IndexUpdateReq,IndexItem>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<ApikeyUpdateReq,ApiKey>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<IndexUpsertReq, IndexItem>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<ApikeyUpsertReq, ApiKey>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<ApiKey, ApikeyUpsertRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<IndexItem, IndexUpsertRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
});
//注册配置
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
//注入服务
builder.Services.AddScoped<IVectorService,VectorService>();
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<IIndexService, IndexService>();
//初始化
builder.Services.AddTransient<DbInitialiser>();
//数据库
builder.Services.AddDbContext<EFCoreDbContext>(opt => {
    //从配置文件中获取key,这种方法需要新增一个类与之对应

    //var connStr = $"Host=localhost;Database=TerraMours;Username=postgres;Password=root";
    var connStr = connectionString;
    opt.UseNpgsql(connStr, o => o.UseVector());
    //设置EF默认AsNoTracking,EF Core不 跟踪
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    if (isDev)
    {
        //启用此选项后，EF Core将在日志中包含敏感数据，例如实体的属性值。这对于调试和排查问题非常有用。
        opt.EnableSensitiveDataLogging();
    }

    opt.EnableDetailedErrors();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    //登录成功之后复制token,在swagger 右上角锁图标位置填入token
    //填入格式为Bearer xxxxxx   
    //注意Bearer后面有一个空格，后面再填入token
    options.AddSecurityDefinition("Api-Key", new OpenApiSecurityScheme
    {
        Description = "Api key needed to access the endpoints. X-Api-Key: My_API_Key",
        In = ParameterLocation.Header,
        Name = "Api-Key",
        Type = SecuritySchemeType.ApiKey
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Name = "Api-Key",
                                Type = SecuritySchemeType.ApiKey,
                                In = ParameterLocation.Header,
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Api-Key"
                                },
                            },
                            new string[] {}
                        }
                    });

    //配置XML备注文档
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TerraMours_Gpt_Vector.xml"));
});


var app = builder.Build();
using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;
//初始化数据库

var initialiser = services.GetRequiredService<DbInitialiser>();

initialiser.Run();
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
