using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NewTelSdk.Constants;
using NewTelSdk.Requests;
using NewTelSdk.Responses;
using NewTelSdk.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NewTelSdk
{
    public class CallPasswordApiClient
    {
        private static readonly HttpClient HttpClient = new();

        private static readonly JsonSerializerSettings JsonSettings = new()
        {
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private readonly string _accessKey;
        private readonly string _signatureKey;

        public CallPasswordApiClient(string accessKey, string signatureKey)
        {
            _accessKey = accessKey;
            _signatureKey = signatureKey;
        }


        /// <summary>
        ///     Позволяет переключать SDK с тестового режима и обратно. По-умолчанию выключен
        /// </summary>
        public static bool IsDeveloperMode { get; set; }

        /// <summary>
        ///     Метод осуществляет вызов “CallPassword” на номер назначения, отображая в качестве источника
        ///     номер из пула системы “CallPassword” для использования авторизации по звонку
        /// </summary>
        /// <param name="destinationNumber"> Номер телефона назначения, строка, от 9 до 15 десятичных цифр, не начиная с 0</param>
        /// <param name="pin">
        ///     Код, который будет использован как последние 5 цифр номера источника,
        ///     строка, 5 десятичных цифр, необязательный параметр, в случае отсутствия
        ///     будет сгенерирован API-сервером
        /// </param>
        public async Task<StartPasswordCallResponse> StartPasswordCallAsync(string destinationNumber, string pin)
        {
            var request = new StartPasswordCallRequest(1, destinationNumber, pin);
            return await SendRequestAsync<StartPasswordCallResponse, StartPasswordCallCallDetails,
                StartPasswordCallRequest>(request);
        }

        private async Task<TResponse> SendRequestAsync<TResponse, TResponseData, TRequest>(TRequest request)
            where TResponse : BaseApiResponse<TResponseData>
            where TRequest : BaseApiRequest<TResponse, TResponseData>
            where TResponseData : ResponseData
        {
            var json = JsonConvert.SerializeObject(request, JsonSettings);
            var url = GetUrl(request.MethodApi);
            using var content = new StringContent(json, Encoding.UTF8, CallPasswordApi.Json);
            var requestKey = CreateRequestKey(request.MethodApi, DateTime.UtcNow, json);
            HttpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {requestKey}");
            var response = await HttpClient.PostAsync(url, content);
            var responseContent = response.Content;
            var responseJson = await responseContent.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseJson);
        }

        /// <summary>
        ///     Функция для вычисления ключа запроса
        /// </summary>
        /// <param name="methodName">Наименование метода</param>
        /// <param name="time">Метка времени запроса</param>
        /// <param name="body">строка параметров запроса</param>
        private string CreateRequestKey(string methodName, DateTime time, string body)
        {
            var secondsSinceUnixEpoch = (int) (time - new DateTime(1970, 1, 1)).TotalSeconds;
            var stringToHash = $"{methodName}\n{secondsSinceUnixEpoch}\n{_accessKey}\n{body}\n{_signatureKey}";
            return $"{_accessKey}{secondsSinceUnixEpoch}{CryptoUtils.Sha256(stringToHash)}";
        }

        /// <summary>
        ///     Возвращает базовый Url для переданного названия метода запроса.
        ///     Зависит от режима работы SDK [NewTelOptions.IsDeveloperMode]
        /// </summary>
        private static string GetUrl(string apiMethod)
        {
            var url = IsDeveloperMode ? CallPasswordApi.ApiUrlDebug : CallPasswordApi.ApiUrlRelease;
            return url.Replace("[0]", apiMethod);
        }
    }
}