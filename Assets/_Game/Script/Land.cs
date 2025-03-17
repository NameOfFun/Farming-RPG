using UnityEngine;

public class Land : MonoBehaviour, ITimeTracker
{
    public enum LandStatus
    {
        Soil, FarmLand, Watered
    }

    public LandStatus landStatus;

    public Material soilMat, farmlandMat, wateredMat;
    new Renderer renderer;

    // The selection gameobject to enable when the player is slecting the land
    public GameObject select;

    // Cache the time the land was watered
    GameTimestamp timeWatered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the renderer component 
        renderer = GetComponent<Renderer>();

        // Set the land to soil by default
        SwitchLandStatus(LandStatus.Soil);

        // Deselect the land by default
        Select(false);

        // Add this to the Listeners
        TimeManager.Instance.RegisterTracker(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        // Set land status accordingly
        landStatus = statusToSwitch;

        Material materialToWSwitch = soilMat;
        switch (statusToSwitch)
        {
            // soil 
            case LandStatus.Soil:
                materialToWSwitch = soilMat;
                break;
            // farmland
            case LandStatus.FarmLand:
                materialToWSwitch = farmlandMat;
                break;
            // watered
            case LandStatus.Watered:
                materialToWSwitch = wateredMat;

                // Cache the time it was watered 
                timeWatered = TimeManager.Instance.GetGameTimeStamp();
                break;
        }

        // Get the renderer to apply the changes
        renderer.material = materialToWSwitch;
    }

    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }

    // when the payer presses the interact button while selecting this land
    public void Interact()
    {
        // Interaction
        //SwitchLandStatus(LandStatus.FarmLand);

        //  Check the Player's tool slot
        ItemData toolSlot = InventoryManager.Instance.equippedTool;

        //  Try casting the itemdata in the toolslot as EquipmentData
        EquipmentData equipmentData = toolSlot as EquipmentData;

        // Check if it is of type EquipmentData
        if (equipmentData != null)
        {
            // Get the tool type 
            EquipmentData.ToolType toolType = equipmentData.toolType;

            switch (toolType)
            {
                case EquipmentData.ToolType.Hoe:
                    SwitchLandStatus(LandStatus.FarmLand);
                    break;
                case EquipmentData.ToolType.WateringCan:
                    SwitchLandStatus(LandStatus.Watered);
                    break;
            }
        }
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        // Cehcked if 24 hours has passed hours has passed since last watered 
        if (landStatus == LandStatus.Watered)
        {
            // Hours since the land was watered
            int hoursElapsed = GameTimestamp.CompareTimestamps(timeWatered, timestamp);

            //Debug.Log(hoursElapsed + " since Season " + timeWatered.season + " to " + timestamp.season);
            //Debug.Log(hoursElapsed + " since Year " + timeWatered.year + " to " + timestamp.year);
            //Debug.Log(hoursElapsed + " since Hour " + timeWatered.hour + " to " + timestamp.hour);
            //Debug.Log(hoursElapsed + " since Minute " + timeWatered.minute + " to " + timestamp.minute);

            if(hoursElapsed > 24)
            {
                // Dry up (Switch back to farmland)
                SwitchLandStatus(LandStatus.FarmLand);
            }
        }
    }
}
