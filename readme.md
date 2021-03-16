# Неофициальный SDK для работы с NewTel Call Password Api (Авторизация по звонку) / Unofficial SDK for New Tel Call Password (Call authorization)

New Tel SDK позволяет создать авторизацию на основе входящего звонка.

Официальный сайт, используемой в SDK API [NEW-TEL][official-call-password].

Возможности SDK - осуществление вызова “CallPassword” на номер назначения, отображая в качестве источника номер из пула системы “CallPassword” для использования авторизации по звонку.

### Требования
Для работы c New Tel Sdk необходимо пройти [регистрацию][official-call-password] для получения access key и signature key.

### Подготовка к работе
Для начала работы с SDK вам понадобятся:
* Access key
* Signature key

Которые выдаются после [регистрации][official-call-password].

SDK позволяет настроить режим работы (debug/prod). По умолчанию - режим prod.
Чтобы настроить debug режим, установите параметры:
```csharp
SmsApiClient.IsDeveloperMode = true // используется тестовый ваш URL
```

### Пример работы
Для проведения авторизации по звонку необходимо вызвать метод **StartPasswordCallAsync**, в который необходимо передать номер назначения и код подтверждения.

### Структура
SDK состоит из следующих модулей:

#### Core
Является базовым модулем для работы с [New Tel Call Password API][official-call-password]. Модуль реализует протокол взаимодействия с сервером и позволяет не осуществлять прямых обращений в API.

Основной класс модуля - **CallPasswordApiClient** - предоставляет интерфейс для взаимодействия с Prostor SMS API. Для работы необходимы логин и пароль (см. **Подготовка к работе**).

### Поддержка
- По возникающим вопросам просьба обращаться на [iurii.aksenov@yandex.ru][support-email]
- Баги и feature-реквесты можно направлять в раздел [issues][issues]

[official-call-password]: https://new-tel.net/uslugi/call-password/
[support-email]: mailto:iurii.aksenov@yandex.ru
[issues]: https://github.com/IuriiAksenov/NewTelSdk/issues
