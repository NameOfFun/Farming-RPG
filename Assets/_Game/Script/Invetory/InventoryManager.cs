using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    // Tool slots
    public ItemData[] tools = new ItemData[8];
    // Tool in the playe's hand
    public ItemData equippedTool = null;

    // Item slots
    public ItemData[] items = new ItemData[8];
    // Item in the playe's hand
    public ItemData equippedItem = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
