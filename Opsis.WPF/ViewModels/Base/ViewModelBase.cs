using Prism.Events;
using Prism.Mvvm;
using System.Collections.Generic;

namespace Opsis.WPF.ViewModels.Base
{
    public abstract class ViewModelBase : BindableBase
    {
        protected IDictionary<EventBase, SubscriptionToken> events;

        public ViewModelBase()
        {
            events = new Dictionary<EventBase, SubscriptionToken>();
        }

        public void UnsubscribeEvents()
        {
            foreach (var e in events)
            {
                e.Key.Unsubscribe(e.Value);
            }
        }
    }
}
