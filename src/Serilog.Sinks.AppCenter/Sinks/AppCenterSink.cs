using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.AppCenter
{
    /// <summary>
    /// Base abstraction for AppCenterSink.
    /// </summary>
    /// <seealso cref="Serilog.Core.ILogEventSink" />
    public abstract class AppCenterSink : ILogEventSink
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppCenterSink"/> class.
        /// </summary>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="appSecret">The application secret.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="restrictedToMinimumLevel">The restricted to minimum level.</param>
        protected AppCenterSink(
            IFormatProvider formatProvider,
            string appSecret,
            IDictionary<string, string> properties,
            LogEventLevel restrictedToMinimumLevel)
        {
            if (string.IsNullOrEmpty(appSecret))
            {
                throw new ArgumentNullException(nameof(appSecret));
            }

            FormatProvider = formatProvider;
            Properties = properties;
            RestrictedToMinimumLevel = restrictedToMinimumLevel;
        }

        /// <summary>
        /// Gets the format provider.
        /// </summary>
        protected IFormatProvider FormatProvider { get; }

        /// <summary>
        /// Gets the restricted to minimum level.
        /// </summary>
        protected LogEventLevel RestrictedToMinimumLevel { get; }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        protected IDictionary<string, string> Properties { get; }

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        public abstract void Emit(LogEvent logEvent);
    }
}