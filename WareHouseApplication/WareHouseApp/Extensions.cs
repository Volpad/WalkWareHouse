using System;

namespace WareHouseApp
{
    public static class Extensions
    {
        internal static string TranslateYarnType(this string yarntype)
        {
            if (yarntype.Equals("0"))
            {
                yarntype = "B140";
            }
            else if (yarntype.Equals("1"))
            {
                yarntype = "B120";
            }
            else if (yarntype.Equals("2"))
            {
                yarntype = "O130";
            }
            else if (yarntype.Equals("3"))
            {
                yarntype = "B260";
            }
            else if (yarntype.Equals("4"))
            {
                yarntype = "B250";
            }
            else if (yarntype.Equals("5"))
            {
                yarntype = "B240";
            }
            else if (yarntype.Equals("6"))
            {
                yarntype = "B234";
            }
            else if (yarntype.Equals("7"))
            {
                yarntype = "B280";
            }
            else
            {
                throw new Exception("Invalid Yarn Type!");
            }
            return yarntype;
        }
    }
}
