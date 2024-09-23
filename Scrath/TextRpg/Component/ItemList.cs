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

        public StringBuilder GetItemListText(bool numberVisuable, bool priceVisuable, bool sellPriceVisuable ,bool eqipVisuable)
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

                    stringBuilder.Append(itemList[i]?.PrintItemInfo(eqipVisuable));

                    if (true == priceVisuable)
                    {
                        if(true == sellPriceVisuable)
                            stringBuilder.AppendFormat("| {0} Gold.", itemList[i]?.GetItemSellPrice());
                        else
                            stringBuilder.AppendFormat("| {0} Gold.", itemList[i]?.GetItemPrice());
                    }

                    stringBuilder.Append(" \n");
                }
            }

            return stringBuilder;
        }

        public bool PushItem(Item item)
        {
            for (int i = 0; i < itemList.Length; i++)
            {
                if (null != itemList[i])
                    continue;

                itemList[i] = item;
                itemListCount++;

                return true;
            }
            return false;
        }
        public void RemoveItem(int index)
        {
            if (itemList[index] != null)
                itemListCount--;

            itemList[index] = null;
            
            for(int i = index; i <= itemListCount; i++)
            {
                itemList[i] = itemList[i + 1];
            }

            itemList[itemListCount] = null;
        }
        public void RemoveItem(Item item)
        {
            int index;
            FindItemtoItem(item, out index);

            if (itemList[index] != null)
                itemListCount--;

            itemList[index] = null;

            for (int i = index; i <= itemListCount; i++)
            {
                itemList[i] = itemList[i + 1];
            }
            itemList[itemListCount] = null;
        }

        public Item? GetItemtoIndex(int index)
        {
            if ((0 > index || 9 < index))
                return null;

            return itemList[index];
        }

        public bool FindItemtoItem(Item? _item)
        {
            if(_item == null) return false;

            foreach(Item? item in itemList)
            {
                if(item?.itemData.itemName.ToString() == _item.itemData.itemName.ToString()) return true;
            }

            return false;
        }
        public bool FindItemtoItem(Item? _item, out int index)
        {
            index = 0;
            if (_item == null) return false;

            foreach (Item? item in itemList)
            {
                if (item == _item) return true;
                index++;
            }

            return false;
        }
    }
}
