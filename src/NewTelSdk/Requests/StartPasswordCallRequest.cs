using System;
using System.Linq;
using System.Text.RegularExpressions;
using NewTelSdk.Constants;
using NewTelSdk.Responses;
using Newtonsoft.Json;

namespace NewTelSdk.Requests
{
    public class StartPasswordCallRequest : BaseApiRequest<StartPasswordCallResponse, StartPasswordCallCallDetails>
    {
        private static readonly Regex Regex = new(CallPasswordApi.PhonePattern);

        public StartPasswordCallRequest(int async, string destinationNumber, string pin, int? timeout = null,
            string? callbackLink = null) : base(CallPasswordApi.StartPasswordCall)
        {
            if (string.IsNullOrEmpty(destinationNumber) || !Regex.IsMatch(destinationNumber))
            {
                throw new Exception($"Phone {destinationNumber} does not math pattern'{CallPasswordApi.PhonePattern}'");
            }

            if (string.IsNullOrEmpty(pin) || pin.Length != 5 || pin.Any(x => !char.IsDigit(x)))
            {
                throw new Exception("Pin is null or empty or not 5 length or has not only digits");
            }

            if (async != 0 && async != 1)
            {
                throw new Exception("Async has to be 0 or 1.");
            }

            Async = async;
            DestinationNumber = destinationNumber;
            Pin = pin;
            Timeout = timeout;
            CallbackLink = callbackLink;
        }


        /// <summary>
        ///     Флаг асинхронности запроса, число 0 или 1
        /// </summary>
        public int Async { get; }

        /// <summary>
        ///     Номер телефона назначения, строка, от 9 до 15 десятичных цифр, не начиная с 0
        /// </summary>
        [JsonProperty("dstNumber")]
        public string DestinationNumber { get; }

        /// <summary>
        ///     Код, который будет использован как последние 5 цифр номера источника,
        ///     строка, 5 десятичных цифр, необязательный параметр, в случае отсутствия
        ///     будет сгенерирован API-сервером
        /// </summary>
        public string Pin { get; }

        /// <summary>
        ///     время ожидания ответа в секундах, число от 20 до 99, необязательный
        ///     параметр, в случае отсутствия, время ожидания ответа будет установлено
        ///     20 секунд
        ///     при синхронном запросе (async=0) API-сервер сформирует ответ только после
        ///     получения статуса вызова, определение которого может продолжаться не более времени
        ///     ожидания ответа, указанного в параметре timeout; при асинхронном(async= 1) – ответ будет
        ///     сформирован сразу после обработки запроса, при этом статус вызова будет неопределён.
        /// </summary>
        public int? Timeout { get; }

        /// <summary>
        ///     Cсылка вызова callback при окончании звонка, строка, адрес http/https запроса,
        ///     необязательный параметр
        /// </summary>
        public string? CallbackLink { get; }
    }
}