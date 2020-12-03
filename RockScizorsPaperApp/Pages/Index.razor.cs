using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace RockScizorsPaperApp.Pages
{
    public partial class Index : IDisposable
    {
        Timer timer;
        List<PlayGame> playsGame = new List<PlayGame>()
        {
            new PlayGame { Image = "rock.jpg", GameOption = GameOption.Rock, BeatsTo = GameOption.Scizors, LoseTo = GameOption.Paper },
            new PlayGame { Image = "paper.jpg", GameOption = GameOption.Paper, BeatsTo = GameOption.Rock, LoseTo = GameOption.Scizors },
            new PlayGame { Image = "scizors.jpg", GameOption = GameOption.Scizors, BeatsTo = GameOption.Paper, LoseTo = GameOption.Rock }
        };

        PlayGame opponentsPlay;
        public string messageResult { get; set; }
        public string messageResultColor { get; set; }
        protected override void OnInitialized()
        {
            opponentsPlay = playsGame[0];
            timer = new Timer();
            timer.Interval = 100;
            timer.Elapsed += TimerOnElapsed;
            timer.Start();
        }

        private void Reset()
        {
            timer.Start();
            messageResult = string.Empty;
            messageResultColor = string.Empty;
        }

        int index = 0;
        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            index = (index + 1) % playsGame.Count;
            opponentsPlay = playsGame[index];
            StateHasChanged();
        }

        public void Dispose()
        {
            if (timer != null)
            {
                timer.Dispose();
            }
        }

        private void SelectPlay(PlayGame play)
        {
            timer.Stop();
            var result = play.PlayAgainst(opponentsPlay);

            switch (result)
            {
                case GameResult.Victory:
                    messageResult = "Victory!!";
                    messageResultColor = "green";
                    break;
                case GameResult.Lose:
                    messageResult = "You Lose :(";
                    messageResultColor = "red";
                    break;
                case GameResult.Draw:
                    messageResult = "Draw";
                    messageResultColor = "yellow";
                    break;
            }
        }
    }

    class PlayGame
    { 
        public GameOption GameOption { get; set; }
        public GameOption BeatsTo { get; set; }
        public GameOption LoseTo { get; set; }
        public string Image { get; set; }

        public GameResult PlayAgainst(PlayGame playGame)
        {
            if (GameOption == playGame.GameOption)
                return GameResult.Draw;
            else
            {
                if (BeatsTo == playGame.GameOption)
                    return GameResult.Victory;
                else
                    return GameResult.Lose;
            }
        }
    }

    enum GameOption
    { 
        Rock, Paper, Scizors
    }

    enum GameResult
    { 
        Victory, Lose, Draw
    }
}
