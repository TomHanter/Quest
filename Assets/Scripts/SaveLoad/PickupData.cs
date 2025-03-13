using System.Collections.Generic;

[System.Serializable]
public class PickupData
{
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public string HideDescription { get; set; }
    public string Picture { get; set; }
    public bool Rendered { get; set; }
    public Dictionary<string, string> SimpleDict { get; set; }
    public SerializableDictionary<string, int> NestedDict { get; set; }
    public List<SerializableDictionary<string, string>> NestedList { get; set; }

    public PickupData(Pickup pickup)
    {
        Name = pickup.Name;
        Type = pickup.Type;
        Description = pickup.Description;
        HideDescription = pickup.HideDescription;
        Picture = pickup.Picture;
        Rendered = pickup.Rendered;
        SimpleDict = pickup.SimpleDict;
        NestedDict = pickup.NestedDict;
        NestedList = pickup.NestedList;
    }
}