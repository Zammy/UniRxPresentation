using System;
using System.IO;
using UniRx;
using UnityEngine;

public class StateManager : Singleton<StateManager>
{
    public static State GetState()
    {
        return StateManager.Instance._state;
    }

    [System.Serializable]
    public class SerializableState
    {
        public int PlayerHealth;
        public Vector2 PlayerPosition;
    }

    public class State
    {
        public ReactiveProperty<int> PlayerHealth { get; private set; }
        public ReactiveProperty<Vector2> PlayerPosition { get; private set; }

        public State()
        {
            PlayerHealth = new ReactiveProperty<int>();
            PlayerPosition = new ReactiveProperty<Vector2>();
        }

        public void LoadState(SerializableState rawState)
        {
            foreach (var field in typeof(SerializableState).GetFields())
            {
                var value = field.GetValue(rawState);
                if (value == null)
                    continue;

                var prop = typeof(State).GetProperty(field.Name);
                var reactiveObj = prop.GetValue(this);
                var valueProp = prop.PropertyType.GetProperty("Value");
                valueProp.SetValue(reactiveObj, value);
            }
        }

        public SerializableState SerializeState()
        {
            var serializableState = new SerializableState();
            foreach (var field in typeof(SerializableState).GetFields())
            {
                var prop = typeof(State).GetProperty(field.Name);
                var reactiveObj = prop.GetValue(this);
                var valueProp = prop.PropertyType.GetProperty("Value");
                field.SetValue(serializableState, valueProp.GetValue(reactiveObj));
            }

            return serializableState;
        }
    }

    public StateManager()
    {
        _state = new State();
    }

    public IObservable<Unit> LoadState()
    {
        var subject = new Subject<Unit>();
        Observable.Start(() =>
        {
            var data = File.ReadAllText(SAVE_FILE_PATH, System.Text.Encoding.UTF8);
            return JsonUtility.FromJson<SerializableState>(data);
        })
        .ObserveOnMainThread()
        .Subscribe(serializableState =>
        {
            _state.LoadState(serializableState);
            subject.OnNext(Unit.Default);
        }, ex => subject.OnError(ex));
        return subject;
    }

    public IObservable<Unit> SaveState()
    {
        var state = _state.SerializeState();
        return Observable.Start(() =>
        {
            var stateStr = JsonUtility.ToJson(state);
            File.WriteAllText(SAVE_FILE_PATH, stateStr, System.Text.Encoding.UTF8);
            return Unit.Default;
        }).ObserveOnMainThread();
    }

    State _state;

    readonly string SAVE_FILE_PATH = new Uri(new Uri(Application.dataPath), "savefile.json").AbsolutePath;
}
