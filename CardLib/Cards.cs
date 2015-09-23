using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CardLib
{
    public class Cards : CollectionBase
    {
        public void Add(Card newCard)
        {
            List.Add(newCard);
        }

        public void Remove(Card oldCard)
        {
            List.Remove(oldCard);
        }

        public Cards()
        {

        }

        /// <summary>
        /// 索引
        /// </summary>
        /// <param name="cardIndex"></param>
        /// <returns></returns>
        public Card this[int cardIndex]
        {
            get
            {
                return (Card)List[cardIndex];
            }
            set
            {
                List[cardIndex] = value;
            }
        }

        /// <summary>
        /// 将Card对象复制到另一个对象的实例方法，在Deck.Shuffle()方法中调用。
        /// 这个实现假定源集合和目标集合是同一大小
        /// </summary>
        /// <param name="targetCards"></param>
        public void CopyTo(Cards targetCards)
        {
            for (int index = 0; index < this.Count; index++)
            {
                targetCards[index] = this[index];
            }
        }

        /// <summary>
        /// 检查Cards集合中是否包含指定Card
        /// 这个方法将通过InnerList属性调用ArrayList的Contains方法
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public bool Contains(Card card)
        {
            return InnerList.Contains(card);
        }

        public object Clone()
        {
            Cards newCards = new Cards();
            foreach (Card sourceCard in List)
            {
                newCards.Add(sourceCard.Clone() as Card);
            }
            return newCards;
        }
    }


}
