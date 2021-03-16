using System;

namespace NewTelSdk.Responses
{
    public abstract class BaseApiResponse<T> where T: ResponseData
    {
        /// <summary>
        ///     Статус запроса (success / error)
        /// </summary>
        public string Status { get; set; } = null!;
        
        /// <summary>
        /// Описание ошибки
        /// </summary>
        public string? Message { get; set; }
        public T? Data { get; set; }
        public bool IsSuccess => Status.Equals("success", StringComparison.OrdinalIgnoreCase);
    }

    public abstract class ResponseData
    {
        /// <summary>
        /// Результат работы запрашиваемого метода,
        /// </summary>
        public string Result { get; set; } = null!;

        public bool IsSuccess => Result.Equals("success", StringComparison.OrdinalIgnoreCase);
    }
}