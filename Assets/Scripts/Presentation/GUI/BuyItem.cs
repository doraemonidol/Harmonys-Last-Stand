using System;
using Common;
using Common.Context;
using DTO;
using Logic.Facade;
using Logic.Helper;
using UnityEngine;

namespace Presentation.GUI
{
    public class BuyItem : MonoBehaviour
    {
        [SerializeField] public ItemType itemType;
        private Item activeItem;
        
        public void SetCurrentItem(Item item)
        {
            activeItem = item;
        }

        public void Buy()
        {
            var ctx = GameContext.GetInstance();
            var cost = activeItem.money;
            if (ctx.Money < cost)
            {
                throw new Exception("Hết tiền rồi!");
            }
            ctx.Money -= cost;
            LogicLayer.GetInstance().Observe(new EventDto
            {
                Event = "BUY",
                ["item"] = activeItem.itemType,
            });
            throw new Exception("Mua thành công!");
        }
    }
}