using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormGenerator.JsonObject;

namespace FormGenerator.Element
{
    class TextArea : IElement
    {
        string name;
        string placeholder;
        bool required;
        string value;
        Label label;
        bool disabled;
        string _class;
        ValidationRule[] validationRules;

        public TextArea(Item item)
        {
            Initialize(item);
        }

        public string GenerateTag()
        {
            StringBuilder textarea = new StringBuilder();
            textarea.Append("<textarea ");
            textarea.Append($"class=\"{_class}\" ");
            textarea.Append($"value=\"{value}\" ");
            textarea.Append($"name=\"{name}\" ");

            if (validationRules.Length != 0)
            {
                StringBuilder validRules = new StringBuilder();
                textarea.Append("data-validationRule=\"");
                foreach (var rule in validationRules)
                    validRules.Append(rule.Type + "|");
                textarea.Append(validRules.ToString().TrimEnd('|'));
                textarea.Append("\"");
            }

            if (placeholder != null)
                textarea.Append($"placeholder=\"{placeholder}\"");

            if (required)
                textarea.Append("required ");

            if (disabled)
                textarea.Append("disabled ");
            textarea.Append(">");
            textarea.Append("</textarea>");

            return label.GenerateTag() + "\n" + textarea.ToString();
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
