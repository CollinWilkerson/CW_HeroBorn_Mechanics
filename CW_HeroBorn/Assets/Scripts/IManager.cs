using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//uses the interface key word to make an interface. an interface is similar to inheritance class, but there is no limit on how many you can use,
// they also have limitations on how they can operate in.
//pillar of polymorphism
public interface IManager
{
    // everyting in an interface is public by default
    //variables in interfaces must have a get.
    string State { get; set; }
    //
    void Initialize();
}
