using Caliburn.Micro;
using CaliburnMicroLearn.LoggerUtils;
using PropertyChanged;

namespace CaliburnMicroLearn.ViewModels;

public class ShellViewModel : Screen
{
    [OnChangedMethod(nameof(OnXXXChanged))]
    public bool IsChecked { get; set; }

    public void OnXXXChanged()
    {
        Logger.Debug("Debug");
        Logger.Info("Info");
        Logger.Warn("Warn");
        Logger.Error("Error");
        try
        {
            var x = 1;
            var y = 0;
            var z = x / y;
        }
        catch (Exception ex)
        {
            Logger.Error("Error", ex);
        }
    }
}
