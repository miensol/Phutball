using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Phutball
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent sampleEvent);
        void Subscribe<TEvent>(Action<TEvent> handler);
    }

    public class EventPublisher : IEventPublisher
    {
        private readonly ConcurrentDictionary<Type, object> _subjects
            = new ConcurrentDictionary<Type, object>();


        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            GetEvent<TEvent>().Subscribe(handler);
        }


        private IObservable<TEvent> GetEvent<TEvent>()
        {
            var subject =
                (ISubject<TEvent>) _subjects.GetOrAdd(typeof (TEvent),
                                                      t => new Subject<TEvent>());
            return subject.AsObservable();
        }

        public void Publish<TEvent>(TEvent sampleEvent)
        {
            OnSubjectDo<TEvent>(sub=> (sub).OnNext(sampleEvent));
        }

        private void OnSubjectDo<TEvent>(Action<ISubject<TEvent>> whenFound)
        {
            object subject;
            if (_subjects.TryGetValue(typeof (TEvent), out subject))
            {
                whenFound((ISubject<TEvent>) subject);
            }
        }

        public static IEventPublisher Empty()
        {
            return new FakeEventPublisher();
        }

        private class FakeEventPublisher : IEventPublisher
        {
            public void Publish<TEvent>(TEvent sampleEvent)
            {
            }

            public void Subscribe<TEvent>(Action<TEvent> handler)
            {
            }

            public void PublishAsync<TEvent>(TEvent @event)
            {                
            }
        }
    }    
}