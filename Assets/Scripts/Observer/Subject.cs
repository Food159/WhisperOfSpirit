using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IOserver> ObserverList = new List<IOserver>(); //Observer List

    public void AddObserver(IOserver observer) //Add Observer
    {
        ObserverList.Add(observer);
    }
    public void RemoveObserver(IOserver observer) //Remove Observer
    {
        ObserverList.Remove(observer);
    }
    public void NotifyObservers(PlayerAction action) //Send notify to all observers in the list
    {
        ObserverList.ForEach ((ObserverList) => { ObserverList.OnNotify(action); }); //Lamda expression
    }
}
