using Caliburn.Micro;
using System.ComponentModel.Composition;
using System.Windows;
using WPFCustomControl.LoggerUtils;

namespace WPFCustomControl.ViewModels;

[Export]
public class ShellViewModel : Conductor<IScreen>
{
    /// <summary>
    /// 所有可选的语言
    /// </summary>
    public List<string> LanguageList { get; set; } = ["中文", "English"];

    /// <summary>
    /// 当前选择的语言
    /// </summary>
    public string Language { get; set; } = "中文";

    public void OnLanguageChanged()
    {
        Logger.Debug($"Language:{Language}");
        ((App)Application.Current).ChangeCulture(Language);
    }

    /// <summary>
    /// 绑定到NumericUpAndDown的内容
    /// </summary>
    public Int32 Number { get; set; } = 10;

    public void OnNumberChanged()
    {
        Logger.Debug($"Number:{Number}");
    }

}