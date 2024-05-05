using Caliburn.Micro;
using CaliburnMicroLearn.LoggerUtils;
using CaliburnMicroLearn.ViewModels;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace CaliburnMicroLearn;

public class Bootstrapper : BootstrapperBase
{
    private CompositionContainer? _container;

    public Bootstrapper()
    {
        Initialize();
    }

    /// <summary>
    /// 启动时显示的页面
    /// 这里显示的是ShellViewModel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override async void OnStartup(object sender, StartupEventArgs e)
    {
        await DisplayRootViewForAsync<ShellViewModel>();
    }

    /// <summary>
    /// 配置IoC
    /// </summary>
    protected override void Configure()
    {
        _container = new CompositionContainer(
            new AggregateCatalog(AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()));
        var batch = new CompositionBatch();
        //把CaliburnMicro的WindowManager放入容器
        batch.AddExportedValue<IWindowManager>(new WindowManager());
        //把CaliburnMicro的EventAggregator放入容器
        batch.AddExportedValue<IEventAggregator>(new EventAggregator());
        batch.AddExportedValue(_container);
        _container.Compose(batch);
    }

    protected override void BuildUp(object instance)
    {
        _container!.SatisfyImportsOnce(instance);
    }

    protected override object GetInstance(Type service, string key)
    {
        string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(service) : key;
        var exports = _container!.GetExportedValues<object>(contract);
        if (exports.Any())
        {
            return exports.First();
        }

        throw new Exception($"Could not locate any instances of contract {contract}.");
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
        return _container!.GetExportedValues<object>(AttributedModelServices.GetContractName(service));
    }

    protected override IEnumerable<Assembly> SelectAssemblies()
    {
        return [Assembly.GetExecutingAssembly()];
    }

    /// <summary>
    /// 程序级的未处理错误
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected override void OnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        Logger.Error("未处理错误", e.Exception);
    }
}


