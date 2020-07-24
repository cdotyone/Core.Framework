using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using Core.Framework.Configuration;

namespace Core.Framework
{
    public class EntityInfoManager
    {
        private static readonly ConcurrentDictionary<string,IEntityInfo> _entitiesFullName = new ConcurrentDictionary<string, IEntityInfo>();

        public static EntityConfig Configuration { get; private set; } = new EntityConfig();

        public static IEntityInfo GetInfo(string module,string entity)
        {
            var allEntities = Configuration.Entities;

            if (!allEntities.ContainsKey(module))
            {
                allEntities.TryAdd(module,new ConcurrentDictionary<string, IEntityInfo>());
                return null;
            };
            var entities = allEntities[module];
            return !entities.ContainsKey(entity) ? null : entities[entity];
        }

        public static void SetInfo(IEntityInfo info)
        {
            var allEntities = Configuration.Entities;

            var module = info.Module;
            if (!allEntities.ContainsKey(module))
            {
                allEntities.TryAdd(module,new ConcurrentDictionary<string, IEntityInfo>());
            };
            allEntities[module][info.Entity] = info;
        }

        public static IEntityInfo GetInfo(Type type)
        {
            return _entitiesFullName.ContainsKey(type.FullName ?? throw new InvalidOperationException()) ? _entitiesFullName[type.FullName] : null;
        }
        
        public static IEntityInfo GetInfo<T>(T entity) where T : IEntityIdentity
        {
            var info = GetInfo(entity.GetType());
            if (info!=null)
            {
                return info;
            }

            return GetInfo(entity._module,  entity._entity);
        }

        public static IEntityInfo GetInfo<T>()
        {
            var t = typeof(T);
            var name = t.FullName;
            if (_entitiesFullName.ContainsKey(name ?? throw new InvalidOperationException()))
            {
                return _entitiesFullName[name];
            }

            IEntityInfo info = new EntityInfo
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

            var loadedInfo = GetInfo(info.Module, info.Entity);
            if (loadedInfo != null)
            {
                info = loadedInfo;
            }
            else
            {
                SetInfo(info);
            }
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
                    prop.PropertyType = property.PropertyType;

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
                    propertyInfo.Type = property.PropertyType.Name;
                    if (propertyInfo.Type.StartsWith("Nullable`"))
                    {
                        propertyInfo.IsNullable = true;
                        propertyInfo.Type = Nullable.GetUnderlyingType(property.PropertyType).Name.ToLowerInvariant();
                    }
                    if (propertyInfo.Type.StartsWith("IEnumerable`"))
                    {
                        propertyInfo.Type = "IEnumerable<"+ property.PropertyType.GetGenericArguments()[0].Name + ">";
                    }
                    if (propertyInfo.Type.StartsWith("List`"))
                    {
                        propertyInfo.Type = "List<"+ property.PropertyType.GetGenericArguments()[0].Name + ">";
                    }
                    if (propertyInfo.Type.StartsWith("Dictionary`"))
                    {
                        propertyInfo.Type = "Dictionary<"+ property.PropertyType.GetGenericArguments()[0].Name + ","+ property.PropertyType.GetGenericArguments()[1].Name + ">";
                    }

                    propertyInfo.Type = propertyInfo.Type.Replace("String", "string").Replace("Int32", "int").Replace("int32", "int").Replace("int64", "long").Replace("Int64", "long");

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
                    if (prop.ForceUpperCase.HasValue && prop.ForceUpperCase.Value)
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



/*        public static bool GetCanView(string schema, string name)
        {
            var info = GetInfo(schema, name);
            return GetCanView(info);
        }

        public static bool GetCanAdd(string schema, string name)
        {
            var info = GetInfo(schema, name);
            return GetCanAdd(info);
        }

        public static bool GetCanModify(string schema, string name)
        {
            var info = GetInfo(schema, name);
            return GetCanModify(info);
        }

        public static bool GetCanRemove(string schema, string name)
        {
            var info = GetInfo(schema, name);
            return GetCanRemove(info);
        }*/

        public static int GetMaxRows(IEntityInfo info)
        {
            if (info != null)
            {
                if (info.Max.HasValue) return info.Max.Value;
            }
            return Configuration.Defaults.Max;
        }

        public static bool GetCanView(IEntityInfo info)
        {
            if (info == null) return false;
            if (info.CanView.HasValue) return info.CanView.Value;
            return Configuration.Defaults.CanView;
        }

        public static bool GetCanAdd(IEntityInfo info)
        {
            if (info == null) return false;
            if (info.CanAdd.HasValue) return info.CanAdd.Value;
            return Configuration.Defaults.CanAdd;
        }

        public static bool GetCanModify(IEntityInfo info)
        {
            if (info == null) return false;
            if (info.CanModify.HasValue) return info.CanModify.Value;
            return Configuration.Defaults.CanModify;
        }

        public static bool GetCanRemove(IEntityInfo info)
        {
            if (info == null) return false;
            if (info.CanRemove.HasValue) return info.CanRemove.Value;
            return Configuration.Defaults.CanRemove;
        }
    }
}
