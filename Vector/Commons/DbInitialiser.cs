using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Terramours_Gpt_Vector.Commons
{
    public class DbInitialiser
    {
        private readonly EFCoreDbContext _context;
        public DbInitialiser(EFCoreDbContext context)
        {
            _context = context;
        }

        public void Run()
        {
            //检查数据库是否存在，不存在则创建,与Migration冲突
            //_context.Database.EnsureCreated();

            if (_context.Database.GetDbConnection().State == ConnectionState.Closed ||
                _context.Database.GetDbConnection().State == ConnectionState.Broken)
            {
                _context.Database.GetDbConnection().Open();
            }
            var tables = _context.Database.GetDbConnection().GetSchema("Tables");
            System.Console.WriteLine($"后端服务版本：V1.0");
            System.Console.WriteLine($"数据库中表数：{tables.Rows.Count}");
            //数据库model有改动的话需要先执行下 add-migrate xxx 命令，然后每次运行程序GetPendingMigrations()就会检测有无更新，有的话自动迁移。
            if (_context.Database.GetPendingMigrations().Any())
            {
                System.Console.WriteLine("执行数据迁移");
                _context.Database.Migrate();
            }
        }
    }
}
