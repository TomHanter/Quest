using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.TriggerOjects;
using UnityEngine.Rendering;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class LoadEnotherScene : MonoBehaviour
{
    [SerializeField] private List<DoorForEnotherScene> doors;
    [SerializeField] private Transform _playerSpawnPoint;

    private void Start()
    {
        foreach (var door in doors)
        {
            if (door != null)
            {
                door.OnActivated += LoadScene;
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithLighting(sceneName));
    }

    private IEnumerator LoadSceneWithLighting(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Универсальный способ обновления освещения
        UpdateLighting();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (_playerSpawnPoint != null)
        {
            _playerSpawnPoint.localPosition = PlayerSpawnData.SpawnPosition;
            _playerSpawnPoint.rotation = PlayerSpawnData.SpawnRotation;
        }

        //SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void UpdateLighting()
    {
#if UNITY_EDITOR
        // Для редактора - альтернативные методы
        try
        {
            // Способ 1 (для новых версий Unity)
            if (Lightmapping.lightingDataAsset != null)
            {
                Lightmapping.Bake(); // Или Lightmapping.ForceUpdate()
            }
        }
        catch
        {
            // Способ 2 (универсальный)
            RenderSettings.ambientMode = AmbientMode.Skybox;
            DynamicGI.UpdateEnvironment();
        }
#else
        // Для билда
        RenderSettings.ambientMode = AmbientMode.Skybox;
        DynamicGI.UpdateEnvironment();
#endif
    }

    private void OnDestroy()
    {
        foreach (var door in doors)
        {
            if (door != null)
            {
                door.OnActivated -= LoadScene;
            }
        }
    }
}