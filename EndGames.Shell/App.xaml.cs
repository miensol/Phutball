using AutoMapper;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Configuration;
using Caliburn.StructureMap;
using EndGames.Shell.Presenters.Interfaces;
using EndGames.Utils;
using StructureMap;

namespace EndGames.Shell
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App: CaliburnApplication
    {

        public App()
        {
            InitializeComponent();
        }


        protected override void ConfigurePresentationFramework(PresentationFrameworkConfiguration module)
        {
            module.RegisterAllScreensWithSubjects();
            ExecuteBootsrapTasks();
        }

        private void ExecuteBootsrapTasks()
        {
            StartupTask.RunAll(ObjectFactory.GetAllInstances<IStartupTask>());
            Mapper.AssertConfigurationIsValid();
        }

        protected override Microsoft.Practices.ServiceLocation.IServiceLocator CreateContainer()
        {
            ObjectFactory.Initialize(ie => ie.AddRegistry<EngGamesRegistry>());
            return new StructureMapAdapter(ObjectFactory.Container);
        }

        protected override object CreateRootModel()
        {
            return Container.GetInstance<IShellPresenter>();
        }

    }
}
