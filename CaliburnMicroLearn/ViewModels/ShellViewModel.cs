using Caliburn.Micro;
using CaliburnMicroLearn.UserControl.ViewModels;
using System.ComponentModel.Composition;

namespace CaliburnMicroLearn.ViewModels;

/// <summary>
/// MEF导出
/// </summary>
[Export]
public class ShellViewModel : Conductor<IScreen>
{
    /// <summary>
    /// CaliburnMicro的WindowManager，可以用来显示其他Window
    /// </summary>
    private readonly IWindowManager _windowManager = IoC.Get<IWindowManager>();

    public UserControl1ViewModel UserControl1ViewModel { get; set; } = new();

    public UserControl2ViewModel UserControl2ViewModel { get; set; } = new();

    /// <summary>
    /// 点击按钮SwitchToActiveItem1触发
    /// </summary>
    public void SwitchToActiveItem1()
    {
        ActivateItemAsync(UserControl1ViewModel);
    }

    /// <summary>
    /// 点击按钮SwitchToActiveItem2触发
    /// </summary>
    public void SwitchToActiveItem2()
    {
        ActivateItemAsync(UserControl2ViewModel);
    }

    /// <summary>
    /// 弹出一个模态窗口
    /// </summary>
    public void NewDialog()
    {
        var x = 1;
        var y = 0;
        var z = x / y;
        _windowManager.ShowDialogAsync(new DialogViewModel());
    }
}
