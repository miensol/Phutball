using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using EndGames.Phutball;
using EndGames.Phutball.Search;
using EndGames.Tests.Phutball.Search;
using ForTesting;
using log4net.Config;
using NUnit.Framework;

namespace EndGames.Tests.Benchmark
{
    public abstract class observations_for_search_strategy_speed : observations_for_static_sut
    {
        static observations_for_search_strategy_speed()
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
        }

        private Stopwatch _timer = new Stopwatch();
        protected MoveFinders _moveFinders = new MoveFinders(new MovesFactory());

        protected TimeSpan MessureTime(Action work)
        {
            _timer.Reset();
            _timer.Start();
            work();
            _timer.Stop();
            return _timer.Elapsed;
        }

        protected IFieldsGraph RandomGraph(int rowCount, int columnCount, double blackDencity)
        {
            return TestGraphs.Random(rowCount, columnCount, blackDencity);
        }

        protected override void Because()
        {
        }
    }

    public static class IntExtensions
    {
        public static IEnumerable<TValue> Times<TValue>(this int i, Func<TValue> work)
        {
            for(var index = 0  ; index < i; ++index)
            {
                yield return work();
            }
        }
    }

    public abstract class observations_for_searching_with_brute_force : observations_for_search_strategy_speed
    {
        private const int TIMES_TO_COUNT_AVERAGE = 1;

        public TimeSpan brute_force_time(IFieldsGraph graph)
        {
            var search = GetSearchEngine(graph);
            return MessureTime(()=> search.Search(graph));
        }

        protected abstract IMoveFindingStartegy GetSearchEngine(IFieldsGraph graph);

        protected void run_benchmark_with_black_dencity_equal_to(double blackDencity)
        {
            board_collection().Select(tuple => 
                                      new { current = tuple, times = TIMES_TO_COUNT_AVERAGE.Times(() => brute_force_time(RandomGraph(tuple.Item1, tuple.Item2, blackDencity))) })
                .Each(times=> Console.WriteLine("{0}\tx\t{1}\t took {2}"
                                                    .ToFormat(times.current.Item1, 
                                                              times.current.Item2, 
                                                              TimeSpan.FromTicks((long)times.times.Average(t=> t.Ticks)))
                                  )
                );
        }

        private IEnumerable<Tuple<int,int>> board_collection()
        {
            yield return new Tuple<int, int>(20,15);
            yield return new Tuple<int, int>(30,22);
            yield return new Tuple<int, int>(40,30);
            yield return new Tuple<int, int>(50,40);
            yield return new Tuple<int, int>(60,50);
            yield return new Tuple<int, int>(70,60);
            yield return new Tuple<int, int>(80,70);
            yield return new Tuple<int, int>(90,80);
            yield return new Tuple<int, int>(100,90);
            yield return new Tuple<int, int>(110,100);
            yield return new Tuple<int, int>(120,110);
            yield return new Tuple<int, int>(130,120);
            yield return new Tuple<int, int>(140,130);
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

    public class when_searching_with_brute_force : observations_for_searching_with_brute_force
    {
        protected override IMoveFindingStartegy GetSearchEngine(IFieldsGraph graph)
        {
            return _moveFinders.DfsUnbounded(PlayersState.SecondIsOnTheMove());
        }
    } 
    
    [TestFixture]
    public class when_searching_with_brute_force_with_limited_search_depth : observations_for_searching_with_brute_force
    {
        protected override IMoveFindingStartegy GetSearchEngine(IFieldsGraph graph)
        {
            return _moveFinders.DfsBounded(PlayersState.SecondIsOnTheMove(), Math.Max(graph.RowCount/10, 5));
        }
    }
}