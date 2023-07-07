using UnityEngine;

/// <summary>
/// A simulated sensor unit consisting of a position in simulation
/// space, and a force reading in Unity's physics system
/// consisting of Newtons
/// </summary>
[System.Serializable]
public class SimulateSensor {

    /// <summary>
    /// Our sensor's position in Vector3 space
    /// </summary>
    [SerializeField] private Vector3 position;

    /// <summary>
    /// Our sensor's Vector force reading
    /// </summary>
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

    /// <summary>
    /// Whether or not this simulate sensor has an arrow due to
    /// having a non-zero force reading
    /// </summary>
    public bool HasArrow {
        get { return hasArrow; }
        set { hasArrow = value; }
    }

    /// <summary>
    /// Updates our simulate sensor with a new position.
    /// </summary>
    /// <param name="newPosition">Our newer Vector3 position</param>
    public void UpdatePosition(Vector3 newPosition) {
        position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
    }

    /// <summary>
    /// Updates our simulate sensor with a new force reading.
    /// </summary>
    /// <param name="newForce">Our newer Vector3 force reading</param>
    public void UpdateForceReading(Vector3 newForce) {
        forceReading = new Vector3(newForce.x, newForce.y, newForce.z);
    }

}
