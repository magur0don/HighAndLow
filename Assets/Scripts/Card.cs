using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public enum CardSuit
    {
        Invalid = -1,
        Club,
        Dia,
        Heart,
        Spade,
        Max
    }
    public int Num;
    public int CardNum;
    public CardSuit Suit;

    /// <summary>
    /// 外部からのアクセサー
    /// </summary>
    /// <param name="_num">0～51の数字</param>
    /// <param name="_cardnum">カードの数字</param>
    /// <param name="_suit">役柄</param>
    public Card(int _num, int _cardnum, CardSuit _suit)
    {
        Num = _num;
        CardNum = _cardnum;
        Suit = _suit;
    }
}
