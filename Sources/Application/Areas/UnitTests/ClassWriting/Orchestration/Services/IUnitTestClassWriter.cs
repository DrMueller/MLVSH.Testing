namespace Mmu.Mlvsh.Testing.Application.Areas.UnitTests.ClassWriting.Orchestration.Services
{
    public interface IUnitTestClassWriter
    {
        void CreateTestClass(string fileToTestPath, string selectedProjectName);
    }
}