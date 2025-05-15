using System;

namespace THEBADDEST.Communication
{
    /// <summary>
    /// Interface defining the contract for signal observers.
    /// </summary>
    /// <typeparam name="T">The type of signal data that the observer can handle.</typeparam>
    public interface IObserver<in T>
    {
        /// <summary>
        /// Called when a signal is triggered with data of type T.
        /// </summary>
        /// <param name="signalData">The data associated with the signal.</param>
        void OnNotify(T signalData);
    }
} 