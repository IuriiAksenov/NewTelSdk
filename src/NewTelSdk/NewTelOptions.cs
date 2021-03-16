namespace NewTelSdk
{
    public class NewTelOptions
    {
        /// <summary>
        /// Ключ авторизации
        /// </summary>
        public string AccessKey { get; set; } = null!;

        /// <summary>
        /// Ключ подписи
        /// </summary>
        public string SignatureKey { get; set; } = null!;
    }
}