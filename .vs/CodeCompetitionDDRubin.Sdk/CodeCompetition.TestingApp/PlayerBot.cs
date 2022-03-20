using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeStrikes.Sdk;

// area - energy for atack
namespace CodeStrikes.TestingApp
{
    class PlayerBot : BotBase
    {

        private readonly DataCollector dataCollector = new DataCollector();
        private readonly Patterns paterns = new Patterns();
        private int roundCounter = 0;
        public static int special = 0;
        int enemyHpInPrevios = 0;
        public override MoveCollection NextMove(RoundContext context)
        {
            var enemyAttacks = context.LastOpponentMoves;
            int enemyHp = context.OpponentLifePoints;
            string dataToResponse = dataCollector.CollectDataToList(enemyAttacks);

            roundCounter++;

            if(dataToResponse == "specialTrick" ^ special == 1 ^ special == 2)
            {
                //if tactic not works
                if ((enemyHpInPrevios - enemyHp) < 10 && enemyHpInPrevios != 0 && special == 1) { 
                    special = 0;
                    return paterns.ElasticDefence(context, dataToResponse);
                }
                //special atack called
                if (special == 1 ^ special == 3)
                {
                    enemyHpInPrevios = context.OpponentLifePoints;
                    special = 2;
                    return paterns.SpecialAttack(context);
                }
                //special trick called
                enemyHpInPrevios = context.OpponentLifePoints;
                special++;
                return paterns.SpecialTrick(context);
            }
            //call first round attack
            else if(roundCounter == 1)
            {
                return paterns.FirstDef(context);
            }
            //chose only attack
            else if(dataToResponse.Length == 1 && roundCounter > 2)
            {

                return paterns.SteadyAttack(context, dataToResponse);
            }
            //chose elastic defence if mixed responce on enemy moves
            else if(roundCounter > 2)
            {

                return paterns.ElasticDefence(context, dataToResponse);
            }
            
            //call second round attack
            return paterns.SecondDef(context);
        }



        public override string ToString()
        {
            return "Player";
        }
    }
}