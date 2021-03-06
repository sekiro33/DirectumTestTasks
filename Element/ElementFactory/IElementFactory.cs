using FormGenerator.JsonObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormGenerator.Element
{
    interface IElementFactory
    {
        IElement CreateElement(string type, Item item);
    }
}
