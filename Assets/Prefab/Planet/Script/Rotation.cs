using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject PlanetObject;
    public Vector3 RotationVector;
        
    private void Update() {
        PlanetObject.transform.Rotate(RotationVector * Time.deltaTime);
    }
}
