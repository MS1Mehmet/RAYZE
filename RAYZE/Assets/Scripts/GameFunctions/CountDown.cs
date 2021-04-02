using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{

    Text countDownText;

    float timer;
    int countDownNumer = 600;
    // Start is called before the first frame update
    void Start()
    {
        countDownText = GetComponent<Text>();
        countDownText.text = countDownNumer.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1 && countDownNumer > 0)
        {
            countDownNumer--;
            countDownText.text = countDownNumer.ToString();
            timer = 0;
        }
    }
}
