using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace TGame.Message
{
    public interface IMessageHander
    {
        Type GetHandlerType();
    }
    [MessageHandler]
    public abstract class MessageHandler<T> : IMessageHander where T : struct
    {
        public Type GetHandlerType()
        {
            return typeof(T);
        }

        public abstract Task HanleMessage(T arg);
        internal abstract Task HandleMessage<T>(T arg) where T : struct;
    }


    sealed class MessageHandlerAttribute : Attribute { }
}

