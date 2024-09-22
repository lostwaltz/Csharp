using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg.Component;

namespace TextRpg.Scene
{
    public enum SHOP_SELET_NUM { SHOP_EXIT, SHOP_BUY, SHOP_SELL, SHOP_END };

    internal class SceneShop : Scene
    {
        private IPlayerInterface Interface;
        ItemList shopItemList;
        private Action<Item, SHOP_SELET_NUM>? actionShopEventCallback = null;

        public SceneShop(IPlayerInterface playerInterface)
        {
            curEnumScene = SCENE.SCENE_SHOP;

            Interface = playerInterface;
            shopItemList = new ItemList();

            stringBuilder.Append("상점\n");
            stringBuilder.Append("필요한 아이템을 얻을 수 있는 상점입니다.\n\n");

            stringBuilder.Append("[보유 골드]\n");

            actionShopEventCallback += Interface.PlayerShopEventCallbackFuntion;

            InitItemList();
        }

        public override void SceneUpdate()
        {
            Console.Write(stringBuilder);

            Console.WriteLine(Interface.GetPlayerGold() + " G\n");

            Console.WriteLine(shopItemList.GetItemListText(false, true));

            Console.WriteLine("1. 아이템 구매\n2. 아이템 판매\n0. 나가기\n");

            Console.Write("원하시는 행동을 입력해주세요.\n>> ");
            int.TryParse(Console.ReadLine(), out selectcNumber);


            if (0 > selectcNumber || 2 < selectcNumber)
            {
                Console.Write("잘못된 입력입니다.");
                Thread.Sleep(1000);
                return;
            }
            else if (1 == selectcNumber || 2 == selectcNumber)
            {
                int preSelectNumber = selectcNumber;
                while (true)
                {
                    Console.Clear();
                    Console.Write(stringBuilder);
                    Console.WriteLine(Interface.GetPlayerGold() + " G\n");
                    Console.WriteLine(shopItemList.GetItemListText(true, true));
                    Console.Write("0. 나가기\n\n");
                    Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                    int.TryParse(Console.ReadLine(), out selectcNumber);
                    if (0 == selectcNumber)
                        return;

                    if (0 > selectcNumber || shopItemList.itemListCount < selectcNumber)
                    {
                        Console.Write("잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        return;
                    }

                    Item? selectItem = shopItemList.GetItemtoIndex(selectcNumber - 1);
                    if (null == selectItem)
                        return;

                    actionShopEventCallback?.Invoke(selectItem, (SHOP_SELET_NUM)preSelectNumber);
                }
            }
            else
            {
                if (null != SceneManager.instance)
                    SceneManager.instance.SceneChange(SCENE.SCENE_LOBY);
            }


        }

        private void InitItemList()
        {
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

            shopItemList.PushItem(item_00);
            shopItemList.PushItem(item_01);
            shopItemList.PushItem(item_02);
        }

    }
}
