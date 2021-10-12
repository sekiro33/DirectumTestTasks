using System.Text;
using FormGenerator.JsonObject;

namespace FormGenerator.Element
{
    class Text : IElement
    {
        string name;
        string placeholder;
        bool required;
        string value;
        Label label;
        bool disabled;
        string _class;
        ValidationRule[] validationRules;

        public Text(Item item)
        {
            Initialize(item);
        }

        public string GenerateTag()
        {
            StringBuilder text = new StringBuilder();
            text.Append("<input type=\"text\" ");
            text.Append($"class=\"{_class}\" ");
            text.Append($"value=\"{value}\" ");
            text.Append($"name=\"{name}\" ");

            if (validationRules.Length != 0)
            {
                StringBuilder validRules = new StringBuilder();
                text.Append("data-validationRule=\"");
                foreach (var rule in validationRules)
                    validRules.Append(rule.Type + "|");
                text.Append(validRules.ToString().TrimEnd('|'));
                text.Append("\"");
            }

            if (placeholder != null)
                text.Append($"placeholder=\"{placeholder}\"");

            if (required)
                text.Append("required ");

            if (disabled)
                text.Append("disabled ");

            text.Append("/>");

            return label.GenerateTag() + "\n" + text.ToString();
        }

        public void Initialize(Item item)
        {
            name = item.Attribute.Name;
            placeholder = item.Attribute.PlaceHolder;
            required = item.Attribute.Required;
            value = item.Attribute.Value;
            label = new Label(item);
            disabled = item.Attribute.Disabled;
            _class = item.Attribute.Class;
            validationRules = item.Attribute.ValidationRules;
        }
    }
}
