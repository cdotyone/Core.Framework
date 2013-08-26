#region Copyright / Comments

// <copyright file="RememberPosition.cs" company="Civic Engineering & IT">Copyright © Civic Engineering & IT 2013</copyright>
// <author>Chris Doty</author>
// <email>cdoty@polaropposite.com</email>
// <date>6/4/2013</date>
// <summary></summary>

#endregion Copyright / Comments

using System;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms;

namespace Civic.Core.WindowsService
{
    [ProvideProperty( "RegistryKey", typeof( Form ) )]
    public partial class RememberPosition : Component, IExtenderProvider
    {
        #region Private Fields

        private bool _loadDone;
        private bool _inSize;
        private string _regKeyString = "";
        private Form _form;
        private FormWindowState _lastState = FormWindowState.Normal;
        private readonly Hashtable _properties = new Hashtable();

        #endregion Private Fields

        #region Construction

        public RememberPosition()
        {
            InitializeComponent();
        }

        public RememberPosition( IContainer container )
        {
            container.Add( this );

            InitializeComponent();
        }

        #endregion 

        #region Core Functionality

        private void formClosing( object sender, FormClosingEventArgs e )
        {
            var info = Screen.FromControl(_form);
            try
            {
                var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey( _regKeyString );
                if (key != null)
                {
                    key.SetValue( "display", parseScreenName( info.DeviceName ) );
                    key.SetValue( "state", _form.WindowState.ToString() );
                }
            }
            catch
            {
            }
        }

        private string parseScreenName( string name )
        {
            var buf = System.Text.Encoding.ASCII.GetBytes( name );
            var display = name;
            for ( var i = 0; i < buf.Length; i++ )
            {
                if (buf[i] != 0) continue;
                display = name.Substring( 0, i );
                break;
            }
            return display;
        }

        private void loadForm( object sender, EventArgs e )
        {
            Screen.FromControl( _form );
            try
            {
                var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey( _regKeyString );
                if (key != null)
                {
                    var display = (string)key.GetValue( "display", "" );

                    if ( !string.IsNullOrEmpty( display ) )
                    {
                        bool found = false;
                        foreach ( Screen s in Screen.AllScreens )
                        {
                            if (parseScreenName(s.DeviceName) != display) continue;

                            found = true;
                            break;
                        }

                        if ( found )
                        {
                            var state = (FormWindowState)Enum.Parse( typeof( FormWindowState ), (string)key.GetValue( "state", _form.WindowState.ToString() ) );
                            _lastState = state;

                            if ( state == FormWindowState.Normal )
                            {
                                _form.Left = int.Parse( (string)key.GetValue( "minX", _form.Left.ToString() ) );
                                _form.Top = int.Parse( (string)key.GetValue( "minY", _form.Top.ToString() ) );
                                _form.Width = int.Parse( (string)key.GetValue( "width", _form.Width.ToString() ) );
                                _form.Height = int.Parse( (string)key.GetValue( "height", _form.Height.ToString() ) );
                            }
                            else
                            {
                                _form.Left = int.Parse( (string)key.GetValue( "x", _form.Left.ToString() ) );
                                _form.Top = int.Parse( (string)key.GetValue( "y", _form.Top.ToString() ) );
                            }
                            _form.WindowState = state;
                        }
                    }
                }
            }
            catch
            {
            }

            _loadDone = true;
        }

        private void formSizeChanged( object sender, EventArgs e )
        {
            if ( !_loadDone ) return;

            try
            {
                var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey( _regKeyString );
                if (key != null && _lastState != _form.WindowState)
                {
                    _inSize = true;

                    if (_form.WindowState == FormWindowState.Normal)
                    {
                        var left = int.Parse((string) key.GetValue("minX", _form.Left.ToString()));
                        var top = int.Parse((string)key.GetValue("minY", _form.Top.ToString()));
                        var width = int.Parse((string)key.GetValue("width", _form.Width.ToString()));
                        var height = int.Parse((string)key.GetValue("height", _form.Height.ToString()));

                        if (_form.Left != left) _form.Left = left;
                        if (_form.Top != top) _form.Top = top;
                        if (_form.Left != left) _form.Left = left;
                        if (_form.Width != width) _form.Width = width;
                        if (_form.Height != height) _form.Height = height;
                    }
                    else
                    {
                        key.SetValue("x", _form.Left.ToString());
                        key.SetValue("y", _form.Top.ToString());
                    }

                    _lastState = _form.WindowState;

                    _inSize = false;
                }
            }
            catch
            {
            }
        }

        private void moveForm( object sender, EventArgs e )
        {
            if ( !_loadDone && !_inSize ) return;

            if ( _lastState == _form.WindowState )
            {
                var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey( _regKeyString );
                if ( key!=null && _form.WindowState == FormWindowState.Normal )
                {
                    key.SetValue( "minX", _form.Left.ToString() );
                    key.SetValue( "minY", _form.Top.ToString() );
                    key.SetValue( "width", _form.Width.ToString() );
                    key.SetValue( "height", _form.Height.ToString() );
                }
            }
        }
        #endregion

        #region IExtenderProvider Members

        public bool CanExtend( object extendee )
        {
            return extendee is Control;
        }

        #endregion

        #region properties class

        /// <summary>
        /// Define class of properties in case other properties are to be provided later
        /// </summary>
        private class properties
        {
            public string RegistryKey;

            public properties()
            {
                RegistryKey = string.Empty;
            }
        }

        #endregion

        #region ensurePropertiesExists
        /// <summary>
        /// Make sure the property has been initialized and entered in the hashtable
        /// </summary>
        /// <param name="key">the object that needs its properties</param>
        /// <returns>The properties belonging to the *key* object</returns>
        private properties ensurePropertiesExists( object key )
        {
            properties p;
            try
            {
                p = (properties)_properties[key];
            }
            catch ( Exception ex )
            {
                System.Diagnostics.Debug.Write( "error in designer: " + ex, "ensurePropertiesExists" );
                return null;
            }

            if ( p == null )
            {
                p = new properties();

                _properties[key] = p;
            }

            return p;
        }
        #endregion

        #region RegistryKey Property

        [Description( "Set this property to the location of the registray key under the users hive to store this forms last position settings." )]
        [Category( "Layout" )]
        public string GetRegistryKey( Form f )
        {
            return ensurePropertiesExists( f ).RegistryKey;
        }

        /// <summary>
        /// Set the RegistryKey property. 
        /// </summary>
        /// <param name="f">The Form changing its Stylesheet property</param>
        /// <param name="value">the new RegistryKey value</param>
        public void SetRegistryKey( Form f, string value )
        {
            _regKeyString = value;
            // set the Forms's RegistryKey property
            ensurePropertiesExists( f ).RegistryKey = value;

            if ( !DesignMode )
            {
                _form = f;
                _form.SizeChanged += formSizeChanged;
                _form.Move += moveForm;
                _form.FormClosing += formClosing;
                _form.Load += loadForm;
            }

        }

        #endregion RegistryKey Property
    }
}
