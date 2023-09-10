using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Button replay;
    [SerializeField] Button exit;
    [SerializeField] Text pauseText;
    private bool paused=false;

    InputChannel inputChannel;

    void start(){
        var beacon = FindObjectOfType<BeaconSO>();
        inputChannel = beacon.inputChannel;
        inputChannel.OtherPauseEvent += HandlePause;
    }

    void HandlePause(){
        if(paused){
            replay.gameObject.SetActive(false);
            pauseText.gameObject.SetActive(false);
            exit.gameObject.SetActive(false); 
            Time.timeScale = 1f;
            Cursor.visible =false;
            paused=!paused;
        }else{
            replay.gameObject.SetActive(true);
            pauseText.gameObject.SetActive(true);
            exit.gameObject.SetActive(true); 
            Time.timeScale = 0f;
            Cursor.visible =true;
            paused=!paused;
        }
    }
}
