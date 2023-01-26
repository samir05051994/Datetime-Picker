using System.Globalization;

namespace DateTimePickerMaui
{
    public static class DateTimeUtil
    {
        public static ObservableRangeCollection<string> GetMonthList()
        {
            try
            {
                return CultureInfo.InstalledUICulture.DateTimeFormat.AbbreviatedMonthGenitiveNames.Where(x => x != string.Empty).ToCollection();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static ObservableRangeCollection<string> GetDayList(int y, int m)
        {
            try
            {
                int days = DateTime.DaysInMonth(y, m);
                ObservableRangeCollection<string> dayList = new();
                for (int i = 1; i <= days; i++)
                {
                    dayList.Add(i.ToString("00"));
                }
                return dayList;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static ObservableRangeCollection<string> GetYearList()
        {
            try
            {
                ObservableRangeCollection<string> yearList = new();
                for (int i = 1970; i <= DateTime.Now.Year; i++)
                {
                    yearList.Add(i.ToString());
                }
                return yearList;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static ObservableRangeCollection<string> GetHourList()
        {
            try
            {
                ObservableRangeCollection<string> hourList = new();
                for (int i = 0; i <= 23; i++)
                {
                    hourList.Add(i.ToString("00"));
                }
                return hourList;
            }
            catch (Exception) { return null; }
        }
        public static ObservableRangeCollection<string> GetMinList()
        {
            try
            {
                ObservableRangeCollection<string> minList = new();
                for (int i = 0; i <= 59; i++)
                {
                    minList.Add(i.ToString("00"));
                }
                return minList;
            }
            catch (Exception) { return null; }
        }
        public static int GetMonthInt(string month)
        {
            try
            {
                switch (month)
                {
                    case "Jan":
                        return 1;
                    case "Feb":
                        return 2;
                    case "Mar":
                        return 3;
                    case "Apr":
                        return 4;
                    case "May":
                        return 5;
                    case "Jun":
                        return 6;
                    case "Jul":
                        return 7;
                    case "Aug":
                        return 8;
                    case "Sep":
                        return 9;
                    case "Oct":
                        return 10;
                    case "Nov":
                        return 11;
                    case "Dec":
                        return 12;
                }
            }
            catch (Exception)
            {
                //
            }
            return 0;
        }
    }
}
