using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Services.Utils
{
    public static class DateTimeExtensions
    {
        public static string ToGeneralTime(this DateTime obj)
        {
            return obj.ToString("yyyy-MM-dd HH:mm.ss");
        }
    }
}
