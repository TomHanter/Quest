using System.Collections.Generic;
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
        if (Repository.TryGetData(out List<Pickup> savedPickups))
        {
            AssembledPickups.Clear();
            foreach (var pickup in savedPickups)
            {
                AssembledPickups.AddPickup(pickup);
            }
            Debug.Log("Collection loaded: " + savedPickups.Count + " items.");
        }
        else
        {
            Debug.LogWarning("No saved collection found. Loading default collection.");
        }
    }

    public void SaveData()
    {
        Repository.SetData(_pickups);
        Debug.Log("Collection saved: " + _pickups.Count + " items.");
    }
}