using System;
using System.ComponentModel.Design;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.Orchestration.Services;
using Mmu.Mlvsh.Testing.Application.Infrastructure.DependencyInjection;

namespace Mmu.Mlvsh.Testing.Application
{
    internal sealed class Command1
    {
        private const int CommandId = 0x0100;
        private static readonly Guid _commandSet = new Guid("10fae917-d775-4c64-8010-943c2f6a0572");
        private readonly AsyncPackage _package;

#pragma warning disable SA1515 // Single-line comment must be preceded by blank line

// ReSharper disable once UnusedAutoPropertyAccessor.Local
        private static Command1 Instance { get; set; }
#pragma warning restore SA1515 // Single-line comment must be preceded by blank line

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

        private async void Execute(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(_package.DisposalToken);

            var dte = (DTE)await _package.GetServiceAsync(typeof(DTE));
            var selectedItems = dte.SelectedItems;

            if (selectedItems == null)
            {
                return;
            }

            foreach (SelectedItem selectedItem in selectedItems)
            {
                if (!(selectedItem.ProjectItem is ProjectItem projectItem))
                {
                    continue;
                }

                var filePath = projectItem.FileNames[0];
                var unitTestClassWriter = ApplicationServiceLocator.GetService<IUnitTestClassWriter>();
                unitTestClassWriter.CreateTestClass(filePath, projectItem.ContainingProject.Name);
            }
        }
    }
}