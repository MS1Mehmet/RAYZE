using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterImagePool : MonoBehaviour
{
    [SerializeField]
    private GameObject afterImagePrefab;

    private Queue<GameObject> avialableobjects = new Queue<GameObject>();

    public static PlayerAfterImagePool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GrowPool();

    }

    private void GrowPool()
    {
        for(int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(afterImagePrefab);
            instanceToAdd.transform.SetParent(transform);
            AddtoPool(instanceToAdd);
        }
    }

    public void AddtoPool(GameObject instance)
    {
        instance.SetActive(false);
        avialableobjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if(avialableobjects.Count == 0)
        {
            GrowPool();
        }

        var instance = avialableobjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }
}
