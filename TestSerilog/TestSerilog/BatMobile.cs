using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO;

namespace Transportation
{
    public class BatMobile
    {
        private readonly Engine _engine;

        public BatMobile()
        {            
            Log.Logger = new LoggerConfiguration()
                   .ReadFrom.AppSettings()
                   .CreateLogger();

            _engine = new Engine();
        }

        public void DriveFor(int timeInSecs)
        {
            _engine.Start();
            Log.Logger.Information("Engine Started at {TimeOfStart}", DateTime.Now);
            _engine.Run(timeInSecs);
            Log.Logger.Information("RPM log {RpmLog}", _engine.RpmLog);
            Log.Logger.Information("Engine ran for {EngineRunDuration}", timeInSecs);
            Log.Logger.Information("Engine stats which will not work {WillNotWork}", _engine);
            Log.Logger.Information("Engine stats {@BatmobileEngine}", _engine);
            
            Log.CloseAndFlush();
        }
    }
}