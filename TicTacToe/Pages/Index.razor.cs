namespace TicTacToe.Pages
{
    using Microsoft.JSInterop;

    public partial class Index
    {
        private const string PlayerOneChar = "X";
        private const string PlayerTwoChar = "O";

        private readonly string[] board = { "", "", "", "", "", "", "", "", "" };
        private string player = PlayerOneChar;
        private readonly int[][] winningCombos =
        {
            new int[3] {0,1,2},
            new int[3] {3,4,5},
            new int[3] {6,7,8},
            new int[3] {0,3,6},
            new int[3] {1,4,7},
            new int[3] {2,5,8},
            new int[3] {0,4,8},
            new int[3] {2,4,6}
        };

        private async Task SquareClicked(int i)
        {
            if (this.board[i] == String.Empty)
            {
                this.board[i] = this.player;
                this.player = this.player == PlayerOneChar ? PlayerTwoChar : PlayerOneChar;
            }

            foreach (int[] combo in this.winningCombos)
            {
                int p1 = combo[0];
                int p2 = combo[1];
                int p3 = combo[2];

                if (this.board[p1] == String.Empty || this.board[p2] == String.Empty || this.board[p3] == String.Empty) continue;

                if (this.board[p1] == this.board[p2] && this.board[p2] == this.board[p3] && this.board[p1] == this.board[p3])
                {
                    string winner = this.player == PlayerOneChar ? "Player TWO" : "Player ONE";
                    await JS.InvokeVoidAsync("ShowSwal", winner);
                    this.ResetGame();
                }
            }

            if (this.board.All(x => x != String.Empty))
            {
                await JS.InvokeVoidAsync("ShowTie");
                this.ResetGame();
            }
        }

        private void ResetGame()
        {
            for (int i = 0; i < this.board.Length; i++)
            {
                this.board[i] = String.Empty;
            }
        }
    }
}