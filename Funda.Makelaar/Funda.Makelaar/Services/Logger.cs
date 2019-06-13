﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funda.Makelaar
{ 
    public class Logger : ILogger
    {
        private static log4net.ILog Log { get; set; }

        static Logger()
        {
            Log = log4net.LogManager.GetLogger(typeof(Logger));
        }

        public void Error(Exception ex)
        {
            Log.Error(ex.Message, ex);
        }

        public void Info(object msg)
        {
            Log.Info(msg);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
