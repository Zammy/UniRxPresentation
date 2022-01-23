using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UniRx;
using UnityEngine;

public interface IPlayerState
{
    ReactiveProperty<int> PlayerHealth { get; }
    ReactiveProperty<Vector2> PlayerPositiong { get; }
}


public class StateManager : Singleton<StateManager>
{
    public static State GetState()
    {
        return StateManager.Instance._state;
    }

    //contains the whole game state
    [System.Serializable]
    public class SerializableState
    {
        public int PlayerHealth;
        public Vector2 PlayerPositiong;
    }


    public class State : IPlayerState
    {
        public ReactiveProperty<int> PlayerHealth { get; private set; }
        public ReactiveProperty<Vector2> PlayerPositiong { get; private set; }

        public State()
        {
            PlayerHealth = new ReactiveProperty<int>();
            PlayerPositiong = new ReactiveProperty<Vector2>();
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

        public string SerializeState()
        {
            var serializableState = new SerializableState();
            foreach (var field in typeof(SerializableState).GetFields())
            {
                var prop = typeof(State).GetProperty(field.Name);
                var reactiveObj = prop.GetValue(this);
                var valueProp = prop.PropertyType.GetProperty("Value");
                field.SetValue(serializableState, valueProp.GetValue(reactiveObj));
            }

            return JsonUtility.ToJson(serializableState);
        }
    }

    public StateManager()
    {
        _state = new State();
    }

    public IObservable<Unit> LoadState()
    {
        return Observable.Start(() => File.ReadAllText(SAVE_FILE_PATH, System.Text.Encoding.UTF8))
            .Select(data => JsonUtility.FromJson<SerializableState>(data))
            .ObserveOnMainThread()
            .Select(serializableState =>
            {
                _state.LoadState(serializableState);
                return Unit.Default;
            });

    }

    public IObservable<Unit> SaveState()
    {
        var stateStr = _state.SerializeState();
        return Observable.Start(() =>
        {
            File.WriteAllText(SAVE_FILE_PATH, stateStr, System.Text.Encoding.UTF8);
            return Unit.Default;
        });
    }

    State _state;

    readonly string SAVE_FILE_PATH = new Uri(new Uri(Application.dataPath), "savefile.json").AbsolutePath;
}
