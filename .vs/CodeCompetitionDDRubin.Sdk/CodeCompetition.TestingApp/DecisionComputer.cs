using CodeStrikes.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeStrikes.TestingApp
{
    class DecisionComputer
    {
        string type = "";
        int signal = 0;
        int correctnes = 0;

        //if enemy very low level. Just attack
        public string AttackOnlyDecision(int DHK, int DHP, int DUP, int DLK)
        {
            type = AttackLow(DHK, DHP, DUP, DLK);

            return type;
        }

        //more complicated decision
        public string CombineDecision(int HK, int HP, int UP, int LK, int DHK, int DHP, int DUP, int DLK)
        {
            int attack = AttackCounter(HK, HP, UP, LK);
            int defend = AttackCounter(DHK, DHP, DUP, DLK);

            // complicated decision (trick for cheater)
            if (attack > 2 && defend > 2) 
            {

                return LowCombineDecisionAttack(HK, HP, UP, LK, DHK, DHP, DUP, DLK); ;
            }

            // def + 1 attack if needed
            else if (attack > 3 && defend < 3) 
            {

                return LowCombineDecisionDefence(HK, HP, UP, LK, DHK, DHP, DUP, DLK); ;
            }

            //  attack + 1 def
            else if (attack < 3 && defend > 3) 
            {

                return LowCombineDecisionAttack(HK, HP, UP, LK, DHK, DHP, DUP, DLK);
            }
            return type;
        }

        //count enemy attacks
        private int AttackCounter(int HK, int HP, int UP, int LK)
        {
            int count = HK + HP + UP+ LK;
            return count;
        }

        //the easeast attack decision
        private string AttackLow(int DHK, int DHP, int DUP, int DLK)
        {
            //default type to attack
            string type = "5";

            if (DHK == 0)
            {
                //attack DHK (default)
                return type; 
            }
            else if (DHP == 0)
            {
                //attack DHP
                type = "6";

                return type;              
            }
            else if (DUP == 0)
            {
                //attack DUP
                type = "7";

                return type;
            }
            else if (DLK == 0)
            {
                //attack DLK
                type = "8";

                return type;
            }

            return type;
        }

        //find attack and choose place to attack in next move
        public string LowCombineDecisionAttack(int HK, int HP, int UP, int LK, int DHK, int DHP, int DUP, int DLK)
        {
            //find what to attack
            string attackType = AttackLow(DHK, DHP, DUP, DLK);
            int defenceType;
            
            //find what to defend
            if(HK != 0)
            {
                defenceType = 1;
                //call to defend HK
            }
            else if(HP != 0)
            {
                defenceType = 2;
                //call to defend HP
            }
            else if(UP != 0)
            {
                defenceType = 3;
                //call to defend UP
            }
            else if(LK > 0 && attackType != "4")
            {
                defenceType = 0; //not defend just attack
            }
            else
            {
                defenceType = 4;
                //defend LK
            }
            //call method for attack and defence
            type = defenceType + "" + attackType;

            return type;
        }

        //decision to defence
        public string  LowCombineDecisionDefence(int HK, int HP, int UP, int LK, int DHK, int DHP, int DUP, int DLK)
        {
            //defence from attacks
            if((HK*10) > (HP * 6) && HK > 15)
            {
                //defence from HK
                if ((HK * 10) > (UP * 3))
                {
                    if ((HK * 10) > (LK * 1))
                    {
                        if ((HP * 6) > (UP * 3) && HP > 8)
                        {
                            if ((HP * 6) > (LK * 1))
                            {
                                if ((UP * 3) > (LK * 1) && UP > 5)
                                {
                                    type = "123";

                                    return type;
                                    //defence from UP
                                }
                                else
                                {
                                    type = "12" + AttackLow(DHK, DHP, DUP, DLK);

                                    return type;
                                    //defence + 1 attack
                                }
                                
                            }
                            //defence from HP
                            type = "12" + AttackLow(DHK, DHP, DUP, DLK);

                            return type;
                        }
                        else if ((UP * 3) > (LK * 1) && UP > 5)
                        {

                            type = "13" + AttackLow(DHK, DHP, DUP, DLK);

                            return type;
                            //defence from UP + 1 att
                        }
                        else
                        {
                            type = "1" + AttackLow(DHK, DHP, DUP, DLK);

                            return type;
                            //defence +  2 attack
                        }
                    }
                }
               
            }
            else if ((HP * 6) > (UP * 3) && HP > 8)
            {
                //defence from HP
                if ((HP * 6) > (LK * 1))
                {
                    if ((UP * 3) > (LK * 1) && UP > 5)
                    {
                        type = "23" + AttackLow(DHK, DHP, DUP, DLK);

                        return type;
                        //defence from UP
                        //defence +  1 attack
                    }
                    else
                    {
                        type = "2" + AttackLow(DHK, DHP, DUP, DLK);

                        return type;
                        //defence +  2 attack
                    }//defence from HP
                }
            }
            else if ((UP * 3) > (LK * 1) && UP > 5)
            {
                type = "3" + AttackLow(DHK, DHP, DUP, DLK);

                return type;
                //defence from UP + 2 attacks
            } 
            else
            {
                type = "12" + AttackLow(DHK, DHP, DUP, DLK);

                return type;
                // 3 attacks
            }

            //attack him with elastic tactic
            return LowCombineDecisionAttack(HK, HP, UP, LK, DHK, DHP, DUP, DLK);
            
        }

        //calls by general more complicated decision
        public string AnalyseEnemyResponce(List<string> enemyForAnalisys, List<string> mineForAnalisys)
        {
            //if he uses patterns
            if (ConfirmPattern(enemyForAnalisys) == true)
            {
                return AnalisysEnemyTactic(enemyForAnalisys); ;
            }

            //if enemy making moves like you or answering on them with delay
            if (ConfirmResponce(enemyForAnalisys, mineForAnalisys) == true)
            {
                return "specialTrick";
            }

            return "elastic";
        }
        //analyser
        private string AnalisysEnemyTactic(List<string> enemyForAnalisys)
        {
            int HK = 0, HP = 0, UP = 0, LK = 0;
            int DHK = 0, DHP = 0, DUP = 0, DLK = 0;

            //anal enemy
            for (int i = enemyForAnalisys.Count(); i > (enemyForAnalisys.Count); i--)
            {
                int temp = 0;
                int enemyMoveCounter = enemyForAnalisys.Count();
                string enemyMove = enemyForAnalisys[(enemyMoveCounter - 1) - temp];
                int toIntegerEnemy = (int)Char.GetNumericValue(enemyMove[i]);

                for (int k = 0; k < enemyMove.Length; k++)
                {
                    //cheak for defence
                    if (toIntegerEnemy < 5)
                    {
                        switch (toIntegerEnemy)
                        {
                            case 1:
                                DHK++;
                                break;

                            case 2:
                                DHP++;
                                break;

                            case 3:
                                DUP++;
                                break;

                            case 4:
                                DLK++;
                                break;
                        }
                    }
                    //check for attack
                    if (toIntegerEnemy > 5)
                    {
                        switch (toIntegerEnemy)
                        {
                            case 5:
                                HK++;
                                break;

                            case 6:
                                HP++;
                                break;

                            case 7:
                                UP++;
                                break;

                            case 8:
                                LK++;
                                break;
                        }
                    }
                }
            }

            return LowCombineDecisionDefence(HK, HP, UP, LK, DHK, DHP, DUP, DLK);
        }

        //checkers
        private Boolean ConfirmResponce(List<string> enemyForAnalisys, List<string> mineForAnalisys)
        {
            //fast check
            if (CheckResponce(enemyForAnalisys, mineForAnalisys, 2) == true)
            {
                return true;
            }

            //med check
            if (CheckResponce(enemyForAnalisys, mineForAnalisys, 6) == true)
            {
                return true;
            }

            //long check
            if (mineForAnalisys.Count() >= 20) {
                if (CheckResponce(enemyForAnalisys, mineForAnalisys, 25) == true)
                {
                    return true;
                }
            }
                
            return false;
        }

        private Boolean CheckResponce(List<string> enemyForAnalisys, List<string> mineForAnalisys, int counter)
        {
            for (int i = mineForAnalisys.Count(); i > (mineForAnalisys.Count - counter); i--)
            {
                string myMove = mineForAnalisys[i - 1];
                int temp = 0;
                int enemyMoveCounter = enemyForAnalisys.Count();
                string enemyMove = enemyForAnalisys[(enemyMoveCounter - 1) - temp];
                temp++;

                for (int j = 0; j < myMove.Length; j++)
                {
                    int toIntegerMy = (int)Char.GetNumericValue(myMove[j]);
                    for (int k = 0; k < enemyMove.Length; k++)
                    {
                        int toIntegerEnemy = (int)Char.GetNumericValue(enemyMove[k]);
                        if (toIntegerMy < 5)
                        {
                            switch (toIntegerMy)
                            {
                                case 1:
                                    if (toIntegerEnemy == 5)
                                    {
                                        correctnes++;
                                    }
                                    break;

                                case 2:
                                    if (toIntegerEnemy == 6)
                                    {
                                        correctnes++;
                                    }
                                    break;

                                case 3:
                                    if (toIntegerEnemy == 7)
                                    {
                                        correctnes++;
                                    }
                                    break;

                                case 4:
                                    if (toIntegerEnemy == 8)
                                    {
                                        correctnes++;
                                    }
                                    break;
                            }
                        }
                        if (toIntegerMy > 5)
                        {
                            switch (toIntegerMy)
                            {
                                case 5:
                                    if (toIntegerEnemy == 1)
                                    {
                                        correctnes++;
                                    }
                                    break;

                                case 6:
                                    if (toIntegerEnemy == 2)
                                    {
                                        correctnes++;
                                    }
                                    break;

                                case 7:
                                    if (toIntegerEnemy == 3)
                                    {
                                        correctnes++;
                                    }
                                    break;

                                case 8:
                                    if (toIntegerEnemy == 4)
                                    {
                                        correctnes++;
                                    }
                                    break;
                            }
                        }
                    }
                }

                if (correctnes >= (myMove.Length * 2))
                {
                    return true;
                }
            }

            return false;
        }

        private Boolean ConfirmPattern(List<string> enemyForAnalisys)
        {
            //6 check
            int enemyMoveCounter = enemyForAnalisys.Count();

            for (int i = 1; i < enemyMoveCounter; i++)
            {
                if (enemyForAnalisys[i] == enemyForAnalisys[i - 1])
                {
                    correctnes++;
                }
                else if (AnaliseData(enemyForAnalisys) == true)
                {
                    correctnes++;
                }
            }

            if (correctnes >= (enemyForAnalisys.Count()))
            {
                signal = 1;
            }

            correctnes = 0;
            if (signal == 1)
            {
                return true;
            }

            return false;
        }

        private Boolean AnaliseData(List<string> enemyForAnalisys)
        {
            for (int i = 1; i < enemyForAnalisys.Count(); i ++ )
            {
                int temp = 0;
                int enemyMoveCounter = enemyForAnalisys.Count();
                string enemyMove = enemyForAnalisys[i];

                if (i < (enemyMoveCounter - 1))
                {
                    string enemyNextMove = enemyForAnalisys[i + 1];
                    foreach (char a in enemyNextMove)
                    {
                        if (enemyMove[temp] == a)
                        {
                            correctnes++;
                        }
                    }
                }

                temp++;

                if (correctnes >= (enemyForAnalisys.Count()))
                {
                    return true;
                }

                correctnes = 0;
            }
            
            return false;
        }
    }
}
