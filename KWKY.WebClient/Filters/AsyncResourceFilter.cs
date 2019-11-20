using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace KWKY.WebClient.Filters
{
    /// <summary>
    ///资源筛选器是授权后最先处理请求的筛选器。 它们可以在筛选器管道的其余阶段运行之前以及管道的其余阶段完成之后运行代码。
    ///出于性能方面的考虑，可以使用它们来实现缓存或以其他方式让筛选器管道短路。 它们在模型绑定之前运行，所以可以影响模型绑定。
    /// </summary>
    public class AsyncResourceFilter : IAsyncResourceFilter
    {

        public async Task OnResourceExecutionAsync (ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            await Task.CompletedTask;
        }
    }
}
