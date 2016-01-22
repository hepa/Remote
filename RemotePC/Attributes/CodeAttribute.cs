using System;

namespace RemotePC.Attributes
{
    public class CodeAttribute : Attribute
    {
         public string Code { get; set; }

        public CodeAttribute(string code)
        {
            Code = code;
        }
    }
}