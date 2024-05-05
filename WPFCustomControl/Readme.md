# 框架建立
1. 使用CaliburnMicro的模板建立工程
2. 创建CustomControls文件夹，里面放各种自定义控件。其中cs文件是继承自原生控件的各种类，
Styles文件夹中是各个控件对应的Style。Generic.xaml是创建自定义控件必须存在的，名字也不能改,
里面就用来引用其他控件的Style.xaml
```xaml
<ResourceDictionary xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation">
    <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="pack://application:,,,/WPFCustomControl;component/CustomControls/Styles/RWindowStyle.xaml" />
    </ResourceDictionary.MergedDictionaries>
</ResourceDictionary>
```
ShellView中的Window也可以换成RWindow
```xaml
<customControls:RWindow x:Class="WPFCustomControl.Views.ShellView"
                        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
                        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
                        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
                        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
                        xmlns:local="clr-namespace:WPFCustomControl.Views"
                        xmlns:customControls="clr-namespace:WPFCustomControl.CustomControls"
                        xmlns:cal="http://caliburnmicro.com"
                        mc:Ignorable="d"
                        Title="CaliburnMicro"
                        xmlns:viewModels="clr-namespace:WPFCustomControl.ViewModels"
                        d:DataContext="{d:DesignInstance Type=viewModels:ShellViewModel,IsDesignTimeCreatable=True}"
                        Height="450"
                        Width="800">

</customControls:RWindow>
```
# RWindow