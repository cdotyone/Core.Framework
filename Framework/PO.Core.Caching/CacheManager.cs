﻿using System;
using System.Threading;
using PO.Core.Caching.Configuration;
using PO.Core.Caching.Providers;
using PO.Core.Configuration;

namespace PO.Core.Caching
{
	public static class CacheManager
    {
		public static ICacheProvider Current
		{
			get
			{
				if( _current==null )
				{
					var config = CacheConfigurationSection.Current;
					Current = DynamicInstance.CreateInstance<ICacheProvider>(config.Assembly, config.Type);
				}

				if (_current == null) Current = new NoCacheProvider();

				return _current;
			}	
			set {
				lock (InternalSyncObject)
				{
					_current = value;
				}
			}
		}
		private static ICacheProvider _current;

		private static object InternalSyncObject
		{
			get
			{
				if (_sInternalSyncObject == null)
				{
					object obj2 = new object();
					Interlocked.CompareExchange(ref _sInternalSyncObject, obj2, null);
				}
				return _sInternalSyncObject;
			}
		}
		private static object _sInternalSyncObject;

        #region Methods

		public static TV ReadCache<TV>(string key, TV nullValue, CacheStore cacheStore = CacheStore.Session)
		{
			try
			{
				return Current.ReadCache(key, nullValue, cacheStore);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Error Accessing Cache Provider.\r\nKey:{0}\r\nCache Store:{1}\r\nError:{2}", key, cacheStore, ex.Message), ex);
			}
        }

		public static void WriteCache<TV>(string key, TV value, CacheStore cacheStore = CacheStore.Session)
        {
            try
			{
				if (cacheStore == CacheStore.DoNotStore) return;
				Current.WriteCache(key, value, cacheStore);
			}
            catch (Exception ex)
            {
            	throw new Exception(string.Format("Error Accessing Cache Provider.\r\nKey:{0}\r\nCache Store:{1}\r\nError:{2}", key, cacheStore, ex.Message), ex);
            }
        }

		public static void WriteCache<TV>(string key, TV value, TimeSpan decay, CacheStore cacheStore = CacheStore.Session)
		{
			try
			{
				if (cacheStore == CacheStore.DoNotStore) return;
				Current.WriteCache(key, value, decay, cacheStore);
			}
			catch (Exception ex)
			{
				throw new Exception(string.Format("Error Accessing Cache Provider.\r\nKey:{0}\r\nCache Store:{1}\r\nError:{2}", key, cacheStore, ex.Message), ex);
			}
		}

        #endregion
    }
}