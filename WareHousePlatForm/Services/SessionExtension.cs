﻿using System.Text.Json;

namespace wareHouse.Services
{
    public static class SessionExtension
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            var data = JsonSerializer.Serialize(value);
            session.SetString(key, data);
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}