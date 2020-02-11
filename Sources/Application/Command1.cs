using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Mmu.Mlvsh.Testing.Application
{
    internal sealed class Command1
    {
        private const int CommandId = 0x0100;
        private static readonly Guid _commandSet = new Guid("10fae917-d775-4c64-8010-943c2f6a0572");
        private readonly AsyncPackage _package;
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private static Command1 Instance { get; set; }

        private Command1(AsyncPackage package, OleMenuCommandService commandService)
        {
            _package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(_commandSet, CommandId);
            var menuItem = new MenuCommand(Execute, menuCommandId);
            commandService.AddCommand(menuItem);
        }

        public static async System.Threading.Tasks.Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);
            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new Command1(package, commandService);
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var message = string.Format(CultureInfo.CurrentCulture, "Inside {0}.MenuItemCallback()", GetType().FullName);
            const string Title = "Command1";

            VsShellUtilities.ShowMessageBox(
                _package,
                message,
                Title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}