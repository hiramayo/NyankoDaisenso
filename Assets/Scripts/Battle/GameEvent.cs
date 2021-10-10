using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
	[NonSerialized]public List<GameEventListener> listeners;

    private void OnEnable()
    {
		listeners = new List<GameEventListener>();
	}

    public void Raise()
	{
		Debug.Log(string.Format("count:{0}", listeners.Count));
        for (int i = listeners.Count - 1; i >= 0; i--) {
            listeners[i].OnEventRaised();
		}
    }

	public void RegisterListener(GameEventListener listener)
	{ listeners.Add(listener); }

	public void UnregisterListener(GameEventListener listener)
	{ listeners.Remove(listener); }
}