using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// A simulated sensor unit consisting of a position in simulation
/// space, and a force reading in Unity's physics system
/// consisting of Newtons
/// </summary>
[System.Serializable]
public class SimulateSensor {

    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 forceReading;
    [SerializeField] private bool hasArrow;

    /// <summary>
    /// The sensor's Vector3 position in our simulation space
    /// </summary>
    public Vector3 Position {
        get { return position; }
        set { position = value; }
    }

    /// <summary>
    /// This sensor's Force reading in our simulation space
    /// </summary>
    public Vector3 ForceReading {
        get { return forceReading; }
        set {
            forceReading = value;
        }
    }

    public bool HasArrow {
        get { return hasArrow; }
        set { hasArrow = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="newPosition"></param>
    public void UpdatePosition(Vector3 newPosition) {
        position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
    }

    /// <summary>
    /// 
    /// 
    /// 
    /// </summary>
    /// <param name="newForce"></param>
    public void UpdateForceReading(Vector3 newForce) {
        forceReading = new Vector3(newForce.x, newForce.y, newForce.z);
    }

}
