using ModestTree;
using Zenject;

public sealed class RepositoryInstaller : Installer
{
    private const string FILE_NAME = "Database.txt"; 
    
    public override void InstallBindings()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FILE_NAME);
        Console.Write(AppDomain.CurrentDomain.BaseDirectory);
        this.Container
            .Bind<Repository>()
            .AsSingle()
            .WithArguments(filePath);
    }
}