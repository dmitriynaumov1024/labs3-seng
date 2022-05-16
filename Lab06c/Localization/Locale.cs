using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public class Locale
{
    public IDictionary<string, string> data { get; set; }

    public string this[string key] {
        get {
            if (this.data.ContainsKey(key)) return this.data[key];
            else return key; 
        }
    }
}

public static class LocaleProvider
{
    static string LocaleKey = "english";

    static IDictionary<string, Locale> KnownLocales = 
    new Func<IDictionary<string, Locale>>(() => {
        var result = new SortedDictionary<string, Locale> ();
        foreach (var fPath in Directory.EnumerateFiles("locale", "*.json")) {
            result[fPath.Split(new[] {'\\', '.'})[1].ToLower()] = new Locale {
                data = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(fPath))
            };
        }
        return result;
    })();

    static Locale CurrentLocale {
        get {
            return KnownLocales[LocaleKey];
        }
    }

    public static event Action<Locale> LocaleChanged;

    public static void UseLocale (this object obj, Action<Locale> handler)
    {
        LocaleChanged += handler;
        handler(CurrentLocale);
    }

    public static void SetLocale (this object obj, string newKey)
    {
        newKey = newKey.ToLower();
        if (KnownLocales.ContainsKey(newKey)) {
            LocaleKey = newKey;
            LocaleChanged.Invoke(CurrentLocale);
        }
    }

    public static List<KeyValuePair<string, string>> LocaleKeysAndNames {
        get {
            return KnownLocales.Select (kvPair => 
                new KeyValuePair<string, string> (
                    kvPair.Key,
                    kvPair.Value["nativeName"]
            )).ToList();
        }
    }
}
