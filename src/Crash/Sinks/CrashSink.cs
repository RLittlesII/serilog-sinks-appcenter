using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AppCenter.Crashes;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.AppCenter.Crash
{
    /// <summary>
    /// Serilog sink for App Center <see cref="Crashes"/>.
    /// </summary>
    public class CrashSink : ILogEventSink
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CrashSink"/> class.
        /// </summary>
        public CrashSink()
            : this(default, default, default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CrashSink"/> class.
        /// </summary>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="restrictedToMinimumLevel">The restricted to minimum level.</param>
        public CrashSink(
            ITextFormatter formatProvider,
            IDictionary<string, string> properties,
            LogEventLevel restrictedToMinimumLevel)
        {
            FormatProvider = formatProvider;
            Properties = properties;
            RestrictedToMinimumLevel = restrictedToMinimumLevel;
        }

        /// <summary>
        /// Gets the format provider.
        /// </summary>
        protected ITextFormatter FormatProvider { get; }

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
        public void Emit(LogEvent logEvent)
        {
            var properties = new Dictionary<string, string>
            {
                { "level", logEvent.Level.ToString() },
                { "message", logEvent.RenderMessage() }
            };

            foreach (var property in logEvent.Properties)
            {
                using (var stringWriter = new StringWriter())
                {
                    property.Value.Render(stringWriter);
                    var str = stringWriter.ToString();
                    properties.Add(property.Key, str);
                }
            }

            TrackError(logEvent.Exception, properties);
        }

        /// <summary>
        /// Tracks the error.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="properties">The properties.</param>
        protected virtual void TrackError(Exception exception, IDictionary<string, string> properties)
        {
            Crashes.TrackError(exception, properties);
        }
    }
}
