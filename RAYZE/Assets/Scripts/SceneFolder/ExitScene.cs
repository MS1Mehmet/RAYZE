using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{
    public string sceneToLoad;
    public string exitName;
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collision)
    {
   

          Debug.Log("Trigger");
          PlayerPrefs.SetString("LastExitName", exitName);
          SceneManager.LoadScene(sceneToLoad);
      
    }

}
