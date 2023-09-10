using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class replay : MonoBehaviour
{
    public void button_replay()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1f;
    }
}
