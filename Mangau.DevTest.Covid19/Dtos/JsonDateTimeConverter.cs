using Newtonsoft.Json.Converters;

namespace Mangau.DevTest.Covid19.Dtos
{
    public class JsonDateTimeConverter : IsoDateTimeConverter
    {
        public JsonDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd hh:mm:ss";
        }
    }
}
