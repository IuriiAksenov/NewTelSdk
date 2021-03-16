namespace NewTelSdk.Constants
{
    /// <summary>
    ///     Содержит константы для создания запросов к Sms API
    /// </summary>
    internal class CallPasswordApi
    {
        public const string StartPasswordCall = "call-password/start-password-call";

        public const string PhonePattern = @"^7[\d]{10}";
        public const string Json = "application/json";
        public const string ApiUrlRelease = "https://api.new-tel.net/[0]";
        public const string ApiUrlDebug = "http://localhost/";
    }
}