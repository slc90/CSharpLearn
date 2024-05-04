using Caliburn.Micro;
using CaliburnMicroLearn.LoggerUtils;

namespace CaliburnMicroLearn.ViewModels;

public class ShellViewModel : Screen
{
    /// <summary>
    /// 勾选或取消勾选CheckBox后属性自动变化
    /// </summary>
    public bool TestCheckBox { get; set; }

    public void OnTestCheckBoxChanged()
    {
        Logger.Debug($"IsChecked:{TestCheckBox}");
    }

    /// <summary>
    /// 绑定到ListView的属性
    /// </summary>
    public BindableCollection<string> Names { get; set; } = ["Name1", "Name2", "Name3"];

    /// <summary>
    /// 点击ListView中的Item后触发，输出被点击的Item的内容
    /// </summary>
    /// <param name="name"></param>
    public void ListViewItemClick(string name)
    {
        Logger.Debug(name);
    }

    /// <summary>
    /// 点击按钮后触发
    /// </summary>
    public void TestButton()
    {
        Logger.Debug("Test");
    }
}
