﻿using AutoMapper;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Configuration;
using Caliburn.StructureMap;
using Phutball.Shell.Presenters.Interfaces;
using Phutball.Utils;
using StructureMap;

namespace Phutball.Shell
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
            ObjectFactory.Configure(ce=> ce.For<App>().Use(this));
            return new StructureMapAdapter(ObjectFactory.Container);
        }

        protected override object CreateRootModel()
        {
            return Container.GetInstance<IShellPresenter>();
        }

    }
}
