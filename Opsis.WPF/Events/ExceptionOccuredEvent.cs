using System;
using Prism.Events;

namespace Opsis.WPF.Events
{
    public class ExceptionOccuredEvent : PubSubEvent<Exception>
    {
    }
}
