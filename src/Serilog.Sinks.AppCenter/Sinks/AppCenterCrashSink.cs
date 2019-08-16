using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Serilog.Events;

namespace Serilog.Sinks.AppCenter
{
    /// <summary>
    /// Serilog sink for App Center <see cref="Crashes"/>.
    /// </summary>
    /// <seealso cref="Serilog.Sinks.AppCenter.AppCenterSink" />
    public class AppCenterCrashSink : AppCenterSink
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppCenterCrashSink"/> class.
        /// </summary>
        /// <param name="formatProvider">The format provider.</param>
        /// <param name="appSecret">The application secret.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="restrictedToMinimumLevel">The restricted to minimum level.</param>
        public AppCenterCrashSink(
            IFormatProvider formatProvider,
            string appSecret,
            IDictionary<string, string> properties,
            LogEventLevel restrictedToMinimumLevel)
            : base(
                formatProvider,
                appSecret,
                properties,
                restrictedToMinimumLevel)
        {
        }

        /// <inheritdoc />
        public override void Emit(LogEvent logEvent)
        {
            logEvent
                .Properties
                .Select(kvp => new
                {
                    Name = kvp.Key,
                    Value = AppCenterPropertyFormatter.Simplify(kvp.Value)
                })
                .ToDictionary(x => x.Name, x => x.Value);

            Properties.Add("RenderedLogMessage", logEvent.RenderMessage(FormatProvider));

            Properties.Add("LogMessageTemplate", logEvent.MessageTemplate.Text);

            Crashes.TrackError(logEvent.Exception);
        }
    }
}
