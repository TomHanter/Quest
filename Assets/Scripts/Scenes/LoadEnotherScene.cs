using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using Assets.Scripts.TriggerOjects;

public class LoadEnotherScene : MonoBehaviour
{
    [SerializeField] private List<DoorForEnotherScene> doors; // ������ �������� DoorForEnotherScene
    [SerializeField] private Transform _playerSpawnPoint;     // ����� ������ ������

    private void Start()
    {
        // ������������� �� ������� OnActivated ��� ���� �������� � ������
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
        
        // ����� �������� ����� ���������� ������ � ����� ������
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); // ���������, ��� � ������ ���� ��� "Player"
        if (player != null)
        {
            // ���������� ������ � ����� ������
            player.transform.localPosition = PlayerSpawnData.SpawnPosition;
            player.transform.rotation = PlayerSpawnData.SpawnRotation;
        }
        // ������������ �� �������, ����� �������� ���������� ������
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // ������������ �� ������� ��� ����������� ������� ��� ���� �������� � ������
        foreach (var door in doors)
        {
            if (door != null)
            {
                door.OnActivated -= LoadScene;
            }
        }
    }
}