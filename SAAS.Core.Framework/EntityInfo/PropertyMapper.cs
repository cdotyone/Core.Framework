using System;

namespace SAAS.Core.Framework
{
    public class PropertyMapper
    {
        public static void Map<T>(IEntityInfo info)
        {
            if (info.Mapped) return;

            var properties = info.Properties;

            foreach (var property in typeof(T).GetProperties())
            {
                var setterType = typeof(Action<,>).MakeGenericType( typeof(T), property.PropertyType);
                Delegate setter = null;
                if(property.SetMethod!=null)
                    setter = Delegate.CreateDelegate(setterType, null, property.GetSetMethod());

                var getterType = typeof(Func<,>).MakeGenericType(typeof(T), property.PropertyType);
                Delegate getter = null;
                if(property.GetMethod!=null)
                    getter = Delegate.CreateDelegate(getterType, null, property.GetGetMethod());

                foreach (var propertyInfo in properties)
                {
                    if (propertyInfo.Key != property.Name) continue;

                    var prop = propertyInfo.Value;
                    prop.Set = setter;
                    prop.Get = getter;
                    break;
                }
            }

            info.Mapped = true;
        }

        public static void ApplyDefaults<T>(T entity) where T : IEntityIdentity
        {
            var info = EntityCreateFactory.GetInfo(entity);
            Map<T>(info);

            var properties = info.Properties;

            foreach (var propertyInfo in properties)
            {
                var prop = propertyInfo.Value;
                var getter = prop.Get;
                var setter = prop.Set;
                var value = getter.DynamicInvoke(entity);

                var change = false;
                if (value == null)
                {
                    value = prop.Default.ConvertTo(prop.PropertyType);
                    change = true;
                }

                if (prop.Type == "string")
                {
                    var sval = value.ToString();
                    if (prop.ForceUpperCase)
                    {
                        sval = sval.ToUpperInvariant();
                        change = true;
                    }
                    if (prop.MaxLength.HasValue && prop.MaxLength.Value > 0 && sval.Length > prop.MaxLength)
                    {
                        sval = sval.Substring(0, prop.MaxLength.Value);
                        change = true;
                    }
                }

                if(change) setter.DynamicInvoke(entity, value);
            }
        }
    }
}
