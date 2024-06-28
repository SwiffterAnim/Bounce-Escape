using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleEntity : MonoBehaviour
{
    [SerializeField] float reactivateHookAfter = 5f;
    
    public bool hooknessActivated;
    public float attractionSpeed = 3f;



    //Not sure if any of this Activation and Deactivation of the hooks should be here on this script.
    private void Start()
    {
    }




}
