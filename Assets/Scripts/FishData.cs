using UnityEngine;

[CreateAssetMenu(fileName = "NewFish", menuName = "Bestiary/Fish")]
public class FishData : ScriptableObject
{
    [Header("Basic Info")]
    public string fishName;
    public string fishScientificName;
    public string fishLocation;
    [TextArea]
    public string fishDescription;

    [Header("Visuals")]
    public Sprite fishSprite;
    public GameObject fishModel; // <-- 3D model prefab
    
}
