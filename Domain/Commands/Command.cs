using System;
using Tolitech.CodeGenerator.Domain.Models.Notification;

namespace Tolitech.CodeGenerator.Domain.Commands
{
    public abstract class Command : Notifiable, ICommand
    {

    }
}
