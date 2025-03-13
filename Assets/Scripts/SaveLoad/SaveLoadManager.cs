using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class SaveLoadManager : MonoBehaviour
{
    private static Transform _playerTransform;

    private ISaveLoader[] saveLoaders;

    //private int _isSave = 0;

    [Inject]
    private void Construct(PlayerMoveController controller)
    {
        _playerTransform = controller.transform;
    }

    private void Awake()
    {
        //FindPlayer();

        // ������������� ������� ����� ��������� ���������� ������
        saveLoaders = new ISaveLoader[]
        {
            new PlayerSaveLoader(_playerTransform),
            //new CollectionSaveLoader(AssembledPickups.GetAllPickups()),
        };
    }

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            _playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found!");
        }
    }
    
    
    public void LoadGame()
    {
        Repository.LoadState();

        foreach (var saveLoader in this.saveLoaders)
        {
            saveLoader.LoadData();
        }
    }

    public void SaveGame()
    {
        foreach (var saveLoader in this.saveLoaders)
        {
            saveLoader.SaveData();
        }

        Repository.SaveState();
    }

    //private void FixedUpdate()
    //{
    //    bool isInteract = InputManager.GetInstance().GetInteractPressed();
    //    bool isSumbit = InputManager.GetInstance().GetSubmitPressed();

    //    if (isSumbit)
    //    {
    //        _isSave++;
    //        //Debug.Log("isInteract " + isInteract);
    //    }
        
    //    //if (isInteract)
    //    //{
    //    //    //_isSave++;
    //    //    Debug.Log("isInteract " + isInteract);
    //    //}

    //    if (_isSave == 2 && isSumbit)
    //    {
    //        //Debug.Log("������ �����������.............");
    //        LoadGame();
    //        _isSave = 0;

    //    }
    //    else if (_isSave == 1 && isSumbit)
    //    {
    //        //Debug.Log("������ �����������.............");
    //        SaveGame();
    //    }
    //}
}