using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using ForTesting;
using log4net;
using log4net.Config;
using NUnit.Framework;
using Phutball.Search;
using Phutball.Tests.Search;

namespace Phutball.Tests.Benchmark
{
    public static class CultureSetter
    {
        public static void SetToPolish()
        {
            var currentUiCulture = CultureInfo.GetCultureInfo("PL");
            Thread.CurrentThread.CurrentCulture = currentUiCulture;
            Thread.CurrentThread.CurrentUICulture = currentUiCulture;
        }
    }

    public abstract class observations_for_search_strategy_speed : observations_for_static_sut
    {
        public int MAX_VISITED_NODES_COUNT = 10000000;
        protected const int TO_COUNT_AVERAGE = 1000;

        static observations_for_search_strategy_speed()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            
        }

        private Stopwatch _timer = new Stopwatch();
        protected RawMoveFinders RawMoveFinders ;
        protected IPhutballOptions options = new PhutballOptions();

        protected Tuple<TimeSpan, TResult> MessureTime<TResult>(Func<TResult> work)
        {
            _timer.Reset();
            _timer.Start();
            var result = work();
            _timer.Stop();
            return Tuple.Create(_timer.Elapsed,result) ;
        }

        protected IFieldsGraph RandomGraph(int rowCount, int columnCount, double blackDencity)
        {
            options.RowCount = rowCount;
            options.ColumnCount = columnCount;
            options.DfsSearchDepth = rowCount*columnCount;
            options.DfsMaxVistedNodes = MAX_VISITED_NODES_COUNT;
            options.BfsMaxVisitedNodes = MAX_VISITED_NODES_COUNT;
            return TestGraphs.Random(rowCount, columnCount, blackDencity);
        }

        protected override void Because()
        {
        }
    }

    public abstract class observations_for_searching_with_brute_force : observations_for_search_strategy_speed
    {
        protected const string INFO_HEADER = "Dencity;Size;Visited;MaxVisited;MaxDepth;Time;ExceededMaxVisited;Cuttoffs;SumOfValues";

        public static readonly ILog Logger = LogManager.GetLogger("Test");

        public Tuple<TimeSpan, PhutballMoveScore> brute_force_time(IFieldsGraph graph)
        {
            var search = GetSearchEngine(graph);
            var time = MessureTime(()=> search.Search(graph));
            return Tuple.Create(time.Item1, time.Item2);
        }

        protected abstract IMoveFindingStartegy GetSearchEngine(IFieldsGraph graph);

        protected void run_benchmark_with_black_dencity_equal_to(double blackDencity)
        {
            board_collection().Select(tuple => 
            new
                {
                    boarSize = tuple, 
                    results = TO_COUNT_AVERAGE.Times(() => brute_force_time(RandomGraph(tuple.Item1, tuple.Item2, blackDencity))).ToList()
                })
                .Each(times=>
                          {
                              Logger.Info("{0};{1};{2};{3};{4};{5};{6};{7};{8}".ToFormat(
                                    blackDencity,
                                    times.boarSize.Item1,
                                    times.results.Average(t=> t.Item2.VisitedNodesCount),
                                    times.results.Max(t=> t.Item2.VisitedNodesCount),
                                    times.results.Average(t=> t.Item2.MaxDepth),
                                    TimeSpan.FromTicks((long) times.results.Average(t => t.Item1.Ticks)).TotalMilliseconds,
                                    times.results.Count(t=> t.Item2.VisitedNodesCount>= MAX_VISITED_NODES_COUNT),
                                    times.results.Average(t=> t.Item2.CuttoffsCount),
                                    times.results.Average(t=> t.Item2.Score)
                                  ));
                          }
                );
        }

        private static IEnumerable<Tuple<int,int>> board_collection()
        {
//            return Enumerable.Range(1, 10).Select(i => i*10)
//                .Select(r => Tuple.Create(r, r));
//            yield return new Tuple<int, int>(10,10);
            yield return new Tuple<int, int>(15,15);
//            yield return new Tuple<int, int>(20,20);
//            yield return new Tuple<int, int>(25,25);
//            yield return new Tuple<int, int>(30,30);
//            yield return new Tuple<int, int>(35,35);
//            yield return new Tuple<int, int>(40,40);
//            yield return new Tuple<int, int>(60,60);
//            yield return new Tuple<int, int>(70,70);
//            yield return new Tuple<int, int>(80,80);
//            yield return new Tuple<int, int>(90,90);
//            yield return new Tuple<int, int>(100,100);            
        }

        [Test]
        public void dencity_equal_to_005()
        {
            run_benchmark_with_black_dencity_equal_to(0.05);
        }

        [Test]
        public void dencity_equal_to_01()
        {
            run_benchmark_with_black_dencity_equal_to(0.1);
        }

        [Test]
        public void dencity_equal_to_02()
        {
            run_benchmark_with_black_dencity_equal_to(0.2);
        }

        [Test]
        public void dencity_equal_to_03()
        {
            run_benchmark_with_black_dencity_equal_to(0.3);
        }

        [Test]
        public void dencity_equal_to_04()
        {
            run_benchmark_with_black_dencity_equal_to(0.4);
        }

        [Test]
        public void dencity_equal_to_05()
        {
            run_benchmark_with_black_dencity_equal_to(0.5);
        }

        [Test]
        public void dencity_equal_to_06()
        {
            run_benchmark_with_black_dencity_equal_to(0.6);
        }

        [Test]
        public void dencity_equal_to_07()
        {
            run_benchmark_with_black_dencity_equal_to(0.7);
        }

        [Test]
        public void dencity_equal_to_08()
        {
            run_benchmark_with_black_dencity_equal_to(0.8);
        }

        [Test]
        public void dencity_equal_to_09()
        {
            run_benchmark_with_black_dencity_equal_to(0.9);
        }
    }

    [TestFixture]
    public class when_searching_with_dfs : observations_for_searching_with_brute_force
    {
        [TestFixtureSetUp]
        public void start_fixture()
        {
            CultureSetter.SetToPolish();
            RandomSource.Reset();
            Logger.Info("Dfs search");
            Logger.Info(INFO_HEADER);
        }

        protected override IMoveFindingStartegy GetSearchEngine(IFieldsGraph graph)
        {
            RawMoveFinders = new RawMoveFinders(new MovesFactory(), PlayersState.SecondIsOnTheMove(), options);
            return RawMoveFinders.DfsNodesBounded();
        }
    } 
    
    
    [TestFixture]
    public class when_searching_dfs_with_cuttoffs_to_white : observations_for_searching_with_brute_force
    {
        [TestFixtureSetUp]
        public void start_fixture()
        {
            CultureSetter.SetToPolish();
            RandomSource.Reset();
            Logger.Info("Dfs with cutoffs to white");
            Logger.Info(INFO_HEADER);
        }

        protected override IMoveFindingStartegy GetSearchEngine(IFieldsGraph graph)
        {
            RawMoveFinders = new RawMoveFinders(new MovesFactory(), PlayersState.SecondIsOnTheMove(), options);
            return RawMoveFinders.DfsCuttoffToWhite();
        }
    }   
    
    [TestFixture]
    public class when_searching_dfs_with_cuttoffs : observations_for_searching_with_brute_force
    {
        [TestFixtureSetUp]
        public void start_fixture()
        {
            CultureSetter.SetToPolish();
            RandomSource.Reset();
            Logger.Info("Dfs with cutoffs");
            Logger.Info(INFO_HEADER);
        }

        protected override IMoveFindingStartegy GetSearchEngine(IFieldsGraph graph)
        {
            RawMoveFinders = new RawMoveFinders(new MovesFactory(), PlayersState.SecondIsOnTheMove(), options);
            return RawMoveFinders.DfsCuttoff();
        }
    } 
    
    [TestFixture]
    public class when_searching_with_bfs : observations_for_searching_with_brute_force
    {
        [TestFixtureSetUp]
        public void start_fixture()
        {
            CultureSetter.SetToPolish();
            Logger.Info("Bfs search");
            Logger.Info(INFO_HEADER);
        }


        protected override IMoveFindingStartegy GetSearchEngine(IFieldsGraph graph)
        {
            RawMoveFinders = new RawMoveFinders(new MovesFactory(), PlayersState.SecondIsOnTheMove(), options);
            return RawMoveFinders.BfsNodesBounded();
        }
    } 
    
    
    [TestFixture]
    public class when_searching_with_bfs_with_cuttoffs_to_white : observations_for_searching_with_brute_force
    {
        [TestFixtureSetUp]
        public void start_fixture()
        {
            CultureSetter.SetToPolish();
            Logger.Info("Bfs cuttoff to white");
            Logger.Info(INFO_HEADER);
        }


        protected override IMoveFindingStartegy GetSearchEngine(IFieldsGraph graph)
        {
            RawMoveFinders = new RawMoveFinders(new MovesFactory(), PlayersState.SecondIsOnTheMove(), options);
            return RawMoveFinders.BfsCuttoffToWhite();
        }
    } 
    
    [TestFixture]
    public class when_searching_in_order_of_board_values : observations_for_searching_with_brute_force
    {
        [TestFixtureSetUp]
        public void start_fixture()
        {
            CultureSetter.SetToPolish();
            Logger.Info("Ordered search");
            Logger.Info(INFO_HEADER);
        }


        protected override IMoveFindingStartegy GetSearchEngine(IFieldsGraph graph)
        {
            RawMoveFinders = new RawMoveFinders(new MovesFactory(), PlayersState.SecondIsOnTheMove(), options);
            return RawMoveFinders.OrderByNodesValues();
        }
    } 
    
    [TestFixture]
    public class when_searching_in_order_of_board_values_with_cuttoffs_to_white : observations_for_searching_with_brute_force
    {
        [TestFixtureSetUp]
        public void start_fixture()
        {
            CultureSetter.SetToPolish();
            Logger.Info("Ordered search with cuttof to white");
            Logger.Info(INFO_HEADER);
        }


        protected override IMoveFindingStartegy GetSearchEngine(IFieldsGraph graph)
        {
            RawMoveFinders = new RawMoveFinders(new MovesFactory(), PlayersState.SecondIsOnTheMove(), options);
            return RawMoveFinders.OrderByNodesValuesWithCuttofsToWhite();
        }
    }
}