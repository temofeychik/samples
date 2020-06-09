namespace Common.Systems
{
    using System;

    public static class EnumExtensions
    {
        public static bool IsNull(this Enum request)
        {
            return request == null;
        }

        public static bool IsNotNull(this Enum request)
        {
            return !request.IsNull();
        }

        public static bool IsDefault(this Enum request)
        {
            return request.IsNotNull() && request.Equals(0);
        }

        public static bool IsNotDefault(this Enum request)
        {
            return !request.IsDefault();
        }

        public static bool IsNullOrDefault(this Enum request)
        {
            return request.IsDefault();
        }

        public static bool IsNotNullOrDefault(this Enum request)
        {
            return !request.IsNullOrDefault();
        }

        public static T ToEnum<T>(this int request) where T : Enum
        {
            return (T)Enum.ToObject(typeof(T), request);
        }

        public static T ToEnum<T>(this int? request) where T : Enum
        {
            if (request == null)
            {
                return default;
            }

            return (T)Enum.ToObject(typeof(T), request);
        }

        public static T ToEnum<T>(this string request) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), request);
        }

        public static object ToEnum(this int? request, Type enumType)
        {
            if (request == null)
            {
                return default;
            }

            return Enum.ToObject(enumType, request);
        }

        public static object ToEnum(this string request, Type enumType)
        {
            return Enum.Parse(enumType, request);
        }
    }
}
