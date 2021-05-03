using Newtonsoft.Json.Converters;

namespace Mangau.DevTest.Covid19.Dtos
{
    public class JsonDateConverter : IsoDateTimeConverter
    {
        public JsonDateConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
