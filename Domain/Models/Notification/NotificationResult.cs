using System;
using System.Collections.Generic;
using System.Linq;

namespace Tolitech.CodeGenerator.Domain.Models.Notification
{
    public class NotificationResult
    {
        internal readonly List<NotificationError> _errors;
        internal readonly List<NotificationMessage> _messages;

        #region Properties

        public bool IsValid
        {
            get { return !_errors.Any(); }
        }

        public dynamic Data { get; set; }

        public IEnumerable<NotificationError> Errors
        {
            get { return _errors; }
        }

        public IEnumerable<NotificationMessage> Messages
        {
            get { return _messages; }
        }

        public NotificationResult()
        {
            _errors = new List<NotificationError>();
            _messages = new List<NotificationMessage>();
        }

        #endregion

        #region Methods

        public void AddMessage(string message)
        {
            _messages.Add(new NotificationMessage(message));
        }

        public void AddMessageOnTop(string message)
        {
            _messages.Insert(0, new NotificationMessage(message));
        }

        public void AddError(string errorMessage)
        {
            _errors.Add(new NotificationError(errorMessage));
        }

        public void AddError(Exception ex)
        {
            _errors.Add(new NotificationError(ex));
        }

        public void AddErrorOnTop(string errorMessage)
        {
            _errors.Insert(0, new NotificationError(errorMessage));
        }

        public void AddMessage(string message, string type)
        {
            if (!string.IsNullOrEmpty(type) && type.ToLower() == "error")
                _errors.Add(new NotificationError(message));
            else
                _messages.Add(new NotificationMessage(message, type));
        }

        public void AddError(string key, string errorMessage)
        {
            _errors.Add(new NotificationError(key, errorMessage));
        }

        public void AddError(string key, Exception ex)
        {
            _errors.Add(new NotificationError(key, ex));
        }

        public void AddMessage(NotificationMessage message)
        {
            _messages.Add(message);
        }

        public void AddError(NotificationError error)
        {
            _errors.Add(error);
        }

        public void Add(params NotificationResult[] validationResults)
        {
            if (validationResults == null) return;

            foreach (var result in validationResults.Where(r => r != null))
            {
                _errors.AddRange(result.Errors);
                _messages.AddRange(result.Messages);
            }
        }

        public void InsertMessage(int index, string message)
        {
            _messages.Insert(index, new NotificationMessage(message));
        }

        public void InsertError(int index, string errorMessage)
        {
            _errors.Insert(index, new NotificationError(errorMessage));
        }

        public void InsertError(int index, Exception ex)
        {
            _errors.Insert(index, new NotificationError(ex));
        }

        public void InsertMessage(int index, string message, string type)
        {
            if (!string.IsNullOrEmpty(type) && type.ToLower() == "error")
                _errors.Insert(index, new NotificationError(message));
            else
                _messages.Insert(index, new NotificationMessage(message, type));
        }

        public void InsertError(int index, string key, string errorMessage)
        {
            _errors.Insert(index, new NotificationError(key, errorMessage));
        }

        public void InsertError(int index, string key, Exception ex)
        {
            _errors.Insert(index, new NotificationError(key, ex));
        }

        public void InsertMessage(int index, NotificationMessage message)
        {
            _messages.Insert(index, message);
        }

        public void InsertError(int index, NotificationError error)
        {
            _errors.Insert(index, error);
        }

        public void RemoveMessage(NotificationMessage message)
        {
            if (_messages.Contains(message))
                _messages.Remove(message);
        }

        public void RemoveError(NotificationError error)
        {
            if (_errors.Contains(error))
                _errors.Remove(error);
        }

        public void RemoveMessage(string key)
        {
            var items = _messages.Where(x => x.Key == key).ToList();
            for (int i = items.Count - 1; i >= 0; i--)
            {
                _messages.Remove(items[i]);
            }
        }

        public void RemoveError(string key)
        {
            var items = _errors.Where(x => x.Key == key).ToList();
            for (int i = items.Count - 1; i >= 0; i--)
            {
                _errors.Remove(items[i]);
            }
        }

        public void ClearMessages()
        {
            _messages.Clear();
        }

        public void ClearErrors()
        {
            _errors.Clear();
        }

        public void Clear()
        {
            _errors.Clear();
            _messages.Clear();
        }

        #endregion
    }
}