using UnityEngine;

public class Land : MonoBehaviour
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the renderer component 
        renderer = GetComponent<Renderer>();

        // Set the land to soil by default
        SwitchLandStatus(LandStatus.Soil);

        // Deselect the land by default
        Select(false);
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
        SwitchLandStatus(LandStatus.FarmLand);
    }
}
