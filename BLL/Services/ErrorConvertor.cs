using DATA.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ErrorConvertor
    {
        public static List<string> ConvertErrorsList(List<ErrorMessage> errorMessages)
        {
            var stringErrorsList = new List<string>();

            foreach (var errorMessage in errorMessages)
            {
                var errorCodeInfo = errorMessage.GetType().GetMember(errorMessage.ToString())[0];
                var descriptionAttribute = (DescriptionAttribute)errorCodeInfo.GetCustomAttribute(typeof(DescriptionAttribute));
                stringErrorsList.Add(descriptionAttribute.Description);
            }

            return stringErrorsList;
        }
        public static string ConvertError(ErrorMessage errorMessage)
        {
            var errorCodeInfo = errorMessage.GetType().GetMember(errorMessage.ToString())[0];
            var descriptionAttribute = (DescriptionAttribute)errorCodeInfo.GetCustomAttribute(typeof(DescriptionAttribute));
            return descriptionAttribute.Description;
        }
    }
}
