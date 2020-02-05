using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class CardDealer : MonoBehaviour
{
    private enum DealerState
    {
        Deal,          //カードを配る
        FaceDown,      //伏せて
        FaceUp,        //オープン
        Result        //勝敗
    }

    private DealerState m_dealerState = DealerState.Deal;

    public PlayerChoice PlayerChoice = null;

    public SpriteAtlas CardDeck = null;

    private List<Card> m_deckCards = new List<Card>(52);

    private Card m_yourCard = null;

    private Card m_dealerCard = null;

    public Image YourCard = null;

    public Image DealerCard = null;

    public TextMeshProUGUI RemainingCard = null;

    public TextMeshProUGUI DealerPoints = null;

    public TextMeshProUGUI YourPoints = null;

    private int m_turn = 0;

    private int m_playerPoints = 0;

    private int m_dealerPoints = 0;

    private void Start()
    {
        m_playerPoints = 0;
        m_dealerPoints = 0;
        
        // Deckにカードをセットする
        for (int i = 0; i < m_deckCards.Capacity; i++)
        {
            var card = new Card(i, CardNumJudge(i), CardSuitJudge(i));
            m_deckCards.Add(card);
        }
        // Deckをシャッフルする
        m_deckCards = m_deckCards.OrderBy(card => Guid.NewGuid()).ToList();
    }

    /// <summary>
    /// カードを配る
    /// </summary>
    public void CardDeal(ref Card _card)
    {
        _card = m_deckCards.First();
        m_deckCards.RemoveAt(0);
        m_turn++;
    }

    /// <summary>
    /// カードの数を決める1~13
    /// </summary>
    /// <returns></returns>
    public int CardNumJudge(int _num)
    {
        for (int i = 0; i < 13; i++)
        {
            if (_num % 13 == i)
            {
                return i + 1;
            }
        }
        return 0;
    }

    /// <summary>
    /// カードの役を決める0～3
    /// </summary>
    /// <returns></returns>
    public Card.CardSuit CardSuitJudge(int _num)
    {
        for (int i = 0; i < (int)Card.CardSuit.Max; i++)
        {
            if (_num / 13 == i)
            {
                return (Card.CardSuit)i;
            }
        }
        return Card.CardSuit.Invalid;
    }

    /// <summary>
    /// 結果
    /// </summary>
    private void JudgeHand(bool _higher, Card _yourCard, Card _dealerCard)
    {
        if (_higher)
        {
            if (_yourCard.CardNum > _dealerCard.CardNum)
            {
                m_playerPoints++;
            }
            else
            {
                if (_yourCard.CardNum != _dealerCard.CardNum)
                {
                    m_dealerPoints++;
                }
            }
        }
        else
        {
            if (_yourCard.CardNum < _dealerCard.CardNum)
            {
                m_playerPoints++;
            }
            else
            {
                if (_yourCard.CardNum != _dealerCard.CardNum)
                {
                    m_dealerPoints++;
                }
            }
        }
    }


    private void Update()
    {
        switch (m_dealerState)
        {
            case DealerState.Deal:
                CardDeal(ref m_yourCard);
                CardDeal(ref m_dealerCard);
                if (m_turn == 2)
                {
                    YourCard.sprite = CardDeck.GetSprite("Card_54");
                    DealerCard.sprite = CardDeck.GetSprite("Card_54");
                    m_dealerState = DealerState.FaceDown;
                }
                break;
            case DealerState.FaceDown:

                if (PlayerChoice.PlayerChoiced)
                {
                    JudgeHand(PlayerChoice.Higher, m_yourCard, m_dealerCard);
                    YourCard.sprite = CardDeck.GetSprite("Card_" + m_yourCard.Num);
                    DealerCard.sprite = CardDeck.GetSprite("Card_" + m_dealerCard.Num);
                    YourPoints.text = "YourPoints:" + m_playerPoints;
                    DealerPoints.text = "DealerPoints:" + m_dealerPoints;
                    m_dealerState = DealerState.FaceUp;
                }
                break;
            case DealerState.FaceUp://結果

                if (Input.GetMouseButtonDown(0))
                {
                    PlayerChoice.ChoiceInit();
                    m_turn = 0;
                    RemainingCard.text = "Cards" + m_deckCards.Count;

                    if (m_deckCards.Count != 0)
                    {
                        m_dealerState = DealerState.Deal;
                    }
                    else
                    {
                        m_dealerState = DealerState.Result;
                    }
                }
                break;
            case DealerState.Result:
                if (m_playerPoints > m_dealerPoints)
                {
                    Debug.Log("Your Win");
                }
                else
                {
                    Debug.Log("Your Lose");

                }
                break;
        }

    }
}
