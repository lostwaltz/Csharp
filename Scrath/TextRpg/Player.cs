using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.Component;
using TextRpg.Scene;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TextRpg
{
    struct PlayerStatusData
    {
        public StringBuilder titleName;
        public StringBuilder name;
        public StringBuilder playerClass;

        public int lv;
        public int attackDamage;
        public int defenseDamage;
        public int helthPoint;
        public int gold;

        public int attackResultStat;
        public int defenseResultStat;
    }
    internal class Player : IPlayerInterface
    {
        private PlayerStatusData playerStatusData;
        private IInvenInterface playerInvenInterface;

        public Player(IInvenInterface invenInterface)
        {
            playerInvenInterface = invenInterface;

            playerStatusData.titleName = new StringBuilder("신병");
            playerStatusData.name = new StringBuilder("르탄");
            playerStatusData.playerClass = new StringBuilder("전사");

            playerStatusData.lv = 1;
            playerStatusData.attackDamage = 10;
            playerStatusData.defenseDamage = 5;
            playerStatusData.helthPoint = 100;
            playerStatusData.gold = 1500;

            playerStatusData.attackResultStat = playerStatusData.attackDamage;
            playerStatusData.defenseResultStat = playerStatusData.defenseDamage;

            //------------------------------------------

            playerInvenInterface.PushFuntion((ITEM_TYPE itemType, int stat) =>
            {
                switch (itemType)
                {
                    case ITEM_TYPE.ITEM_SWORD:
                        playerStatusData.attackResultStat = playerStatusData.attackDamage + playerInvenInterface.GetEquipSlotStat(itemType);
                        break;
                    case ITEM_TYPE.ITEM_ARMOR:
                        playerStatusData.defenseResultStat = playerStatusData.defenseDamage + playerInvenInterface.GetEquipSlotStat(itemType); ;
                        break;
                }
            });
        }

        #region <PlayerInterface>
        public StringBuilder GetPlaterStatusText()
        {
            StringBuilder playerStatusText = new StringBuilder();

            playerStatusText.Append("상태 보기\n");
            playerStatusText.Append("캐릭터의 정보가 표시됩니다.\n\n");

            playerStatusText.Append("Lv. ");
            playerStatusText.AppendFormat("{0:D2} \n", playerStatusData.lv);
            playerStatusText.Append(playerStatusData.titleName);
            playerStatusText.Append(' ');
            playerStatusText.Append(playerStatusData.name);
            playerStatusText.Append(" ( ");
            playerStatusText.Append(playerStatusData.playerClass);
            playerStatusText.Append(" )\n");

            playerStatusText.Append("공격력 : " + playerStatusData.attackResultStat);
            if (playerStatusData.attackResultStat != playerStatusData.attackDamage)
                playerStatusText.AppendFormat(" (+{0})", (playerStatusData.attackResultStat - playerStatusData.attackDamage));


            playerStatusText.Append("\n방어력 : " + playerStatusData.defenseResultStat);
            if (playerStatusData.defenseResultStat != playerStatusData.defenseDamage)
                playerStatusText.AppendFormat(" (+{0})", (playerStatusData.defenseResultStat - playerStatusData.defenseDamage));

            playerStatusText.Append("\n체 력 : " + playerStatusData.helthPoint);
            playerStatusText.Append("\nGold : " + playerStatusData.gold + " G\n");

            return playerStatusText;
        }

        public StringBuilder GetPlayerItemListText(bool numberVisuable, bool priceVisualbe)
        {
            return   playerInvenInterface.GetInvenItemListText(numberVisuable, priceVisualbe);
        }

        public void SetEquipItemTogle(int itemIndex)
        {
            playerInvenInterface.SetEquipItemTogle((int)itemIndex);
        }

        public int GetPlayerGold()
        {
            return playerStatusData.gold;
        }

        public void PlayerShopEventCallbackFuntion(Item item, SHOP_SELET_NUM shopSelectNum)
        {
            switch (shopSelectNum)
            {
                case SHOP_SELET_NUM.SHOP_BUY:
                    if (true == playerInvenInterface.FindItemtoItem(item))
                        Console.Write("이미 구매한 아이템입니다.");
                    else if(playerStatusData.gold >= item.GetItemPrice())
                        Console.Write("구매를 완료했습니다.");
                    else
                        Console.Write("Gold 가 부족합니다.");

                    Thread.Sleep(1000);
                    break;
                case SHOP_SELET_NUM.SHOP_SELL:
                    break;
            }
        }

        #endregion
    }

    internal interface IPlayerInterface
    {
        StringBuilder GetPlaterStatusText();

        StringBuilder GetPlayerItemListText(bool numberVisuable, bool priceVisualbe);

        void SetEquipItemTogle(int itemIndex);

        int GetPlayerGold();

        void PlayerShopEventCallbackFuntion(Item item, SHOP_SELET_NUM shopSelectNum);
    }
}


