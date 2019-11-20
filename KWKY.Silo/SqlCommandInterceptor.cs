using Microsoft.EntityFrameworkCore.Diagnostics;
using NLog;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace KWKY.Silo
{
    public class SqlCommandInterceptor: DbCommandInterceptor
    {
        ILogger _logger;
        public SqlCommandInterceptor ()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }


        /// <summary>
        /// 此处添加可优化Sql代码
        /// </summary>
        /// <param name="command"></param>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<InterceptionResult<DbDataReader>> ReaderExecutingAsync (DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result, CancellationToken cancellationToken = default)
        {
            _logger.Info(command.CommandText);
            return await Task.FromResult(result);
        }
    }
}
