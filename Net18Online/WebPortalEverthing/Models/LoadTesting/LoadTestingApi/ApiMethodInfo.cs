using System.Reflection;

namespace WebPortalEverthing.Models.LoadTesting.LoadTestingApi
{
    public class ApiMethodInfo
    {
        public string ControllerName { get; set; }
        public string MethodName { get; set; }
        public string HttpMethod { get; set; }
        public string ReturnType { get; set; }
        public List<ParameterInfo> Parameters { get; set; }
    }
}
