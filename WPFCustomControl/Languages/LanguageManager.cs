using System.Windows;

namespace WPFCustomControl;

public partial class App : Application
{
    /// <summary>
    /// 加载对应语言的资源字典
    /// </summary>
    /// <param name="language"></param>
    private void LoadLanguageResources(string language)
    {
        // 清空当前资源字典
        Resources.MergedDictionaries.Clear();
        // 加载指定语言的资源字典
        var dict = new ResourceDictionary
        {
            Source = language switch
            {
                "中文" => new Uri("Languages/zh-CN.xaml", UriKind.Relative),
                "English" => new Uri("Languages/en-US.xaml", UriKind.Relative),
                _ => new Uri("Languages/zh-CN.xaml", UriKind.Relative),
            }
        };
        Resources.MergedDictionaries.Add(dict);
    }

    /// <summary>
    /// 切换到
    /// </summary>
    /// <param name="language"></param>
    public void SwitchLanguage(string language)
    {
        LoadLanguageResources(language);
    }

    /// <summary>
    /// 根据给定的Key找到当前语言下对应的内容
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string? FindCurrentDynamicLanguage(string key)
    {
        return Resources[key] as string;
    }
}

