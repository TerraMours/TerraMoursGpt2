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
//ע������
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
//ע�����
builder.Services.AddScoped<IVectorService,VectorService>();
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<IIndexService, IndexService>();
//��ʼ��
builder.Services.AddTransient<DbInitialiser>();
//���ݿ�
builder.Services.AddDbContext<EFCoreDbContext>(opt => {
    //�������ļ��л�ȡkey,���ַ�����Ҫ����һ������֮��Ӧ

    //var connStr = $"Host=localhost;Database=TerraMours;Username=postgres;Password=root";
    var connStr = connectionString;
    opt.UseNpgsql(connStr, o => o.UseVector());
    //����EFĬ��AsNoTracking,EF Core�� ����
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    if (isDev)
    {
        //���ô�ѡ���EF Core������־�а����������ݣ�����ʵ�������ֵ������ڵ��Ժ��Ų�����ǳ����á�
        opt.EnableSensitiveDataLogging();
    }

    opt.EnableDetailedErrors();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    //��¼�ɹ�֮����token,��swagger ���Ͻ���ͼ��λ������token
    //�����ʽΪBearer xxxxxx   
    //ע��Bearer������һ���ո񣬺���������token
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

    //����XML��ע�ĵ�
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TerraMours_Gpt_Vector.xml"));
});


var app = builder.Build();
using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;
//��ʼ�����ݿ�

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
