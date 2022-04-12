
namespace UserService.Helpers
{
    public class JsonValuesHelper
    {
        public const string sectionName = "JWT";
        public string JWTkey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int Hours { get; set; }

    }

    public class JsonApiKeyHelper
    {
        public const string sectionName = "APIkey";

        public string Key { get; set; }
    }
}
