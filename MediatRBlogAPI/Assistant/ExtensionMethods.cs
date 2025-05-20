using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace MediatRBlogAPI;

public static class ExtensionMethods
{
    public static string ToShamsi(this DateTime val)
    {
        try
        {
			PersianCalendar pc = new PersianCalendar();
			return string.Format("{0:0000}/{1:00}/{2:00} {3:00}:{4:00}:{5:00}", pc.GetYear(val),
				pc.GetMonth(val), pc.GetDayOfMonth(val), val.Hour, val.Minute, val.Second);
		}
        catch
        {

            return "نامشخص";
        }
    }
    public static string ToShamsiString(this DateTime val)
    {
		try
		{
			string[] month = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
			PersianCalendar pc = new PersianCalendar();
			return string.Format("{0:0000} {1} {2:00}", pc.GetYear(val),
				month[pc.GetMonth(val) - 1], pc.GetDayOfMonth(val));
		}
		catch
		{

			return "نامشخص";
		}
	}
    public static string GetYearShamsi(this DateTime val)
    {
		try
		{
			PersianCalendar pc = new PersianCalendar();
			return pc.GetYear(val).ToString();
		}
		catch
		{

			return "نامشخص";
		}
	}
    public static string GetMonthShamsi(this DateTime val)
    {
		try
		{
			string[] month = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
			PersianCalendar pc = new PersianCalendar();
			return month[pc.GetMonth(val) - 1];
		}
		catch
		{

			return "نامشخص";
		}
	}
    public static string GetStatus(this int status)
    {
        switch (status)
        {
            case 0: return "وارد درگاه نشد";
            case 1: return "پرداخت انجام نشده است";
            case 2: return "پرداخت ناموفق بوده است";
            case 3: return "خطا رخ داده است";
            case 4: return "بلوکه شده";
            case 5: return "برگشت به پرداخت کننده";
            case 6: return "برگشت خورده سیستمی";
            case 10: return "در انتظار تایید پرداخت";
            case 100: return "پرداخت تایید شده است";
            case 101: return "پرداخت قبلا تایید شده است";
            case 200: return "به دریافت کننده واریز شد";
            default: return "نا معلوم";
        }
    }

    public static string Truncate(this string input, int length)
    {
        if (string.IsNullOrEmpty(input) || input.Length <= length)
        {
            return input;
        }
        else
        {
            return $"{input.Substring(0, length)}...";
        }
    }

    public static string GetEnumDisplayName(this Enum value)
    {
        if (value == null) return null;

        var field = value.GetType().GetField(value.ToString());

        if (field == null) return value.ToString();

        var displayAttribute = field.GetCustomAttribute<DisplayAttribute>();
        if (displayAttribute != null)
        {
            return displayAttribute.Name;
        }

        var descriptionAttribute = field.GetCustomAttribute<DescriptionAttribute>();
        if (descriptionAttribute != null)
        {
            return descriptionAttribute.Description;
        }

        return value.ToString();
    }
}