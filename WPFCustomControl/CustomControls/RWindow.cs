using System.Windows;

namespace WPFCustomControl.CustomControls;

public class RWindow : Window
{
    static RWindow()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RWindow), new FrameworkPropertyMetadata(typeof(RWindow)));
    }
}
