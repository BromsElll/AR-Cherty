using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public string modelName;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}