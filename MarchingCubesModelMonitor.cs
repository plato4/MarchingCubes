using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(MarchingCubesModel))]
public class MarchingCubesModelMonitor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var m = GetComponent<MarchingCubesModel>();
        if (m.dirty)
        {
            m.Generate();
        }
    }
}
