using System;
using System.Collections.Generic;
using System.Text;
using Business2Business.Common.B2BDAL;
using Business2Business.Common.ErrorLoggers.Models;
using Business2Business.Common.ErrorLoggers.Interfaces;

namespace Business2Business.Common.ErrorLoggers.Repository
{
    public class LoggerRepository : ILoggerRepository
    {
        private B2BDBContext? _dbContext;
        public LoggerRepository(B2BDBContext? dbContext)
        {
            _dbContext = dbContext;
        }
        public void WriteMessage(string controller, string action, string errorMessage, string userId = "")
        {
            ErrorMessage error = new ErrorMessage()
            {
                Controller = controller,
                Action = action,
                Message = errorMessage,
                UserId = !string.IsNullOrEmpty(userId) ? Guid.Parse(userId) : Guid.Empty,
                DateActioned = DateTime.Now
            };
            _dbContext.ErrorMessages.Add(error);
            _dbContext.SaveChanges();
        }
    }
}
