using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Observer : MonoBehaviour
{
    public abstract void onDrumHit(object value);
    
}

public abstract class Drum: MonoBehaviour
{


    private List<Observer> observers = new List<Observer>();
    public void RegisterObserver(Observer observer)
    {
        observers.Add(observer);
    }
    public void Notify (object value)
    {
        foreach (var observer in observers)
        {
            observer.onDrumHit(value);
        }

    }

}
