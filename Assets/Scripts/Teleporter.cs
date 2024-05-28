using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] private Transform distination;


    public Transform GetDistination()
    {
        return distination;
    }
}
