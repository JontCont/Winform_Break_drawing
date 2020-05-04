using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMSN
{
    class GameSet
    {
        string[] food =
        {
            "花椰菜","大白菜","薑","大蔥","萵苣","蘑菇","豌豆","馬鈴薯","冬瓜","芋頭",
            "橘子","洋蔥","辣椒","黃瓜","蒜頭","小紅蘿蔔","菠菜","空心菜","白木耳","玉米粒",
            "豆芽","蘆筍","山芋","芥菜","橄欖","金針菇","四季豆","甜菜","茄子","結球菜心",
            "白花菜","地瓜","番茄","水梨","榴槤","草莓","蘋果","奇異果","荔枝","龍眼",
            "火龍果","橘子","哈密瓜","櫻桃","芭樂","水蜜桃","檸檬","芒果","香瓜","李子",
            "文旦","包心菜","蔥","芹菜","紅蘿蔔","蓮霧","香蕉","葡萄","木瓜","鳳梨",
            "椰子","西瓜","牛排","白飯","玉米","秋刀魚","鐵板麵","漢堡","蛋包飯","炒飯",
            "蛋餅","蔥抓餅","雞塊","牛肉麵","三明治","三星蔥","蔥肉派","玉米濃湯","蛋捲","鬆餅"
        };

        public string RandomNum()
        {
            int temp = 0;
            Random crandom = new Random();
            for (int i = 0; i < food.Length; i++) temp = crandom.Next(0, food.Length);
            for (int i = 0; i < food.Length; i++) temp = crandom.Next(0, food.Length);
            for (int i = 0; i < food.Length; i++) temp = crandom.Next(0, food.Length);

            return food[temp];
            //return temp;
        }

    }
}
