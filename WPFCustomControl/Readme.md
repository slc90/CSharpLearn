# 框架建立
1. 使用CaliburnMicro的模板建立工程
2. 创建Themes文件夹，里面放各种自定义控件。其中cs文件是继承自原生控件的各种类，
Styles文件夹中是各个控件对应的Style。Generic.xaml是创建自定义控件必须存在的，名字也不能改,
里面就用来引用其他控件的Style.xaml
```xaml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation">
    <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="pack://application:,,,/WPFCustomControl;component/Themes/Styles/CustomWindowStyle.xaml" />
    </ResourceDictionary.MergedDictionaries>
</ResourceDictionary>
```
ShellView中的Window也可以换成CustomWindow
```xaml
<customControls:CustomWindow x:Class="WPFCustomControl.Views.ShellView"
                             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                             xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
                             xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
                             xmlns:customControls="clr-namespace:WPFCustomControl.Themes"
                             xmlns:cal="http://caliburnmicro.com"
                             mc:Ignorable="d"
                             xmlns:viewModels="clr-namespace:WPFCustomControl.ViewModels"
                             d:DataContext="{d:DesignInstance Type=viewModels:ShellViewModel,IsDesignTimeCreatable=False}"
                             Width="800"
                             Height="600">
</customControls:CustomWindow>
```
# CustomWindow
第一个实现的自定义控件就是自定义窗口。主要需要实现以下功能:  
1. 自定义标题栏
2. 最小化、最大化、关闭、拖动
# NumericUpAndDown
1. 能够输入数字以及通过+-按钮增减内容
# 添加实时中英文切换
1. 方法是清空当前资源字典，然后添加一份新的对应的语言的资源xaml，绑定只能用DynamicResource
2. zh-CN.xaml和en-US.xaml要设置成Resource
# Behaviour 
有时需要给控件添加一些行为逻辑，但是又不是同一类的控件都需要，只是在有些场景下需要。此时就可以使用Behaviour类来为某类控件添加一些行为，
然后在xaml里给需要的控件添加。
