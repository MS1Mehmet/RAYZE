using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerPlattform : MonoBehaviour
{
    [SerializeField]
    private PlayerInputHandler InputHandler;

    private PlatformEffector2D effector;
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputHandler.NormInputY == -1)
        {
            Debug.Log("Bin Drin");
            waitTime = 0.5f;

            if (waitTime <= 0)
            {
                effector.rotationalOffset = 150f;
                waitTime = 0.5f;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            effector.rotationalOffset = 0;
        }
    }
}
