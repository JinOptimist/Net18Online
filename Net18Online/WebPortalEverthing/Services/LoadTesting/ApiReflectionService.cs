using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebPortalEverthing.Models.LoadTesting.LoadTestingApi;
using ParameterInfo = WebPortalEverthing.Models.LoadTesting.LoadTestingApi.ParameterInfo;


namespace WebPortalEverthing.Services.LoadTesting
{
    /// <summary>
    /// класс LoadAuthService (AuthService тоже) будет зарегистрирован автоматически в контейнере (AutoRegisterFlagAttribute)  если установить аттрибут [AutoRegisterFlag]
    /// благодаря рефлексии, сервис ищется по атрибуту AutoRegisterFlag и регистрируется
    /// </summary>
  //  [AutoRegisterFlag] пока вручную зарегаю в bi контейнере
    public class ApiReflectionService
    {
        public List<ApiMethodInfo> GetApiMethods()
        {
            var methodsInfo = new List<ApiMethodInfo>();
            var assembly = Assembly.GetExecutingAssembly();

            // Ищем все контроллеры в пространстве имен Controllers.ApiControllers
            var controllers = assembly.GetTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && type.Namespace?.Contains("Controllers.ApiControllers") == true);

            foreach (var controller in controllers)
            {
                var methods = controller.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => m.GetCustomAttributes<HttpMethodAttribute>().Any());

                foreach (var method in methods)
                {
                    var httpAttribute = method.GetCustomAttributes<HttpMethodAttribute>().FirstOrDefault();
                    var parameters = method.GetParameters()
                        .Select(p => new ParameterInfo
                        {
                            Name = p.Name,
                            Type = p.ParameterType.Name
                        })
                        .ToList();

                    methodsInfo.Add(new ApiMethodInfo
                    {
                        ControllerName = controller.Name,
                        MethodName = method.Name,
                        HttpMethod = httpAttribute?.HttpMethods.FirstOrDefault() ?? "GET",
                        ReturnType = method.ReturnType.Name,
                        Parameters = parameters
                    });
                }
            }

            return methodsInfo;
        }
    }

}
