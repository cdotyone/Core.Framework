using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace SAAS.Core.Framework
{
    public class PropertyMapper
    {
        private static readonly ConcurrentDictionary<string,IEntityInfo> _entities = new ConcurrentDictionary<string, IEntityInfo>();
        private static readonly ConcurrentDictionary<string,IEntityInfo> _entitiesFullName = new ConcurrentDictionary<string, IEntityInfo>();

        public static IEntityInfo GetInfo(string name)
        {
            return _entities.ContainsKey(name) ? _entities[name] : null;
        }

        public static IEntityInfo GetInfo(Type type)
        {
            return _entitiesFullName.ContainsKey(type.FullName ?? throw new InvalidOperationException()) ? _entitiesFullName[type.FullName] : null;
        }

        public static IEntityInfo GetInfo<T>(T entity) where T : IEntityIdentity
        {
            var info = GetInfo<T>();
            if (info!=null)
            {
                return info;
            }

            return GetInfo(entity._module + entity._entity);
        }

        public static IEntityInfo GetInfo<T>()
        {
            var t = typeof(T);
            var name = t.FullName;
            if (_entities.ContainsKey(name ?? throw new InvalidOperationException()))
            {
                return _entitiesFullName[name];
            }

            var info = new EntityInfo
            {
                Entity = t.Name.ToLowerInvariant(), Properties = new Dictionary<string, IEntityPropertyInfo>()
            };

            var attributes = t.GetCustomAttributes(true);
            foreach (var attribute in attributes)
            {
                if (attribute is ModuleAttribute moduleAttribute)
                {
                    info.Module = moduleAttribute.Name;
                }
                if (attribute is DataContractAttribute contractAttribute)
                {
                    info.Entity = contractAttribute.Name;
                }
                if (attribute is RelatedAttribute relatedAttribute)
                {
                    info.RelatedModule = relatedAttribute.RelatedModule;
                    info.RelatedEntity = relatedAttribute.RelatedEntity;
                }
            }

            info.Name = info.Module + "." + info.Entity;

            _entities[info.Name] = info;
            _entitiesFullName[name] = info;

            Map<T>(info);

            return info;
        } 

        public static void Map<T>(IEntityInfo info)
        {
            if (info.Mapped) return;

            if(info.Properties==null) info.Properties = new Dictionary<string, IEntityPropertyInfo>();
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

                var found = false;
                var name = property.Name;

                foreach (var propertyInfo in properties)
                {
                    if (propertyInfo.Key != name) continue;

                    var prop = propertyInfo.Value;
                    prop.Set = setter;
                    prop.Get = getter;

                    found = true;
                    break;
                }

                if (!found && name!="_key" && name!="_module" && name!="_entity" && name!="_extra")
                {
                    var propertyInfo = new EntityPropertyInfo();

                    string lowerName = null;
                    foreach (var attribute in property.GetCustomAttributes(true))
                    {
                        if (attribute is DataMemberAttribute dataMemberAttribute)
                        {
                            lowerName = dataMemberAttribute.Name;
                        }
                        if (attribute is DefaultValueAttribute defaultValueAttribute)
                        {
                            if (defaultValueAttribute.Value != null)
                            {
                                propertyInfo.Default = defaultValueAttribute.Value.ToString();
                            }
                        }
                        if (attribute is PrimaryKeyAttribute)
                        {
                            propertyInfo.IsKey = true;
                        }
                    }
                    if( string.IsNullOrEmpty(lowerName) ) lowerName = name.Substring(0, 1).ToLowerInvariant() + name.Substring(1);

                    propertyInfo.Name = lowerName;
                    propertyInfo.Type = property.PropertyType.Name.ToLowerInvariant();
                    propertyInfo.PropertyType = property.PropertyType;
                    propertyInfo.Set = setter;
                    propertyInfo.Get = getter;

                    properties[name] = propertyInfo;
                }
            }

            info.Mapped = true;
        }

        public static void ApplyDefaults<T>(T entity) where T : IEntityIdentity
        {
            var info = GetInfo(entity);

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
