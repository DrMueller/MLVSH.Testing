using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualStudio.Shell;

namespace Mmu.Mlvsh.Testing.Application.Areas.ClassWriting.Commands
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideMenuResource("Menus1.ctmenu", 1)]
    [Guid(PackageGuidString)]
    public sealed class CreateUnitTestClassCommandPackage : AsyncPackage
    {
        private const string PackageGuidString = "0b41d893-b68e-487e-87b5-fb2312499274";

        protected override async System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await CreateUnitTestClassCommand.InitializeAsync(this);
        }
    }
}