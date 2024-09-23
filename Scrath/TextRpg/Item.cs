using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TextRpg
{
    public enum ITEM_TYPE { ITEM_SWORD, ITEM_ARMOR, ITEM_END }

    public struct ItemData
    {
        public ItemData()
        {
            itemName = new StringBuilder();
            itemDescription = new StringBuilder();
        }

        public ItemData(ItemData itemData)
        {
            itemName = new StringBuilder(itemData.itemName.ToString());
            itemDescription = new StringBuilder(itemData.itemDescription.ToString());

            itemType = itemData.itemType;
            itemStat = itemData.itemStat;
            isEquip = itemData.isEquip;
            itemPrice = itemData.itemPrice;
            itemSellPrice = itemData.itemSellPrice;
        }

        public StringBuilder itemName;
        public StringBuilder itemDescription;
        public ITEM_TYPE itemType;
        public int itemStat;
        public int itemPrice;
        public int itemSellPrice;
        public bool isEquip;
    }

    internal class Item
    {
        public Item(ItemData _itemData)
        {
            itemData = new ItemData(_itemData);
        }
        public Item(string itemName)
        {
            CreateItemPreset(itemName);
        }

        public ItemData itemData;

        public StringBuilder PrintItemInfo(bool eqipVisualbe)
        {
            StringBuilder stringBuilder = new StringBuilder();

            int totalNameLenght = 20;
            int totalDescLenght = 30;

            if (true == itemData.isEquip && true == eqipVisualbe)
            {
                stringBuilder.Append("[E]");
                totalNameLenght -= 3;
            }

            totalNameLenght -= itemData.itemName.Length;
            totalDescLenght -= itemData.itemDescription.Length;

            stringBuilder.AppendFormat("{0, " + -totalNameLenght + "} |", itemData.itemName);

            switch (itemData.itemType)
            {
                case ITEM_TYPE.ITEM_SWORD:
                    stringBuilder.AppendFormat(" {0, -5}", "공격력 + ");
                    break;
                case ITEM_TYPE.ITEM_ARMOR:
                    stringBuilder.AppendFormat(" {0, -5}", "방어력 + ");
                    break;
            }

            stringBuilder.AppendFormat("{0, -4} | ", itemData.itemStat);

            stringBuilder.AppendFormat("{0, " + -totalDescLenght + "} ", itemData.itemDescription);

            return stringBuilder;
        }

        public ITEM_TYPE GetItemType()
        {
            return itemData.itemType;
        }
        public int GetItemStat()
        {
            return itemData.itemStat;
        }
        public int GetItemPrice()
        {
            return itemData.itemPrice;
        }
        public int GetItemSellPrice()
        {
            return itemData.itemSellPrice;
        }
        public bool GetItemisEquip()
        {
            return itemData.isEquip;
        }
        public StringBuilder GetItemName()
        {
            return itemData.itemName;
        }

        public void SetItemEquip(bool isEquip)
        {
            itemData.isEquip = isEquip;
        }

        private void CreateItemPreset(string itemName)
        {
            switch (itemName)
            {
                case "수련자 갑옷":
                    itemData.itemName = new StringBuilder(itemName);
                    itemData.itemDescription = new StringBuilder("수련에 도움을 주는 갑옷입니다.                   ");
                    itemData.itemType = ITEM_TYPE.ITEM_ARMOR;
                    itemData.itemStat = 5;
                    itemData.itemPrice = 1000;
                    itemData.itemSellPrice = (int)(itemData.itemPrice * 0.85f);
                    break;
                case "무쇠 갑옷":
                    itemData.itemName = new StringBuilder(itemName);
                    itemData.itemDescription = new StringBuilder("무쇠로 만들어져 튼튼한 갑옷입니다.               ");
                    itemData.itemType = ITEM_TYPE.ITEM_ARMOR;
                    itemData.itemStat = 9;
                    itemData.itemPrice = 2000;
                    itemData.itemSellPrice = (int)(itemData.itemPrice * 0.85f);
                    break;
                case "스파르타의 갑옷":
                    itemData.itemName = new StringBuilder(itemName);
                    itemData.itemDescription = new StringBuilder("스파르타의 전사들이 사용했다는 전설의 갑옷입니다.");
                    itemData.itemType = ITEM_TYPE.ITEM_ARMOR;
                    itemData.itemStat = 15;
                    itemData.itemPrice = 3500;
                    itemData.itemSellPrice = (int)(itemData.itemPrice * 0.85f);
                    break;
                case "낡은 검":
                    itemData.itemName = new StringBuilder(itemName);
                    itemData.itemDescription = new StringBuilder("쉽게 볼 수 있는 낡은 검 입니다.                  ");
                    itemData.itemType = ITEM_TYPE.ITEM_SWORD;
                    itemData.itemStat = 2;
                    itemData.itemPrice = 600;
                    itemData.itemSellPrice = (int)(itemData.itemPrice * 0.55f);
                    break;
                case "청동 도끼":
                    itemData.itemName = new StringBuilder(itemName);
                    itemData.itemDescription = new StringBuilder("어디선가 사용됐던거 같은 도끼입니다.             ");
                    itemData.itemType = ITEM_TYPE.ITEM_SWORD;
                    itemData.itemStat = 5;
                    itemData.itemPrice = 1500;
                    itemData.itemSellPrice = (int)(itemData.itemPrice * 0.85f);
                    break;
                case "스파르타의 창":
                    itemData.itemName = new StringBuilder(itemName);
                    itemData.itemDescription = new StringBuilder("스파르타의 전사들이 사용했다는 전설의 창입니다.  ");
                    itemData.itemType = ITEM_TYPE.ITEM_SWORD;
                    itemData.itemStat = 7;
                    itemData.itemPrice = 3000;
                    itemData.itemSellPrice = (int)(itemData.itemPrice * 0.85f);
                    break;
                case "취업의 돌":
                    itemData.itemName = new StringBuilder(itemName);
                    itemData.itemDescription = new StringBuilder("소유한자를 취업시켜준다는 취업의 돌...열심히 하자");
                    itemData.itemType = ITEM_TYPE.ITEM_SWORD;
                    itemData.itemStat = 100;
                    itemData.itemPrice = 9999;
                    itemData.itemSellPrice = (int)(itemData.itemPrice * 0.0f);
                    break;
            }
        }
    }
}

