using System;
using Caliburn.StructureMap;
using Microsoft.Practices.ServiceLocation;
using NUnit.Framework;
using Rhino.Mocks;
using StructureMap;
using StructureMap.AutoMocking;
using ServiceLocator=Microsoft.Practices.ServiceLocation.ServiceLocator;

namespace ForTesting
{
    /// <summary>
    /// Bazowa klasa dla testow zgodna z AAA
    /// </summary>
    [TestFixture]
    public abstract class observations_for_sut
    {
        [SetUp]
        public virtual void Setup()
        {
            EstablishContext();
            Because();
        }

        /// <summary>
        /// Wywolywana po kazdym tescie
        /// </summary>
        protected virtual void AfterEachObservation()
        {
        }

        [TearDown]
        private void TearDown()
        {
            AfterEachObservation();
        }

        /// <summary>
        /// Przeprowadz test (Act)
        /// </summary>
        protected abstract void Because();

        /// <summary>
        /// Przygotuwuje œrodowisko do testowania (Arrange)
        /// </summary>
        protected virtual void EstablishContext()
        {
        }

    }

    /// <summary>
    /// Bazowa klasa testow konkretnej klasy, zgodna z AAA
    /// </summary>
    /// <typeparam name="TSut">Testowana klasa</typeparam>	
    public abstract class observations_for_sut_of_type<TSut>
        : observations_for_sut
    {

        protected TSut Sut;

        [SetUp]
        public override void Setup()
        {
            EstablishContext();
            Sut = CreateSut();
            AfterSutCreation();
            Because();
        }

        protected virtual void AfterSutCreation()
        {
        }
        /// <summary>
        /// Tworzy testowany system
        /// </summary>
        /// <returns>Testowany system</returns>
        protected abstract TSut CreateSut();

    }

    public abstract class observations_for_auto_created_sut_of_type<TSut>
        : observations_for_sut_of_type<TSut> where TSut : class
    {
        private MockRepository _mocks;
        private RhinoAutoMocker<TSut> _autoMocker;

        protected IContainer Container 
        {
            get 
            { 
                return _autoMocker.Container;
            }
        }
        [SetUp]
        public override void Setup()
        {
            _mocks = new MockRepository();
            _autoMocker = new RhinoAutoMocker<TSut>(MockMode.AAA);
            var serviceLocator = new StructureMapAdapter(_autoMocker.Container);
            ServiceLocator.SetLocatorProvider(()=> serviceLocator);
            ProvideImplementationOf<IServiceLocator>(serviceLocator);
            base.Setup();
        }

        protected T Stub<T>() where T : class
        {
            var stub = MockRepository.GenerateStub<T>();
            _autoMocker.Container.Inject(stub);
            return stub;
        }

        protected T GenerateStub<T>() where T : class
        {
            return MockRepository.GenerateStub<T>();
        }

        protected void ProvideImplementationOf<TType>(TType implementation)
        {
            _autoMocker.Container.Inject(implementation);
        }

        protected void ProvideBindingOf<TWath, TWiht>() where TWiht : TWath
        {
            _autoMocker.Container.Configure(cf=> cf.For<TWath>().Singleton().Use<TWiht>());
        }

        protected void ProvideBindingOf(Type what, Type with)
        {
            _autoMocker.Container.Configure(cf => cf.For(what).Singleton().Use(with));
        }

        protected T Dependency<T>()
            where T : class
        {
            T result = null;
//            try
//            {
                  result = _autoMocker.Get<T>();
//                if (!_mocks.IsInReplayMode(result))
//                {
//                    _mocks.Replay(result);
//                }
//            }
//            catch
//            {
                //result = null;
//            }
            return result;
        }


        protected override void AfterEachObservation()
        {
            _autoMocker = null;
            _mocks = null;
            base.AfterEachObservation();
        }

        protected override TSut CreateSut()
        {
            return _autoMocker.ClassUnderTest;
        }
    }


    public abstract class observations_for_static_sut : observations_for_sut
    {
    }

    public abstract class observations_for_static_sut_with_ioc : observations_for_auto_created_sut_of_type<object> 
    {
        protected override object CreateSut()
        {
            return null;
        }

    }
}
