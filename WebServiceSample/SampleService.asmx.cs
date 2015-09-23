using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Threading;

namespace WebServiceSample
{
    /// <summary>
    /// Summary description for SampleService
    /// </summary>
    [WebService(Namespace = "http://www.wrox.com/BeginningCSharp/2014")]
    //WebServiceBinding属性与WebServices的交互操作性相关
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SampleService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string ReverseString(string message)
        {
            //可以用于异步调用的测试
            //Thread.Sleep(5000);
            return new string(message.Reverse().ToArray());
        }

        [WebMethod]
        public GetWeatherResponse GetWeather(GetWeatherRequest req)
        {
            var resp = new GetWeatherResponse();
            var r = new Random();
            int celsius = r.Next(-20, 50);
            if (req.TemperatureType == TemperatureType.Celsius)
            {
                resp.Temperature = celsius;
            }
            else
            {
                resp.Temperature = (212 - 32) / 100 * celsius + 12;
            }

            if (req.City == "Redmond")
            {
                resp.TemperatureCondition = TemperatureCondition.Rainy;
            }
            else
            {
                resp.TemperatureCondition = (TemperatureCondition)r.Next(1, 3);
            }
            return resp;
        }

    }
}
