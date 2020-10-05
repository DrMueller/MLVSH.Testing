using System;
using System.ComponentModel.Design;
using System.Linq;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.Orchestration.Commands;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.Orchestration.Services;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.SetupTestClass.Services;
using Mmu.Mlvsh.Testing.Application.Infrastructure.DependencyInjection;

namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.SetupTestClass.Commands
{
    internal sealed class SetupTestClassCommand
    {
        private const int CommandId = 0x0100;

        private static readonly Guid _commandSet = new Guid("4ce333ee-828f-4b5c-8f08-5910a1366b80");
        private readonly AsyncPackage _package;

#pragma warning disable IDE0052 // Remove unread private members
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private static SetupTestClassCommand Instance { get; set; }
#pragma warning restore IDE0052 // Remove unread private members

        private SetupTestClassCommand(AsyncPackage package, OleMenuCommandService commandService)
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
            Instance = new SetupTestClassCommand(package, commandService);
        }

#pragma warning disable VSTHRD100 // Avoid async void methods
        private async void Execute(object sender, EventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(_package.DisposalToken);

            var dte = (DTE)await _package.GetServiceAsync(typeof(DTE));
            if (dte == null)
            {
                throw new ArgumentNullException(nameof(dte));
            }

            var selectedItems = dte.SelectedItems;

            if (selectedItems == null)
            {
                return;
            }

            var projectItem = selectedItems.OfType<ProjectItem>().First();
            var filePath = projectItem.FileNames[0];
            var unitTestClassWriter = ApplicationServiceLocator.GetService<ITestClassSetupService>();
            unitTestClassWriter.SetupTestClass(filePath);
        }
    }
}