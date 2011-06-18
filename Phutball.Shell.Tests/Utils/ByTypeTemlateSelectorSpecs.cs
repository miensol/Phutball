using System;
using System.Windows;
using EndGames.Shell.Utils;
using ForTesting;
using NUnit.Framework;
using Rhino.Mocks;

namespace EndGames.Shell.Tests.Utils
{
    public class when_getting_template_for_view_model : observations_for_auto_created_sut_of_type<ByTypeTemplateSelector>
    {
        private DataTemplate _resolvedDataTemplate;
        private object _inputViewModel;
        private IResourceProvider _resourceProvider;
        private DataTemplate _expectedDataTemlate;

        protected override void Because()
        {
            _resolvedDataTemplate = Sut.GetDataTemlate(_inputViewModel, _resourceProvider);
        }

        protected override void EstablishContext()
        {
            _inputViewModel = new TestViewModel();
            _resourceProvider = Dependency<IResourceProvider>();
            _expectedDataTemlate = new DataTemplate();
            _resourceProvider.Stub(provider => provider.FindDataTemplate(Arg.Is("TestViewModel"))).Return(_expectedDataTemlate);
        }

        [Test]
        public void should_return_proper_data_template()
        {
            _resolvedDataTemplate.ShouldEqual(_expectedDataTemlate);
        }
    }


    public class TestViewModel
    {
    }
}