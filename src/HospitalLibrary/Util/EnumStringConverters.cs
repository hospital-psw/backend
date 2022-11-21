namespace HospitalLibrary.Util
{
    using HospitalLibrary.Core.Model.Blood.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class EnumStringConverters
    {
        public static string GetString(BloodType type)
        {
            switch (type)
            {
                case BloodType.A_PLUS:
                    return "A positive";
                case BloodType.A_MINUS:
                    return "A negative";
                case BloodType.B_PLUS:
                    return "B positive";
                case BloodType.B_MINUS:
                    return "B negative";
                case BloodType.AB_PLUS:
                    return "AB positive";
                case BloodType.AB_MINUS:
                    return "AB negative";
                case BloodType.O_PLUS:
                    return "O positive";
                case BloodType.O_MINUS:
                    return "O negative";
                default:
                    return "";
            }
                
        }
    }
}
