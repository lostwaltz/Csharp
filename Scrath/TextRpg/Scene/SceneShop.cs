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

            StringBuilder itemListText = shopItemList.GetItemListText(true, true, false, false);
            if (null == shopItemList)
                return;

            for (int i = 0; i < shopItemList?.itemListCount; i++)
            {
                if (true == Interface.FindItemtoItem(shopItemList?.GetItemtoIndex(i)))
                    itemListText.Replace(shopItemList?.GetItemtoIndex(i)?.GetItemPrice() + " Gold.", "구매완료");
            }
            Console.WriteLine(itemListText);

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

                    if(preSelectNumber == 1)
                    {
                        if (null == shopItemList)
                            return;

                        itemListText.Clear();
                        itemListText = shopItemList.GetItemListText(true, true, false, false);

                        for (int i = 0; i < shopItemList?.itemListCount; i++)
                        {
                            if(true == Interface.FindItemtoItem(shopItemList?.GetItemtoIndex(i)))
                                itemListText.Replace(shopItemList?.GetItemtoIndex(i)?.GetItemPrice() + " Gold.", "구매완료");
                        }
                        Console.WriteLine(itemListText);
                    }
                    else
                        Console.WriteLine(Interface.GetPlayerItemListText(true, true, true));

                    Console.Write("0. 나가기\n\n");
                    Console.Write("원하시는 행동을 입력해주세요.\n>> ");

                    int.TryParse(Console.ReadLine(), out selectcNumber);
                    if (0 == selectcNumber || null == shopItemList)
                        return;

                    if (0 > selectcNumber || (shopItemList.itemListCount < selectcNumber))
                    {
                        Console.Write("잘못된 입력입니다.");
                        Thread.Sleep(1000);
                        return;
                    }

                    Item? selectItem = shopItemList.GetItemtoIndex(selectcNumber - 1);
                    if(2 == preSelectNumber)
                    {
                        if(Interface.GetPlayerItemListCount() < selectcNumber)
                        {
                            Console.Write("잘못된 입력입니다.");
                            Thread.Sleep(1000);
                            return;
                        }    
                        selectItem = Interface.GetPlayerItemtoIndex(selectcNumber - 1);
                    }

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
            shopItemList.PushItem(new Item("수련자 갑옷"));
            shopItemList.PushItem(new Item("무쇠 갑옷"));
            shopItemList.PushItem(new Item("스파르타의 갑옷"));
            shopItemList.PushItem(new Item("낡은 검"));
            shopItemList.PushItem(new Item("청동 도끼"));
            shopItemList.PushItem(new Item("스파르타의 창"));
            shopItemList.PushItem(new Item("취업의 돌"));
        }

    }
}
