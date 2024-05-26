using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace WPFCustomControl.Behaviours;

/// <summary>
/// 给Button添加一个行为:当点击Button时弹出一个MessageBox
/// </summary>
public class ButtonBehaviour : Behavior<Button>
{
    /// <summary>
    /// 当Behavior被添加到xaml的控件时触发
    /// </summary>
    protected override void OnAttached()
    {
        base.OnAttached();
        //AssociatedObject就是添加这个Behavior的控件，本身是个泛型，这里就是Button
        AssociatedObject.Click += OnButtonClick;
    }

    /// <summary>
    /// 当行为被移除或者控件被卸载时，OnDetaching 方法会被调用
    /// </summary>
    protected override void OnDetaching()
    {
        base.OnDetaching();
        //释放注册的事件
        AssociatedObject.Click -= OnButtonClick;
    }

    /// <summary>
    /// Button点击时触发
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(((App)Application.Current).FindCurrentDynamicLanguage("ButtonBehaviourTest"));
    }
}

