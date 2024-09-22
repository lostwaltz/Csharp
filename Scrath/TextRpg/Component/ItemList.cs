using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpg.Component
{
    internal class ItemList
    {
        public ItemList()
        {
            itemListMaxCount = 10;
            itemList = new Item?[itemListMaxCount];
        }
        private int itemListMaxCount;
        private Item?[] itemList;
        public int itemListCount { get; private set; } = 0;

        public StringBuilder GetItemListText(bool numberVisuable, bool priceVisuable)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("[아이템 목록]\n");

            for (int i = 0; i < itemList.Length; i++)
            {
                if (null != itemList[i])
                {
                    stringBuilder.Append("- ");
                    if (true == numberVisuable)
                        stringBuilder.AppendFormat("{0}. ", i + 1);

                    stringBuilder.Append(itemList[i]?.PrintItemInfo());

                    if (true == priceVisuable)
                        stringBuilder.AppendFormat("| {0} Gold.", itemList[i]?.GetItemPrice());

                    stringBuilder.Append(" \n");
                }
            }

            return stringBuilder;
        }

        public void PushItem(Item item)
        {
            for (int i = 0; i < itemList.Length; i++)
            {
                if (null != itemList[i])
                    continue;

                itemList[i] = item;
                itemListCount++;

                return;
            }
        }
        public void RemoveItem(int index)
        {
            if (itemList[index] != null)
                itemListCount--;

            itemList[index] = null;
        }

        public Item? GetItemtoIndex(int index)
        {
            if ((0 > index || 9 < index))
                return null;

            return itemList[index];
        }

        public bool FindItemtoItem(Item _item)
        {
            foreach(Item? item in itemList)
            {
                if(item == _item) return true;
            }

            return false;
        }
    }
}
