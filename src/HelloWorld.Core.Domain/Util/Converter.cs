using System;
using Newtonsoft.Json.Linq;
using HelloWorld.Core.Domain.Interfaces.Entity;

namespace HelloWorld.Core.Domain.Util
{
    public static class Converter
    {
        public static TEntity ConvertTo<TEntity>(dynamic request) where TEntity : IEntity
        {
            if (!(request is JObject jObject)) throw new InvalidCastException();

            var objectConverted = jObject.ToObject<TEntity>();

            if (objectConverted == null) throw new InvalidCastException();
            
            return objectConverted;
        }


    }
}