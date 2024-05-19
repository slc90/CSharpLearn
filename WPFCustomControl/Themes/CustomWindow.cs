using System.Windows;
using System.Windows.Controls;

namespace WPFCustomControl.Themes;

public class CustomWindow : Window
{
    static CustomWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(typeof(CustomWindow)));
    }

    /// <summary>
    /// 
    /// </summary>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _minButton = (GetTemplateChild("PART_MinButton") as Button)!;
        _maxButton = (GetTemplateChild("PART_MaxButton") as Button)!;
        _closeButton = (GetTemplateChild("PART_CloseButton") as Button)!;
        _closeButton!.Click += CloseButton_Click;
    }

    /// <summary>
    /// 用于标题栏放的Control
    /// </summary>
    public object TitleBarContent
    {
        get { return GetValue(TitleBarContentProperty); }
        set { SetValue(TitleBarContentProperty, value); }
    }

    public static readonly DependencyProperty TitleBarContentProperty =
        DependencyProperty.Register("TitleBarContentProperty", typeof(object), typeof(CustomWindow));

    /// <summary>
    /// 用于找xaml中的最小化按钮
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Button _minButton;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>
    /// 用于找xaml中的最大化按钮
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Button _maxButton;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>
    /// 用于找xaml中的关闭按钮
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Button _closeButton;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>
    /// 点击关闭按钮，关闭窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        //窗口关闭前释放注册的事件
        _closeButton.Click -= CloseButton_Click;
        Close();
    }
}
