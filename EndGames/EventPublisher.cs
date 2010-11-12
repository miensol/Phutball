using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EndGames
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent sampleEvent);
        IObservable<TEvent> GetEvent<TEvent>();
    }

    public class EventPublisher : IEventPublisher
    {
        private readonly ConcurrentDictionary<Type, object> _subjects
            = new ConcurrentDictionary<Type, object>();

        #region IEventPublisher Members

        public IObservable<TEvent> GetEvent<TEvent>()
        {
            var subject =
                (ISubject<TEvent>) _subjects.GetOrAdd(typeof (TEvent),
                                                      t => new Subject<TEvent>());
            return subject.AsObservable();
        }

        public void Publish<TEvent>(TEvent sampleEvent)
        {
            object subject;
            if (_subjects.TryGetValue(typeof (TEvent), out subject))
            {
                ((ISubject<TEvent>) subject)
                    .OnNext(sampleEvent);
            }
        }

        #endregion
    }
}