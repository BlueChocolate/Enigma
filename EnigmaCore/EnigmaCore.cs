using EnigmaCore;
using System;
using System.Dynamic;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace EnigmaCore
{
    public class Enigma
    {
        public List<Rotor> Rotors { get; set; }
        public Reflector Reflector { get; set; }
        public Plugboard Plugboard { get; set; }

        public Enigma()
        {
            Rotors = new List<Rotor>();
        }



        #region 序列化
        private static JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            // 整齐打印
            WriteIndented = true,
            // 设置Json字符串支持的编码，默认情况下，序列化程序会转义所有非 ASCII 字符。 即，会将它们替换为 \uxxxx，其中 xxxx 为字符的 Unicode
            // 代码。 可以通过设置Encoder来让生成的josn字符串不转义指定的字符集而进行序列化 下面指定了基础拉丁字母和中日韩统一表意文字的基础 Unicode 块（U+4E00-U+9FCC）。 
            // 基本涵盖了除使用西里尔字母以外所有西方国家的文字和亚洲中日韩越的文字
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs, UnicodeRanges.CjkSymbolsandPunctuation),
            // 反序列化不区分大小写
            PropertyNameCaseInsensitive = true,
            //// 驼峰命名
            //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            //// 对字典的键进行驼峰命名
            //DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            // 忽略只读属性，因为只读属性只能序列化而不能反序列化，所以在以json为储存数据的介质的时候，序列化只读属性意义不大
            IgnoreReadOnlyProperties = true,
            // 允许在反序列化的时候原本应为数字的字符串（带引号的数字）转为数字
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        public string GetJson()
        {
            string jsonString = JsonSerializer.Serialize(this);
            return jsonString;
        }
        public static string GetJsonByEnigma(Enigma enigma)
        {
            string jsonString = JsonSerializer.Serialize(enigma, serializerOptions);
            return jsonString;
        }
        public static Enigma GetEnigmaByJson(string json)
        {
            var enigma = JsonSerializer.Deserialize<Enigma>(json, serializerOptions);
            return enigma ?? new Enigma();
        }

        #endregion

        #region 事件
        // 事件数据
        public class LampArgs : EventArgs
        {
            public int LampNum { get; set; }
            public char LampChar { get; set; }

            public LampArgs(int lampNum, char lampChar)
            {
                LampNum = lampNum;
                LampChar = lampChar;
            }
        }
        // 事件
        public event EventHandler<LampArgs> OnLampChanged = delegate { };
        #endregion
    }

    public class Rotor
    {
        // 名称
        public string Name { get; set; }
        // 接线方式
        public string Encode { get; set; }
        // 字母环
        public int RingSetting { get; set; }
        // 初始位置
        public int RotorPosition { get; set; }
        // 刻痕位置
        public int[] NotchPosition { get; set; }

        public Rotor(string name = "Default", string encode = "ABCDEFGHIJKLMNOPQRSTUVWXYZ", int ringSetting = 0, int rotorPosition = 0, params int[] notchPosition)
        {
            Name = name;
            Encode = encode;
            RingSetting= ringSetting;
            RotorPosition = rotorPosition;
            NotchPosition = notchPosition;
        }



        public bool IsVerified()
        {
            return true;
        }

        public static class RotorLibrary
        {
            public static readonly Rotor RotorI = new Rotor("I", "EKMFLGDQVZNTOWYHXUSPAIBRCJ", 0, 0, 16);
            public static readonly Rotor RotorII = new Rotor("II", "AJDKSIRUXBLHWTMCQGZNPYFVOE", 0, 0, 4);
            public static readonly Rotor RotorIII = new Rotor("III", "BDFHJLCPRTXVZNYEIWGAKMUSQO", 0, 0, 21);
            public static readonly Rotor RotorIV = new Rotor("IV", "ESOVPZJAYQUIRHXLNFTGKDCMWB", 0, 0, 9);
            public static readonly Rotor RotorV = new Rotor("V", "VZBRGITYUPSDNHLXAWMJQOFECK", 0, 0, 25);
            public static readonly Rotor RotorVI = new Rotor("VI", "JPGVOUMFYQBENHZRDKASXLICTW", 0, 0, 12, 25);
            public static readonly Rotor RotorVII = new Rotor("VII", "NZJHGRCXMYSWBOUFAIVLPEKQDT", 0, 0, 12, 25);
            public static readonly Rotor RotorVIII = new Rotor("VIII", "FKQHTLXOCBJSPDZRAMEWNIUYGV", 0, 0, 12, 25);
        }
    }

    public class Reflector
    {
        // 名称
        public string Name { get; set; }
        // 接线方式
        public string Encode { get; set; }
        // 反射器旋转位置
        public int ReflectorPosition { get; set; }

        public Reflector(string name="Default", string encode = "ZYXWVUTSRQPONMLKJIHGFEDCBA", int reflectorPosition=0)
        {
            Name = name;
            Encode = encode;
            ReflectorPosition = reflectorPosition;
        }
        public static class ReflectorLibrary
        {
            public static readonly Reflector ReflectorB = new Reflector("B", "YRUHQSLDPXNGOKMIEBFZCWVJAT", 0);
            public static readonly Reflector ReflectorC = new Reflector("C", "FVPJIAOYEDRZXWGCTKUQSBNMHL", 0);
        }
    }

    public class Plugboard
    {
        public string[] Connections { get; set; }
        public Plugboard(params string[] connections)
        {
            Connections = connections;
        }
        public static class PlugboardLibrary
        {
            public static readonly Plugboard PlugboardCross = new Plugboard("AZ", "BY", "CX", "DW", "EV", "FU", "GT", "HS", "IR", "JQ", "KP", "LO", "MM");
            public static readonly Plugboard PlugboardShift = new Plugboard("AN", "BO", "CP", "DQ", "ER", "FS", "GT", "HU", "IV", "JW", "KX", "LY", "MZ");
        }
    }
    public class Lampboard { }
    public class Keyboard
    {
        class Stecker { }
    }
}