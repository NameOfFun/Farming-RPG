using UnityEngine;

[System.Serializable]
public class GameTimestamp
{
    public int year;
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }
    public Season season;

    public enum DayOfWeek
    {
        Saturday,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
    }
    public DayOfWeek dayOfWeek;
    public int day;
    public int hour;
    public int minute;

    // Constructor
    public GameTimestamp(int year, Season season, int day, int hour, int minute)
    {
        this.year = year;
        this.season = season;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    // Creating a new instance of a GameTimestamp from another pre-existing one
    public GameTimestamp(GameTimestamp timestamp)
    {
        this.year = timestamp.year;
        this.day = timestamp.day;
        this.hour = timestamp.hour;
        this.minute = timestamp.minute;
    }
    public void UpdateClock()
    {
        minute++;
        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }

        if (hour >= 24)
        {
            hour = 0;
            day++;
        }

        if (day > 90)
        {
            day = 1;
            if (season == Season.Winter)
            {
                season = Season.Spring;
                year++;
            }
            else
            {
                season++;
            }
        }
    }

    public DayOfWeek GetDayOfTheWeek()
    {
        int daysPassed = YearsToDays(year) + SeasonsToDays(season) + day;
        // Remainder after dividing by 7
        int dayIndex = daysPassed % 7;

        return (DayOfWeek)dayIndex; 
    }
    public static int HoursToMinutes(int hour)
    {
        return hour * 60;
    }

    public static int DaysToHours(int day)
    {
        return day * 24;
    }

    public static int SeasonsToDays(Season season)
    {
        int seasonIndex = (int)season;
        return seasonIndex * 30 * 3;
    }

    public static int YearsToDays(int year)
    {
        return year * 30 * 4;
    }

    public static int CompareTimestamps(GameTimestamp timestamp1, GameTimestamp timestamp2)
    {
        // Convert timestamps to the hours
        int timestamp1Hours = DaysToHours(YearsToDays(timestamp1.year)) + DaysToHours(SeasonsToDays(timestamp1.season)) + DaysToHours(timestamp1.day) + timestamp1.hour;
        int timestamp2Hours = DaysToHours(YearsToDays(timestamp2.year)) + DaysToHours(SeasonsToDays(timestamp2.season)) + DaysToHours(timestamp2.day) + timestamp2.hour;
        int different = timestamp1Hours - timestamp2Hours;
        return Mathf.Abs(different);
    }
}