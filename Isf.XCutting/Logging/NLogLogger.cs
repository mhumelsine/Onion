using System;
using System.Collections.Generic;
using System.Text;

namespace Isf.XCutting.Logging
{
    public class NLogLogger : ILogger
    {

        protected string process;
        protected IUsernameProvider usernameProvider;
        protected NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public NLogLogger(string process, IUsernameProvider usernameProvider)
        {
            this.process = process;
            this.usernameProvider = usernameProvider;
        }

        protected virtual NLog.LogEventInfo GetLogEvent(NLog.LogLevel level, Exception ex = null, string message = null)
        {
            var info = new NLog.LogEventInfo
            {
                Message = message,
                Level = level,
                LoggerName = process,
                Exception = ex
            };


            string source = null,
                className = null,
                method = null,
                exceptionType = null,
                exceptionMessage = null,
                stacktrace = null,
                innerExceptionMessage = null;

            if (ex != null)
            {

                source = ex.Source;
                className = ex.TargetSite.DeclaringType.FullName;
                method = ex.TargetSite.Name;
                exceptionType = ex.GetType().FullName;
                exceptionMessage = ex.Message;
                stacktrace = ex.StackTrace;
                innerExceptionMessage = this.GetInnerException(ex);
            }


            info.Properties.Add("username", this.usernameProvider.Username);
            info.Properties.Add("process", this.process);
            info.Properties.Add("source", source);
            info.Properties.Add("classname", className);
            info.Properties.Add("method", method);
            info.Properties.Add("exceptionType", exceptionType);
            info.Properties.Add("exceptionMessage", exceptionMessage);
            info.Properties.Add("stacktrace", stacktrace);
            info.Properties.Add("innerException", innerExceptionMessage);

            return info;
        }

        private string GetInnerException(Exception ex)
        {
            if (ex.InnerException != null)
            {
                return this.GetInnerException(ex.InnerException);
            }

            return ex.Message;
        }

        public virtual void Debug(string message, Exception ex = null)
        {
            this.logger.Log(this.GetLogEvent(NLog.LogLevel.Debug, ex, message));
        }

        public virtual void Trace(string message, Exception ex = null)
        {
            this.logger.Log(this.GetLogEvent(NLog.LogLevel.Trace, ex, message));
        }

        public virtual void Info(string message, Exception ex = null)
        {
            this.logger.Log(this.GetLogEvent(NLog.LogLevel.Info, ex, message));
        }

        public virtual void Error(string message, Exception ex = null)
        {
            this.logger.Log(this.GetLogEvent(NLog.LogLevel.Error, ex, message));
        }

        public virtual void Fatal(string message, Exception ex = null)
        {
            this.logger.Log(this.GetLogEvent(NLog.LogLevel.Fatal, ex, message));
        }
    }
}
