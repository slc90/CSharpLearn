using Caliburn.Micro;
using CaliburnMicroLearn.LoggerUtils;

namespace CaliburnMicroLearn.UserControl.ViewModels;

public class UserControl1ViewModel : Screen, IHandle<EventAggregatorTest>
{
    /// <summary>
    /// 使用IoC找到全局的事件聚合器
    /// </summary>
    private readonly IEventAggregator _eventAggregator = IoC.Get<IEventAggregator>();

    /// <summary>
    /// 初始化ViewModel时监听事件聚合器
    /// </summary>
    public UserControl1ViewModel()
    {
        //在后台线程监听
        _eventAggregator.SubscribeOnBackgroundThread(this);
    }

    protected override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Logger.Debug("ActiveItem1 OnActivateAsync");
        return base.OnActivateAsync(cancellationToken);
    }

    /// <summary>
    /// Deactivate时把事件监听也取消掉
    /// </summary>
    /// <param name="close"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
        Logger.Debug("ActiveItem1 OnDeactivateAsync");
        _eventAggregator.Unsubscribe(this);
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

    /// <summary>
    /// 收到EventAggregatorTest时触发
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task HandleAsync(EventAggregatorTest message, CancellationToken cancellationToken)
    {
        Logger.Debug($"Message:{message.Message}");
        return Task.CompletedTask;
    }
}

