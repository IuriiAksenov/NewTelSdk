using NewTelSdk.Responses;
using Newtonsoft.Json;

namespace NewTelSdk.Requests
{
    public abstract class BaseApiRequest<TResponse, TResponseData> where TResponse : BaseApiResponse<TResponseData> where TResponseData: ResponseData
    {
        protected BaseApiRequest(string methodApi)
        {
            MethodApi = methodApi;
        }

        /// <summary>
        ///     Метод url
        /// </summary>
        [JsonIgnore]
        public string MethodApi { get; }
    }
}