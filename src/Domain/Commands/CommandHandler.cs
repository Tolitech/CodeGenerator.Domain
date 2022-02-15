using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Tolitech.CodeGenerator.Domain.Services;
using Tolitech.CodeGenerator.Notification;

namespace Tolitech.CodeGenerator.Domain.Commands
{
    public abstract class CommandHandler : ICommandHandler
    {
        private readonly IUnitOfWorkService _unitOfWork;
        protected readonly ILogger _logger;

        public CommandHandler(IUnitOfWorkService unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public NotificationResult BeginTransaction()
        {
            _unitOfWork.BeginTransaction();
            return new NotificationResult();
        }

        public NotificationResult Commit(NotificationResult result)
        {
            if (result.IsValid)
                _unitOfWork.Commit();
            else
                _unitOfWork.Rollback();

            return result;
        }

        public NotificationResult Rollback(NotificationResult result)
        {
            _unitOfWork.Rollback();
            return result;
        }

        #region Logger

        public void LogCommandHandlerException<T>(Exception ex, T command) where T : Command
        {
            const string template = "CommandParams: {parameters}";
            string parameters = JsonSerializer.Serialize(command, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull });
            _logger.LogWarning(ex, ex.Message + "\n" + template, parameters);
        }

        #endregion

        #region Messages

        public void DefaultMessageInsert(NotificationResult result)
        {
            ReplaceMessage(result, Resources.CommandHandler.CommandHandlerResource.InsertSuccess_Message, Resources.CommandHandler.CommandHandlerResource.InsertError_Message);
        }

        public void DefaultMessageUpdate(NotificationResult result)
        {
            ReplaceMessage(result, Resources.CommandHandler.CommandHandlerResource.UpdateSuccess_Message, Resources.CommandHandler.CommandHandlerResource.UpdateError_Message);
        }

        public void DefaultMessageUpload(NotificationResult result)
        {
            ReplaceMessage(result, Resources.CommandHandler.CommandHandlerResource.UploadSuccess_Message, Resources.CommandHandler.CommandHandlerResource.UpdateError_Message);
        }

        public void DefaultMessageDelete(NotificationResult result)
        {
            ReplaceMessage(result, Resources.CommandHandler.CommandHandlerResource.DeleteSuccess_Message, Resources.CommandHandler.CommandHandlerResource.DeleteError_Message);
        }

        public void ReplaceMessage(NotificationResult result, string messageSuccess, string messageError)
        {
            bool isValid = result.IsValid;
            result.Clear();

            if (isValid)
                result.AddMessage(messageSuccess);
            else
                result.AddError(messageError);
        }

        #endregion
    }
}