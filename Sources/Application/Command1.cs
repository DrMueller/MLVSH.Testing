using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace Mmu.Mlvsh.Testing.Application
{
    internal sealed class Command1
    {
        public const int CommandId = 0x0100;
        public static readonly Guid CommandSet = new Guid("10fae917-d775-4c64-8010-943c2f6a0572");
        private readonly AsyncPackage _package;
        public static Command1 Instance { get; private set; }
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider => _package;

        private Command1(AsyncPackage package, OleMenuCommandService commandService)
        {
            _package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(Execute, menuCommandID);
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
            var title = "Command1";

            VsShellUtilities.ShowMessageBox(
                _package,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }
    }
}