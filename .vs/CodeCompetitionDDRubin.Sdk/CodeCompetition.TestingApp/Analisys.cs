using CodeStrikes.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeStrikes.TestingApp
{
    class Analisys
    {
        private static readonly List<string> stringCollectionOfEnemyMoves = new List<string>();

        private readonly List<string> sortedDataForAnalysis = new List<string>();
        private readonly List<string> enemySix = new List<string>();
        private readonly List<string> enemyTwentyFive = new List<string>();

        string type = "";

        //first analysis for data
        public string AnalysisEntry(List<string> dataForAnalysis, int roundCounter)
        {
            //analysis if round is every 6 (med anal)
            if ((roundCounter-1) >= 6 && ((roundCounter-1) % 6) == 0)
            {

                for (int i = roundCounter - 6; i < roundCounter; i++)
                {
                    sortedDataForAnalysis.Add(dataForAnalysis[i]);
                }

                PlayerBot.special = 0;

                type = AnalysisMethod(sortedDataForAnalysis, 6);
                //clear data for new anal
                if ((roundCounter % 25) != 0)
                {
                    sortedDataForAnalysis.Clear();
                }

                //if chosed tactic is elastic
                if (type == "elastic")
                {
                    return AnalysisEntry(dataForAnalysis, (roundCounter - 1));
                }

                return type;
            }

            //analysis if round is every 25 (long anal)
            if ((roundCounter-1) >= 25 && ((roundCounter-1) % 25) == 0)
            {
                for (int i = roundCounter - 25; i < roundCounter; i++)
                {
                    sortedDataForAnalysis.Add(dataForAnalysis[i]);
                }
                //make static variable equal 0 for clear start
                PlayerBot.special = 0;

                type = AnalysisMethod(sortedDataForAnalysis, 25);
                //clear data for new anal
                sortedDataForAnalysis.Clear();

                //if chosed tactic is elastic
                if (type == "elastic")
                {
                    return AnalysisEntry(dataForAnalysis, (roundCounter-1));
                }

                return type;
            }

            //analysis if round is every 2
            if (roundCounter >= 2)
            {
                //short analis 2------ every round
                sortedDataForAnalysis.Add(dataForAnalysis[roundCounter-2]);
                sortedDataForAnalysis.Add(dataForAnalysis[roundCounter-1]);
                type = AnalysisMethod(sortedDataForAnalysis, 2);

                //clear data for new anal
                if ((roundCounter % 6) != 0 ^ (roundCounter % 25) != 0)
                {
                    sortedDataForAnalysis.Clear();
                }

                return type;
            }

            return type;
        } 
        //anal information and call decision or general to decide
        private string AnalysisMethod(List<string> dataForAnalisys, int count)
        {
            int HK = 0, HP = 0, UP = 0, LK = 0;
            int DHK = 0, DHP = 0, DUP = 0, DLK = 0;
            int roundCounter = 3;
            string enemyMoves;

            //collect enemy data
            string data = EnemyMovesString(dataForAnalisys);
            stringCollectionOfEnemyMoves.Add(data);

            //anal collected data with numbers
            if (count == 2)
            {
                //decipher
                for (int i = 0; i < count; i++)
                {
                    enemyMoves = DecodedEnemyMoves(dataForAnalisys[i]);
                    for (int k = 0; k < enemyMoves.Length; k++)
                    {
                        //find attack
                        if (enemyMoves[k] < '5')
                        {
                            switch (enemyMoves[k])
                            {
                                case '1':
                                    HK++;

                                    break;
                                case '2':
                                    HP++;

                                    break;
                                case '3':
                                    UP++;

                                    break;
                                case '4':
                                    LK++;

                                    break;
                            }
                        }
                        //find defence
                        if (enemyMoves[k] > '4')
                        {
                            switch (enemyMoves[k])
                            {
                                case '5':
                                    DHK++;

                                    break;
                                case '6':
                                    DHP++;

                                    break;
                                case '7':
                                    DUP++;

                                    break;
                                case '8':
                                    DLK++;

                                    break;
                            }
                        }
                    }
                }
            }            

            //decision for 6 moves anal
            if (count == 6)
            {
                return EnemyData(dataForAnalisys, count, roundCounter);
                
            }

            //decision for 25 moves anal
            if (count == 25)
            {
                return EnemyData(dataForAnalisys, count, roundCounter);
            }
            
            roundCounter++;

            //decision for 2 moves anal
            return DataToAnalysis(HK, HP, UP, LK, DHK, DHP, DUP, DLK);
        }

        //decision for elastic defence or steady attack
        private string DataToAnalysis(int HK, int HP, int UP, int LK, int DHK, int DHP, int DUP, int DLK)
        {
            DecisionComputer decision = new DecisionComputer();

            int sumDefence = DHK + DHP + DUP + DLK;
            int sumAttack = HK + HP + UP + LK;

            if (HK == 0 && HP == 0 && UP == 0 && LK == 0 ^ (sumDefence == 0 && sumAttack == 0))
            {
                //analisys to steady attack decision
                type = decision.AttackOnlyDecision(DHK, DHP, DUP, DLK);

                return type;
            }
            else
            {
                //analisys to complicated decision
                type = decision.CombineDecision(HK, HP, UP, LK, DHK, DHP, DUP, DLK);

                return type;
            }
        }

        //anal for generals
        public string EnemyData(List<string> dataForAnalisys, int counter, int roundCount)
        {
            General general = new General();

            //add data to collection
            for (int i = 0; i < counter; i++)
            {
               stringCollectionOfEnemyMoves.Add(EnemyMovesString(dataForAnalisys));
            }

            //call general for med decision
            if(counter  == 6) { 

                return general.DecisionForTactics(stringCollectionOfEnemyMoves, roundCount); 
            }

            //call general for making strategy
            else
            {
                return general.DecisionForStrategy(stringCollectionOfEnemyMoves, roundCount);
            }
        }

        //Change data type for easer anal
        private string EnemyMovesString(List<string> dataForAnalisys)
        {
            string data = "";
            string enemyMoves;

            enemyMoves = DecodedEnemyMoves(dataForAnalisys.Last());

            //decode raw data
            for (int k = 0; k < enemyMoves.Length; k++)
            {
                //decode enemy attacks
                if (enemyMoves[k] < '5')
                {
                    switch (enemyMoves[k])
                    {
                        case '1':
                            data += "5";

                            break;
                        case '2':
                            data += "6";

                            break;
                        case '3':
                            data += "7";

                            break;
                        case '4':
                            data += "8";

                            break;
                    }
                }

                //decode enemy defence
                if (enemyMoves[k] > '4')
                {
                    switch (enemyMoves[k])
                    {
                        case '5':
                            data += "1";

                            break;
                        case '6':
                            data += "2";

                            break;
                        case '7':
                            data += "3";

                            break;
                        case '8':
                            data += "4";

                            break;
                    }
                }
            }

            return data;
        }

        //decode the rawest data to string
        private string DecodedEnemyMoves(string line)
        {
            string enemyMoves = "";

            //start decoding
            //codes atk: 10 -> 14 for def: 11 -> 15
            for (int i = 0; i < line.Length; i++)
            {

                //decode atacks
                if (line[i] == 'A')
                {
                    switch (line[i + 10])
                       {
                        case 'H':
                            if (line[i + 14] == 'K')
                            {
                                enemyMoves += "1";
                            }
                            else if (line[i + 14] == 'P')
                            {
                                enemyMoves += "2";
                            }

                            break;
                        case 'L':
                            enemyMoves += "4";

                            break;
                        case 'U':
                            enemyMoves += "3";

                            break;
                    } 
                }

                //decode defence
                if (line[i] == 'D')
                {
                    switch (line[i + 11])
                    {
                        case 'H':
                            if (line[i + 15] == 'K')
                            {
                                enemyMoves += "5";
                            }

                            else if (line[i + 15] == 'P')
                            {
                                enemyMoves += "6";
                            }

                            break;
                        case 'L':
                            enemyMoves += "8";

                            break;
                        case 'U':
                            enemyMoves += "7";

                            break;
                    }
                }
            }

            return enemyMoves;
        }

        //data for general
        public List<string> MyMovesList(int counter)
        {
            if (counter == 6)
            {

                return EnemySix();
            }
            else
            {
                return EnemyTwentyFive();
            }

        }

        //handler to send data for 6 lenght decision
        private List<string> EnemySix()
        {
            for (int i = 6; i > 0; i--)
            {
                enemySix.Add(stringCollectionOfEnemyMoves[stringCollectionOfEnemyMoves.Count - i]);
            }

            return enemySix;
        }

        //handler to send data for 25 lenght decision
        private List<string> EnemyTwentyFive()
        {
            for (int i = 25; i > 0; i--)
            {
                enemyTwentyFive.Add(stringCollectionOfEnemyMoves[stringCollectionOfEnemyMoves.Count - i]);
            }

            return enemyTwentyFive;
        }




    } 
}
