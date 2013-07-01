#region Copyright / Comments

// <copyright file="ILogMessage.cs" company="Polar Opposite">Copyright © Polar Opposite 2013</copyright>
// <author>Chris Doty</author>
// <email>cdoty@polaropposite.com</email>
// <date>6/4/2013</date>
// <summary></summary>

#endregion Copyright / Comments

#region References

using System.Collections.Generic;

#endregion References

namespace PO.Core.Logging
{
    #region Enumerations

    #endregion Enumerations

    /// <summary>
    /// Describes a generic log message class
    /// </summary>
    public interface ILogMessage
    {
        #region Properties

        /// <summary>
        /// gets/sets the title text for this message
        /// </summary>
        string Title
        {
            get; set;
        }

        /// <summary>
        /// gets/sets the message text for this message
        /// </summary>
        string Message
        {
            get; set;
        }

        /// <summary>
        /// gets/sets the message text for this message
        /// </summary>
        LogSeverity Type
        {
            get;set;
        }

        /// <summary>
        /// gets/sets the extended properties
        /// </summary>
        Dictionary<string, object> Extended { get; set; }

        /// <summary>
        /// gets/sets the Layer Boundary
        /// </summary>
        LoggingBoundaries Boundary { get; set; }

        #endregion Properties
    }
}