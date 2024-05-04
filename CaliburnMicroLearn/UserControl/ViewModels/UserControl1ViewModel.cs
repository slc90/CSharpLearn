using Caliburn.Micro;
using CaliburnMicroLearn.LoggerUtils;

namespace CaliburnMicroLearn.UserControl.ViewModels;

public class UserControl1ViewModel : Screen
{
    protected override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Logger.Debug("ActiveItem1 OnActivateAsync");
        return base.OnActivateAsync(cancellationToken);
    }

    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
        Logger.Debug("ActiveItem1 OnDeactivateAsync");
        return base.OnDeactivateAsync(close, cancellationToken);
    }

    public override Task<bool> CanCloseAsync(CancellationToken cancellationToken = default)
    {
        Logger.Debug("ActiveItem1 CanCloseAsync");
        return base.CanCloseAsync(cancellationToken);
    }

    public override Task TryCloseAsync(bool? dialogResult = null)
    {
        Logger.Debug("ActiveItem1 TryCloseAsync");
        return base.TryCloseAsync(dialogResult);
    }
}

