using NewTelSdk.Enums;
using Newtonsoft.Json;

namespace NewTelSdk.Responses
{
    public class StartPasswordCallResponse : BaseApiResponse<StartPasswordCallCallDetails>
    {
    }

    public class StartPasswordCallCallDetails : ResponseData
    {
        /// <summary>
        ///     идентификатор сгенерированного вызова, строка
        /// </summary>
        public string CallId { get; set; } = null!;

        /// <summary>
        ///     Код, который использован как последние 5 цифр номера источника, строка
        /// </summary>
        public string Pin { get; set; } = null!;

        /// <summary>
        ///     Cтатус вызова, строка, возможные значения при синхронном запросе
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        ///     принадлежность dstNumber к оператору связи
        /// </summary>
        [JsonProperty("oper")]
        public string PhoneOperator { get; set; } = null!;
        public string Region { get; set; } = null!;
        public bool IsValidNumber { get; set; }

        public CallStatusType GetCallStatusType()
        {
            return CallStatusTypeConverter.FromString(Status);
        }
    }
}