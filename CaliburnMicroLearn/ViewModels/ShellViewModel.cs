using Caliburn.Micro;
using PropertyChanged;
using System.Windows;

namespace CaliburnMicroLearn.ViewModels;

public class ShellViewModel : Screen
{
    [OnChangedMethod(nameof(OnXXXChanged))]
    public bool IsChecked { get; set; }

    public void OnXXXChanged()
    {
        MessageBox.Show("123");
    }
}
