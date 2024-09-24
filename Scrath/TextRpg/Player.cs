using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.Component;
using TextRpg.Scene;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.IO;
using System.Text.Json;

namespace TextRpg
{
    struct PlayerStatusData
    {
        public StringBuilder titleName;
        public StringBuilder name;
        public StringBuilder playerClass;

        public int lv;
        public float attackDamage;
        public float defenseDamage;
        public int MaxhelthPoint;
        public int helthPoint;
        public int gold;

        public float attackResultStat;
        public float defenseResultStat;
    }
    internal class Player : IPlayerInterface
    {
        //string path = @"C:\WorkSpace\C#\Csharp\Scrath\TextRpg\SAVE.txt";
        string path = @"../../../playerSave.txt";
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
            playerStatusData.MaxhelthPoint = 100;
            playerStatusData.helthPoint = playerStatusData.MaxhelthPoint;
            playerStatusData.gold = 1500;

            playerStatusData.attackResultStat = playerStatusData.attackDamage;
            playerStatusData.defenseResultStat = playerStatusData.defenseDamage;

            //------------------------------------------

            // SetEquipCallback Lamda
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

            if (!File.Exists(path))
                Console.WriteLine("저장 파일이 존재하지 않습니다.");
            else
            {
                string saveString = File.ReadAllText(path);

                string[] saveDataArray = saveString.Split('_');

                //File.WriteAllText(path, "_" + playerStatusData.titleName.ToString() +
                //        "_" + playerStatusData.name +
                //        "_" + playerStatusData.playerClass +
                //        "_" + playerStatusData.lv +
                //        "_" + playerStatusData.attackDamage +
                //        "_" + playerStatusData.defenseDamage +
                //        "_" + playerStatusData.MaxhelthPoint +
                //        "_" + playerStatusData.helthPoint +
                //        "_" + playerStatusData.gold +
                //        "_" + playerStatusData.attackResultStat +
                //        "_" + playerStatusData.defenseResultStat);


                //for (int i = 0; i < itemNameArray.Length; i++)
                //{
                //    File.AppendAllText(path, "_" + itemNameArray[i]);
                //    File.AppendAllText(path, "_" + playerInvenInterface?.GetItemtoIndex(i)?.GetItemisEquip());
                //}

                playerStatusData.titleName = new StringBuilder(saveDataArray[0]);
                playerStatusData.name = new StringBuilder(saveDataArray[1]);
                playerStatusData.playerClass = new StringBuilder(saveDataArray[2]);
                playerStatusData.lv = int.Parse(saveDataArray[3]);
                playerStatusData.attackDamage = float.Parse(saveDataArray[4]);
                playerStatusData.defenseDamage = float.Parse(saveDataArray[5]);
                playerStatusData.MaxhelthPoint = int.Parse(saveDataArray[6]);
                playerStatusData.helthPoint = int.Parse(saveDataArray[7]);
                playerStatusData.gold = int.Parse(saveDataArray[8]);
                playerStatusData.attackResultStat = float.Parse(saveDataArray[9]);
                playerStatusData.defenseResultStat = float.Parse(saveDataArray[10]);

                for(int i = 11; i < saveDataArray.Length; i += 2)
                {
                    Item item = new Item(saveDataArray[i]);
                    playerInvenInterface.PushItemInven(item);

                    if (true == bool.Parse(saveDataArray[i + 1]))
                        playerInvenInterface.SetEquipItemTogle(item);
                }
            }
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

        public StringBuilder GetPlayerItemListText(bool numberVisuable, bool priceVisualbe, bool sellPriceVisablee)
        {
            return playerInvenInterface.GetInvenItemListText(numberVisuable, priceVisualbe, sellPriceVisablee);
        }

        public void SetEquipItemTogle(int itemIndex)
        {
            playerInvenInterface.SetEquipItemTogle((int)itemIndex);
        }
        public void SetEquipItemTogle(Item item)
        {
            playerInvenInterface.SetEquipItemTogle(item);
        }

        public int GetPlayerGold()
        {
            return playerStatusData.gold;
        }
        public void AddPlayerGold(int gold)
        {
            playerStatusData.gold += gold;
        }
        public void AddPlayerHelth(int helth)
        {
            playerStatusData.helthPoint = Math.Min(playerStatusData.MaxhelthPoint, playerStatusData.helthPoint + helth);
        }

        public void PlayerShopEventCallbackFuntion(Item item, SHOP_SELET_NUM shopSelectNum)
        {
            switch (shopSelectNum)
            {
                case SHOP_SELET_NUM.SHOP_BUY:
                    if (true == playerInvenInterface.FindItemtoItem(item))
                        Console.Write("이미 구매한 아이템입니다.");
                    else if (playerStatusData.gold >= item.GetItemPrice())
                    {
                        if (false == playerInvenInterface.PushItemInven(item))
                        {
                            Console.Write("가방 공간이 부족합니다.");
                            Thread.Sleep(1000);
                            return;
                        }
                        Console.Write("구매를 완료했습니다.");
                        playerStatusData.gold -= item.GetItemPrice();
                    }
                    else
                        Console.Write("Gold 가 부족합니다.");

                    Thread.Sleep(1000);
                    break;
                case SHOP_SELET_NUM.SHOP_SELL:
                    playerStatusData.gold += item.GetItemSellPrice();

                    if (true == item.GetItemisEquip())
                        playerInvenInterface.SetEquipItemTogle(item);

                    playerInvenInterface.RemoveItemtoItem(item);
                    break;
            }
        }

        public Item? GetPlayerItemtoIndex(int index)
        {
            return playerInvenInterface.GetItemtoIndex(index);
        }

        public bool FindItemtoItem(Item? item)
        {
            return playerInvenInterface.FindItemtoItem(item);
        }

        public int GetPlayerItemListCount()
        {
            return playerInvenInterface.GetItemListCount();
        }

        public void AddPlayerLevel(int addLv)

        {
            playerStatusData.lv += addLv;

            playerStatusData.attackDamage += (addLv * 0.5f);
            playerStatusData.defenseDamage += (addLv * 1f);

            playerStatusData.attackResultStat += (addLv * 0.5f);
            playerStatusData.defenseResultStat += (addLv * 1f);
        }

        public PlayerStatusData GetPlayerStatusData()
        {
            return playerStatusData;
        }

        public void SavePlayerData()
        {
            string[] itemNameArray = new string[playerInvenInterface.GetItemListCount()];
            for(int i = 0; i < playerInvenInterface?.GetItemListCount(); i++)
            {
                string? itemName = playerInvenInterface?.GetItemtoIndex(i)?.GetItemName().ToString();

                if(null != itemName)
                    itemNameArray[i] = itemName;
            }


            File.WriteAllText(path, playerStatusData.titleName.ToString() +
                                    "_" + playerStatusData.name +
                                    "_" + playerStatusData.playerClass +
                                    "_" + playerStatusData.lv +
                                    "_" + playerStatusData.attackDamage +
                                    "_" + playerStatusData.defenseDamage +
                                    "_" + playerStatusData.MaxhelthPoint +
                                    "_" + playerStatusData.helthPoint +
                                    "_" + playerStatusData.gold +
                                    "_" + playerStatusData.attackResultStat +
                                    "_" + playerStatusData.defenseResultStat);


            for(int i = 0; i < itemNameArray.Length; i++)
            {
                File.AppendAllText(path, "_" + itemNameArray[i]);
                File.AppendAllText(path, "_" + playerInvenInterface?.GetItemtoIndex(i)?.GetItemisEquip());
            }

        }


        #endregion
    }

    internal interface IPlayerInterface
    {
        StringBuilder GetPlaterStatusText();

        StringBuilder GetPlayerItemListText(bool numberVisuable, bool priceVisualbe, bool sellPriceVisablee);

        Item? GetPlayerItemtoIndex(int index);

        void SetEquipItemTogle(int itemIndex);
        void SetEquipItemTogle(Item item);

        int GetPlayerGold();
        void AddPlayerGold(int gold);
        void AddPlayerHelth(int helth);

        PlayerStatusData GetPlayerStatusData();

        void PlayerShopEventCallbackFuntion(Item item, SHOP_SELET_NUM shopSelectNum);

        bool FindItemtoItem(Item? item);

        int GetPlayerItemListCount();

        void AddPlayerLevel(int addLv);

        void SavePlayerData();
    }
}


