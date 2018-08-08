using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;
using System;
using Opsis.WPF.Events;

namespace Opsis.WPF.ViewModels.Base
{
    public abstract class ViewModelBase : BindableBase
    {
        public delegate void ExceptionOccuredDelegate(Exception ex);
        public event ExceptionOccuredDelegate ExceptionOccuredEvent;

        protected IEventAggregator eventAggregator;
        protected IDictionary<EventBase, SubscriptionToken> events;

        public ViewModelBase(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            events = new Dictionary<EventBase, SubscriptionToken>();
        }

        public void UnsubscribeEvents()
        {
            foreach (var e in events)
            {
                e.Key.Unsubscribe(e.Value);
            }
        }

        public void SubscribeExceptionHandling()
        {
            events.Add(
                eventAggregator.GetEvent<ExceptionOccuredEvent>(),
                eventAggregator.GetEvent<ExceptionOccuredEvent>().Subscribe(e => ExceptionOccuredEvent?.Invoke(e)));
        }

        public void ExecuteSafety(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                eventAggregator.GetEvent<ExceptionOccuredEvent>().Publish(ex);
            }
        }
    }
}
