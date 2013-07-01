using System.Globalization;
using System.Resources;
using System.Threading;

namespace PO.Core.Configuration
{
    internal class StringResources
    {
		private static object _internalSyncObject;
		private static StringResources _loader;
		private readonly ResourceManager _resources;

		StringResources()
		{
			_resources = new ResourceManager("PO.Core.Configuration.Properties.Resources", GetType().Assembly);
		}

		private static StringResources GetLoader()
		{
			if (_loader == null)
			{
				lock (InternalSyncObject)
				{
					if (_loader == null)
					{
						// ReSharper disable PossibleMultipleWriteAccessInDoubleCheckLocking
						_loader = new StringResources();
						// ReSharper restore PossibleMultipleWriteAccessInDoubleCheckLocking
					}
				}
			}

			return _loader;
		}

		public static string GetString(string name)
		{
			StringResources loader = GetLoader();
			if (loader == null)
				return null;

			return loader._resources.GetString(name, Culture);
		}

		private static CultureInfo Culture
		{
			get { return null; }
		}

		private static object InternalSyncObject
		{
			get
			{
				if (_internalSyncObject == null)
				{
					var obj2 = new object();
					Interlocked.CompareExchange(ref _internalSyncObject, obj2, null);
				}
				return _internalSyncObject;
			}
		}
    }
}
