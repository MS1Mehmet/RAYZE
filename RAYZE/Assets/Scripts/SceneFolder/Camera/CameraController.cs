using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance2;
    // Start is called before the first frame update
    void Start()
    {
        checkPlayer();
    }


    public void checkPlayer()
    {
        if (instance2 != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance2 = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}
