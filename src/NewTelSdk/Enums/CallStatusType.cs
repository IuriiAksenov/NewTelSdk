using System;

namespace NewTelSdk.Enums
{
    public static class CallStatusTypeConverter
    {
        public static CallStatusType FromString(string? status)
        {
            if (status is null)
            {
                return CallStatusType.Null;
            }

            return status switch
            {
                "null" => CallStatusType.Null,
                "answered" => CallStatusType.Answered,
                "busy" => CallStatusType.Busy,
                "no answer" => CallStatusType.NoAnswer,
                "no such number" => CallStatusType.NoSuchNumber,
                "not available" => CallStatusType.NotAvailable,
                _ => throw new ArgumentOutOfRangeException($"{nameof(CallStatusType)} has no value: '{status}'")
            };
        }
    }

    public enum CallStatusType
    {
        Null,
        Answered,
        Busy,
        NoAnswer,
        NoSuchNumber,
        NotAvailable
    }
}