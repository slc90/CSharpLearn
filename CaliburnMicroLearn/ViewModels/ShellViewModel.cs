using Caliburn.Micro;
using CaliburnMicroLearn.UserControl.ViewModels;

namespace CaliburnMicroLearn.ViewModels;

public class ShellViewModel : Conductor<IScreen>
{
    UserControl1ViewModel UserControl1ViewModel = new();

    UserControl2ViewModel UserControl2ViewModel = new();

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
}
