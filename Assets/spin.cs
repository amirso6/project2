using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool paused=false;
    private float originalSpeed;
    
    void Start(){
        originalSpeed=speed;
    }

    void Update()
    {
        transform.Rotate(0.1f*speed,0.1f*speed,0.1f*speed);
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(paused){
                speed=originalSpeed;
                paused=!paused;
            }else{
                speed=0;
                paused=!paused;
            }
        }
    }
}
