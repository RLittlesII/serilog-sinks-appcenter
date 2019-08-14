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
    /// The AppCenter implementation of <see cref="ILogEventSink"/>.
    /// </summary>
    /// <seealso cref="Serilog.Core.ILogEventSink" />
    public class AppCenterSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly IDictionary<string, string> _properties;
        private readonly LogEventLevel _restrictedToMinimumLevel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppCenterSink"/> class.
        /// </summary>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="appSecret">The application secret.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="restrictedToMinimumLevel">The restricted to minimum level.</param>
        /// <exception cref="ArgumentNullException">appSecret</exception>
        public AppCenterSink(
            IFormatProvider formatProvider,
            string appSecret,
            IDictionary<string, string> properties,
            LogEventLevel restrictedToMinimumLevel)
        {
            if (string.IsNullOrEmpty(appSecret))
            {
                throw new ArgumentNullException(nameof(appSecret));
            }

            _formatProvider = formatProvider;
            _properties = properties;
            _restrictedToMinimumLevel = restrictedToMinimumLevel;
        }

        /// <summary>
        /// Emit the provided log event to the sink.
        /// </summary>
        /// <param name="logEvent">The log event to write.</param>
        public void Emit(LogEvent logEvent)
        {
            var properties =
                logEvent
                    .Properties
                    .Select(kvp => new
                    {
                        Name = kvp.Key,
                        Value = AppCenterPropertyFormatter.Simplify(kvp.Value)
                    })
                    .ToDictionary(x => x.Name, x => x.Value);

            properties.Add("RenderedLogMessage", logEvent.RenderMessage(_formatProvider));

            properties.Add("LogMessageTemplate", logEvent.MessageTemplate.Text);

            Analytics.TrackEvent(logEvent.RenderMessage());
        }
    }
}