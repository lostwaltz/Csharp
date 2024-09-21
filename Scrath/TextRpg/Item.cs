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
        }

        public StringBuilder itemName;
        public StringBuilder itemDescription;
        public ITEM_TYPE itemType;
        public int itemStat;
        public int itemPrice;
        public bool isEquip;
    }

    internal class Item
    {
        public Item(ItemData _itemData)
        {
            itemData = new ItemData(_itemData);
        }

        private ItemData itemData;

        public StringBuilder PrintItemInfo()
        {
            StringBuilder stringBuilder = new StringBuilder();

            int totalNameLenght = 20;
            int totalDescLenght = 30;

            if (true == itemData.isEquip)
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
        public bool GetItemisEquip()
        {
            return itemData.isEquip;
        }

        public void SetItemEquip(bool isEquip)
        {
            itemData.isEquip = isEquip;
        }

    }
}

