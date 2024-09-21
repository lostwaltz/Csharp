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

            ItemData itemDataStruct = new ItemData();

            itemDataStruct.itemType = ITEM_TYPE.ITEM_SWORD;
            itemDataStruct.itemDescription.Append("0 번째 아이템입니다.");

            itemDataStruct.itemName.Append("낡은 검");
            itemDataStruct.itemStat = 5;

            Item item_00 = new Item(itemDataStruct);

            itemDataStruct.itemName.Clear();
            itemDataStruct.itemName.Append("스파르타의 창");
            itemDataStruct.itemStat = 15;
            itemDataStruct.itemDescription.Clear();
            itemDataStruct.itemDescription.Append("1 번째 아이템입니다.");
            Item item_01 = new Item(itemDataStruct);

            itemDataStruct.itemType = ITEM_TYPE.ITEM_ARMOR;
            itemDataStruct.itemName.Clear();
            itemDataStruct.itemName.Append("무쇠 갑옷");
            itemDataStruct.itemStat = 7;
            itemDataStruct.itemDescription.Clear();
            itemDataStruct.itemDescription.Append("2 번째 아이템입니다.");
            Item item_02 = new Item(itemDataStruct);

            itemList.PushItem(item_00);
            itemList.PushItem(item_01);
            itemList.PushItem(item_02);
        }
        private ItemList itemList;


        public StringBuilder GetInvenItemListText(bool numberVisuable, bool priceVisualbe)
        {
            return   itemList.GetItemListText(numberVisuable, priceVisualbe);
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

            if(null == equipSlot[(int)item.GetItemType()])
                actionSetEquipCallback?.Invoke(item.GetItemType(), 0);
            else
                actionSetEquipCallback?.Invoke(item.GetItemType(), item.GetItemStat());
        }

        public void PushFuntion(Action<ITEM_TYPE, int> funtionCallback)
        {
            actionSetEquipCallback += funtionCallback;
        }
        public int GetEquipSlotStat(ITEM_TYPE itemType)
        {
            int stat = 0;
            int? statNullable = equipSlot[(int)itemType]?.GetItemStat();

            if(true == statNullable.HasValue)
                stat = statNullable.Value;

            return stat;
        }

    }

    public interface IInvenInterface
    {
        StringBuilder GetInvenItemListText(bool numberVisuable, bool priceVisualbe);

        void SetEquipItemTogle(int itemIndex);

        void PushFuntion(Action<ITEM_TYPE, int> funtionCallback);

        int GetEquipSlotStat(ITEM_TYPE itemType);
    }
}
