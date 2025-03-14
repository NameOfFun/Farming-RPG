using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    public string descrtiption;

    // Icon to be displayed in Ui;
    public Sprite thumbnails;

    // GameObject to be shown in the scene
    public GameObject gameModel;
}
