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

        if (day > 30)
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
}