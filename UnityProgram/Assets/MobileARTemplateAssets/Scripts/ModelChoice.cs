using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class ModelChoice : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ModelWait());

        //забрать данные из  GameObject.Find("ModelSpawner").GetComponent<DontDestroy>().modelName
        //GameObject.Find("ObjectSpawner").GetComponent<ObjectSpawner>.m_ObjectPrefabs[0] = GameObject.Find("ModelSpawner").GetComponent<DontDestroy>().modelName
        string modelName = GameObject.Find("ModelSpawner").GetComponent<DontDestroy>().modelName;

        if(modelName == null)
        {
            Debug.LogError("ModelName is not loaded");
        }

        GameObject modelPref = Resources.Load<GameObject>($"Models/{modelName}");

        if( modelPref == null )
        {
            Debug.LogError("ModelPref is not loaded");
        }

        ObjectSpawner spawner = GameObject.Find("Object Spawner").GetComponent<ObjectSpawner>();

        if( spawner == null )
        {
            Debug.LogError("There is no ObjectSpawner");
        }

        spawner.m_ObjectPrefabs[0] = modelPref;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameObject.Find("ModelSpawner").GetComponent<DontDestroy>().modelName);
    }

    private IEnumerator ModelWait()
    {
        yield return new WaitForSeconds(5f);
    }
}
