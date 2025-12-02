using System.Text.RegularExpressions;
using GMutagenEngine.Infrastructure.Storing.Storages.Sync.Indexed;
using GMutagenEngine.Utils;

namespace GMutagenEngine.Infrastructure.Naming;

public interface IWordFormatter
{
    string FormatWord(string word);
}

public interface IWordsFormatter
{
    IEnumerable<string> FormatWords(IEnumerable<string> words);
}

public class SplitterByChars(StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries, params char[] chars)
    : IStringSplitter
{
    public SplitterByChars(StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries) : this(options,
        Literals.DELIMITERS_CHARS)
    {
    }

    public string[] Split(string data)
    {
        return data.Split(chars, options);
    }
}



public interface IObjectFormatter
{
    string FormatObject(object obj);
}

public interface IObjectFormatter<in T> : IObjectFormatter
{
    string FormatObject(T obj);
}

public interface ITranslator
{
    string Translate(string word, string targetLanguage);
}

public interface IStringInterpolator
{
    string Interpolate(string template, IDictionary<string, object> context);
}

// ReSharper disable InconsistentNaming
public enum Language
{
    AA, // Afar
    AB, // Abkhazian
    AF, // Afrikaans
    AK, // Akan
    AM, // Amharic
    AR, // Arabic
    AN, // Aragonese
    AS, // Assamese
    AV, // Avaric
    AE, // Avestan
    AY, // Aymara
    AZ, // Azerbaijani
    BA, // Bashkir
    BM, // Bambara
    BE, // Belarusian
    BN, // Bengali
    BH, // Bihari
    BI, // Bislama
    BO, // Tibetan
    BS, // Bosnian
    BR, // Breton
    BG, // Bulgarian
    CA, // Catalan
    CE, // Chechen
    CH, // Chamorro
    CO, // Corsican
    CR, // Cree
    CS, // Czech
    CU, // Church Slavic
    CV, // Chuvash
    CY, // Welsh
    DA, // Danish
    DE, // German
    DV, // Dhivehi
    DZ, // Dzongkha
    EL, // Greek
    EN, // English
    EO, // Esperanto
    ES, // Spanish
    ET, // Estonian
    EU, // Basque
    FA, // Persian
    FF, // Fulah
    FI, // Finnish
    FJ, // Fijian
    FO, // Faroese
    FR, // French
    FY, // Western Frisian
    GA, // Irish
    GD, // Scottish Gaelic
    GL, // Galician
    GN, // Guarani
    GU, // Gujarati
    GV, // Manx
    HA, // Hausa
    HE, // Hebrew
    HI, // Hindi
    HO, // Hiri Motu
    HR, // Croatian
    HT, // Haitian
    HU, // Hungarian
    HY, // Armenian
    HZ, // Herero
    IA, // Interlingua
    ID, // Indonesian
    IE, // Interlingue
    IG, // Igbo
    II, // Sichuan Yi
    IK, // Inupiaq
    IO, // Ido
    IS, // Icelandic
    IT, // Italian
    IU, // Inuktitut
    JA, // Japanese
    JV, // Javanese
    KA, // Georgian
    KG, // Kongo
    KI, // Kikuyu
    KJ, // Kuanyama
    KK, // Kazakh
    KL, // Kalaallisut
    KM, // Central Khmer
    KN, // Kannada
    KO, // Korean
    KR, // Kanuri
    KS, // Kashmiri
    KU, // Kurdish
    KV, // Komi
    KW, // Cornish
    KY, // Kirghiz
    LA, // Latin
    LB, // Luxembourgish
    LG, // Ganda
    LI, // Limburgan
    LN, // Lingala
    LO, // Lao
    LT, // Lithuanian
    LU, // Luba-Katanga
    LV, // Latvian
    MG, // Malagasy
    MH, // Marshallese
    MI, // Maori
    MK, // Macedonian
    ML, // Malayalam
    MN, // Mongolian
    MR, // Marathi
    MS, // Malay
    MT, // Maltese
    MY, // Burmese
    NA, // Nauru
    NB, // Norwegian Bokmål
    ND, // North Ndebele
    NE, // Nepali
    NG, // Ndonga
    NL, // Dutch
    NN, // Norwegian Nynorsk
    NO, // Norwegian
    NR, // South Ndebele
    NV, // Navajo
    NY, // Chichewa
    OC, // Occitan
    OJ, // Ojibwa
    OM, // Oromo
    OR, // Oriya
    OS, // Ossetian
    PA, // Panjabi
    PI, // Pali
    PL, // Polish
    PS, // Pashto
    PT, // Portuguese
    QU, // Quechua
    RM, // Romansh
    RN, // Rundi
    RO, // Romanian
    RU, // Russian
    RW, // Kinyarwanda
    SA, // Sanskrit
    SC, // Sardinian
    SD, // Sindhi
    SE, // Northern Sami
    SG, // Sango
    SI, // Sinhala
    SK, // Slovak
    SL, // Slovenian
    SM, // Samoan
    SN, // Shona
    SO, // Somali
    SQ, // Albanian
    SR, // Serbian
    SS, // Swati
    ST, // Southern Sotho
    SU, // Sundanese
    SV, // Swedish
    SW, // Swahili
    TA, // Tamil
    TE, // Telugu
    TG, // Tajik
    TH, // Thai
    TI, // Tigrinya
    TK, // Turkmen
    TL, // Tagalog
    TN, // Tswana
    TO, // Tonga
    TR, // Turkish
    TS, // Tsonga
    TT, // Tatar
    TW, // Twi
    TY, // Tahitian
    UG, // Uighur
    UK, // Ukrainian
    UR, // Urdu
    UZ, // Uzbek
    VE, // Venda
    VI, // Vietnamese
    VO, // Volapük
    WA, // Walloon
    WO, // Wolof
    XH, // Xhosa
    YI, // Yiddish
    YO, // Yoruba
    ZA, // Zhuang
    ZH, // Chinese
    ZU // Zulu
}

// ReSharper disable InconsistentNaming
public enum Culture
{
    // Английский

    en_US, // English (United States)
    en_GB, // English (United Kingdom)
    en_CA, // English (Canada)
    en_AU, // English (Australia)
    en_NZ, // English (New Zealand)
    en_IE, // English (Ireland)
    en_ZA, // English (South Africa)
    en_IN, // English (India)

    // Испанский
    es_ES, // Spanish (Spain)
    es_MX, // Spanish (Mexico)
    es_AR, // Spanish (Argentina)
    es_CO, // Spanish (Colombia)
    es_CL, // Spanish (Chile)
    es_PE, // Spanish (Peru)
    es_VE, // Spanish (Venezuela)
    es_EC, // Spanish (Ecuador)
    es_GT, // Spanish (Guatemala)
    es_CR, // Spanish (Costa Rica)
    es_DO, // Spanish (Dominican Republic)
    es_PA, // Spanish (Panama)
    es_BO, // Spanish (Bolivia)
    es_SV, // Spanish (El Salvador)
    es_HN, // Spanish (Honduras)
    es_PY, // Spanish (Paraguay)
    es_NI, // Spanish (Nicaragua)
    es_UY, // Spanish (Uruguay)
    es_PR, // Spanish (Puerto Rico)
    es_US, // Spanish (United States)

    // Французский
    fr_FR, // French (France)
    fr_CA, // French (Canada)
    fr_BE, // French (Belgium)
    fr_CH, // French (Switzerland)
    fr_LU, // French (Luxembourg)
    fr_MA, // French (Morocco)
    fr_TN, // French (Tunisia)
    fr_DZ, // French (Algeria)

    // Немецкий
    de_DE, // German (Germany)
    de_AT, // German (Austria)
    de_CH, // German (Switzerland)
    de_LU, // German (Luxembourg)
    de_BE, // German (Belgium)
    de_LI, // German (Liechtenstein)

    // Португальский
    pt_BR, // Portuguese (Brazil)
    pt_PT, // Portuguese (Portugal)
    pt_AO, // Portuguese (Angola)
    pt_MZ, // Portuguese (Mozambique)

    // Русский
    ru_RU, // Russian (Russia)
    ru_UA, // Russian (Ukraine)
    ru_BY, // Russian (Belarus)
    ru_KZ, // Russian (Kazakhstan)
    ru_KG, // Russian (Kyrgyzstan)

    // Китайский
    zh_CN, // Chinese (Simplified, China)
    zh_TW, // Chinese (Traditional, Taiwan)
    zh_HK, // Chinese (Traditional, Hong Kong)
    zh_SG, // Chinese (Simplified, Singapore)
    zh_MO, // Chinese (Traditional, Macao)

    // Арабский
    ar_SA, // Arabic (Saudi Arabia)
    ar_EG, // Arabic (Egypt)
    ar_AE, // Arabic (United Arab Emirates)
    ar_DZ, // Arabic (Algeria)
    ar_MA, // Arabic (Morocco)
    ar_IQ, // Arabic (Iraq)
    ar_QA, // Arabic (Qatar)
    ar_KW, // Arabic (Kuwait)
    ar_JO, // Arabic (Jordan)
    ar_LB, // Arabic (Lebanon)
    ar_OM, // Arabic (Oman)
    ar_SY, // Arabic (Syria)
    ar_YE, // Arabic (Yemen)
    ar_BH, // Arabic (Bahrain)
    ar_TN, // Arabic (Tunisia)
    ar_LY, // Arabic (Libya)
    ar_SD, // Arabic (Sudan)

    // Японский
    ja_JP, // Japanese (Japan)

    // Корейский
    ko_KR, // Korean (Korea)
    ko_KP, // Korean (North Korea)

    // Итальянский
    it_IT, // Italian (Italy)
    it_CH, // Italian (Switzerland)
    it_SM, // Italian (San Marino)
    it_VA, // Italian (Vatican City)

    // Нидерландский
    nl_NL, // Dutch (Netherlands)
    nl_BE, // Dutch (Belgium)
    nl_SR, // Dutch (Suriname)

    // Польский
    pl_PL, // Polish (Poland)

    // Турецкий
    tr_TR, // Turkish (Turkey)
    tr_CY, // Turkish (Cyprus)

    // Украинский
    uk_UA, // Ukrainian (Ukraine)

    // Вьетнамский
    vi_VN, // Vietnamese (Vietnam)

    // Тайский
    th_TH, // Thai (Thailand)

    // Хинди
    hi_IN, // Hindi (India)

    // Бенгальский
    bn_BD, // Bengali (Bangladesh)
    bn_IN, // Bengali (India)

    // Индонезийский
    id_ID, // Indonesian (Indonesia)

    // Малайский
    ms_MY, // Malay (Malaysia)
    ms_BN, // Malay (Brunei)
    ms_SG, // Malay (Singapore)

    // Филиппинский
    fil_PH, // Filipino (Philippines)

    // Греческий
    el_GR, // Greek (Greece)
    el_CY, // Greek (Cyprus)

    // Чешский
    cs_CZ, // Czech (Czech Republic)

    // Шведский
    sv_SE, // Swedish (Sweden)
    sv_FI, // Swedish (Finland)

    // Датский
    da_DK, // Danish (Denmark)

    // Финский
    fi_FI, // Finnish (Finland)

    // Норвежский
    nb_NO, // Norwegian Bokmål (Norway)
    nn_NO, // Norwegian Nynorsk (Norway)

    // Венгерский
    hu_HU, // Hungarian (Hungary)

    // Румынский
    ro_RO, // Romanian (Romania)
    ro_MD, // Romanian (Moldova)

    // Болгарский
    bg_BG, // Bulgarian (Bulgaria)

    // Хорватский
    hr_HR, // Croatian (Croatia)
    hr_BA, // Croatian (Bosnia and Herzegovina)

    // Сербский
    sr_RS, // Serbian (Serbia)
    sr_BA, // Serbian (Bosnia and Herzegovina)
    sr_ME, // Serbian (Montenegro)

    // Словацкий
    sk_SK, // Slovak (Slovakia)

    // Словенский
    sl_SI, // Slovenian (Slovenia)

    // Литовский
    lt_LT, // Lithuanian (Lithuania)

    // Латышский
    lv_LV, // Latvian (Latvia)

    // Эстонский
    et_EE, // Estonian (Estonia)

    // Иврит
    he_IL, // Hebrew (Israel)

    // Персидский
    fa_IR, // Persian (Iran)

    // Урду
    ur_PK, // Urdu (Pakistan)
    ur_IN, // Urdu (India)

    // Непальский
    ne_NP, // Nepali (Nepal)
    ne_IN, // Nepali (India)

    // Сингальский
    si_LK, // Sinhala (Sri Lanka)

    // Кхмерский
    km_KH, // Khmer (Cambodia)

    // Бирманский
    my_MM, // Burmese (Myanmar)

    // Монгольский
    mn_MN, // Mongolian (Mongolia)

    // Амхарский
    am_ET, // Amharic (Ethiopia)

    // Суахили
    sw_KE, // Swahili (Kenya)
    sw_TZ, // Swahili (Tanzania)
    sw_UG, // Swahili (Uganda)

    // Йоруба
    yo_NG, // Yoruba (Nigeria)
    yo_BJ, // Yoruba (Benin)

    // Игбо
    ig_NG, // Igbo (Nigeria)

    // Хауса
    ha_NG, // Hausa (Nigeria)
    ha_GH, // Hausa (Ghana)
    ha_NE, // Hausa (Niger)

    // Зулу
    zu_ZA, // Zulu (South Africa)

    // Африкаанс
    af_ZA, // Afrikaans (South Africa)
    af_NA, // Afrikaans (Namibia)

    // Каталонский
    ca_ES, // Catalan (Spain)
    ca_AD, // Catalan (Andorra)
    ca_FR, // Catalan (France)
    ca_IT, // Catalan (Italy)

    // Баскский
    eu_ES, // Basque (Spain)
    eu_FR, // Basque (France)

    // Галисийский
    gl_ES, // Galician (Spain)

    // Валлийский
    cy_GB, // Welsh (United Kingdom)

    // Ирландский
    ga_IE, // Irish (Ireland)
    ga_GB, // Irish (United Kingdom)

    // Шотландский гэльский
    gd_GB, // Scottish Gaelic (United Kingdom)

    // Мальтийский
    mt_MT, // Maltese (Malta)

    // Исландский
    is_IS, // Icelandic (Iceland)

    // Фарерский
    fo_FO, // Faroese (Faroe Islands)

    // Нейтральные культуры
    en, // English (Neutral)
    es, // Spanish (Neutral)
    fr, // French (Neutral)
    de, // German (Neutral)
    pt, // Portuguese (Neutral)
    ru, // Russian (Neutral)
    zh, // Chinese (Neutral)
    ar, // Arabic (Neutral)
    ja, // Japanese (Neutral)
    ko, // Korean (Neutral)
    it, // Italian (Neutral)
    nl, // Dutch (Neutral)
    pl, // Polish (Neutral)
    tr, // Turkish (Neutral)
    uk, // Ukrainian (Neutral)
    vi, // Vietnamese (Neutral)
    th, // Thai (Neutral)
    hi, // Hindi (Neutral)
    bn, // Bengali (Neutral)
    id, // Indonesian (Neutral)
    ms, // Malay (Neutral)
    el, // Greek (Neutral)
    cs, // Czech (Neutral)
    sv, // Swedish (Neutral)
    da, // Danish (Neutral)
    fi, // Finnish (Neutral)
    no, // Norwegian (Neutral)
    hu, // Hungarian (Neutral)
    ro, // Romanian (Neutral)
    bg, // Bulgarian (Neutral)
    hr, // Croatian (Neutral)
    sr, // Serbian (Neutral)
    sk, // Slovak (Neutral)
    sl, // Slovenian (Neutral)
    lt, // Lithuanian (Neutral)
    lv, // Latvian (Neutral)
    et, // Estonian (Neutral)
    he, // Hebrew (Neutral)
    fa, // Persian (Neutral)
    ur, // Urdu (Neutral)
    ne, // Nepali (Neutral)
    si, // Sinhala (Neutral)
    km, // Khmer (Neutral)
    my, // Burmese (Neutral)
    mn, // Mongolian (Neutral)
    am, // Amharic (Neutral)
    sw, // Swahili (Neutral)
    yo, // Yoruba (Neutral)
    ig, // Igbo (Neutral)
    ha, // Hausa (Neutral)
    zu, // Zulu (Neutral)
    af, // Afrikaans (Neutral)
    ca, // Catalan (Neutral)
    eu, // Basque (Neutral)
    gl, // Galician (Neutral)
    cy, // Welsh (Neutral)
    ga, // Irish (Neutral)
    gd, // Scottish Gaelic (Neutral)
    mt, // Maltese (Neutral)

    //is,    // Icelandic (Neutral)
    fo // Faroese (Neutral)
}

public static class CultureHelper
{
    public static string GetDisplayName(this Culture culture)
    {
        var cultureInfo = new System.Globalization.CultureInfo(culture.ToString().Replace('_', '-'));
        return cultureInfo.DisplayName;
    }

    public static string GetNativeName(this Culture culture)
    {
        var cultureInfo = new System.Globalization.CultureInfo(culture.ToString().Replace('_', '-'));
        return cultureInfo.NativeName;
    }

    public static System.Globalization.CultureInfo ToCultureInfo(this Culture culture)
    {
        return new System.Globalization.CultureInfo(culture.ToString().Replace('_', '-'));
    }

    public static Language GetLanguage(this Culture culture)
    {
        string languageCode = culture.ToString().Split('_')[0];
        return (Language)Enum.Parse(typeof(Language), languageCode);
    }
}

public abstract class UpperFirstWordFormatter : IWordFormatter
{
    public string FormatWord(string word)
        => Format(word);

    public static string Format(string word)
    {
        if (string.IsNullOrEmpty(word))
            return string.Empty;

        return word.Length > 1
            ? char.ToUpper(word[0]) + word.Substring(1).ToLower()
            : word.ToUpper();
    }
}

public abstract class LowerFirstWordFormatter : IWordFormatter
{
    public string FormatWord(string word)
        => Format(word);

    public static string Format(string word)
    {
        if (string.IsNullOrEmpty(word))
            return string.Empty;

        return word.Length > 1
            ? char.ToLower(word[0]) + word.Substring(1).ToLower()
            : word.ToLower();
    }
}

public abstract class UpperWordFormatter : IWordFormatter
{
    public string FormatWord(string word)
        => Format(word);

    public static string Format(string word)
    {
        return word?.ToUpper() ?? string.Empty;
    }
}

public abstract class LowerWordFormatter : IWordFormatter
{
    public string FormatWord(string word)
        => Format(word);

    public static string Format(string word)
    {
        return word?.ToLower() ?? string.Empty;
    }
}

public abstract class LowerFirstWordValidator
{
    public bool IsValid(string word)
        => IsValidStatic(word);

    public static bool IsValidStatic(string word)
    {
        if (string.IsNullOrEmpty(word))
            return false;

        return Regex.IsMatch(word, @"^[a-z][a-z0-9]*$");
    }
}

public abstract class UpperFirstWordValidator
{
    public bool IsValid(string word)
        => IsValidStatic(word);

    public static bool IsValidStatic(string word)
    {
        if (string.IsNullOrEmpty(word))
            return false;

        return Regex.IsMatch(word, @"^[A-Z][a-z0-9]*$");
    }
}

public abstract class UpperCaseWordValidator
{
    public bool IsValid(string word)
        => IsValidStatic(word);

    public static bool IsValidStatic(string word)
    {
        if (string.IsNullOrEmpty(word))
            return false;

        return word == word.ToUpper();
    }
}

public abstract class LowerCaseWordValidator
{
    public bool IsValid(string word)
        => IsValidStatic(word);

    public static bool IsValidStatic(string word)
    {
        if (string.IsNullOrEmpty(word))
            return false;

        return word == word.ToLower();
    }
}

public class NumericWordValidator
{
    public bool IsValid(string word)
        => IsValidStatic(word);

    public static bool IsValidStatic(string word)
    {
        if (string.IsNullOrEmpty(word))
            return false;

        return Regex.IsMatch(word, @"^\d+$");
    }
}

public class AlphabeticWordValidator
{
    public bool IsValid(string word)
        => IsValidStatic(word);

    public static bool IsValidStatic(string word)
    {
        if (string.IsNullOrEmpty(word))
            return false;

        return Regex.IsMatch(word, @"^[a-zA-Z]+$");
    }
}

public class PascalCaseWordsFormatter : IWordsFormatter
{
    public IEnumerable<string> FormatWords(IEnumerable<string> words)
        => Format(words);

    public static IEnumerable<string> Format(IEnumerable<string> words)
    {
        return words.Select(UpperFirstWordFormatter.Format);
    }

    public bool IsValid(IEnumerable<string> words)
        => IsValidStatic(words);

    public static bool IsValidStatic(IEnumerable<string> words)
    {
        return words.All(UpperFirstWordValidator.IsValidStatic);
    }
}

public class CamelCaseWordsFormatter : IWordsFormatter
{
    public IEnumerable<string> FormatWords(IEnumerable<string> words)
        => Format(words);

    public static IEnumerable<string> Format(IEnumerable<string> words)
    {
        var wordArray = words.ToArray();
        if (!wordArray.Any())
            return Enumerable.Empty<string>();

        var result = new List<string>
        {
            LowerFirstWordFormatter.Format(wordArray.First())
        };

        foreach (var word in wordArray.Skip(1))
            result.Add(UpperFirstWordFormatter.Format(word));

        return result;
    }

    public bool IsValid(IEnumerable<string> words)
        => IsValidStatic(words);

    public static bool IsValidStatic(IEnumerable<string> words)
    {
        var wordArray = words.ToArray();
        if (!wordArray.Any())
            return false;

        if (!LowerFirstWordValidator.IsValidStatic(wordArray.First()))
            return false;

        return wordArray.Skip(1).All(UpperFirstWordValidator.IsValidStatic);
    }
}

public class UpperSnakeCaseWordsFormatter : IWordsFormatter
{
    public IEnumerable<string> FormatWords(IEnumerable<string> words)
        => Format(words);

    public static IEnumerable<string> Format(IEnumerable<string> words)
    {
        return words.Select(UpperWordFormatter.Format);
    }

    public bool IsValid(IEnumerable<string> words)
        => IsValidStatic(words);

    public static bool IsValidStatic(IEnumerable<string> words)
    {
        return words.All(UpperCaseWordValidator.IsValidStatic);
    }
}

public class LowerSnakeCaseWordsFormatter : IWordsFormatter
{
    public IEnumerable<string> FormatWords(IEnumerable<string> words)
        => Format(words);

    public static IEnumerable<string> Format(IEnumerable<string> words)
    {
        return words.Select(LowerWordFormatter.Format);
    }

    public bool IsValid(IEnumerable<string> words)
        => IsValidStatic(words);

    public static bool IsValidStatic(IEnumerable<string> words)
    {
        return words.All(LowerCaseWordValidator.IsValidStatic);
    }
}

public class UpperKebabCaseWordsFormatter : IWordsFormatter
{
    public IEnumerable<string> FormatWords(IEnumerable<string> words)
        => Format(words);

    public static IEnumerable<string> Format(IEnumerable<string> words)
    {
        return words.Select(UpperWordFormatter.Format);
    }

    public bool IsValid(IEnumerable<string> words)
        => IsValidStatic(words);

    public static bool IsValidStatic(IEnumerable<string> words)
    {
        return words.All(UpperCaseWordValidator.IsValidStatic);
    }
}

public class LowerKebabCaseWordsFormatter : IWordsFormatter
{
    public IEnumerable<string> FormatWords(IEnumerable<string> words)
        => Format(words);

    public static IEnumerable<string> Format(IEnumerable<string> words)
    {
        return words.Select(LowerWordFormatter.Format);
    }

    public bool IsValid(IEnumerable<string> words)
        => IsValidStatic(words);

    public static bool IsValidStatic(IEnumerable<string> words)
    {
        return words.All(LowerCaseWordValidator.IsValidStatic);
    }
}

public class TitleCaseWordsFormatter : IWordsFormatter
{
    public IEnumerable<string> FormatWords(IEnumerable<string> words)
        => Format(words);

    public static IEnumerable<string> Format(IEnumerable<string> words)
    {
        return words.Select(UpperFirstWordFormatter.Format);
    }

    public bool IsValid(IEnumerable<string> words)
        => IsValidStatic(words);

    public static bool IsValidStatic(IEnumerable<string> words)
    {
        return words.All(UpperFirstWordValidator.IsValidStatic);
    }
}

public class LowerCaseWordsFormatter : IWordsFormatter
{
    public IEnumerable<string> FormatWords(IEnumerable<string> words)
        => Format(words);

    public static IEnumerable<string> Format(IEnumerable<string> words)
    {
        return words.Select(LowerWordFormatter.Format);
    }

    public bool IsValid(IEnumerable<string> words)
        => IsValidStatic(words);

    public static bool IsValidStatic(IEnumerable<string> words)
    {
        return words.All(LowerCaseWordValidator.IsValidStatic);
    }
}

public class UpperCaseWordsFormatter : IWordsFormatter
{
    public IEnumerable<string> FormatWords(IEnumerable<string> words)
        => Format(words);

    public static IEnumerable<string> Format(IEnumerable<string> words)
    {
        return words.Select(UpperWordFormatter.Format);
    }

    public bool IsValid(IEnumerable<string> words)
        => IsValidStatic(words);

    public static bool IsValidStatic(IEnumerable<string> words)
    {
        return words.All(UpperCaseWordValidator.IsValidStatic);
    }
}

public interface IStringFormatter
{
    string FormatString(string data);
}

public interface IWordsJoiner
{
    string Join(IEnumerable<string> words);
}

public interface IStringSplitter
{
    string[] Split(string data);
}

public interface IStringTransformer : IStringSplitter, IWordsJoiner, IStringFormatter
{
}

public class SeparatorWordsJoiner(string separator = Literals.SPACE_STRING) : IWordsJoiner
{
    public string Join(IEnumerable<string> words)
        => JoinStatic(words, separator);

    public static string JoinStatic(IEnumerable<string> words, string separator = Literals.SPACE_STRING)
    {
        return string.Join(separator, words);
    }
}

public class CharSplitter(StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries, params char[]? separators)
    : IStringSplitter
{
    private readonly char[] _separators = separators ?? Literals.DELIMITERS_CHARS;

    public string[] Split(string data)
        => SplitStatic(data, options, _separators);

    public static string[] SplitStatic(string data,
        StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries, params char[]? separators)
    {
        if (string.IsNullOrEmpty(data))
            return Array.Empty<string>();

        separators ??= Literals.DELIMITERS_CHARS;
        return data.Split(separators, options);
    }
}

public class RegexSplitter(Regex regex) : IStringSplitter
{
    public RegexSplitter(string pattern = @"\W+") : this(new Regex(pattern, RegexOptions.Compiled))
    {
    }

    public string[] Split(string data)
        => SplitStatic(data, regex);

    public static string[] SplitStatic(string data, Regex regex)
    {
        if (string.IsNullOrEmpty(data))
            return Array.Empty<string>();

        return regex.Split(data).Where(s => !string.IsNullOrEmpty(s)).ToArray();
    }

    public static IEnumerable<string> SplitStatic(string data, string pattern = @"\W+")
    {
        if (string.IsNullOrEmpty(data))
            return Enumerable.Empty<string>();

        var regex = new Regex(pattern, RegexOptions.Compiled);
        return regex.Split(data).Where(s => !string.IsNullOrEmpty(s));
    }
}

public class DelegateSplitter(Func<string, string[]> splitFunction) : IStringSplitter
{
    public string[] Split(string data)
        => SplitStatic(data, splitFunction);

    public static string[] SplitStatic(string data, Func<string, string[]> splitFunction)
    {
        if (string.IsNullOrEmpty(data))
            return Array.Empty<string>();

        return splitFunction(data);
    }
}

public class DelegateJoiner(Func<IEnumerable<string>, string> joinFunction) : IWordsJoiner
{
    public string Join(IEnumerable<string> words)
        => JoinStatic(words, joinFunction);

    public static string JoinStatic(IEnumerable<string> words, Func<IEnumerable<string>, string> joinFunction)
    {
        return joinFunction(words);
    }
}

public class CompositeStringTransformer(IStringSplitter splitter, IWordsJoiner joiner) : IStringFormatter
{
    public string FormatString(string data)
    {
        var words = splitter.Split(data);
        var str = joiner.Join(words);
        return str;
    }

    public static string FormatStatic(string data, IStringSplitter splitter, IWordsJoiner joiner)
    {
        var words = splitter.Split(data);
        return joiner.Join(words);
    }

    public static string FormatStatic(string data, Func<string, IEnumerable<string>> splitFunc,
        Func<IEnumerable<string>, string> joinFunc)
    {
        var words = splitFunc(data);
        return joinFunc(words);
    }
}

public class EmailValidator
{
    public bool IsValid(string data)
        => IsValidStatic(data);

    public static bool IsValidStatic(string data)
    {
        if (string.IsNullOrEmpty(data))
            return false;

        return Regex.IsMatch(data, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}

public class HttpUrlValidator
{
    public bool IsValid(string data)
        => IsValidStatic(data);

    public static bool IsValidStatic(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
            return false;

        return Uri.TryCreate(data, UriKind.Absolute, out Uri? uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}

public class FtpUrlValidator
{
    public bool IsValid(string data)
        => IsValidStatic(data);

    public static bool IsValidStatic(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
            return false;

        return Uri.TryCreate(data, UriKind.Absolute, out Uri? uriResult) &&
               uriResult.Scheme == Uri.UriSchemeFtp;
    }
}

public class AbsoluteUrlValidator
{
    public bool IsValid(string data)
        => IsValidStatic(data);

    public static bool IsValidStatic(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
            return false;

        return Uri.TryCreate(data, UriKind.Absolute, out _);
    }
}

public class RelativeUrlValidator
{
    public bool IsValid(string data)
        => IsValidStatic(data);

    public static bool IsValidStatic(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
            return false;

        return Uri.TryCreate(data, UriKind.Relative, out _);
    }
}

public abstract class WordFormatterStorage : InMemoryIndexedSyncStorage<string, IWordFormatter>
{
}

public abstract class StringFormatterStorage : InMemoryIndexedSyncStorage<string, IStringFormatter>
{
}

public enum WordFormatterType
{
    UpperFirst,
    LowerFirst,
    Upper,
    Lower
}

public enum StringFormatterType
{
    Camel,
    Pascal,
    LowerSnake,
    UpperSnake,
    LowerKebab,
    UpperKebab,
    Lower,
    Upper,
    Title,
}

public static class WordFormatterTypeExtensions
{
    public static string AsString(this WordFormatterType type)
    {
        return type switch
        {
            WordFormatterType.UpperFirst => "upper_first",
            WordFormatterType.LowerFirst => "lower_first",
            WordFormatterType.Upper => "upper",
            WordFormatterType.Lower => "lower",
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }
}

public static class StringFormatterTypeExtensions
{
    public static string AsString(this StringFormatterType type)
    {
        return type switch
        {
            StringFormatterType.Camel => "camel",
            StringFormatterType.Pascal => "pascal",
            StringFormatterType.LowerSnake => "snake",
            StringFormatterType.UpperSnake => "upper_snake",
            StringFormatterType.LowerKebab => "kebab",
            StringFormatterType.UpperKebab => "upper_kebab",
            StringFormatterType.Lower => "lower",
            StringFormatterType.Upper => "upper",
            StringFormatterType.Title => "title",
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }
}

public static class NamingRegistryExtensions
{
    public static IStringFormatter Get(this StringFormatterStorage storage, StringFormatterType type)
    {
        return storage.Get(type.AsString());
    }

    public static IWordFormatter Get(this WordFormatterStorage storage, WordFormatterType type)
    {
        return storage.Get(type.AsString());
    }
}