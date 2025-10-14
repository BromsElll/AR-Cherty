using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ModelSpawner : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private ARRaycastManager raycastManager;

    private GameObject spawnedObject;
    private GameObject modelPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string modelName = PlayerPrefs.GetString("ModelName", ""); //перенос имени модели
        Debug.Log($"[ModelSpawner] Имя модели из PlayerPrefs: {modelName}");
        if (string.IsNullOrEmpty(modelName))
        {
            Debug.LogError("[ModelSpawner] Имя модели пустое!");
            return;
        }
        modelPrefab = Resources.Load<GameObject>($"Models/{modelName}");
        if (modelPrefab == null)
        {
            Debug.LogError($"[ModelSpawner] Не удалось найти модель по пути: Resources/Models/{modelName}");
            return;
        }
        Debug.Log($"[ModelSpawner] Модель {modelName} успешно загружена!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0 || modelPrefab == null)
            return;
        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;
        List<ARRaycastHit> hits = new List<ARRaycastHit>(); //попадания луча в плоскости

        // Если луч пересёк реальную плоскость (распознанную AR Plane Manager), hits заполнится
        if (raycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
        {
            //Первая точка пересечения (самая близкая)
            Pose hitPose = hits[0].pose;

            // Если модель ещё не создана — она создаётся
            if (spawnedObject == null)
            {
                //Создание копии modelPrefab в точке касания с правильным вращением
                spawnedObject = Instantiate(modelPrefab, hitPose.position, hitPose.rotation);

                Vector3 lookDirection = arCamera.transform.position - hitPose.position;
                lookDirection.y = 0; //ликвидация наклона
                spawnedObject.transform.rotation = Quaternion.LookRotation(lookDirection);
            }
            else
            {
                spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
            }
        }
    }
}
//ОН СЪЕЛ У МЕНЯ 10 МОРКОВКА, 3 КИЛОГРАММ СТОЛЯРНЫЙ КЛЕЙ И СОВСЕМ НОВЫЙ ПЛОСКОГУБЦЫ!!!