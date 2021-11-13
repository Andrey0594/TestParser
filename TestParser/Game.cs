using System;

namespace TestParser
{
    public class Game
    {
        public string Datet { get; set; }
        public string Host { get; set; }
        public string Score { get; set; }
        public string Visitor { get; set; }

        public Game(string host, string score, string visitor)
        {
            Host = host;
            Score = score;
            Visitor = visitor;
            Datet = DateTime.Now.ToString("yyyy-MM-dd");
        }


    }
}
