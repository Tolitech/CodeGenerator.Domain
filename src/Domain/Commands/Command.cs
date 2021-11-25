using System;
using Tolitech.CodeGenerator.Notification;

namespace Tolitech.CodeGenerator.Domain.Commands
{
    public abstract class Command : Notifiable, ICommand
    {

    }
}
