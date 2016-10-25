using Newtonsoft.Json.Serialization;

namespace RecipeMs.CrossCutting.Common.Query
{

    public class QueryFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        public QueryFilter(string propertyName, string value)
        {
            PropertyName = propertyName;
            Value = value;
        }
        public QueryFilter(string propertyName, string value, Operator operatorValue)
        {
            PropertyName = propertyName;
            Value = value;
            Operator = operatorValue;
        }

        public string PropertyName { get; set; }
        public string Value { get; set; }
        public Operator Operator { get; set; } = Operator.Equals;
    }
}