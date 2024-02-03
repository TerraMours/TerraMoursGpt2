using Microsoft.EntityFrameworkCore;
using TerraMours.Framework.Infrastructure.EFCore;
using TerraMours_Gpt.Domains.GptDomain.IServices;
using ILogger = Serilog.ILogger;

namespace TerraMours_Gpt.Framework.Infrastructure.EFCore {
    public class DbInitialiser {
        private readonly FrameworkDbContext _context;
        private readonly ISeedDataService _seedDataService;
        private readonly Serilog.ILogger _logger;
        public DbInitialiser(FrameworkDbContext context, ISeedDataService seedDataService, ILogger logger) {
            _context = context;
            _seedDataService = seedDataService;
            _logger = logger;
        }

        public void Run() {
            _context.Database.GetDbConnection().Open();
            var tables = _context.Database.GetDbConnection().GetSchema("Tables");
            _logger.Information($"后端服务版本：V2.0");
            System.Console.WriteLine($"数据库中表数：{tables.Rows.Count}");
            _logger.Information($"数据库中表数：{tables.Rows.Count}");
            //数据库model有改动的话需要先执行下 add-migrate xxx 命令，然后每次运行程序GetPendingMigrations()就会检测有无更新，有的话自动迁移。
            if (_context.Database.GetPendingMigrations().Any()) {
                _logger.Information($"执行数据迁移");
                System.Console.WriteLine("执行数据迁移");
                _context.Database.Migrate();
            }
            //初始化基础数据
            _seedDataService.EnsureSeedData();
        }
    }
}
