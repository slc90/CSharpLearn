using Microsoft.Windows.Shell;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using WPFCustomControl.LoggerUtils;

namespace WPFCustomControl.Themes;

public class CustomWindow : Window
{
    static CustomWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(typeof(CustomWindow)));
    }

    [DllImport("user32", EntryPoint = "GetMonitorInfoW", ExactSpelling = true, CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetMonitorInfo([In] IntPtr hMonitor, [Out] MONITORINFO lpmi);

    [DllImport("user32")]
    internal static extern IntPtr MonitorFromWindow([In] IntPtr handle, [In] int flags);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    /// <summary>
    /// 这个窗口的handle
    /// </summary>
    private IntPtr _handle;

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
        //用WindowChrome的方式来制作自定义窗口
        System.Windows.Shell.WindowChrome.SetWindowChrome(this, windowChrome);
        AllowsTransparency = false;
        WindowStyle = WindowStyle.None;
    }

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
            IntPtr monitor = MonitorFromWindow(_handle, 0x00000002);
            var monitorInfo = new MONITORINFO();
            GetMonitorInfo(monitor, monitorInfo);
            var x = monitorInfo.rcMonitor.left;
            var y = monitorInfo.rcMonitor.top;
            var cx = Math.Abs(monitorInfo.rcMonitor.right - x);
            var cy = Math.Abs(monitorInfo.rcMonitor.bottom - y);
            Logger.Debug($"x:{x},y:{y},cx:{cx},cy:{cy}");
            SetWindowPos(_handle, new IntPtr(-2), x, y, cx, cy, 0x0040);
        }
        BorderThickness = new Thickness(0);
    }
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
internal class MONITORINFO
{
    public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
    public RECT rcMonitor = new RECT();
    public RECT rcWork = new RECT();
    public int dwFlags = 0;

    public enum MonitorOptions : uint
    {
        MONITOR_DEFAULTTONULL = 0x00000000,
        MONITOR_DEFAULTTOPRIMARY = 0x00000001,
        MONITOR_DEFAULTTONEAREST = 0x00000002
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 0)]
internal struct RECT
{
    public int left;
    public int top;
    public int right;
    public int bottom;

    public static readonly RECT Empty = new RECT();

    public int Width
    {
        get { return Math.Abs(right - left); }  // Abs needed for BIDI OS
    }

    public int Height
    {
        get { return bottom - top; }
    }

    public RECT(int left, int top, int right, int bottom)
    {
        this.left = left;
        this.top = top;
        this.right = right;
        this.bottom = bottom;
    }

    public RECT(RECT rcSrc)
    {
        left = rcSrc.left;
        top = rcSrc.top;
        right = rcSrc.right;
        bottom = rcSrc.bottom;
    }

    public bool IsEmpty
    {
        get
        {
            // BUGBUG : On Bidi OS (hebrew arabic) left > right
            return left >= right || top >= bottom;
        }
    }

    public override string ToString()
    {
        if (this == Empty)
            return "RECT {Empty}";
        return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
    }

    /// <summary> Determine if 2 RECT are equal (deep compare) </summary>
    public override bool Equals(object obj)
    {
        if (!(obj is Rect)) { return false; }
        return (this == (RECT)obj);
    }

    /// <summary>Return the HashCode for this struct (not garanteed to be unique)</summary>
    public override int GetHashCode()
    {
        return left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
    }

    public static bool operator ==(RECT rect1, RECT rect2)
    {
        return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom);
    }

    public static bool operator !=(RECT rect1, RECT rect2)
    {
        return !(rect1 == rect2);
    }
}

[StructLayout(LayoutKind.Sequential)]
internal struct MARGINS
{
    public int leftWidth;
    public int rightWidth;
    public int topHeight;
    public int bottomHeight;
}