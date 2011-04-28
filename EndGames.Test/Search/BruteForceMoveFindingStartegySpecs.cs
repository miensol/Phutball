using System;
using System.Collections.Generic;
using System.Linq;
using ForTesting;
using NUnit.Framework;
using Phutball.Moves;
using Phutball.PlayerMoves;
using Phutball.Search;
using Phutball.Search.Strategies;

namespace Phutball.Tests.Search
{
    public class when_applying_best_move_with_dfs : observations_for_applying_best_move_with_dfs
    {
        [Test]
        public void should_jump_over_one_black_stone_to_winning_border()
        {
            AfterMoveOn(TestGraphs.BlackStonToJumpToWinningBorder()).ShouldHaveWhiteFieldAt(1,2);
        }

        [Test]
        public void should_not_jump_over_black_stone_to_loosing_border()
        {
            AfterMoveOn(TestGraphs.BlackStoneToJumpToLoosingBorder()).ShouldHaveWhiteFieldAt(3, 2);
        }

        [Test]
        public void should_jump_backwards_to_find_winning_move()
        {
            AfterMoveOn(TestGraphs.OneBackwardJumpToFindWinningPath()).ShouldHaveWhiteFieldAt(1, 1);
        }

        [Test]
        public void should_jump_backwards_to_improve_final_postion()
        {
            AfterMoveOn(TestGraphs.TwoBackWardJumpsToImprovePosition()).ShouldHaveWhiteFieldAt(2, 1);
        }

        [Test]
        public void should_jump_backwards_to_improve_final_position_but_avoid_loosing()
        {
            AfterMoveOn(TestGraphs.TwoWaysToJumpBackwardsOneWillLose()).ShouldHaveWhiteFieldAt(2, 1);
        }

        [Test]
        public void should_jump_backwards_picking_best_way()
        {
            AfterMoveOn(TestGraphs.TwoWaysToJumpBackwardsOneWins()).ShouldHaveWhiteFieldAt(0, 5);
        }

        [Test]
        public void should_not_pick_move_passing_though_losing_fields()
        {
            AfterMoveOn(TestGraphs.WinningWayPassingThoughLossingField()).ShouldHaveWhiteFieldAt(3, 3);
        }

        [Test]
        public void should_continue_searching_after_improving_position()
        {
            AfterMoveOn(TestGraphs.ContinueSearchAfterImprovingPosition()).ShouldHaveWhiteFieldAt(0, 5);
        }
    }

    public class when_applying_best_move_with_dfs_with_cuttoffs_to_white : observations_for_applying_best_move
    {

        [Test]
        public void should_jump_over_one_black_stone_to_winning_border()
        {
            AfterMoveOn(TestGraphs.BlackStonToJumpToWinningBorder()).ShouldHaveWhiteFieldAt(1,2);
        }

        [Test]
        public void should_not_jump_over_black_stone_to_loosing_border()
        {
            AfterMoveOn(TestGraphs.BlackStoneToJumpToLoosingBorder()).ShouldHaveWhiteFieldAt(3, 2);
        }

        [Test]
        public void should_jump_backwards_to_find_winning_move()
        {
            AfterMoveOn(TestGraphs.OneBackwardJumpToFindWinningPath()).ShouldHaveWhiteFieldAt(1, 1);
        }

        [Test]
        public void should_jump_backwards_to_improve_final_postion()
        {
            AfterMoveOn(TestGraphs.TwoBackWardJumpsToImprovePosition()).ShouldHaveWhiteFieldAt(2, 1);
        }

        [Test]
        public void should_jump_backwards_to_improve_final_position_but_avoid_loosing()
        {
            AfterMoveOn(TestGraphs.TwoWaysToJumpBackwardsOneWillLose()).ShouldHaveWhiteFieldAt(2, 1);
        }

        [Test]
        public void should_jump_backwards_picking_best_way()
        {
            AfterMoveOn(TestGraphs.TwoWaysToJumpBackwardsOneWins()).ShouldHaveWhiteFieldAt(0, 5);
        }

        [Test]
        public void should_not_pick_move_passing_though_losing_fields()
        {
            AfterMoveOn(TestGraphs.WinningWayPassingThoughLossingField()).ShouldHaveWhiteFieldAt(3, 3);
        }

        [Test]
        public void should_continue_searching_after_improving_position()
        {
            AfterMoveOn(TestGraphs.ContinueSearchAfterImprovingPosition()).ShouldHaveWhiteFieldAt(0, 5);
        }

        [Test]
        public void should_cuttoff_to_white_properly()
        {
            AfterMoveOn(TestGraphs.BestResultIs2ThereIs1CuttoffToWhite()).ShouldHaveWhiteFieldAt(2, 5);
            _bestMove.CuttoffsCount.ShouldEqual(1);

        }

        protected override IMoveFindingStartegy GetSearchStrategy()
        {
            return RawMoveFinders.DfsCuttoffToWhite();
        }
    }


    public class when_searching_for_best_move_with_cuttoffs_to_target_border : observations_for_applying_best_move
    {

        [Test]
        public void should_cuttoff_from_start_if_cant_win()
        {
            AfterMoveOn(TestGraphs.TwoWaysToJumpBackwardsNoneWillWin()).ShouldHaveWhiteFieldAt(6, 3);
            _bestMove.CuttoffsCount.ShouldEqual(1);
        }


        protected override IMoveFindingStartegy GetSearchStrategy()
        {
            return RawMoveFinders.DfsCuttoff();
        }
    }


    public class when_applying_best_move_with_bfs : observations_for_applying_best_move_with_bfs
    {
        [Test]
        public void should_jump_over_one_black_stone_to_winning_border()
        {
            AfterMoveOn(TestGraphs.BlackStonToJumpToWinningBorder()).ShouldHaveWhiteFieldAt(1, 2);
        }

        [Test]
        public void should_not_jump_over_black_stone_to_loosing_border()
        {
            AfterMoveOn(TestGraphs.BlackStoneToJumpToLoosingBorder()).ShouldHaveWhiteFieldAt(3, 2);
        }

        [Test]
        public void should_jump_backwards_to_find_winning_move()
        {
            AfterMoveOn(TestGraphs.OneBackwardJumpToFindWinningPath()).ShouldHaveWhiteFieldAt(1, 1);
        }

        [Test]
        public void should_jump_backwards_to_improve_final_postion()
        {
            AfterMoveOn(TestGraphs.TwoBackWardJumpsToImprovePosition()).ShouldHaveWhiteFieldAt(2, 1);
        }

        [Test]
        public void should_jump_backwards_to_improve_final_position_but_avoid_loosing()
        {
            AfterMoveOn(TestGraphs.TwoWaysToJumpBackwardsOneWillLose()).ShouldHaveWhiteFieldAt(2, 1);
        }

        [Test]
        public void should_jump_backwards_picking_best_way()
        {
            AfterMoveOn(TestGraphs.TwoWaysToJumpBackwardsOneWins()).ShouldHaveWhiteFieldAt(0, 5);
        }

        [Test]
        public void should_not_pick_move_passing_though_losing_fields()
        {
            AfterMoveOn(TestGraphs.WinningWayPassingThoughLossingField()).ShouldHaveWhiteFieldAt(3, 3);
        }

        [Test]
        public void should_continue_searching_after_improving_position()
        {
            AfterMoveOn(TestGraphs.ContinueSearchAfterImprovingPosition()).ShouldHaveWhiteFieldAt(0, 5);
        }
    }

    public class when_searching_in_order_of_graph_values : observations_for_applying_best_move
    {

        [Test]
        public void should_firstly_traverse_bettern_nodes()
        {
            AfterMoveOn(TestGraphs.ImproveBy1AndGoDownDepply()).ShouldHaveWhiteFieldAt(2, 1);
        }


        [Test]
        public void should_jump_over_one_black_stone_to_winning_border()
        {
            AfterMoveOn(TestGraphs.BlackStonToJumpToWinningBorder()).ShouldHaveWhiteFieldAt(1, 2);
        }

        [Test]
        public void should_not_jump_over_black_stone_to_loosing_border()
        {
            AfterMoveOn(TestGraphs.BlackStoneToJumpToLoosingBorder()).ShouldHaveWhiteFieldAt(3, 2);
        }

        [Test]
        public void should_jump_backwards_to_find_winning_move()
        {
            AfterMoveOn(TestGraphs.OneBackwardJumpToFindWinningPath()).ShouldHaveWhiteFieldAt(1, 1);
        }

        [Test]
        public void should_jump_backwards_to_improve_final_postion()
        {
            AfterMoveOn(TestGraphs.TwoBackWardJumpsToImprovePosition()).ShouldHaveWhiteFieldAt(2, 1);
        }

        [Test]
        public void should_jump_backwards_to_improve_final_position_but_avoid_loosing()
        {
            AfterMoveOn(TestGraphs.TwoWaysToJumpBackwardsOneWillLose()).ShouldHaveWhiteFieldAt(2, 1);
        }

        [Test]
        public void should_jump_backwards_picking_best_way()
        {
            AfterMoveOn(TestGraphs.TwoWaysToJumpBackwardsOneWins()).ShouldHaveWhiteFieldAt(0, 5);
        }

        [Test]
        public void should_not_pick_move_passing_though_losing_fields()
        {
            AfterMoveOn(TestGraphs.WinningWayPassingThoughLossingField()).ShouldHaveWhiteFieldAt(3, 3);
        }

        [Test]
        public void should_continue_searching_after_improving_position()
        {
            AfterMoveOn(TestGraphs.ContinueSearchAfterImprovingPosition()).ShouldHaveWhiteFieldAt(0, 5);
        }



        protected override IMoveFindingStartegy GetSearchStrategy()
        {
            return RawMoveFinders.OrderByNodesValues();
        }
    }




    public abstract class observations_for_applying_best_move : observations_for_static_sut_with_ioc
    {
        protected override void Because()
        {
        }

        protected IPlayersState _playersState;
        private IMoveFindingStartegy _strategy;
        protected RawMoveFinders RawMoveFinders;
        private PerformMoves _performMoves;
        protected PhutballMoveScore _bestMove;
        protected abstract IMoveFindingStartegy GetSearchStrategy();

        protected IFieldsGraph AfterMoveOn(TestFieldsGraph graphToSearch)
        {            
            var actualGraph = graphToSearch.Build();
            _playersState = PlayersState.SecondIsOnTheMove();
            var phutballOptions = new PhutballOptions()
                                      {
                                          RowCount = actualGraph.RowCount,
                                          ColumnCount = actualGraph.ColumnCount
                                      };
            RawMoveFinders = new RawMoveFinders(new MovesFactory(), _playersState,  phutballOptions);
            _performMoves = new PerformMoves(actualGraph, new NulloPlayersSwapper());
            _strategy = GetSearchStrategy();
            _bestMove = _strategy.Search(actualGraph);
            if (_bestMove.Move != null)
            {
                _performMoves.Perform(_bestMove.Move);                
            }
            return actualGraph;
        }
    }

    public abstract class observations_for_applying_best_move_with_dfs : observations_for_applying_best_move
    {
        protected override IMoveFindingStartegy GetSearchStrategy()
        {
            return RawMoveFinders.DfsUnbounded();
        }
    }

    public abstract class observations_for_applying_best_move_with_bfs : observations_for_applying_best_move
    {
        protected override IMoveFindingStartegy GetSearchStrategy()
        {
            return RawMoveFinders.BfsUnbounded();
        }

    }


    public class when_searching_on_board_with_one_black_stone_to_jump : observations_for_brute_force_searching
    {

        protected override TestFieldsGraph GraphBuilder()
        {
            return TestGraphs.BlackStonToJumpToWinningBorder();
        }

        [Test]
        public void should_find_a_move()
        {
            _bestMove.ShouldNotBeNull();
        }

        private IList<IPhutballMove> GetMoves()
        {
            return _bestMove.ShouldBeOfType<CompositeMove>().GetMoves().ToList();
        }

        [Test]
        public void should_first_select_white_field()
        {
            GetMoves().First().ShouldBeOfType<SelectWhiteFieldMove>();
        }

        [Test]
        public void should_then_jump_black_stone()
        {
            GetMoves().Last().ShouldBeOfType<JumpWhiteStoneMove>();
        }

        [Test]
        public void should_put_white_field_behind_black()
        {
            _performMoves.Perform(_bestMove);            
            _fieldsGraph.GetWhiteField().RowIndex.ShouldEqual(1);
            _fieldsGraph.GetWhiteField().ColumnIndex.ShouldEqual(2);
        }
    }


    public class when_searching_on_board_with_only_one_white_field : observations_for_brute_force_searching
    {
        [Test]
        public void should_return_null()
        {
            _bestMove.ShouldBeNull();
        }

        [Test]
        public void should_not_select_white_field()
        {
            _fieldsGraph.GetWhiteField().Selected.ShouldBeFalse();
        }


        protected override TestFieldsGraph GraphBuilder()
        {
            return new TestFieldsGraph(7,6);
        }
    }


    public abstract class observations_for_brute_force_searching : observations_for_auto_created_sut_of_type<BruteForceMoveFindingStartegy>
    {
        protected IFieldsGraph _fieldsGraph;
        protected IPhutballMove _bestMove;
        protected IPerformMoves _performMoves;
        private IPlayersState _playersState;
        private RawMoveFinders _moveFinders;

        protected override void Because()
        {
            _bestMove = Sut.Search(_fieldsGraph).Move;
        }

        protected override BruteForceMoveFindingStartegy CreateSut()
        {            
            return (BruteForceMoveFindingStartegy) _moveFinders.DfsUnbounded();
        }

        protected override void EstablishContext()
        {
            _fieldsGraph = GraphBuilder().Build();
            _playersState = PlayersState.SecondIsOnTheMove();
            _performMoves = new PerformMoves(_fieldsGraph, new NulloPlayersSwapper());
            var testPhutballOptions = new PhutballOptions
                                          {
                                              RowCount = _fieldsGraph.RowCount,
                                              ColumnCount = _fieldsGraph.ColumnCount
                                          };
            _moveFinders = new RawMoveFinders(new MovesFactory(), _playersState, testPhutballOptions);
            ProvideImplementationOf<IPhutballOptions>(testPhutballOptions);

        }
        protected abstract TestFieldsGraph GraphBuilder();
    
    }
}