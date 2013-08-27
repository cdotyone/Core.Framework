#region Copyright / Comments

// <copyright file="AssemblyInfo.cs" company="Civic Engineering & IT">Copyright © Civic Engineering & IT 2013</copyright>
// <author>Chris Doty</author>
// <email>dotyc@civicinc.com</email>
// <date>6/4/2013</date>
// <summary></summary>

#endregion Copyright / Comments

#region References

using System;
using System.Collections.Generic;
using System.Threading;
using Civic.Core.Data;

#endregion References

namespace Civic.Core.Logging.DB
{
    /// <summary>
    /// Database Exception and Trace Logging Writer
    /// </summary>
    public class DBClient : ILogWriter
    {
        #region Fields

        private Dictionary<string, string> _additionalParameters;
        private bool _canThread;
        private string _dbcode;
        private int _errorCount;
        private string _errorDateTimeParamName;
        private string _errorLineNumParamName;
        private string _errorLocationParamName;
        private string _errorMessageParamName;

        [NonSerialized]
        private AutoResetEvent _logWaiting;

        private bool _running;
        private AutoResetEvent _shutDown;
        private string _spName;
        private Thread _tm;
        private Queue<DBLogMessageUnit> eventQueue;

        #endregion Fields

        #region Constructors

        public DBClient()
        {
            _logWaiting = new AutoResetEvent(false);
            _shutDown = new AutoResetEvent(false);
            eventQueue = new Queue<DBLogMessageUnit>();
        }

        ~DBClient()
        {
            Shutdown();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// gets the application name given to this logger
        /// </summary>
        /// <value></value>
        public string ApplicationName { get; private set; }

        /// <summary>
        /// true if the ILogWriter supports a delete command
        /// false if it does not
        /// </summary>
        /// <value></value>
        public bool CanDelete
        {
            get { return false; }
        }

        /// <summary>
        /// gets the log name given to this log
        /// </summary>
        /// <value></value>
        public string LogName { get; private set; }

        /// <summary>
        /// gets the display name given to this logger
        /// </summary>
        /// <value>Always Returns "Databsase Logger"</value>
        public string Name
        {
            get { return "Database Logger"; }
        }

        /// <summary>
        /// gets current log entries left to process
        /// </summary>
        protected int PendingLogs
        {
            get { return eventQueue.Count; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initliazes the logger
        /// </summary>
        /// <param name="applicationname">Name of the application using this log</param>
        /// <param name="logname">Name of the log, this can be interperted the way the class want to, but it must identify a unique logger.</param>
        /// <param name="canThread">should the logger us a thread, generally false is suggested for web sites</param>
        /// <param name="additionalParameters">any additional configuration parameters found on the configuration node for this logger</param>
        public ILogWriter Create( string applicationname, string logname, bool canThread, Dictionary<string, string> additionalParameters )
        {
            _canThread = canThread;
            DBClient sl = new DBClient();

            sl.ApplicationName = applicationname;
            sl.LogName = logname;

            try { sl._errorMessageParamName = additionalParameters["MessageParamName"]; }
            catch { sl._errorMessageParamName = "ERROR_MESSAGE"; }

            try { sl._errorLineNumParamName = additionalParameters["LineNumParamName"]; }
            catch { sl._errorLineNumParamName = "LOCATION_LINE"; }

            try { sl._errorLocationParamName = additionalParameters["MessageParamName"]; }
            catch { sl._errorLocationParamName = "LOCATION"; }

            try { sl._errorDateTimeParamName = additionalParameters["DateTimeParamName"]; }
            catch { sl._errorDateTimeParamName = "MODIFIED"; }

            try
            {
                sl._dbcode = additionalParameters["DBCode"];
            }
            catch
            {
                throw new Exception(  "DBCode must be configured for the POKE.Lib.Logger.DBClient logger" );
            }

            try
            {
                sl._spName = additionalParameters["ProcedureName"];
            }
            catch
            {
                throw new Exception( "ProcedureName must be configured for the POKE.Lib.Logger.DBClient logger" );
            }

            sl._additionalParameters = additionalParameters;

            if ( _canThread )
            {
                sl._tm = new Thread( Process );
                sl._tm.Name = "File Logger Process";
                sl._tm.Start();
            }

            return sl;
        }

        /// <summary>
        /// On logs that can be deleted.
        /// This will delete the log
        /// </summary>
        public void Delete()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Flush()
        {
        }

        /// <summary>
        /// Logs a message to the log class
        /// </summary>
        /// <param name="message">the message to write the the log</param>
        /// <returns></returns>
        public bool Log(ILogMessage message)
        {
            DBLogMessageUnit dblmu = new DBLogMessageUnit( message );

            if ( dbLog( dblmu ) )
                return true;

            if ( _canThread )
            {
                // Lock for writing
                lock ( eventQueue )
                {
                    eventQueue.Enqueue( dblmu );
                }
                _logWaiting.Set();

                if ( !_running ) RunQueue();
                return true;
            }

            return false;
        }

        /// <summary>
        /// shuts down and cleans up after logger
        /// </summary>
        public void Shutdown()
        {
            _running = false;

            if ( _canThread )
            {
                // Set shutdown flag
                _shutDown.Set();
                _logWaiting.Set();
                Thread.Sleep( 100 );
            }

            // Double check there are no more events on queue, if so, run them.
            if ( PendingLogs > 0 )
            {
                RunQueue();
            }

            if ( _canThread )
            {
                try
                {
                    _tm.Abort();
                    _tm = null;
                }
                catch
                {
                    // Do nothing...
                }
            }
        }

        /// <summary>
        /// Process the queue on a thread
        /// </summary>
        protected void Process()
        {
            int[] sleepTime = { 100, 60000, 300000, 600000 };

            _running = true;
            // Loop and monitor every second.
            while ( _logWaiting.WaitOne() )
            {
                if ( _shutDown.WaitOne( 0, true ) )
                    break;
                try
                {
                    RunQueue();
                }
                catch
                {
                    // Catch everything and do nothing
                }

                if ( _errorCount > 3 ) Thread.Sleep( sleepTime[sleepTime.Length-1] );
                else Thread.Sleep( sleepTime[_errorCount] ); // slow us down
            }
        }

        /// <summary>
        /// Run any events currently on queue
        /// </summary>
        protected void RunQueue()
        {
            // Loop until queue is empty
            while ( PendingLogs > 0 )
            {
                DBLogMessageUnit m;

                // Lock for writing
                lock ( eventQueue )
                {
                    m = eventQueue.Dequeue();
                }

                // Output log line
                if ( m != null )
                {
                    if ( !dbLog( m ) )
                    {
                        // something went wroung, send log entry to bottom of queue
                        lock ( eventQueue )
                        {
                            eventQueue.Enqueue( m );
                        }

                        //  bounce out and force a wait
                        _errorCount++;
                        break;
                    }
                }
            }
        }

        private bool dbLog( DBLogMessageUnit messageUnit )
        {
            try
            {
                ILogMessage message = messageUnit.Message;

                IDBConnection conn = DatabaseFactory.CreateDatabase( _dbcode );
                IDBCommand command = conn.CreateStoredProcCommand( _spName );

                foreach ( string key in _additionalParameters.Keys )
                {
                    string value = _additionalParameters[key];
                    if ( !string.IsNullOrEmpty( value ) )
                    {
                        command.AddInParameter( key, value );
                    }
                }

                var otherParameters = message.Extended;
                foreach ( string key in otherParameters.Keys )
                {
                    string value = otherParameters[key].ToString();
                    if ( !string.IsNullOrEmpty( value ) )
                    {
                        command.AddInParameter( key, value );
                    }
                }

                command.AddInParameter( _errorMessageParamName, message.Message );
                command.AddInParameter( _errorLineNumParamName, -1 );
                command.AddInParameter( _errorLocationParamName, message.Title );
                command.AddInParameter( _errorDateTimeParamName, messageUnit.Logged );
                command.ExecuteNonQuery();

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion Methods
    }

    // this logger allows for out of sequence logging
    // so we tag a time to each item as we recieved it
}