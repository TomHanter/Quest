using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Zenject;

public sealed class SaveLoadManager : MonoBehaviour
{
    private static Transform _playerTransform;

    private ISaveLoader[] saveLoaders;

    [Inject]
    private void Construct(PlayerMoveController controller)
    {
        _playerTransform = controller.transform;
    }

    private void Awake()
    {
        //FindPlayer();

        // Инициализация массива после получения трансформа игрока
        saveLoaders = new ISaveLoader[]
        {
            //new PlayerSaveLoader(_playerTransform),
            new CollectionSaveLoader(AssembledPickups.GetAllPickups()),
        };
        //WriteJsonToFile();
        LoadGame();
    }

    private void WriteJsonToFile()
    {
        // Создаем JSON-строку
        string jsonString = @"
{
  ""List`1"": ""[{\""Name\"":\""Conc1\"",\""Type\"":\""Type\"",\""Description\"":\""Conc1\"",\""HideDescription\"":\""Hide Description\"",\""Picture\"":\""Picture\"",\""Rendered\"":false,\""RenderedOnScreen\"":false,\""SimpleDict\"":{},\""NestedDict\"":{},\""NestedList\"":[]},{\""Name\"":\""Conc2\"",\""Type\"":\""Type\"",\""Description\"":\""Conc2\"",\""HideDescription\"":\""Hide Description\"",\""Picture\"":\""Picture\"",\""Rendered\"":false,\""RenderedOnScreen\"":false,\""SimpleDict\"":{},\""NestedDict\"":{},\""NestedList\"":[]},{\""Name\"":\""Conc3\"",\""Type\"":\""Type\"",\""Description\"":\""Conc3\"",\""HideDescription\"":\""Hide Description\"",\""Picture\"":\""Picture\"",\""Rendered\"":false,\""RenderedOnScreen\"":false,\""SimpleDict\"":{},\""NestedDict\"":{},\""NestedList\"":[]},{\""Name\"":\""Conc4\"",\""Type\"":\""Type\"",\""Description\"":\""Conc4\"",\""HideDescription\"":\""Hide Description\"",\""Picture\"":\""Picture\"",\""Rendered\"":false,\""RenderedOnScreen\"":false,\""SimpleDict\"":{},\""NestedDict\"":{},\""NestedList\"":[]},{\""Name\"":\""Conc5\"",\""Type\"":\""Type\"",\""Description\"":\""Conc5\"",\""HideDescription\"":\""Hide Description\"",\""Picture\"":\""Picture\"",\""Rendered\"":false,\""RenderedOnScreen\"":false,\""SimpleDict\"":{},\""NestedDict\"":{},\""NestedList\"":[]}]""
}";

        // Путь к файлу, куда будет записан JSON
        string filePath = Path.Combine(Application.persistentDataPath, "saveData.json");

        // Записываем JSON-строку в файл
        try
        {
            File.WriteAllText(filePath, jsonString);
            Debug.Log("JSON data successfully written to file: " + filePath);
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to write JSON data to file: " + e.Message);
        }
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
    //        //Debug.Log("Данные загружаются.............");
    //        LoadGame();
    //        _isSave = 0;

    //    }
    //    else if (_isSave == 1 && isSumbit)
    //    {
    //        //Debug.Log("Данные сохраняются.............");
    //        SaveGame();
    //    }
    //}
}