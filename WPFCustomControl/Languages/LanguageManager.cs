using System.Windows;

namespace WPFCustomControl;

public partial class App : Application
{
    public void LoadLanguageResources(string culture)
    {
        // 清空当前资源字典
        Resources.MergedDictionaries.Clear();
        // 加载指定语言的资源字典
        var dict = new ResourceDictionary
        {
            Source = culture switch
            {
                "中文" => new Uri("Languages/zh-CN.xaml", UriKind.Relative),
                "English" => new Uri("Languages/en-US.xaml", UriKind.Relative),
                _ => new Uri("Languages/zh-CN.xaml", UriKind.Relative),
            }
        };
        Resources.MergedDictionaries.Add(dict);
    }

    public void ChangeCulture(string cultureCode)
    {
        LoadLanguageResources(cultureCode);
    }

}

