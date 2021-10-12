using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormGenerator.JsonObject;

namespace FormGenerator.Element
{
    class Select : IElement
    {
        string name;
        string placeholder;
        bool required;
        string value;
        Label label;
        bool disabled;
        string _class;
        ValidationRule[] validationRules;
        SelectOption[] selectOptions;

        public Select(Item item)
        {
            Initialize(item);
        }

        public string GenerateTag()
        {
            StringBuilder select = new StringBuilder();
            select.Append("<select ");
            select.Append($"class=\"{_class}\" ");
            select.Append($"value=\"{value}\" ");
            select.Append($"name=\"{name}\" ");

            if (validationRules.Length != 0)
            {
                StringBuilder validRules = new StringBuilder();
                select.Append("data-validationRule=\"");
                foreach (var rule in validationRules)
                    validRules.Append(rule.Type + "|");
                select.Append(validRules.ToString().TrimEnd('|'));
                select.Append("\"");
            }

            if (placeholder != null)
                select.Append($"placeholder=\"{placeholder}\"");

            if (required)
                select.Append("required ");

            if (disabled)
                select.Append("disabled ");

            select.Append(">");

            foreach(var option in selectOptions)
            {
                select.Append("<option ");
                select.Append($"value=\"{option.Value}\" ");
                if (option.Selected)
                    select.Append("selected");
                select.AppendLine($">{option.Text}</option>");  
            }

            select.AppendLine("</select>");

            return label.GenerateTag() + "\n" + select.ToString();
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
            selectOptions = item.Attribute.SelectOptions;
        }
    }
}
