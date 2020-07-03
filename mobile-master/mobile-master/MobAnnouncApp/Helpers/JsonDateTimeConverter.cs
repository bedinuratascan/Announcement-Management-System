using System;
using Newtonsoft.Json.Converters;

namespace MobAnnouncApp.Helpers
{
    public class JsonDateTimeConverter : IsoDateTimeConverter
    {
        public JsonDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-ddTHH:mm:ss.ffffffK";
        }
    }
}
