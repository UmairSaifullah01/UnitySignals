using System;

namespace THEBADDEST.Communication
{
    /// <summary>
    /// Concrete implementation of IObserver that wraps an Action delegate.
    /// </summary>
    /// <typeparam name="T">The type of signal data that the observer can handle.</typeparam>
    public class EventObserver<T> : IObserver<T>
    {
        private readonly Action<T> eventDelegate;

        /// <summary>
        /// Initializes a new instance of the EventObserver class.
        /// </summary>
        /// <param name="eventDelegate">The delegate to be invoked when a signal is received.</param>
        public EventObserver(Action<T> eventDelegate)
        {
            this.eventDelegate = eventDelegate;
        }

        /// <summary>
        /// Invokes the stored delegate with the signal data.
        /// </summary>
        /// <param name="signalData">The data associated with the signal.</param>
        public void OnNotify(T signalData)
        {
            eventDelegate?.Invoke(signalData);
        }
    }
} 