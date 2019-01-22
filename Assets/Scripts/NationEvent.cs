using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/NationEvent", order = 1)]
public class NationEvent : ScriptableObject {
    public string eventName;
    public string eventDescription;
    public int economicImpact;
    public int environmentalImpact;
    public int socialImpact;
}