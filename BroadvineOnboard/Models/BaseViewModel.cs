using System.Linq;
using System.ComponentModel;

namespace BroadvineOnboard.Models
{
    public abstract class BaseViewModel
    {
        protected BaseViewModel()
        {
            var propertyInfos = this.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                var attributes = propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), true);
                if (attributes.Any())
                {
                    var attribute = (DefaultValueAttribute)attributes[0];
                    propertyInfo.SetValue(this, attribute.Value, null);
                }
            }
        }
    }
}