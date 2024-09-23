using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Scene
{
    internal class SceneDungeon : Scene
    {
        private IPlayerInterface Interface;

        public SceneDungeon(IPlayerInterface playerInterface)
        {
            curEnumScene = SCENE.SCENE_DUNGEON;
            Interface = playerInterface;

            stringBuilder.Append("던전입장\n");
            stringBuilder.Append("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n\n");

            stringBuilder.Append("1. 쉬운 던전     | 방어력 5  이상 권장\n");
            stringBuilder.Append("2. 일반 던전     | 방어력 11 이상 권장\n");
            stringBuilder.Append("3. 어려운 던전   | 방어력 17 이상 권장\n");

            stringBuilder.Append("0. 나가기\n\n원하시는 행동을 입력해주세요.\n>>");
        }

        public override void SceneUpdate()
        {
            Console.Write(stringBuilder);

            int.TryParse(Console.ReadLine(), out selectcNumber);

            if (selectcNumber == 0)
            {
                SceneManager.instance?.SceneChange(SCENE.SCENE_LOBY);
                return;
            }

            int playerAttack = (int)Interface.GetPlayerStatusData().attackResultStat;
            int playerDefense = (int)Interface.GetPlayerStatusData().defenseResultStat;

            bool isClear = false;
            int recommendDefense = 0;
            int clearGold = 0;
            StringBuilder dungeonName = new StringBuilder();

            switch (selectcNumber)
            {
                case 1:
                    dungeonName.Append("쉬운 던전");
                    recommendDefense = 5;
                    clearGold = 1000;
                    break;
                case 2:
                    dungeonName.Append("일반 던전");
                    recommendDefense = 11;
                    clearGold = 1700;
                    break;
                case 3:
                    dungeonName.Append("어려운 던전");
                    recommendDefense = 17;
                    clearGold = 2500;
                    break;
            }

            if (playerDefense < recommendDefense)
            {
                if (4 < new Random().Next(1, 11))
                    isClear = true;
            }
            else
                isClear = true;

            Console.Clear();

            if(true == isClear)
            {
                Console.Write("던전 클리어\n축하합니다!!\n" + dungeonName + "을 클리어 하였습니다.\n\n");
            }
            else
            {
                Console.Write("클리어 실패\n" + dungeonName + "클리어에 실패했습니다.\n\n");
            }

            ResultScreen(isClear, playerAttack, recommendDefense - playerDefense, clearGold);

            Console.Write("0. 나가기\n\n");
            Console.Write("원하시는 행동을 입력해주세요.\n>>");

            int.TryParse(Console.ReadLine(), out selectcNumber);

        }

        private void ResultScreen(bool isClear, int attackStat, int defenseDifference, int clearGold)
        {
            int damage = new Random().Next(20, 35);
            int rand = new Random().Next(attackStat, attackStat * 2);
            clearGold = (int)(clearGold * (1f + rand / 100f));


            if(true == isClear)
            {
                damage += defenseDifference;

                Console.Write("[탐험 결과]\n");
                Console.Write("체력 {0} -> {1}\n", Interface.GetPlayerStatusData().helthPoint, Interface.GetPlayerStatusData().helthPoint - damage);
                Console.Write("Gold {0} G -> {1} G\n\n", Interface.GetPlayerGold(), Interface.GetPlayerGold() + clearGold);

                Interface.AddPlayerHelth(-damage);
                Interface.AddPlayerGold(clearGold);
                Interface.AddPlayerLevel(1);
            }
            else
            {
                Console.Write("[탐험 결과]\n");
                Console.Write("체력 {0} -> {1}\n\n", Interface.GetPlayerStatusData().helthPoint, Interface.GetPlayerStatusData().helthPoint - (damage / 2));
                Interface.AddPlayerHelth(-(damage / 2));
            }

        }
    }
}
