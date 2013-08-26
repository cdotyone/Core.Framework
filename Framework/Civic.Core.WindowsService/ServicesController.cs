#region Copyright / Comments

// <copyright file="ServicesController.cs" company="Civic Engineering & IT">Copyright © Civic Engineering & IT 2013</copyright>
// <author>Chris Doty</author>
// <email>cdoty@polaropposite.com</email>
// <date>6/4/2013</date>
// <summary></summary>

#endregion Copyright / Comments

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Civic.Core.WindowsService.Properties;

namespace Civic.Core.WindowsService
{
    internal partial class ServicesController : Form
    {
        public delegate void ServicesControllerClose(ServicesLoader.ServiceInfo serviceInfo);
        public event ServicesControllerClose WindowClosed;
        private readonly List<ServicesLoader.ServiceInfo> _serviceInfos;
        
        public ServicesController(List<ServicesLoader.ServiceInfo> serviceInfos)
        {
            _serviceInfos = serviceInfos;
            InitializeComponent();
            
            RefreshList();
        }

        private void RefreshList()
        {
            _lvServices.Enabled = false;
            var services = GetAutoLoadServices();

            for (var i = 0; i < _serviceInfos.Count; i++)
            {
                var info = _serviceInfos[i];
                var name = _serviceInfos[i].Service.ServiceName;
                var item = new ListViewItem(name);
                item.SubItems.Add(Enum.GetName(typeof(ServicesLoader.ServiceState), info.State));
                _lvServices.Items.Add(item);
                item.Checked = services.Contains(name);
            }
            _lvServices.Enabled = true;
        }

        private void BtnExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnPlayPauseClick(object sender, EventArgs e)
        {
            var info = GetCurrentServiceInfo();
            if (info != null)
            {
                switch (info.State)
                {
                    case ServicesLoader.ServiceState.Running:
                        ServicesLoader.CallMethodOnServiceInfo(ServicesLoader.ServiceOperation.Pause, info);
                        break;
                    case ServicesLoader.ServiceState.Stopped:
                        ServicesLoader.CallMethodOnServiceInfo(ServicesLoader.ServiceOperation.Start, info);
                        break;
                    default:
                        ServicesLoader.CallMethodOnServiceInfo(ServicesLoader.ServiceOperation.Continue, info);
                        break;
                }
            }

            UpdateItemInfo();
        }

        private void UpdateItemInfo()
        {
            for (var i = 0; i < _lvServices.Items.Count; i++)
            {
                var item = _lvServices.Items[i];

                for (var j = 0; j < _serviceInfos.Count; j++)
                {
                    if (_serviceInfos[j].Service.ServiceName == item.SubItems[0].Text)
                        item.SubItems[1].Text = Enum.GetName(typeof(ServicesLoader.ServiceState), _serviceInfos[j].State);
                }
            }
            
            RefreshToolbar();
        }

        private void BtnStopClick(object sender, EventArgs e)
        {
            var info = GetCurrentServiceInfo();
            if (info != null)
            {
                ServicesLoader.CallMethodOnServiceInfo(ServicesLoader.ServiceOperation.Stop, info);
            }

            UpdateItemInfo();
        }

        private void BtnRestartClick(object sender, EventArgs e)
        {
            var info = GetCurrentServiceInfo();
            if (info != null)
            {
                ServicesLoader.CallMethodOnServiceInfo(ServicesLoader.ServiceOperation.Stop, info);
                ServicesLoader.CallMethodOnServiceInfo(ServicesLoader.ServiceOperation.Start, info);
            }

            UpdateItemInfo();
        }

        private ServicesLoader.ServiceInfo GetCurrentServiceInfo()
        {
            if (_lvServices.SelectedIndices.Count > 0)
            {
                var index = _lvServices.SelectedIndices[0];
                return _serviceInfos[index];
            }
            return null;
        }

        private void LvServicesSelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshToolbar();
        }

        private void RefreshToolbar()
        {
            var info = GetCurrentServiceInfo();
            if (info != null)
            {
                switch (info.State)
                {
                    case ServicesLoader.ServiceState.Stopped:
                        _btnPlayPause.Image = Resources.Play;
                        _btnPlayPause.Enabled = true;
                        _btnStop.Enabled = false;
                        _btnRestart.Enabled = false;
                        break;
                    case ServicesLoader.ServiceState.Running:
                        _btnPlayPause.Image = Resources.Pause;
                        _btnPlayPause.Enabled = info.Service.CanPauseAndContinue;
                        _btnStop.Enabled = true;
                        _btnRestart.Enabled = true;
                        break;
                    case ServicesLoader.ServiceState.Paused:
                        _btnPlayPause.Image = Resources.Play;
                        _btnPlayPause.Enabled = true;
                        _btnStop.Enabled = true;
                        _btnRestart.Enabled = true;
                        break;
                }
            }
            else
            {
                // Disable buttons if nothing is selected
                _btnPlayPause.Enabled = false;
                _btnStop.Enabled = false;
                _btnRestart.Enabled = false;
            }

            _btnSettings.Visible = info.SettingsForm != null;
        }

        private void ServicesControllerFormClosed(object sender, FormClosedEventArgs e)
        {
            if (WindowClosed == null) return;

            foreach (var si in _serviceInfos)
            {
                WindowClosed(si);                    
            }
        }

        private void LvServicesItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                if(!_lvServices.Enabled) return;

                var services = GetAutoLoadServices();
                if (e.Item.Checked) services.Add(e.Item.SubItems[0].Text);
                else services.Remove(e.Item.SubItems[0].Text);

                var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(_rememberPosition.GetRegistryKey(this));
                if (key != null) key.SetValue("autoload", string.Join(",", services.ToArray()));
            }
            catch
            {
            }
        }

        private List<string> GetAutoLoadServices()
        {
            var services = new List<string>();

            try
            {
                var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(_rememberPosition.GetRegistryKey(this));
                if (key != null)
                {
                    var sval = (string)key.GetValue("autoload", "");
                    services = new List<string>(sval.Split(','));
                    services.Remove("");
                }
            }
            catch
            {
            }

            return services;
        }

        private void ServicesControllerLoad(object sender, EventArgs e)
        {
            var services = GetAutoLoadServices();
            
            if (services.Count <= 0) return;
            for (var i = 0; i < _serviceInfos.Count; i++)
            {
                var info = _serviceInfos[i];
                if (services.Contains(info.Service.ServiceName))
                    ServicesLoader.CallMethodOnServiceInfo(ServicesLoader.ServiceOperation.Start, info);
            }

            UpdateItemInfo();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var info = GetCurrentServiceInfo();
            if (info.SettingsForm != null)
                info.SettingsForm.ShowDialog(this);
        }
    }
}