using KWKY.IGrains;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.EventSourcing;
using Orleans.Runtime;
using System.Net.Http;
using System.Threading.Tasks;

namespace KWKY.Grains
{
    //效率优化点
    //Span<int> numbers = stackalloc[] { 1, 2, 3, 4, 5, 6 };        Stack-Only
    //Memory<int> numbers
    ////返回多个数据
    //ValueTuple<T1,T2,..>
    ////可能同步执行也可能异步执行的返回
    //ValueTask<T>



    //public class DefaultGrain : Orleans.Grain, IDefaultGrain
    //{
    //    //JournaledGrain
    //    //IIncomingGrainCallFilter
    //    public override void Participate (IGrainLifecycle lifecycle)
    //    {
    //        base.Participate(lifecycle);
    //        //lifecycle.Subscribe
    //    }
    //}


    //常用Attribute 
    //[Reentrant]               GrainClass 可重入
    //[AlwaysInterleave]        GrainMethod 可重入
    //[Obsolete]                过期
    //[OneWay]                  Method单项调用，不返回任何消息
    //[ReadOnly]                Method 不改变Grain State值得方法添加此标记 可提高性能
    //[StatelessWorker(2)]      GrainClass视为无状态（实际可以有状态）可重复创建相同Grain 调用时直接在本Silo创建实例不跨Silo， Arg:单个Silo最多重建个数
    //[Serializable]            可序列化
    //[Version]                 接口版本号  没有无状态工作者的版本控制     流式接口未进行版本控制 默认向后兼容
    //[OneInstancePerCluster]   一个集群内该Grain类型只有一个实例
    //[Immutable]               类型实例化后不会改变  可避免深拷贝 （参数），默认先深拷贝然后序列化，然后传递
    //Immutable<T>              T obj 不会改变，可避免深拷贝，默认先深拷贝然后序列化，然后传递
    //Participate               注册事件，创建对象后可预执行部分操作提升效率,激活Grain时 提高效率
    //[Conditional]             标记单元测试方法

    //用不到
    //框架Attribute 
    //[GlobalSingleInstance]    多集群时使用 单集群用不到
    //[Serializer]
    //[CopierMethod]
    //[MethodId]
    //[MethodInvoker]
    //[SerializerMethod]
    //[NonSerialized]           序列化时跳过标记的属性，如果业务不需要   
    //[Unordered]


    public class DefaultGrain : Orleans.Grain, IDefaultGrain
    {
        private readonly ILogger<DefaultGrain> _logger;
        private IHttpClientFactory _clientFactory;
        public DefaultGrain ( ILogger<DefaultGrain> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }
        public async Task TestLog ()
        {
            //var a = await user.PerformRead(o => o.Password = "");
            _logger.LogInformation("aaa");
            await Task.CompletedTask;
            _clientFactory.CreateClient();
        }
    }
}
