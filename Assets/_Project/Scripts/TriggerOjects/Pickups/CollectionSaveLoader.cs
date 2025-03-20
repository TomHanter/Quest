using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CollectionSaveLoader : ISaveLoader
{
    private List<Pickup> _pickups;

    public CollectionSaveLoader(List<Pickup> pickups)
    {
        _pickups = pickups;
    }

    public void LoadData()
    {
        if (Repository.TryGetData(out List<PickupData> savedPickups))
        {
            //AssembledPickups.Clear();
            foreach (var pickup in savedPickups)
            {
                AssembledPickups.AddPickup(pickup.ToPickup());
            }
            Debug.Log("Collection loaded: " + savedPickups.Count + " items.");
            foreach (var pickup in savedPickups)
            {
                Debug.Log($"Pickup {pickup.Name}");
            }
        }
        else
        {
            string jsonString = @"
{
  ""List`1"": ""[{\""Name\"":\""Conc1\"",\""Type\"":\""Type\"",\""Description\"":\""Conc1\"",\""HideDescription\"":\""Hide Description\"",\""Picture\"":\""Picture\"",\""Rendered\"":false,\""RenderedOnScreen\"":false,\""SimpleDict\"":{},\""NestedDict\"":{},\""NestedList\"":[]},{\""Name\"":\""Conc2\"",\""Type\"":\""Type\"",\""Description\"":\""Conc2\"",\""HideDescription\"":\""Hide Description\"",\""Picture\"":\""Picture\"",\""Rendered\"":false,\""RenderedOnScreen\"":false,\""SimpleDict\"":{},\""NestedDict\"":{},\""NestedList\"":[]},{\""Name\"":\""Conc3\"",\""Type\"":\""Type\"",\""Description\"":\""Conc3\"",\""HideDescription\"":\""Hide Description\"",\""Picture\"":\""Picture\"",\""Rendered\"":false,\""RenderedOnScreen\"":false,\""SimpleDict\"":{},\""NestedDict\"":{},\""NestedList\"":[]},{\""Name\"":\""Conc4\"",\""Type\"":\""Type\"",\""Description\"":\""Conc4\"",\""HideDescription\"":\""Hide Description\"",\""Picture\"":\""Picture\"",\""Rendered\"":false,\""RenderedOnScreen\"":false,\""SimpleDict\"":{},\""NestedDict\"":{},\""NestedList\"":[]},{\""Name\"":\""Conc5\"",\""Type\"":\""Type\"",\""Description\"":\""Conc5\"",\""HideDescription\"":\""Hide Description\"",\""Picture\"":\""Picture\"",\""Rendered\"":false,\""RenderedOnScreen\"":false,\""SimpleDict\"":{},\""NestedDict\"":{},\""NestedList\"":[]}]""
}";

            // Десериализация JSON-строки
            try
            {
                var jsonData = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
                if (jsonData != null && jsonData.ContainsKey("List`1"))
                {
                    string pickupsJson = jsonData["List`1"];
                    savedPickups = JsonConvert.DeserializeObject<List<PickupData>>(pickupsJson);
                    LoadPickups(savedPickups);
                }
                else
                {
                    Debug.LogWarning("Invalid JSON format. 'List`1' key not found.");
                }
            }
            catch (JsonException ex)
            {
                Debug.LogError("Failed to deserialize JSON: " + ex.Message);
            }
        }
    }

    private void LoadPickups(List<PickupData> savedPickups)
    {
        // Очистка текущих пикапов (если нужно)
        //AssembledPickups.Clear();

        // Добавление загруженных пикапов
        foreach (var pickup in savedPickups)
        {
            AssembledPickups.AddPickup(pickup.ToPickup());
        }

        Debug.Log("Collection loaded: " + savedPickups.Count + " items.");
        foreach (var pickup in savedPickups)
        {
            Debug.Log($"Pickup {pickup.Name}");
        }
    }

    public void SaveData()
    {
        _pickups = AssembledPickups.GetAllPickups();
        var pickupData = _pickups.Select(PickupData.FromPickup).ToList();
        Repository.SetData(pickupData);
        Debug.Log("Collection saved: " + _pickups.Count + " items.");
    }
}