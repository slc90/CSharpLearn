using SkiaSharp.Views.Desktop;
using SkiaSharp;
using System.Windows;

namespace SkiaLearn;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
    {
        // 获取绘图表面和画布
        var surface = e.Surface;
        var canvas = surface.Canvas;
        // 清除画布
        canvas.Clear(SKColors.White);
        // 创建绘图对象
        using var paint = new SKPaint();
        paint.Color = SKColors.Red;
        paint.IsAntialias = true;
        // 绘制一个圆
        canvas.DrawCircle(e.Info.Width / 2, e.Info.Height / 2, 100, paint);
    }
}
