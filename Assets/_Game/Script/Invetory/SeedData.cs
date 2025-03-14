using UnityEngine;

[CreateAssetMenu(menuName ="Items/Seed")]
public class SeedData : ItemData
{
    // Time it takes before the seed matures into a crop
    public int daysToGrow;

    // The crop the seed will yeild
    public ItemData cropToYield;  

}
