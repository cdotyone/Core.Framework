using System;

namespace Civic.Core.Logging.DB
{
    class DBLogMessageUnit
    {
        #region Fields

        public DateTime Logged = DateTime.Now;
        public ILogMessage Message;

        #endregion Fields

        #region Constructors

        public DBLogMessageUnit( ILogMessage message )
        {
            Message = message;
        }

        #endregion Constructors
    }
}