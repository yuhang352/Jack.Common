using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace YH.Common.Extensions
{
    public static class GlobalExtensions
    {
        public static bool IsNullOrEmpty(this string strValue)
        {
            if (!string.IsNullOrEmpty(strValue) && !(strValue == "") && strValue.Trim().Length != 0)
            {
                return strValue.ToLower() == "null";
            }

            return true;
        }

        public static bool IsNullOrEmpty(this string[] str)
        {
            if (str != null && str.Length != 0)
            {
                if (str.Length == 1)
                {
                    return str[0].IsNullOrEmpty();
                }

                return false;
            }

            return true;
        }

        public static bool IsNullOrEmpty(this ArrayList arrList)
        {
            if (arrList != null && arrList.Count != 0)
            {
                if (arrList.Count == 1)
                {
                    return arrList[0]!.ToString().IsNullOrEmpty();
                }

                return false;
            }

            return true;
        }

        public static bool IsNullOrEmpty(this List<string> list)
        {
            if (list != null && list.Count != 0)
            {
                if (list.Count == 1)
                {
                    return list[0].IsNullOrEmpty();
                }

                return false;
            }

            return true;
        }

        public static bool IsNullOrEmpty(this object strValue)
        {
            if (strValue != null)
            {
                return strValue == DBNull.Value;
            }

            return true;
        }

        public static int ToInt(this object strValue)
        {
            return strValue.ToInt(0);
        }

        public static int ToInt(this object strValue, int defValue)
        {
            if (strValue == null)
            {
                return defValue;
            }

            int.TryParse(strValue.ToString()!.Trim(), out defValue);
            return defValue;
        }

        public static long ToLong(this object str)
        {
            return str.ToLong(0L);
        }

        public static long ToLong(this object strValue, long defValue)
        {
            if (strValue == null)
            {
                return defValue;
            }

            long.TryParse(strValue.ToString()!.Trim(), out defValue);
            return defValue;
        }

        public static decimal ToDecimal(this object strValue)
        {
            return strValue.ToDecimal(0m);
        }

        public static decimal ToDecimal(this object strValue, decimal defValue)
        {
            if (strValue == null || strValue.ToString()!.Trim().Length > 30)
            {
                return defValue;
            }

            strValue = strValue.ToString()!.Replace(",", "").Trim();
            decimal result = defValue;
            if (strValue != null && new Regex("^([-]|[0-9])[0-9]*(\\.\\w*)?$").IsMatch(strValue.ToString()))
            {
                result = Convert.ToDecimal(Convert.ToDecimal(strValue).ToString());
            }

            return result;
        }

        public static double ToDouble(this object strValue)
        {
            return strValue.ToDouble(0.0);
        }

        public static double ToDouble(this object strValue, double defValue)
        {
            double result = defValue;
            if (strValue != null && strValue != DBNull.Value)
            {
                double.TryParse(strValue.ToString(), out result);
                return result;
            }

            return result;
        }

        public static float ToFloat(this object strValue)
        {
            return strValue.ToFloat(0f);
        }

        public static float ToFloat(this object strValue, float defValue)
        {
            if (strValue == null || strValue.ToString()!.Length > 10)
            {
                return defValue;
            }

            float result = defValue;
            if (strValue != null && new Regex("^([-]|[0-9])[0-9]*(\\.\\w*)?$").IsMatch(strValue.ToString()))
            {
                result = Convert.ToSingle(strValue);
            }

            return result;
        }

        public static short ToShort(this object strValue)
        {
            return (short)strValue.ToInt(0);
        }

        public static bool ToBool(this object strValue)
        {
            return strValue.ToBool(defValue: false);
        }

        public static bool ToBool(this object strValue, bool defValue)
        {
            if (strValue != null)
            {
                if (string.Compare(strValue.ToString(), "true", ignoreCase: true) == 0)
                {
                    return true;
                }

                if (string.Compare(strValue.ToString(), "false", ignoreCase: true) == 0)
                {
                    return false;
                }
            }

            return defValue;
        }

        public static Guid ToGuid(this object strValue)
        {
            return strValue.ToGuid(Guid.Empty);
        }

        public static Guid ToGuid(this object strValue, Guid defValue)
        {
            if (strValue == null)
            {
                return defValue;
            }

            try
            {
                return new Guid(strValue.ToString());
            }
            catch
            {
                return defValue;
            }
        }

        public static DateTime ToDate(this object date)
        {
            return date.ToDate(StaticConst.DATEBEGIN);
        }

        public static DateTime ToDate(this object date, DateTime defValue)
        {
            try
            {
                if (date == null || string.IsNullOrEmpty(date.ToString()))
                {
                    return defValue;
                }

                return DateTime.Parse(date.ToString());
            }
            catch
            {
                return defValue;
            }
        }

        public static string ToStringDefault(this object strValue)
        {
            return strValue.ToStringDefault("");
        }

        public static string ToStringDefault(this object strValue, string defValue)
        {
            if (strValue == null || strValue == DBNull.Value)
            {
                return defValue;
            }

            return strValue.ToString();
        }

        public static string ToStringDefault(this Dictionary<string, string> strValue)
        {
            return strValue.ToStringDefault("");
        }

        public static string ToStringDefault(this Dictionary<string, string> strValue, string defValue)
        {
            if (strValue == null || strValue.Count == 0)
            {
                return defValue;
            }

            string text = "";
            foreach (KeyValuePair<string, string> item in strValue)
            {
                text = text + item.Key + "=" + item.Value + "&";
            }

            return "?" + text.TrimEnd('&');
        }

        public static decimal PercentToDecimal(this string strValue)
        {
            if (strValue.IsNullOrEmpty())
            {
                return 0M;
            }
            if (decimal.TryParse(strValue.TrimEnd('%'), out decimal d) && d > 0)
            {
                return d / 100;
            }
            return 0M;
        }

        /// <summary>
        /// Converts a decimal of type Decimal to a percentage string.
        /// Round by default.
        /// </summary>
        /// <param name="d">A decimal type value.</param>
        /// <param name="decimals">The number of decimal places in the return value.</param>
        /// <returns>The string of type Decimal to a percentage string.</returns>
        public static string ToPercent(this decimal d, int decimals = 0)
        {
            if (d.IsNullOrEmpty()
                || d <= 0)
            {
                return "0%";
            }

            return d.ToString($"P{decimals}");
        }

        /// <summary>
        /// Converts a double of type Decimal to a percentage string.
        /// Round by default.
        /// </summary>
        /// <param name="d">A double type value.</param>
        /// <param name="decimals">The number of double places in the return value.</param>
        /// <returns>The string of type Double to a percentage string.</returns>
        public static string ToPercent(this double d, int decimals = 0)
        {
            if (d.IsNullOrEmpty()
                || d <= 0)
            {
                return "0%";
            }

            return d.ToString($"P{decimals}");
        }
    }
}
