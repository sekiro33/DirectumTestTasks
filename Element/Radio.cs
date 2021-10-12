using FormGenerator.JsonObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormGenerator.Element
{
    class Radio : IElement
    {
        string name;
        string placeholder;
        bool required;
        string value;
        Label label;
        bool disabled;
        string _class;
        ValidationRule[] validationRules;
        RadioOption[] radioOptions;

        public Radio(Item item)
        {
            Initialize(item);
        }

        public string GenerateTag()
        {
            StringBuilder radio = new StringBuilder();
            foreach (var option in radioOptions)
            {
                Label label = new Label(option.Label);
                radio.AppendLine(label.GenerateTag());
                radio.Append($"<input type=\"radio\" ");
                radio.Append($"value=\"{option.Value}\" ");
                radio.Append($"class=\"{_class}\" ");
                radio.Append($"name=\"{name}\" ");

                if (validationRules.Length != 0)
                {
                    StringBuilder validRules = new StringBuilder();
                    radio.Append("data-validationRule=\"");
                    foreach (var rule in validationRules)
                        validRules.Append(rule.Type + "|");
                    radio.Append(validRules.ToString().TrimEnd('|'));
                    radio.Append("\"");
                }

                if (placeholder != null)
                    radio.Append($"placeholder=\"{placeholder}\"");

                if (option.Checked)
                    radio.Append("checked ");

                if (required)
                    radio.Append("required ");

                if (disabled)
                    radio.Append("disabled ");

                radio.AppendLine("/>");
            }

            return label.GenerateTag() + "\n" + radio.ToString();
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
            radioOptions = item.Attribute.RadioOptions;
        }
    }
}
