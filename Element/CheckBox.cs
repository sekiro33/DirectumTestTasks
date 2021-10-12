using FormGenerator.JsonObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormGenerator.Element
{
    class CheckBox : IElement
    {
        string name;
        string placeholder;
        bool required;
        Label label;
        bool disabled;
        string _class;
        bool _checked;
        ValidationRule[] validationRules;

        public CheckBox(Item item)
        {
            Initialize(item);
        }

        public string GenerateTag()
        {
            StringBuilder checkBox = new StringBuilder();
            checkBox.Append("<input type=\"checkbox\" ");
            checkBox.Append($"class=\"{_class}\" ");
            checkBox.Append($"name=\"{name}\" ");

            if (validationRules.Length != 0)
            {
                StringBuilder validRules = new StringBuilder();
                checkBox.Append("data-validationRule=\"");
                foreach (var rule in validationRules)
                    validRules.Append(rule.Type + "|");
                checkBox.Append(validRules.ToString().TrimEnd('|'));
                checkBox.Append("\"");
            }

            if (placeholder != null)
                checkBox.Append($"placeholder=\"{placeholder}\"");

            if (required)
                checkBox.Append("required ");

            if (disabled)
                checkBox.Append("disabled ");

            if (_checked)
                checkBox.Append("checked ");

            checkBox.Append("/>");

            return label.GenerateTag() + "\n" + checkBox.ToString();
        }

        public void Initialize(Item item)
        {
            name = item.Attribute.Name;
            placeholder = item.Attribute.PlaceHolder;
            required = item.Attribute.Required;
            label = new Label(item);
            disabled = item.Attribute.Disabled;
            _class = item.Attribute.Class;
            _checked = item.Attribute.Checked;
            validationRules = item.Attribute.ValidationRules;
        }
    }
}
