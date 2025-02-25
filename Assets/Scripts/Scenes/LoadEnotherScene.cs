using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Assets.Scripts.TriggerOjects;

public class LoadEnotherScene : MonoBehaviour
{
    [SerializeField] private List<DoorForEnotherScene> doors; // Список объектов DoorForEnotherScene
    [SerializeField] private Transform _playerSpawnPoint;     // Точка спавна игрока

    private void Start()
    {
        // Подписываемся на событие OnActivated для всех объектов в списке
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
        SceneManager.LoadScene(sceneName);
        
        // После загрузки сцены перемещаем игрока в точку спавна
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // Убедитесь, что у игрока есть тег "Player"
        if (player != null)
        {
            // Перемещаем игрока в точку спавна
            player.transform.localPosition = PlayerSpawnData.SpawnPosition;
            player.transform.rotation = PlayerSpawnData.SpawnRotation;
        }
        // Отписываемся от события, чтобы избежать повторного вызова
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Отписываемся от события при уничтожении объекта для всех объектов в списке
        foreach (var door in doors)
        {
            if (door != null)
            {
                door.OnActivated -= LoadScene;
            }
        }
    }
}