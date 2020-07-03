using System;
using System.Linq;
using System.Web;

namespace MobAnnouncApp.Helpers
{
    public class ParamConverter
    {
        public static string Object(object obj)
        {
            if (obj == null)
                return "";

            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }
    }
}
