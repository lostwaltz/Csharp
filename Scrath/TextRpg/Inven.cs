using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.Component;

namespace TextRpg
{
    internal class Inven : IInvenInterface
    {
        private Action<ITEM_TYPE, int>? actionSetEquipCallback = null;

        private Item?[] equipSlot = new Item[(int)ITEM_TYPE.ITEM_END];

        public Inven(ItemList itemListDI)
        {
            itemList = itemListDI;
        }
        private ItemList itemList;


        public StringBuilder GetInvenItemListText(bool numberVisuable, bool priceVisualbe, bool sellPriceVisable)
        {
            return itemList.GetItemListText(numberVisuable, priceVisualbe, sellPriceVisable, true);
        }

        public void SetEquipItemTogle(int itemIndex)
        {
            if (itemIndex > itemList.itemListCount - 1)
            {
                Console.Write("잘못된 입력입니다.");
                Thread.Sleep(1000);
                return;
            }

            Item? item = itemList.GetItemtoIndex(itemIndex);
            if (null == item)
                return;

            if (equipSlot[(int)item.GetItemType()] == item)
            {
                item.SetItemEquip(!item.GetItemisEquip());
                if (false == item.GetItemisEquip())
                    equipSlot[(int)item.GetItemType()] = null;
            }
            else
            {
                equipSlot[(int)item.GetItemType()]?.SetItemEquip(false);
                equipSlot[(int)item.GetItemType()] = item;
                item.SetItemEquip(true);
            }

            if (null == equipSlot[(int)item.GetItemType()])
                actionSetEquipCallback?.Invoke(item.GetItemType(), 0);
            else
                actionSetEquipCallback?.Invoke(item.GetItemType(), item.GetItemStat());
        }

        public void SetEquipItemTogle(Item item)
        {
            if (equipSlot[(int)item.GetItemType()] == item)
            {
                item.SetItemEquip(!item.GetItemisEquip());
                if (false == item.GetItemisEquip())
                    equipSlot[(int)item.GetItemType()] = null;
            }
            else
            {
                equipSlot[(int)item.GetItemType()]?.SetItemEquip(false);
                equipSlot[(int)item.GetItemType()] = item;
                item.SetItemEquip(true);
            }

            if (null == equipSlot[(int)item.GetItemType()])
                actionSetEquipCallback?.Invoke(item.GetItemType(), 0);
            else
                actionSetEquipCallback?.Invoke(item.GetItemType(), item.GetItemStat());
        }

        public void PushFuntion(Action<ITEM_TYPE, int> funtionCallback)
        {
            actionSetEquipCallback += funtionCallback;
        }

        public bool PushItemInven(Item item)
        {
            return itemList.PushItem(item);
        }

        public int GetEquipSlotStat(ITEM_TYPE itemType)
        {
            int stat = 0;
            int? statNullable = equipSlot[(int)itemType]?.GetItemStat();

            if (true == statNullable.HasValue)
                stat = statNullable.Value;

            return stat;
        }
        public bool FindItemtoItem(Item? item)
        {
            return itemList.FindItemtoItem(item);
        }
        public Item? GetItemtoIndex(int index)
        {
            if (index > itemList.itemListCount) return null;

            return itemList.GetItemtoIndex(index);
        }
        public void RemoveItemtoItem(Item item)
        {
            itemList.RemoveItem(item);
        }

        public int GetItemListCount()
        {
            return itemList.itemListCount;
        }
    }

    internal interface IInvenInterface
    {
        StringBuilder GetInvenItemListText(bool numberVisuable, bool priceVisualbe, bool sellPriceVisuable);

        void SetEquipItemTogle(int itemIndex);
        void SetEquipItemTogle(Item item);

        void PushFuntion(Action<ITEM_TYPE, int> funtionCallback);

        bool PushItemInven(Item item);

        int GetEquipSlotStat(ITEM_TYPE itemType);

        bool FindItemtoItem(Item? item);

        Item? GetItemtoIndex(int index);

        void RemoveItemtoItem(Item item);

        int GetItemListCount();
    }
}
