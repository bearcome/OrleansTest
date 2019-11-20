using KWKY.Common.Extensions;
using KWKY.Model.Base;
using KWKY.Model.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KWKY.DBUtility
{
    /// <summary>
    /// EF 基本增删改查 
    /// 仅针对单表操作
    /// </summary>
    public class EFBase<T> where T : class, IDBModel, new()
    {
        private KWKYDBContext _dbContext;
        public EFBase (KWKYDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> Insert (T obj)
        {
            if ( obj == null ) return 0;
            await _dbContext.AddAsync(obj);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 插入多条记录
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public async Task<int> InsertList (IEnumerable<T> objs)
        {
            if ( objs == null || !objs.Any() ) return 0;
            await _dbContext.AddRangeAsync(objs);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> Update (T obj)
        {
            if ( obj == null ) return 0;
            _dbContext.Update(obj);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 更新多条记录
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public async Task<int> UpdateList (IEnumerable<T> objs)
        {
            if ( objs == null || !objs.Any() ) return 0;
            _dbContext.UpdateRange(objs);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<int> Delete (T obj)
        {
            if ( obj == null ) return 0;
            _dbContext.Remove(obj);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public async Task<int> DeleteList (IEnumerable<T> objs)
        {
            if ( objs == null || !objs.Any() ) return 0;
            _dbContext.RemoveRange(objs);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 根据Id 查找一条记录
        /// </summary>
        /// <param name="keyValues">Id</param>
        /// <returns></returns>
        public async Task<T> Find (params object[] keyValues)
        {
            return keyValues == null ? null : await _dbContext.FindAsync<T>(keyValues);
        }

        /// <summary>
        /// 根据条件表达式查一条记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<T> Get (Expression<Func<T, bool>> predicate)
        {
            
            return predicate == null ?
                await _dbContext.Set<T>().FirstOrDefaultAsync()
                : await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 根据条件表达式查多条记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetList (Expression<Func<T, bool>> predicate)
        {
            
            return predicate == null ?
                await _dbContext.Set<T>().ToListAsync()
                : await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<PagerRet<T>> PageQuery (Expression<Func<T, bool>> predicate, Pager pager)
        {
            PagerRet<T> ret = new PagerRet<T>()
            {
                PageSize = pager.PageSize,
                PageIndex = pager.PageIndex
            };

            
            IQueryable<T> totalPredicate = _dbContext.Set<T>();
            IQueryable<T> dataPredicate = _dbContext.Set<T>();
            if ( predicate != null )
            {
                totalPredicate = totalPredicate.Where(predicate);
                dataPredicate = dataPredicate.Where(predicate);
            }
            ret.Total = await totalPredicate.CountAsync();

            dataPredicate = dataPredicate.OrderByStrProp(pager.SortPropName, pager.Asc);
            ret.DataCollection = await dataPredicate.ToListAsync();
            return ret;
        }
    }
}
