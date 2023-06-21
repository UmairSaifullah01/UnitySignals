# Signals

Signals is a lightweight event-driven communication system that enables decoupled communication between different parts of your application. It allows you to define custom signals and subscribe to them with observers, making it easy to broadcast and receive messages without direct dependencies.

## Usage

### Subscribing to a Signal

To subscribe an observer to a specific signal, use the `Signals.Subscribe` method. Provide the signal name and the observer that implements the `IObserver<T>` interface, where `T` is the type of signal data.

```csharp
Signals.Subscribe("SignalName", new EventObserver<int>(OnSignalReceived));
```

### Unsubscribing from a Signal
To unsubscribe an observer from a specific signal, use the `Signals.Unsubscribe` method. Provide the signal name and the observer that was previously subscribed.
```csharp
Signals.Unsubscribe("SignalName", observer);
```

### Unsubscribing All Observers from a Signal

To unsubscribe all observers from a specific signal, use the `Signals.UnsubscribeAll` method. Provide the signal name.

```csharp
Signals.UnsubscribeAll("SignalName");
```
### Triggering a Signal

To trigger a specific signal and notify all subscribed observers, use the `Signals.Trigger` method. Provide the signal name and the signal data.
```csharp
Signals.Trigger("SignalName", signalData);
```
### Creating Custom Observers

To create a custom observer, implement the `IObserver<T>` interface, where `T` is the type of signal data.
```csharp
public class CustomObserver : IObserver<int>
{
    public void OnNotify(int signalData)
    {
        // Handle the signal data
    }
}
```

### Example

Here's an example of how to use the `Signals` class in your application:

```csharp
// Subscribe an observer to a signal
Signals.Subscribe("PlayerDied", new EventObserver<PlayerData>(OnPlayerDied));

// Trigger the signal with signal data
PlayerData playerData = new PlayerData();
Signals.Trigger("PlayerDied", playerData);

// Unsubscribe all observers from the signal
Signals.UnsubscribeAll("PlayerDied");

// Custom observer implementation
void OnPlayerDied(PlayerData playerData)
{
    // Handle the player death event
}
```

### License
This project is licensed under the MIT License.
