using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star {
    public string starName { get; protected set; }
    public int numberOfPlanets { get; protected set; }

    public Star(string name, int planets)
    {
        this.starName = name;
        this.numberOfPlanets = planets;
    }

}
