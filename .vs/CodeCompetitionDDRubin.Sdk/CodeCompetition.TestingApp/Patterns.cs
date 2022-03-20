using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeStrikes.Sdk;

namespace CodeStrikes.TestingApp
{
    class Patterns
    {
        /// for first round defence ABC (firstDef)
        /// second round defence ABD (secDef)  --- A = HK, B = HP, C = UP, D = LK
        /// A = 5, B = 6, C = 7, D = 8
        /// 3 round analiz and dec for atc or def

        //first round moves
        public MoveCollection FirstDef(RoundContext context)
        {
            context.MyMoves
                 .AddDefence(Area.UppercutPunch)
                 .AddDefence(Area.HookKick)
                 .AddDefence(Area.HookPunch);

            return context.MyMoves;
        }

        //second round moves
        public MoveCollection SecondDef(RoundContext context)
        {
            context.MyMoves
                 .AddDefence(Area.LowKick)
                 .AddDefence(Area.HookKick)
                 .AddDefence(Area.HookPunch);

            return context.MyMoves;
        }

        //special trick
        public MoveCollection SpecialTrick(RoundContext context)
        {
            context.MyMoves
                 .AddDefence(Area.UppercutPunch)
                 .AddDefence(Area.HookKick)
                 .AddDefence(Area.HookPunch);

            return context.MyMoves;
        }

        //special attack for trick enemy (void attack for beat cheater)
        public MoveCollection SpecialAttack(RoundContext context)
        {
            context.MyMoves
                 .AddAttack(Area.HookKick)
                 .AddAttack(Area.HookKick)
                 .AddDefence(Area.LowKick);
                 

            return context.MyMoves;
        }

        //full attack
        public MoveCollection SteadyAttack(RoundContext context, string dataToAttack)
        {
            switch (dataToAttack)
            {
                case "5":
                    context.MyMoves
                                 .AddAttack(Area.HookKick)
                                 .AddAttack(Area.HookKick)
                                 .AddAttack(Area.HookKick);
                    return context.MyMoves;

                case "6":
                    context.MyMoves
                                 .AddAttack(Area.HookPunch)
                                 .AddAttack(Area.HookPunch)
                                 .AddAttack(Area.HookPunch)
                                 .AddAttack(Area.HookPunch);
                    return context.MyMoves;

                case "7":
                    context.MyMoves
                                 .AddAttack(Area.UppercutPunch)
                                 .AddAttack(Area.UppercutPunch)
                                 .AddAttack(Area.UppercutPunch)
                                 .AddAttack(Area.UppercutPunch)
                                 .AddAttack(Area.UppercutPunch)
                                 .AddAttack(Area.UppercutPunch);
                    return context.MyMoves;

                case "8":
                    context.MyMoves
                                 .AddAttack(Area.LowKick)
                                 .AddAttack(Area.LowKick)
                                 .AddAttack(Area.LowKick)
                                 .AddAttack(Area.LowKick)
                                 .AddAttack(Area.LowKick)
                                 .AddAttack(Area.LowKick)
                                 .AddAttack(Area.LowKick)
                                 .AddAttack(Area.LowKick)
                                 .AddAttack(Area.LowKick)
                                 .AddAttack(Area.LowKick)
                                 .AddAttack(Area.LowKick)
                                 .AddAttack(Area.LowKick);
                    return context.MyMoves;

            }

            return context.MyMoves;
        }

        //combine attack
        public MoveCollection ElasticDefence(RoundContext context, string dataToDefend)
        {
            int counter = 0;

            for (int i = 0; i < dataToDefend.Length; i++)
            {
                //chose place to defend and attack (1-4 = defend, 5-8 = attack)
                switch (dataToDefend[i])
                {
                    case '1':
                        counter++;
                        
                        context.MyMoves.AddDefence(Area.HookKick);


                        break;

                    case '2':
                        counter++;
                        context.MyMoves.AddDefence(Area.HookPunch);


                        break;

                    case '3':
                        counter++;
                        context.MyMoves.AddDefence(Area.UppercutPunch);


                        break;

                    case '4':
                        counter++;
                        context.MyMoves.AddDefence(Area.LowKick);


                        break;

                    case '5':
                        for (int k = 4; k < 12 - (counter * 4); k += 4) {
                            context.MyMoves.AddAttack(Area.HookKick);
                            k += 4;
                        }
                        
                        return context.MyMoves;

                    case '6':
                        for (int k = 3; k < (12 - (counter * 4)); )
                        {
                            context.MyMoves.AddAttack(Area.HookPunch);
                            k += 3;
                        }

                        return context.MyMoves;

                    case '7':
                        for (int k = 2; k < 12 - (counter * 4); k += 2)
                        {
                            context.MyMoves.AddAttack(Area.UppercutPunch);
                            k += 2;
                        }

                        return context.MyMoves;

                    case '8':
                        for (int k = 1; k < 12 - (counter * 4); k += 1)
                        {
                            context.MyMoves.AddAttack(Area.LowKick);
                            k += 1;
                        }

                        return context.MyMoves;
                }
            }

            return context.MyMoves;
        }
    }
}
