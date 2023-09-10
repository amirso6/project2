using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class collection : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text overText;
    [SerializeField] Text WonText;
    [SerializeField] Text hintText;
    [SerializeField] Text sprintText;
    [SerializeField] Button replay;
    [SerializeField] Button exit;
    [SerializeField] int maxScore;
    private int score=0;
    public int runSpeed=0;

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "collectable"){
            collision.gameObject.SetActive(false);
            score+=1;
            scoreText.text = "Score: "+score;
        }
        if (collision.gameObject.tag == "SpeedUp"){
            if(runSpeed==0){
                StartCoroutine(SprintUnlock());
            }
            collision.gameObject.SetActive(false);
            runSpeed+=1;
        }
        if (collision.gameObject.tag == "End"){
            if(score<maxScore){
                hintText.gameObject.SetActive(true);
            }else{
                collision.gameObject.SetActive(false);
                Time.timeScale = 0f;
                WonText.gameObject.SetActive(true);
                replay.gameObject.SetActive(true);
                exit.gameObject.SetActive(true);
                Cursor.visible =true;
            }
        }
        if (collision.gameObject.tag == "Death"){
            overText.gameObject.SetActive(true);
            replay.gameObject.SetActive(true);
            exit.gameObject.SetActive(true);
            Cursor.visible =true;
            Time.timeScale = 0f;
        }
    }

    IEnumerator SprintUnlock(){
        sprintText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        sprintText.gameObject.SetActive(false);
    }

    void OnCollisionExit(Collision collision) {
        if (collision.gameObject.tag == "End"){
            hintText.gameObject.SetActive(false);
        }
    }
}
