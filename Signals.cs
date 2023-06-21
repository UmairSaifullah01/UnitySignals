using System;
using System.Collections.Generic;


namespace THEBADDEST.Communication
{


	public interface IObserver<in T>
	{

		void OnNotify(T signalData);

	}

	public class EventObserver<T> : IObserver<T>
	{

		private readonly Action<T> eventDelegate;

		public EventObserver(Action<T> eventDelegate)
		{
			this.eventDelegate = eventDelegate;
		}

		public void OnNotify(T signalData)
		{
			eventDelegate?.Invoke(signalData);
		}

	}


	/// <summary>
	/// Represents a signals manager that allows communication between different parts of the application using events and observers.
	/// </summary>
	public class Signals
	{

		private static Signals instance;

		/// <summary>
		/// Gets the singleton instance of the Signals manager.
		/// </summary>
		static Signals Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Signals();
				}

				return instance;
			}
		}

		private readonly Dictionary<string, List<IObserver<object>>> observers;

		private Signals()
		{
			observers = new Dictionary<string, List<IObserver<object>>>();
		}

		/// <summary>
		/// Subscribes an observer to a specific signal.
		/// </summary>
		/// <typeparam name="T">The type of signal data.</typeparam>
		/// <param name="signalName">The name of the signal.</param>
		/// <param name="observer">The observer to subscribe.</param>
		public static void Subscribe<T>(string signalName, IObserver<T> observer)
		{
			Instance.SubscribeInternal(signalName, observer);
		}

		/// <summary>
		/// Unsubscribes an observer from a specific signal.
		/// </summary>
		/// <typeparam name="T">The type of signal data.</typeparam>
		/// <param name="signalName">The name of the signal.</param>
		/// <param name="observer">The observer to unsubscribe.</param>
		public static void Unsubscribe<T>(string signalName, IObserver<T> observer)
		{
			Instance.UnsubscribeInternal(signalName, observer);
		}

		/// <summary>
		/// Unsubscribes all observers from a specific signal.
		/// </summary>
		/// <param name="signalName">The name of the signal.</param>
		public static void UnsubscribeAll(string signalName)
		{
			Instance.UnsubscribeAllInternal(signalName);
		}

		/// <summary>
		/// Triggers a specific signal and notifies all subscribed observers.
		/// </summary>
		/// <typeparam name="T">The type of signal data.</typeparam>
		/// <param name="signalName">The name of the signal.</param>
		/// <param name="signalData">The data associated with the signal.</param>
		public static void Trigger<T>(string signalName, T signalData)
		{
			Instance.TriggerInternal(signalName, signalData);
		}


		private void SubscribeInternal<T>(string signalName, IObserver<T> observer)
		{
			if (string.IsNullOrEmpty(signalName))
			{
				throw new ArgumentNullException(nameof(signalName), "Invalid signal name");
			}

			if (!observers.TryGetValue(signalName, out var signalObservers))
			{
				// Create a new list of observers for the signal name
				signalObservers       = new List<IObserver<object>>();
				observers[signalName] = signalObservers;
			}

			// Add the observer to the list of observers for the signal name
			signalObservers.Add(observer as IObserver<object>);
		}

		private void UnsubscribeInternal<T>(string signalName, IObserver<T> observer)
		{
			if (string.IsNullOrEmpty(signalName))
			{
				throw new ArgumentNullException(nameof(signalName), "Invalid signal name");
			}

			if (observers.TryGetValue(signalName, out var signalObservers))
			{
				// Remove the observer from the list of observers for the signal name
				signalObservers.Remove(observer as IObserver<object>);
			}
		}

		private void UnsubscribeAllInternal(string signalName)
		{
			if (string.IsNullOrEmpty(signalName))
			{
				throw new ArgumentNullException(nameof(signalName), "Invalid signal name");
			}

			// Remove all observers associated with the signal name
			observers.Remove(signalName);
		}

		private void TriggerInternal<T>(string signalName, T signalData)
		{
			if (string.IsNullOrEmpty(signalName))
			{
				throw new ArgumentNullException(nameof(signalName), "Invalid signal name");
			}

			if (observers.TryGetValue(signalName, out var signalObservers))
			{
				// Notify all subscribed observers with the signal data
				foreach (var observer in signalObservers)
				{
					(observer as IObserver<T>)?.OnNotify(signalData);
				}
			}
		}

	}


}