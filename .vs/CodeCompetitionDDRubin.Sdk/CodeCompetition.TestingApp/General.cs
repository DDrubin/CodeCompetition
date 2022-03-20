using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodeStrikes.TestingApp
{
    class General
    {
        //tactics: specials. 
        //find tactics:
        /// find if opponent responce and make tricky attacks +
        /// find if opponent usuing same type and perfome tacic to elastic defence
        /// find if opponent change his moves with patterns for 6 rounds and make decision 
        /// between elastic defence and predictable attack

        //decipher alhorytm and decide what youse from specials or from low alhorytm
        //find strategy:
        ///25 moves to understand how enemy responds 
        ///main combinations
        ///favorite tricks/moves and tactics
        ///choose response

        private readonly DataCollector myMovesFromData = new DataCollector();
        private readonly DecisionComputer decisionMaker = new DecisionComputer();
        private readonly Analisys analysis = new Analisys();
        private readonly List<string> myMoves = new List<string>();
        private readonly List<string> enemyMoves = new List<string>();

        //decide for 6 moves tactic
        private string DecisionSixTurns()
        {
            myMoves.AddRange(myMovesFromData.MyMovesList(6));
            enemyMoves.AddRange(analysis.MyMovesList(6));

            return decisionMaker.AnalyseEnemyResponce(myMoves, enemyMoves);
        }

        //decide for 25 moves strategy
        private string DecisionTwentyFiveTurns()
        {
            myMoves.AddRange(myMovesFromData.MyMovesList(25));
            enemyMoves.AddRange(analysis.MyMovesList(25));

            return decisionMaker.AnalyseEnemyResponce(myMoves, enemyMoves);
        }

        //public acces
        public string DecisionForTactics(List<string> dataForAnalisys, int counter)
        {
            return DecisionSixTurns();
        }

        public string DecisionForStrategy(List<string> dataForAnalisys, int counter)
        {
            return DecisionTwentyFiveTurns();
        }
    }
}
