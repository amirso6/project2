using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool paused=false;
    private float originalSpeed;

    InputChannel inputChannel;
    
    void Start(){
        originalSpeed=speed;
        var beacon = FindObjectOfType<BeaconSO>();
        inputChannel = beacon.inputChannel;
        inputChannel.PauseEvent += HandlePause;
    }

    void HandlePause(){
        if(paused){
            speed=originalSpeed;
            paused=!paused;
        }else{
            speed=0;
            paused=!paused;
        }
    }

    void Update()
    {
        transform.Rotate(0.1f*speed,0.1f*speed,0.1f*speed);
    }
}
