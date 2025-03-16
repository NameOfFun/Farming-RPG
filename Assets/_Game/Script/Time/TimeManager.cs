using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }
    [SerializeField] GameTimestamp timeStamp;
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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeStamp = new GameTimestamp(0, GameTimestamp.Season.Fall, 3, 4, 9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
