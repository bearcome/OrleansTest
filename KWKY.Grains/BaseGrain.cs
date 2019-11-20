using KWKY.DBUtility;
using KWKY.IGrains;
using KWKY.Model.Base;
using Orleans;
using System;
using System.Threading.Tasks;

namespace KWKY.Grains
{
    /// <summary>
    /// 封装Grain 基础功能
    /// Grain是单个实例 不适合集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseGrain<T> : Grain, IBaseGrain<T> where T : class, IDBModel, new()
    {
        protected T State { get; set; }
        protected readonly EFBase<T> _EFContext;


        public BaseGrain (KWKYDBContext dbContext)
        {
            _EFContext = new EFBase<T>(dbContext);
        }


        public override async Task OnActivateAsync ()
        {
            Guid guid = this.GetGrainIdentity().PrimaryKey;
            string id = guid.ToString();

            State = await _EFContext.Find(id);
            if ( State == null )
            {
                State = new T();
                State.SetId(id);
            }
            await base.OnActivateAsync();
        }


        public async Task<int> WriteStateAsync ()
        {
            T dbState = await _EFContext.Find(State.GetId());
            return dbState==null ? await SaveStateAsync() : await UpdateStateAsync() ;
        }


        protected async Task<int> SaveStateAsync ()
        {
            return await _EFContext.Insert(State);
        }


        protected async Task<int> UpdateStateAsync ()
        {
            return await _EFContext.Update(State);
        }


        public async Task<int> DeleteStateAsync ()
        {
            int res = await _EFContext.Delete(State);
            DeactivateOnIdle();
            return res;
        }
    }

}
