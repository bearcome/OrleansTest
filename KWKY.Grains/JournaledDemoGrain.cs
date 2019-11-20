using KWKY.Common;
using KWKY.IGrains;
using KWKY.Model.KWKYFormModel;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Core;
using Orleans.EventSourcing;
using Orleans.LogConsistency;
using Orleans.Providers;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KWKY.Grains
{
    #region 
    /*----------------------------------------------------------------
	// 文件名：JournaledDemoGrain
	// 文件功能描述：
	//
	// 
	// 创建者：杨明
	// 时间：2019/8/12 9:22:51
	//
	// 修改人：
	// 时间：
	// 修改说明：
	//----------------------------------------------------------------*/
    #endregion
    [StorageProvider(ProviderName = ConstData.AdoNetGrainStorageName)]
    public class JournaledDemoGrain: JournaledGrain<Crf>, IJournaledDemoGrain
    {
        private readonly ILogger _logger;
        public JournaledDemoGrain (ILogger<DefaultGrain> logger) : base()
        {
            _logger = logger;
        }

        //public override Task OnActivateAsync ()
        //{
        //    base.OnActivateAsync();
        //    State.Id = IdentityString;
        //    return Task.CompletedTask;
        //}
        public override Task OnActivateAsync ()
        {
            return base.OnActivateAsync();
            //Guid primaryKey = this.GetPrimaryKey();
            //State.Id = primaryKey.ToString();
            //return Task.CompletedTask;
        }

        public async Task WriteMethodDemo ()
        {

            RaiseEvent<EventDemo1>(new EventDemo1() { Id = this.GetPrimaryKey().ToString(), Name = "Zhang San", Age = 80 });
            await ConfirmEvents();
        }
        public async Task WriteMethodDemo2 ()
        {
            RaiseEvent<EventDemo1>(new EventDemo1() { Id = this.GetPrimaryKey().ToString(), Name = "Zhao Liu", Age = 93 });
            await ConfirmEvents();
        }


        //每次ConfirmEvents 会触发2次  不知道为啥是两次
        protected override void OnStateChanged ()
        {
            base.OnStateChanged();
            _logger.Info("OnStateChanged");
        }

        protected override void OnConnectionIssue (ConnectionIssue issue)
        {
            base.OnConnectionIssue(issue);
            _logger.LogError("OnConnectionIssue");
            /// handle the observed error described by issue             
        }
        protected override void OnConnectionIssueResolved (ConnectionIssue issue)
        {
            base.OnConnectionIssueResolved(issue);
            _logger.LogWarning("OnConnectionIssueResolved");
            /// handle the resolution of a previously reported issue             
        }
    }
}
