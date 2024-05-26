using Microsoft.Windows.Shell;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;

namespace WPFCustomControl.Themes;

public class CustomWindow : Window
{
    static CustomWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(typeof(CustomWindow)));
    }

    public CustomWindow()
    {
        var windowChrome = new System.Windows.Shell.WindowChrome
        {
            //设置 CaptionHeight为0，以便我们可以完全自定义标题栏。
            CaptionHeight = 0,
            //设置 ResizeBorderThickness 使窗口可以调整大小。
            ResizeBorderThickness = SystemParameters2.Current.WindowResizeBorderThickness,
            CornerRadius = new CornerRadius(0),
            GlassFrameThickness = new Thickness(0),
        };
        //用WindowChrome的方式来制作自定义窗口，这样可以保留Resize的功能
        System.Windows.Shell.WindowChrome.SetWindowChrome(this, windowChrome);
        //设置为无边框窗口,不然最大化时显示不正确
        WindowStyle = WindowStyle.None;
    }

    [DllImport("user32")]
    internal static extern IntPtr MonitorFromWindow([In] IntPtr handle, [In] Int32 flags);

    [DllImport("user32", EntryPoint = "GetMonitorInfoW", ExactSpelling = true, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetMonitorInfo([In] IntPtr hMonitor, [Out] MONITORINFO lpmi);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
        Int32 X, Int32 Y, Int32 cx, Int32 cy, UInt32 uFlags);

    /// <summary>
    /// 这个窗口的handle
    /// </summary>
    private IntPtr _handle;

    /// <summary>
    /// xaml中Template被应用时触发
    /// </summary>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _minButton = GetTemplateChild("PART_MinButton") as Button;
        _minButton!.Click += MinButton_Click;
        _maxButton = GetTemplateChild("PART_MaxButton") as Button;
        _maxButton!.Click += MaxButton_Click;
        _closeButton = GetTemplateChild("PART_CloseButton") as Button;
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
    private Button? _minButton;

    /// <summary>
    /// 用于找xaml中的最大化按钮
    /// </summary>
    private Button? _maxButton;

    /// <summary>
    /// 用于找xaml中的关闭按钮
    /// </summary>
    private Button? _closeButton;

    /// <summary>
    /// 点击关闭按钮，关闭窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MinButton_Click(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    /// <summary>
    /// 点击关闭按钮，关闭窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MaxButton_Click(object sender, RoutedEventArgs e)
    {
        if (WindowState == WindowState.Maximized)
        {
            WindowState = WindowState.Normal;
        }
        else
        {
            WindowState = WindowState.Maximized;
        }
    }

    /// <summary>
    /// 点击关闭按钮，关闭窗口
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CloseButton_Click(object sender, RoutedEventArgs e)
    {
        //窗口关闭前释放注册的事件
        _minButton!.Click -= MinButton_Click;
        _maxButton!.Click -= MaxButton_Click;
        _closeButton!.Click -= CloseButton_Click;
        Close();
    }

    /// <summary>
    /// 拖动窗口
    /// </summary>
    /// <param name="e"></param>
    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
        base.OnMouseLeftButtonDown(e);
        if (e.ChangedButton == MouseButton.Left)
        {
            DragMove();
        }
    }

    /// <summary>
    /// 窗口初始化好时获得窗口的handler，之后可以使用win32的函数操作
    /// </summary>
    /// <param name="e"></param>
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        _handle = new WindowInteropHelper(this).Handle;
    }

    /// <summary>
    /// 最大化时需要处理
    /// </summary>
    /// <param name="e"></param>
    protected override void OnStateChanged(EventArgs e)
    {
        base.OnStateChanged(e);
        if (WindowState == WindowState.Maximized)
        {
            //根据显示器找到左上角的坐标以及窗口最大化时的宽和高，再用win32的函数设置位置
            IntPtr monitor = MonitorFromWindow(_handle, 0x00000002);
            var monitorInfo = new MONITORINFO();
            GetMonitorInfo(monitor, monitorInfo);
            var x = monitorInfo.rcMonitor.left;
            var y = monitorInfo.rcMonitor.top;
            var cx = Math.Abs(monitorInfo.rcMonitor.right - x);
            var cy = Math.Abs(monitorInfo.rcMonitor.bottom - y);
            SetWindowPos(_handle, new IntPtr(-2), x, y, cx, cy, 0x0040);
        }
    }
}

/// <summary>
/// 传入win32函数需要的结构体
/// </summary>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
internal class MONITORINFO
{
    public Int32 cbSize = Marshal.SizeOf(typeof(MONITORINFO));
    public RECT rcMonitor = new();
    public RECT rcWork = new();
    public Int32 dwFlags = 0;
}

/// <summary>
/// 传入win32函数需要的结构体
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 0)]
internal struct RECT(Int32 left, Int32 top, Int32 right, Int32 bottom)
{
    public Int32 left = left;
    public Int32 top = top;
    public Int32 right = right;
    public Int32 bottom = bottom;
}