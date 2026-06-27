using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SourceMusic : MonoBehaviour
{
   
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    
    void Update()
    {
        SceneManager.LoadScene(1);
    }
}
