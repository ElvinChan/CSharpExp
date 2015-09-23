using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardLib
{
    public class Deck
    {
        private Cards cards = new Cards();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public Deck()
        {
            for (int suitVal = 0; suitVal < 4; suitVal++)
            {
                for (int rankVal = 1; rankVal < 14; rankVal++)
                {
                    cards.Add(new Card((Suit)suitVal, (Rank)rankVal));
                }
            }
        }

        private Deck(Cards newCards)
        {
            cards = newCards;
        }

        /// <summary>
        /// 不使用默认构造函数，将尖子设为最大
        /// </summary>
        /// <param name="isArcHigh"></param>
        public Deck(bool isArcHigh)
            : this()
        {
            Card.isAceHigh = isArcHigh;
        }

        /// <summary>
        /// 不使用默认构造函数，允许使用王牌及王牌组合
        /// </summary>
        /// <param name="useTrumps"></param>
        /// <param name="trump"></param>
        public Deck(bool useTrumps, Suit trump)
            : this()
        {
            Card.useTrumps = useTrumps;
            Card.trump = trump;
        }

        /// <summary>
        /// 不使用默认构造函数，允许使用尖子，王牌及王牌组合
        /// </summary>
        /// <param name="isArcHigh"></param>
        /// <param name="useTrumps"></param>
        /// <param name="trump"></param>
        public Deck(bool isArcHigh, bool useTrumps, Suit trump)
            : this()
        {
            Card.isAceHigh = isArcHigh;
            Card.useTrumps = useTrumps;
            Card.trump = trump;
        }

        public Card GetCard(int cardNum)
        {
            if (cardNum >= 0 && cardNum <= 51)
                return cards[cardNum];
            else
                throw (new System.ArgumentOutOfRangeException("cardNum", cardNum,
                    "Value must be between 0 and 51."));
        }

        public void Shuffle()
        {
            Cards newDeck = new Cards();
            //初始时assigned数组全为false，随机填入Card后对应数组中的元素变为true
            bool[] assigned = new bool[52];
            Random sourceGen = new Random();
            for (int i = 0; i < 52; i++)
            {
                int sourceCard = 0;
                //foundCard作用是只产生一次相同的Card
                bool foundCard = false;
                while (!foundCard)
                {
                    sourceCard = sourceGen.Next(52);
                    if (assigned[sourceCard] == false)
                    {
                        foundCard = true;
                    }
                }
                assigned[sourceCard] = true;
                newDeck.Add(cards[sourceCard]);
            }
            newDeck.CopyTo(cards);
        }

        public object Clone()
        {
            Deck newDeck = new Deck(cards.Clone() as Cards);
            return newDeck;
        }
    }
}
