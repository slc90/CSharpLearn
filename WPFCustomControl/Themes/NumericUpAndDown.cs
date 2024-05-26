using System.Windows;
using System.Windows.Controls;

namespace WPFCustomControl.Themes;

[TemplatePart(Name = "PART_Down", Type = typeof(Button))]
[TemplatePart(Name = "PART_Up", Type = typeof(Button))]
[TemplatePart(Name = "PART_TextContent", Type = typeof(TextBox))]
[TemplatePart(Name = "PART_Prompt", Type = typeof(TextBlock))]
public class NumericUpAndDown : Control
{
    static NumericUpAndDown()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpAndDown), new FrameworkPropertyMetadata(typeof(NumericUpAndDown)));
    }

    /// <summary>
    /// TextBox的内容
    /// </summary>
    public Int32 NumericalContent
    {
        get { return (Int32)GetValue(NumericalContentProperty); }
        set { SetValue(NumericalContentProperty, value); }
    }

    /// <summary>
    /// 显示内容的TextBox
    /// </summary>
    private TextBox _contentTextBox;

    /// <summary>
    /// 注册一个Callback,TextBox内容变化时触发
    /// </summary>
    public static readonly DependencyProperty NumericalContentProperty =
        DependencyProperty.Register("NumericalContent", typeof(Int32), typeof(NumericUpAndDown),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIntegerValueChanged));

    private static void OnIntegerValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is NumericUpAndDown numericUpAndDown)
        {
            //设置值为非0时，会直接触发这里，此时还没找到_textBox
            if (numericUpAndDown._contentTextBox is null)
            {
                return;
            }
            //更新TextBox的文本
            numericUpAndDown._contentTextBox.Text = e.NewValue.ToString();
        }
    }

    /// <summary>
    /// TextBox内容出错时的提示语
    /// </summary>
    public string Prompt
    {
        get { return (string)GetValue(PromptProperty); }
        set { SetValue(PromptProperty, value); }
    }

    public static readonly DependencyProperty PromptProperty =
        DependencyProperty.Register("Prompt", typeof(string), typeof(NumericUpAndDown), new PropertyMetadata(""));

    /// <summary>
    /// 显示提示语的TextBlock
    /// </summary>
    private TextBlock _promptTextBlock;

    public NumericUpAndDown()
    {
        Unloaded += OnUnloaded;
    }

    /// <summary>
    /// 减少数值的按钮
    /// </summary>
    private Button _downButton;

    /// <summary>
    /// 增加数值的按钮
    /// </summary>
    private Button _upButton;

    /// <summary>
    /// xaml中ControlTemplate被应用时调用
    /// </summary>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _downButton = GetTemplateChild("PART_Down") as Button;
        _downButton.Click += DecrementContent;
        _upButton = GetTemplateChild("PART_Up") as Button;
        _upButton.Click += IncrementContent;
        _contentTextBox = GetTemplateChild("PART_TextContent") as TextBox;
        _contentTextBox.TextChanged += OnTextChanged;
        _promptTextBlock = GetTemplateChild("PART_Prompt") as TextBlock;
    }


    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is not TextBox textBox)
        {
            return;
        }
        //尝试将TextBox的文本转换为整数
        if (Int32.TryParse(textBox.Text, out Int32 newValue))
        {
            NumericalContent = newValue;
            //转换成功时不要显示提示语
            _promptTextBlock.Visibility = Visibility.Collapsed;
        }
        else
        {
            //如果转换失败,显示一下提示语
            _promptTextBlock.Visibility = Visibility.Visible;
        }
    }

    /// <summary>
    /// 数值减少
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DecrementContent(object sender, RoutedEventArgs e)
    {
        //如果TextBox当前显示的不是整数类型，就不要做什么
        if (!Int32.TryParse(_contentTextBox.Text, out _))
        {
            return;
        }
        NumericalContent -= 1;
    }

    /// <summary>
    /// 数值增加
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void IncrementContent(object sender, RoutedEventArgs e)
    {
        if (!Int32.TryParse(_contentTextBox.Text, out _))
        {
            return;
        }
        NumericalContent += 1;
    }

    /// <summary>
    /// 控件被卸载时释放事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        Unloaded -= OnUnloaded;
        _downButton.Click -= DecrementContent;
        _upButton.Click -= IncrementContent;
        _contentTextBox.TextChanged -= OnTextChanged;
    }
}

