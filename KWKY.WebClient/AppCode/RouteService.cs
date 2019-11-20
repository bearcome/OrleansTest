using KWKY.Model.System;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;

namespace KWKY.WebClient.AppCode
{
    /// <summary>
    /// 获取系统所有路由信息   已注入 services.AddScoped
    /// </summary>
    public class RouteService
    {
        private IActionDescriptorCollectionProvider _provider;
        public RouteService (IActionDescriptorCollectionProvider provider)
        {
            _provider = provider;
        }
        /// <summary>
        /// 获取系统所有路由信息
        /// </summary>
        /// <returns></returns>
        private List<RouteInfo> GetRoutes ()
        {
#warning 
            //var routes = _provider.ActionDescriptors.Items.Select(x => new RouteInfo
            //{
            //    Controller = x.RouteValues["Controller"],
            //    Action = x.RouteValues["Action"],
            //    ControllerName = (((ControllerActionDescriptor)x).MethodInfo.DeclaringType.GetCustomAttributes(true).FirstOrDefault(o=>o is RouteAttribute) as RouteAttribute).Name,
            //    ActionName = x.AttributeRouteInfo.Name,
            //    HttpMethods = string.Join(ConstData.CommaChar,((HttpMethodActionConstraint)x.ActionConstraints.FirstOrDefault(o=>o is HttpMethodActionConstraint)).HttpMethods),
            //    Template = x.AttributeRouteInfo.Template
            //}).ToList();
            return null;
        }
    }
}
