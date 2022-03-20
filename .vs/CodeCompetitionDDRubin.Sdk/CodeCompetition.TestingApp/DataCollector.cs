using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeStrikes.Sdk;

namespace CodeStrikes.TestingApp
{
    //class to collect data from ReadonlyMoveCollection
    //and send them to proccesing
    class DataCollector
    {
        private readonly Analisys analysis = new Analisys();
        private readonly List<string> dataForAnalysis = new List<string>();
        private readonly List<ReadonlyMoveCollection> opponentsMoves = new List<ReadonlyMoveCollection>();
        public static List<string> myMoves = new List<string>();
        private readonly List<string> myMoves6 = new List<string>();
        private readonly List<string> myMoves25 = new List<string>();

        int roundNumber = 0;
        string type = "";

        //collect data for decipher
        public string CollectDataToList(ReadonlyMoveCollection context)
        {
            if (context != null) {
                opponentsMoves.Add(context);

                DataToString(opponentsMoves[roundNumber]);
                
                //Console.WriteLine(roundNumber + "number" + opponentsMoves[roundNumber]);
            }
            return type;

        }

        //make data more readable
        private string DataToString(ReadonlyMoveCollection opponentsMoves)
        {
            string attacks = opponentsMoves.ToString();

            dataForAnalysis.Add(attacks);
            type = analysis.AnalysisEntry(dataForAnalysis, roundNumber + 1);
            roundNumber++;
            myMoves.Add(type);

            return type;
        }

        //decision for general to read data
        public List<string> MyMovesList(int counter)
        {
            if (counter == 6)
            {

                return MyMovesSix();
            }
            else
            {
                return MyMovesTwentyFive();
            }
        }

        //handler to send data for 6 lenght decision
        private List<string> MyMovesSix()
        {
            for (int i = 6; i > 0; i--)
            {
                myMoves6.Add(myMoves[myMoves.Count - i]);
            }

            return myMoves6;
        }

        //handler to send data for 25 lenght decision
        private List<string> MyMovesTwentyFive()
        {
            for (int i = 25; i > 0; i--)
            {
                myMoves25.Add(myMoves[myMoves.Count - i]);
            }
         
            return myMoves25;
        }
    }
}
