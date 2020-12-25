using System;
using System.Collections.Generic;
using MTCG.Entity;
using Newtonsoft.Json;
using Npgsql;

namespace MTCG.Helpers
{
    public static class Utility
    {
        /// <summary>
        /// https://stackoverflow.com/questions/78536/deep-cloning-objects
        /// </summary>
        /// <param name="source"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CloneJson<T>(this T source)
        {            
            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            // initialize inner objects individually
            // for example in default constructor some list property initialized with some values,
            // but in 'source' these items are cleaned -
            // without ObjectCreationHandling.Replace default constructor values will be added to result
            var serializeSettings = new JsonSerializerSettings {ObjectCreationHandling = ObjectCreationHandling.Replace
                ,TypeNameHandling = TypeNameHandling.All,NullValueHandling = NullValueHandling.Include, DefaultValueHandling = DefaultValueHandling.Include};

            var temp = JsonConvert.SerializeObject(source, serializeSettings);
            var ret = JsonConvert.DeserializeObject<T>(temp,serializeSettings);
            return ret;
        }

      

        public static void GenerateIdForCard(this CardEntity source)
        {
            source.Id = Guid.NewGuid().ToString("N");
        }
        
        public static string SafeGetString(this NpgsqlDataReader reader, int colIndex)
        {
            if(!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
    }
}