using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SecondSemesterProject.Controllers
{
    [ApiController]
    public class WeatherForecastController(IConfiguration configuration, IOptions<MyInfoConfig> myInfoConfig) : ControllerBase
    {

        [HttpGet("count")]
        public int GetTotalPresentCount()
        {
            int totalStudent = 0;
            totalStudent = configuration.GetValue<int>("TotalPresent");
            return totalStudent;
        }

        [HttpGet("myinfo")]
        public object GetMyInfo()
        {

            var info = new
            {
                Name = configuration["MyInfo:Name"],
                Age = configuration["MyInfo:Age"],
                Address = configuration["MyInfo:Address"]
            };

            return info;
        }

        [HttpGet("myinfo/v2")]
        public object GetMyInformationOptionPattern()
        {
            var myInfoConfigValue = myInfoConfig.Value;

            var data = new
            {
                Name = myInfoConfigValue.Name,
                Age = myInfoConfigValue.Age,
                Address = myInfoConfigValue.Address,
            };

            return data;
        }
    }
}
