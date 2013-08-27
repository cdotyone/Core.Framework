#region Copyright / Comments

// <copyright file="ServicesLoader.cs" company="Civic Engineering & IT">Copyright © Civic Engineering & IT 2013</copyright>
// <author>Chris Doty</author>
// <email>dotyc@civicinc.com</email>
// <date>6/4/2013</date>
// <summary></summary>

// AndersonImes

#endregion Copyright / Comments

using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Reflection;
using System.Windows.Forms;

namespace Civic.Core.WindowsService
{
    public static class ServicesLoader
    {
        internal enum ServiceOperation { Stop, Start, Pause, Continue };

        internal enum ServiceState { Stopped, Running, Paused };

        internal class ServiceInfo
        {
            readonly ServiceBase _service;

            public ServiceBase Service
            {
              get { return _service; }
            }

            public ServiceState State { get; set; }

            public Form SettingsForm { get; set; }

            public ServiceInfo(ServiceBase sb, ServiceState ss)
            {
                _service = sb;
                State = ss;
            }

        }

        public static void StartServices(ServiceBase[] servicesToRun)
        {
            StartServices(servicesToRun, false, false);
        }
        
        public static void StartServices(ServiceBase[] servicesToRun, bool autoStartAll, bool forceAppStart)
        {
            if (servicesToRun == null || servicesToRun.Length == 0)
            {
                throw new ArgumentException("servicesToRun cannot be null or empty", "servicesToRun");
            }

            if (System.Diagnostics.Debugger.IsAttached || forceAppStart)
            {
               showInterface(servicesToRun, autoStartAll);
            }
            else
            {
               ServiceBase.Run(servicesToRun);
            }

        }

        private static void showInterface(ServiceBase[] servicesToRun, bool autoStartAll)
        {

            var serviceInfos = new List<ServiceInfo>();
            for (var i = 0; i < servicesToRun.Length; i++)
            {               
                var si = new ServiceInfo(servicesToRun[i], ServiceState.Stopped);
                var service = servicesToRun[i] as IServiceSettings;
                if (service!=null) si.SettingsForm = service.SettingsForm;

                if (autoStartAll)
                {
                    CallMethodOnServiceInfo(ServiceOperation.Start, si);
                }
                serviceInfos.Add(si);
            }

            var controller = new ServicesController(serviceInfos);
            controller.WindowClosed += controllerClosed;
            controller.ShowDialog();
        }

        static void controllerClosed(ServiceInfo serviceInfo)
        {
            if(serviceInfo.State != ServiceState.Stopped)
                serviceInfo.Service.Stop();
        }

        internal static void CallMethodOnServiceInfo(ServiceOperation operation, ServiceInfo info)
        {
            callMethodOnService(operation, info.Service);
            switch (operation)
            {
                case ServiceOperation.Stop:
                    info.State = ServiceState.Stopped;
                    break;
                case ServiceOperation.Start:
                    info.State = ServiceState.Running;
                    break;
                case ServiceOperation.Pause:
                    info.State = ServiceState.Paused;
                    break;
                case ServiceOperation.Continue:
                    info.State = ServiceState.Running;
                    break;
                default:
                    break;
            }
        }

        private static void callMethodOnService(ServiceOperation operation, ServiceBase serviceBase)
        {
            var serviceBaseType = serviceBase.GetType();
            object[] parameters = null;
            if (operation == ServiceOperation.Start)
            {
                parameters = new object[] { null };
            }

            string methodName = "On" + Enum.GetName(typeof(ServiceOperation), operation);

            try
            {
                serviceBaseType.InvokeMember(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, serviceBase, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("An exception was thrown while trying to call the {0} of the {1} service.  Examine the inner exception for more information.", methodName, serviceBase.ServiceName), ex.InnerException);
            }
        }

    }
}
