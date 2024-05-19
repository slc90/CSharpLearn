using Caliburn.Micro;
using System.ComponentModel.Composition;

namespace WPFCustomControl.ViewModels;

[Export]
public class ShellViewModel : Conductor<IScreen>
{
}