using System;
using System.Collections.Generic;
using System.Text;

namespace Business2Business.Common.ErrorLoggers.Interfaces
{
    public interface ILoggerRepository
    {
        void WriteMessage(string controller, string action, string errorMessage, string userId = "");
    }
}
