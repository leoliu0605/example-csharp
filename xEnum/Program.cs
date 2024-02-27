using System.Reflection;

namespace xEnum;

class Program
{
    static void Main(string[] args)
    {
        var text = string.Empty;
        Console.WriteLine("/// <output>");
        text = MyEnum.None.GetSampleEnumText();
        Console.WriteLine(
            $"/// {nameof(MyEnum.None)} 的 Attribute 訊息: {text}, {nameof(string.IsNullOrEmpty)}: {string.IsNullOrEmpty(text)}"
        );
        text = MyEnum.MyEnum01.GetSampleEnumText();
        Console.WriteLine(
            $"/// {nameof(MyEnum.MyEnum01)} 的 Attribute 訊息: {text}, {nameof(string.IsNullOrEmpty)}: {string.IsNullOrEmpty(text)}"
        );
        text = MyEnum.MyEnum02.GetSampleEnumText();
        Console.WriteLine(
            $"/// {nameof(MyEnum.MyEnum02)} 的 Attribute 訊息: {text}, {nameof(string.IsNullOrEmpty)}: {string.IsNullOrEmpty(text)}"
        );
        text = MyEnum.MyEnum03.GetSampleEnumText();
        Console.WriteLine(
            $"/// {nameof(MyEnum.MyEnum03)} 的 Attribute 訊息: {text}, {nameof(string.IsNullOrEmpty)}: {string.IsNullOrEmpty(text)}"
        );
        Console.WriteLine("/// </output>");
        Console.WriteLine();
    }
}

public enum MyEnum
{
    [EnumAttribute("This is the empty one.")]
    None,

    [EnumAttribute("This is the first one.")]
    MyEnum01,

    [EnumAttribute("This is the second one.")]
    MyEnum02,

    MyEnum03,
};

public static class EnumExtension
{
    public static string GetSampleEnumText(this MyEnum sample)
    {
        FieldInfo? fi = sample.GetType().GetField(sample.ToString());
        if (fi != null)
        {
            if (fi.GetCustomAttribute(typeof(EnumAttribute), false) is EnumAttribute attribute)
            {
                return attribute.Text;
            }
        }
        return string.Empty;
    }
}

public class EnumAttribute : Attribute
{
    private readonly string _text = string.Empty;

    public EnumAttribute(string text)
    {
        _text = text;
    }

    public string Text => _text;
}
