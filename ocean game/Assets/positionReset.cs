using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionReset : MonoBehaviour
{
    public Vector3 StartPos;

    private void OnEnable()
    {
        transform.position = StartPos;
    }

}
