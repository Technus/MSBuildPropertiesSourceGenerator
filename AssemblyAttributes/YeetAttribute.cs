namespace AssemblyAttributes
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class YeetAttribute:Attribute
    {
        readonly string someText;
        public YeetAttribute() : this(string.Empty) { }
        public YeetAttribute(string txt) { someText = txt; }
    }
}