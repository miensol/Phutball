namespace Phutball.Search
{
    public interface IMoveFinders
    {
        [StrategyDesctiption("Wykonaj skok przy użyciu przeszukiwania dfs bez ograniczenia na głębokość")]
        IMoveFindingStartegy DfsUnbounded();
        [StrategyDesctiption("Wykonaj skok przy użyciu przeszukiwania bfs bez ograniczenia na głębokość")]
        IMoveFindingStartegy BfsUnbounded();
        [StrategyDesctiption("Wykonaj skok przy użyciu przeszukiwania dfs o maksymalnej głębokości zadanej w opcjach")]
        IMoveFindingStartegy DfsBounded();
        [StrategyDesctiption(@"Wykonaj skok przy użyciu przeszukiwania dfs przerywając przeszukiwanie jeśli nie można zwyciężyć lub poprawić pozycji.
W opcjach ustawiamy maksymalna liczbę odwiedzonych wierzchołków.")]        
        IMoveFindingStartegy DfsCuttoff();
        [StrategyDesctiption(@"Wykonaj skok przy użyciu przeszukiwania dfs przerywając przeszukiwanie jeśli nie można poprawić pozycji.
W opcjach ustawiamy limit na liczbę odwiedzonych wierzchołków.")]
        IMoveFindingStartegy DfsCuttoffToWhite();
        [StrategyDesctiption(@"Wykonaj skok przeglądając najpierw najbardziej korzystne wierzhołki i przerywając przeszukiwanie jeśli nie można poprawić pozyacji.
W opcjach ustawiamy limit na liczbę odwiedzonych wierzchołków.")]
        IMoveFindingStartegy OrderByNodesValuesWithCuttofsToWhite();
        [StrategyDesctiption(@"Wykonaj skok przy użyciu przeszukiwania bfs przerywając przeszukiwanie jeśli nie można poprawić pozycji.
W opcjach ustawiamy limit na liczbę odwiedzonych wierzchołków")]
        IMoveFindingStartegy BfsCuttoffToWhite();
        [StrategyDesctiption("Wykonaj skok przy użyciu przeszukiwania dfs z limitem na liczbę odwiedzonych wierzchołków")]
        IMoveFindingStartegy DfsNodesBounded();
        [StrategyDesctiption("Wykonaj skok przy użyciu przeszukiwania bfs o maksymalnej głębokości zadanej w opcjach")]
        IMoveFindingStartegy BfsBounded();
        [StrategyDesctiption(@"Wykonaj skok przy użyciu przeszukiwania alfa-beta.
W opcjach ustawiamy głębokość drzewa mini-max oraz maksymalna głębokość drzewa pojedyńczego skoku")]
        IMoveFindingStartegy AlphaBetaJumps();
        [StrategyDesctiption(@"Wykonaj skok poprawiający pozycje przy użyciu przeszukiwania alfa-beta lub pozostaw piłkarz na miejscu jeśli nie można poprawić pozycji.
W opcjach ustawiamy głębokość drzewa mini-max oraz maksymalna głębokość drzewa pojedyńczego skoku")]
        IMoveFindingStartegy AlphaBetaJumpsOrStay();
        [StrategyDesctiption(@"Wykonaj ruch przy użyciu przeszukiwania alfa-beta. 
W opcjach ustawiamy głębokość drzewa mini-max, maksymalna głębokość drzewa pojedyńczego skoku oraz 
maksymlną odległość pomiędzy piłką i pustymi polami, na których algorytm będzie dokładał piłkarzy")]
        IMoveFindingStartegy AlphaBeta();
        [StrategyDesctiption(@"Wykonaj ruch przy użyciu przeszukiwania alfa-beta. 
W opcjach ustawiamy głębokość drzewa mini-max, maksymalna głębokość drzewa pojedyńczego skoku.
Piłkarze będą stawiani na polach sąsiadująych z możliwymi pozyacjami piłki w kierunku bramki.")]
        IMoveFindingStartegy SmartAlphaBeta();
        [StrategyDesctiption("Wykonaj skok przy użyciu przeszukiwania bfs z limitem na liczbę odwiedzonych wierzchołków")]
        IMoveFindingStartegy BfsNodesBounded();
        [StrategyDesctiption(@"Wykonaj skok przeglądając najpierw najbardziej korzystne wierzhołki.
W opcjach ustawiamy limit na liczbę odwiedzonych wierzchołków.")]
        IMoveFindingStartegy OrderByNodesValues();

        [StrategyDesctiption(@"Wykonaj ruch przy użyciu przeszukiwania alfa-beta lub nie rób nic jeśli nie można poprawić pozycji. 
W opcjach ustawiamy głębokość drzewa mini-max, maksymalna głębokość drzewa pojedyńczego skoku.
Piłkarze będą stawiani na polach sąsiadująych z możliwymi pozyacjami piłki w kierunku bramki.")]
        IMoveFindingStartegy SmartAlphaBetaJumpOrStay();
    }
}