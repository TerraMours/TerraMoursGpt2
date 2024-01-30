//用于启用或禁用 Npgsql 客户端与 Postgres 服务器之间的时间戳行为。它并不会直接修改 Postgres 的时区设置。
//时间设置要提前，之前在身份检验后会导致失效
using Serilog.Events;
using Serilog;
using TerraMours.Framework.Infrastructure.Contracts.Commons;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.Commons;
using TerraMours.Framework.Infrastructure.Services;
using AutoMapper;
using FluentValidation.AspNetCore;
using FluentValidation;
using TerraMours.Domains.LoginDomain.Contracts.Req;
using TerraMours.Domains.LoginDomain.Contracts.ReqValidators;
using TerraMours_Gpt.Domains.LoginDomain.Contracts.Req;
using TerraMours.Framework.Infrastructure.Contracts.SystemModels;
using TerraMours.Domains.LoginDomain.Contracts.Res;
using TerraMours_Gpt.Domains.GptDomain.Contracts.Res;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.GptModels;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Req;
using TerraMours_Gpt.Domains.PayDomain.Contracts.Res;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.PaymentModels;
using TerraMours.Domains.LoginDomain.IServices;
using TerraMours.Domains.LoginDomain.Services;
using TerraMours_Gpt.Domains.GptDomain.IServices;
using TerraMours_Gpt.Domains.GptDomain.Services;
using TerraMours_Gpt.Domains.LoginDomain.IServices;
using TerraMours_Gpt.Domains.LoginDomain.Services;
using TerraMours_Gpt.Domains.PayDomain.IServices;
using TerraMours_Gpt.Domains.PayDomain.Services;
using TerraMours_Gpt.Framework.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using TerraMours.Framework.Infrastructure.EFCore;
using Hangfire;
using TerraMours.Framework.Infrastructure.Redis;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TerraMours.Framework.Infrastructure.Filters;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.FileProviders;
using TerraMours_Gpt.Framework.Infrastructure.Middlewares;
using TerraMours_Gpt.Domains.GptDomain.Hubs;
using TerraMours_Gpt.Domains.PayDomain.Hubs;
using Microsoft.OpenApi.Models;
using TerraMours_Gpt.Framework.Infrastructure.Contracts;
using TerraMours_Gpt.Framework.Infrastructure.Contracts.ProductModels;
using TerraMours_Gpt_Api.Domains.GptDomain.IServices;
using TerraMours_Gpt_Api.Domains.GptDomain.Services;
using TerraMours_Gpt_Api.Domains.GptDomain.Contracts.Res;
using TerraMours_Gpt_Api.Framework.Infrastructure.Contracts.GptModels;
using TerraMours_Gpt_Api.Domains.GptDomain.Contracts.Req;
using Npgsql;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

var builder = WebApplication.CreateBuilder(args);
//健康检查
builder.Services.AddHealthChecks()
//这里是添加自己的自定义的健康检查逻辑 使用默认的可以注释
    .AddCheck<HealthCheckService>("HealthCheck");
builder.Services.AddHealthChecksUI().AddInMemoryStorage();
//automapper
// 配置映射规则
//添加忽略null值的配置：.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)
MapperConfiguration mapperConfig = new(cfg => {
    cfg.CreateMap<SysUserUpdateReq, SysUser>().ForMember(m => m.UserId, n => n.Ignore()).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<SysUser, SysUserDetailRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<SysUserAddReq, SysUser>().ForMember(m => m.UserId, n => n.Ignore()).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<SysRole, SysRoleRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<SysMenuReq, SysMenus>().ForMember(m => m.MenuId, n => n.Ignore()).ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<SysMenus, SysMenuRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<KeyOptions, KeyOptionRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<Sensitive, SensitiveRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<ChatConversation, ChatConversationRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<ChatRecord, ChatRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<ChatRes, ChatRecord>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<ImageRecord, ImageRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<PromptOptions, PromptOptionRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<ProductReq, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<Product, ProductRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<CategoryReq, Category>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<Category, CategoryRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<Order, OrderRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<KnowledgeItem, KnowledgeRes>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<KnowledgeReq, KnowledgeItem>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    cfg.CreateMap<KnowledgeUpdateReq, KnowledgeItem>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
});
//注册配置
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
// Add services to the container.
builder.Services.AddScoped<ISysUserService, SysUserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISysRoleService, SysRoleService>();
builder.Services.AddScoped<ISysMenuService, SysMenuService>();
builder.Services.AddScoped<ISettingsService, SettingsService>();
builder.Services.AddScoped<IAnalysisService, AnalysisService>();
builder.Services.AddScoped<ISeedDataService, SeedDataService>();
//gpt
builder.Services.AddScoped<IChatService, ChatService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IGptOptionsService, GptOptionsService>();

//支付宝pay
builder.Services.AddScoped<IPayService, AliPayService>();

//商品服务
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
//知识库
builder.Services.AddScoped<IKnowledgeService,KnowledgeService>();
builder.Services.AddScoped<IVectorService,VectorService>();
//初始化
builder.Services.AddTransient<DbInitialiser>();
// 添加Paylink依赖注入
builder.Services.AddAlipay();
//过滤器
builder.Services.AddScoped<ExceptionFilter>();
//获取appsetting配置文件
IConfiguration configuration = builder.Configuration;

//添加配置文件与实体类绑定
builder.Services.Configure<SysSettings>(configuration.GetSection("SysSettings"));
var sysSettings = builder.Configuration.GetSection("SysSettings").Get<SysSettings>() ?? throw new Exception("用户或者密码不正确");
var isDev = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development;
var dbConnStr = isDev ? sysSettings.connection.DbConnectionString : (Environment.GetEnvironmentVariable("ENV_DB_CONNECTION") ?? sysSettings.connection.DbConnectionString);
var seqUrl = isDev ? sysSettings.SeqUrl : (Environment.GetEnvironmentVariable("ENV_SEQ_HOST") ?? sysSettings.SeqUrl);
Console.WriteLine("当前连接数据库：" + dbConnStr);
var redisConnStr = isDev ? sysSettings.connection.RedisHost : (Environment.GetEnvironmentVariable("ENV_REDIS_HOST") ?? sysSettings.connection.RedisHost);
builder.Services.Configure<GptOptions>(configuration.GetSection("GptOptions"));
builder.Services.Configure<Essensoft.Paylink.Alipay.AlipayOptions>(configuration.GetSection("Alipay"));
//数据库
builder.Services.AddDbContext<FrameworkDbContext>(opt => {
    //从配置文件中获取key,这种方法需要新增一个类与之对应

    //var connStr = $"Host=localhost;Database=TerraMours;Username=postgres;Password=root";
    var connStr = dbConnStr;
    opt.UseNpgsql(new NpgsqlDataSourceBuilder(connStr)
                                .EnableDynamicJson()
                                .Build());
    //设置EF默认AsNoTracking,EF Core不 跟踪
    //opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    //设置是否启用缓存
    opt.EnableServiceProviderCaching(false);
    if (isDev) {
        //启用此选项后，EF Core将在日志中包含敏感数据，例如实体的属性值。这对于调试和排查问题非常有用。
        opt.EnableSensitiveDataLogging();
    }

    opt.EnableDetailedErrors();
});
// Add Hangfire services.
builder.Services.AddHangfire(config => config.UseStorage(new PostgreSqlStorage(dbConnStr)));
//redis 缓存 这个实现了IDistributedCache
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = redisConnStr;
    options.InstanceName = sysSettings.connection.RedisInstanceName;
});
builder.Services.AddScoped<IDistributedCacheHelper, DistributedCacheHelper>();
//注入日志
// 配置 Serilog 日志记录器
Console.WriteLine("当前连接seq：" + seqUrl);
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    //.WriteTo.File()
    .WriteTo.Seq(seqUrl)
    .CreateLogger();
builder.Host.UseSerilog(Log.Logger);

// 可用 启动自动验证 但是对外方法也要加东西 体验不好
builder.Services.AddFluentValidationAutoValidation();
//注入 ModifyUser2 ModifyUserIntendedEffect 对应上面两个cs文件
builder.Services.AddScoped<IValidator<SysUserReq>, SysUserReqValidator>();
builder.Services.AddScoped<IValidator<SysLoginUserReq>, SysLoginUserReqValidator>();

builder.Services.AddCors(options => {
    options.AddPolicy(name: "MyPolicy",
                      policy => {
                          policy.SetIsOriginAllowed(_ => true)
                                   .AllowAnyMethod()
                                   .AllowAnyHeader()
                          .AllowCredentials();
                      });
});

builder.Services.AddControllers();

//json小写的问题
builder.Services.Configure<JsonOptions>(options => {
    //net6的 options.JsonSerializerOptions.PropertyNamingPolicy = null;

    //net7 PropertyNameCaseInsensitive = true
    //保留原样字段名
    //options.SerializerOptions.PropertyNamingPolicy = null;
    //不区分大小写
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

// SignalR
builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    //登录成功之后复制token,在swagger 右上角锁图标位置填入token
    //填入格式为Bearer xxxxxx   
    //注意Bearer后面有一个空格，后面再填入token
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() {
        Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
    //配置XML备注文档
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TerraMours_Gpt_Api.xml"));
});
//添加认证  授权服务
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        //ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidIssuer = sysSettings.jwt.Issuer,
        ValidateAudience = true,
        //ValidAudience = builder.Configuration["JWT:Audience"],
        ValidAudience = sysSettings.jwt.Audience,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sysSettings.jwt.SecretKey))
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
    };
});
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
//    options.Authority = sysSettings.jwt.Issuer;
//    options.Audience = sysSettings.jwt.Audience;
//});

var app = builder.Build();

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;
//初始化数据库

var initialiser = services.GetRequiredService<DbInitialiser>();

initialiser.Run();

//健康检查
//app.UseHealthChecks("/health");
app.UseHealthChecksUI();
//日志
app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();


// Use Hangfire server and dashboard.
app.UseHangfireServer(new BackgroundJobServerOptions {
    Queues = new[] { "default", "img-queue" },
    WorkerCount = 1
});
app.UseHangfireDashboard();// 使用 Hangfire 控制面板

//app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions {
    FileProvider = new PhysicalFileProvider(AppDomain.CurrentDomain.BaseDirectory + "/images"),
    RequestPath = ""
});

//`UseCors` 添加 CORS 中间件。 对 `UseCors` 的调用必须放在 `UseRouting` 之后，但在 `UseAuthorization` 之前。 不然会出现前端获取不到response的现象
app.UseCors("MyPolicy");
//请求中间件
app.UseMiddleware<KeyMiddleware>();
app.UseAuthorization();

app.MapControllers();
// SignalR hub
app.MapHub<GraphGenerationHub>("/graphhub");
app.MapHub<PaymentHub>("/Hubs/QueryPaymentStatus");
app.Run();
