namespace Lab4.Classes;

public class Date
{

	private int year;
    public int Year
    {
        get { return year; }
        set
        {
            if (isValid(day, month, value))
                year = value;
        }
    }

    private int month;
    public int Month
    {
        get { return month; }
        set
        {
            if (isValid(day, value, year))
                month = value;

        }
    }
    private int day;

	public int Day
	{
		get { return day; }
		set {
            if(isValid(value, month, year))
                day = value;
        }
	}
    public Date(int day, int month, int year)
    {
        if (isValid(day, month, year))
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }
    }
    private bool isValid(int day, int month, int year)
    {
        if (year > DateTime.Now.Year)
            return false;
        if (year == DateTime.Now.Year && month > DateTime.Now.Month)
            return false;
        if(year == DateTime.Now.Year && month == DateTime.Now.Month && day > DateTime.Now.Day)
            return false;
        if (month < 1 || month > 12)
            return false;
        if ((month == 1 || month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12) && (day <1 || day > 31))
            return false;
        if ((month == 4 || month == 6 || month == 9 || month == 11) && (day <1 || day > 30))
            return false;
        if(month == 2)
        {
            if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
            {
                if (day <1 || day > 29)
                    return false;
            }
            else
            {
                if (day <1 || day > 28)
                    return false;
            }
        }
        return true;
    }

}
