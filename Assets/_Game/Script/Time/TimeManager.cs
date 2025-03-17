using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("Internal Clock")]
    [SerializeField] GameTimestamp timeStamp;
    public float timeScale = 1f;

    [Header("Day and Night cycle")]
    // The transform of the directional light(sun)
    public Transform sunTransform;

    // (Observer pattern) List of Object to inform of changes to the time
    List<ITimeTracker> listeners = new List<ITimeTracker>();
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
        timeStamp = new GameTimestamp(0, GameTimestamp.Season.Spring, 77, 12, 38);
        StartCoroutine(TimeUpdate());
    }

    IEnumerator TimeUpdate()
    {
        while (true)
        {
            Tick();
            yield return new WaitForSeconds(1 / timeScale);
        }
    }
    // A tick of the in-gamee time
    public void Tick()
    {
        timeStamp.UpdateClock();

        // Inform each of the listeners of the new time state
        foreach (ITimeTracker listener in listeners)
        {
            listener.ClockUpdate(timeStamp);
        }
        UpdateSunMovement();

    }

    // Day and Night cycle
    void UpdateSunMovement()
    {
        // Convert the current time to minutes
        int timeInMinutes = GameTimestamp.HoursToMinutes(timeStamp.hour) + timeStamp.minute;

        // Sun moves 15 degrees in an hour
        // .25 degree in a minutes
        // At midnight (0:00), the angle of the sun should be -90
        float sunAngle = 0.25f * timeInMinutes - 90f;

        // Apply the angle to the direction light 
        sunTransform.eulerAngles = new Vector3(sunAngle, 0, 0);
    }

    // Get the timestamp
    public GameTimestamp GetGameTimeStamp()
    {
        // Return a cloned instance
        return new GameTimestamp(timeStamp);
    }
    // Handle Listeners
    // Add the obect to the list of listeners
    public void RegisterTracker(ITimeTracker listener)
    {
        listeners.Add(listener);
    }

    // Remove the obect from the list of listeners
    public void UnregisterTracker(ITimeTracker listener)
    {
        listeners.Remove(listener);
    }
}
