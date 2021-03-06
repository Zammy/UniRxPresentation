# UniRx Outline

## What is Rx?

> Reactive programming is a declarative programming paradigm concerned with data streams and the propagation of change.

A stream is a sequence of ongoing events ordered in time. It can emit three different things: a value (of some type), an error, or a "completed" signal. Consider that the "completed" takes place, for instance, when the current window or view containing that button is closed.

We capture these emitted events only asynchronously, by defining a function that will execute when a value is emitted, another function when an error is emitted, and another function when 'completed' is emitted. Sometimes these last two can be omitted and you can just focus on defining the function for values. The "listening" to the stream is called subscribing. The functions we are defining are observers. The stream is the subject (or "observable") being observed.

(taken from [The introduction to Reactive Programming you've been missing](https://gist.github.com/staltz/868e7e9bc2a7b8c1f754))


## Main Types

`Observer`, a writable data stream. You can send a new value with `OnNext()`, complete the stream (meaning no new data will be streamed) with `OnCompleted()` and in case of error `OnError()`.
```C#
    public interface IObserver<T>
    {
        void OnCompleted();
        void OnError(Exception error);
        void OnNext(T value);
    }
```

`Observable`, a readable data stream. You can listen to data events with `Subscribe()`. Notice that it returns an `IDisposable` object. Data streams need to be disposed of after complete event. Will talk about how this fits with Unity3D game object lifecycle. 
```C#
    public interface IObservable<T>
    {
        IDisposable Subscribe(IObserver<T> observer);
    }
```

You will notice that an `Observable` needs an `Observer` to subscribe to. UniRx provides us with a convenient extensions methods that take lambdas and return an `Observer`. Extensions  methods for subscribing with error and/or complete event.

```C#
        public static IDisposable Subscribe<T>(this IObservable<T> source);
        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext);
        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError);
        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action onCompleted);
        public static IDisposable Subscribe<T>(this IObservable<T> source, Action<T> onNext, Action<Exception> onError, Action onCompleted);
```

## Agenda

All examples have a coresponding folder.

1. (**Demo**)
    * Handle button click
    * Make a double click on button
    * Make a counter using only streams
1. With the help of UniRx extensions methods any UI event can be wrapped in `Observable`. 
    ```C#
        public static class UnityUIComponentExtensions
        {
            public static IObservable<Unit> OnClickAsObservable(this Button button);
            public static IObservable<string> OnEndEditAsObservable(this InputField inputField);
            public static IObservable<bool> OnValueChangedAsObservable(this Toggle toggle);
            public static IObservable<float> OnValueChangedAsObservable(this Scrollbar scrollbar);
            public static IObservable<Vector2> OnValueChangedAsObservable(this ScrollRect scrollRect);
            public static IObservable<float> OnValueChangedAsObservable(this Slider slider);
            public static IObservable<string> OnValueChangedAsObservable(this InputField inputField);
            public static IObservable<int> OnValueChangedAsObservable(this Dropdown dropdown);
            public static IDisposable SubscribeToInteractable(this IObservable<bool> source, Selectable selectable);
            public static IDisposable SubscribeToText(this IObservable<string> source, Text text);
            public static IDisposable SubscribeToText<T>(this IObservable<T> source, Text text);
            public static IDisposable SubscribeToText<T>(this IObservable<T> source, Text text, Func<T, string> selector);
        }
    ```

1. (**Example1**) Demonstrates making multiple http calls simultaniously and synchronizing them.

1. (**Example2**) Demonstrates keeping state at one place that facilitates game state serailization and ReactiveProperties.

1. `Subject`, readable/writable data stream. Subject is both observer and observable. Useful when you want to create your own data streams. UniRx gives you a lot of event wrappers out of the box but sometimes we want to create our own.

    ```C#
        public interface ISubject<TSource, TResult> : IObserver<TSource>, IObservable<TResult>
        {
        }

        public interface ISubject<T> : ISubject<T, T>, IObserver<T>, IObservable<T>
        {
        }
    ```
1. Framecount-based time operators

    Method | 
    -------|
    EveryUpdate|
    EveryFixedUpdate|
    EveryEndOfFrame|
    EveryGameObjectUpdate|
    EveryLateUpdate|
    ObserveOnMainThread|
    NextFrame|
    IntervalFrame|
    TimerFrame|
    DelayFrame|
    SampleFrame|
    ThrottleFrame|
    ThrottleFirstFrame|
    TimeoutFrame|
    DelayFrameSubscription|
    FrameInterval|
    FrameTimeInterval|
    BatchFrame|

1. Subscription to `Observable` should be released if object providing a stream dies. UniRx gives us a lot of extension methods to help us with that.

    ```C#
    public static class DisposableExtensions
    {
        public static T AddTo<T>(this T disposable, ICollection<IDisposable> container) where T : IDisposable;
        public static T AddTo<T>(this T disposable, GameObject gameObject) where T : IDisposable;
        public static T AddTo<T>(this T disposable, Component gameObjectComponent) where T : IDisposable;
        public static T AddTo<T>(this T disposable, ICollection<IDisposable> container, GameObject gameObject) where T : IDisposable;
        public static T AddTo<T>(this T disposable, ICollection<IDisposable> container, Component gameObjectComponent) where T : IDisposable;
    }
    ```


## Great resources:
* https://github.com/neuecc/UniRx - UniRx website. A lot of goodies in the readme. Best place to start with UniRx.
* https://reactivex.io/ - The best place to learn more about reactive language agnostic. Great documentation with visual help.