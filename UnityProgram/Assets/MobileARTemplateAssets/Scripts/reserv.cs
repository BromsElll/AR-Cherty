//using UnityEngine;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;
//using UnityEngine.EventSystems;
//using System.Collections.Generic;

//public class ModelSpawner : MonoBehaviour
//{
//    [SerializeField] Camera arCamera;
//    [SerializeField] ARRaycastManager raycastManager;
//    List<ARRaycastHit> _hits = new List<ARRaycastHit>();


//    GameObject spawnedObject;
//    [SerializeField] GameObject modelPrefab;

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {
//        spawnedObject = null;

//        string modelName = PlayerPrefs.GetString("ModelName", ""); //перенос имени модели
//        Debug.Log($"[ModelSpawner] Имя модели из PlayerPrefs: {modelName}");


//        if (string.IsNullOrEmpty(modelName))
//        {
//            Debug.LogError("[ModelSpawner] Имя модели пустое!");
//            return;
//        }

//        if (modelPrefab == null)
//        {
//            modelPrefab = Resources.Load<GameObject>($"Models/{modelName}"); //задание префаба
//        }

//        if (modelPrefab == null)
//        {
//            Debug.LogError($"[ModelSpawner] Не удалось найти модель по пути: Resources/Models/{modelName}");
//            return;
//        }
//        Debug.Log($"[ModelSpawner] Модель {modelName} успешно загружена!");
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.touchCount == 0) //if (Input.touchCount == 0 || modelPrefab == null)
//            return;

//        Touch touch = Input.GetTouch(0);

//        if (raycastManager.Raycast(touch.position, _hits))
//        {
//            if (touch.phase == TouchPhase.Began)
//                SpawnPrefab(_hits[0].pose.position);

//            else if (touch.phase == TouchPhase.Moved && spawnedObject != null)
//            {
//                spawnedObject.transform.position = _hits[0].pose.position;
//            }
//            if (touch.phase == TouchPhase.Ended)
//            {
//                spawnedObject = null;
//            }
//        }
//    }

//    private void SpawnPrefab(Vector3 spawnPosition)
//    {
//        spawnedObject = Instantiate(modelPrefab, spawnPosition, Quaternion.identity);
//    }
//}
////ОН СЪЕЛ У МЕНЯ 10 МОРКОВКА, 3 КИЛОГРАММ СТОЛЯРНЫЙ КЛЕЙ И СОВСЕМ НОВЫЙ ПЛОСКОГУБЦЫ!!!