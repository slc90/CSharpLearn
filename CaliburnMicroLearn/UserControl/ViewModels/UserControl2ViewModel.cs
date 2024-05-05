using Caliburn.Micro;
using CaliburnMicroLearn.LoggerUtils;

namespace CaliburnMicroLearn.UserControl.ViewModels;

public class UserControl2ViewModel : Screen
{
    private readonly IEventAggregator _eventAggregator = IoC.Get<IEventAggregator>();

    /// <summary>
    /// 在激活时发出事件
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Logger.Debug("ActiveItem2 OnActivateAsync");
        _eventAggregator.PublishOnBackgroundThreadAsync(new EventAggregatorTest("Test"), cancellationToken);
        return base.OnActivateAsync(cancellationToken);
    }

    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
        Logger.Debug("ActiveItem2 OnDeactivateAsync");
        return base.OnDeactivateAsync(close, cancellationToken);
    }

    public override Task<bool> CanCloseAsync(CancellationToken cancellationToken = default)
    {
        Logger.Debug("ActiveItem2 CanCloseAsync");
        return base.CanCloseAsync(cancellationToken);
    }

    public override Task TryCloseAsync(bool? dialogResult = null)
    {
        Logger.Debug("ActiveItem2 TryCloseAsync");
        return base.TryCloseAsync(dialogResult);
    }
}

public class EventAggregatorTest(string message)
{
    public string Message { get; set; } = message;
}