using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InvokeWebService
{
    public partial class WeatherClient : Form
    {
        public WeatherClient()
        {
            InitializeComponent();
        }

        private void buttonGetWeather_Click(object sender, EventArgs e)
        {
            //创建返回数据对象实例
            var req = new GetWeatherService.GetWeatherRequest();
            if (radioButtonCelsius.Checked)
            {
                req.TemperatureType = GetWeatherService.TemperatureType.Celsius;
            }
            else
            {
                req.TemperatureType = GetWeatherService.TemperatureType.Fahrenheit;
            }
            req.City = textCity.Text;

            //创建WebService请求实例
            var client = new GetWeatherService.SampleServiceSoapClient();
            GetWeatherService.GetWeatherResponse resp = client.GetWeather(req);
            textWeatherCondition.Text = resp.TemperatureCondition.ToString();
            textTemperature.Text = resp.Temperature.ToString();

        }
    }
}
