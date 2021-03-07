using System;

namespace UNO.Domain
{
    public class Card
    {
        public Numbers Number { get; set; }
        public Colors Color { get; set; }
        public Actions Action { get; set; }
        public Wilds Wild { get; set; }
        public Types Type { get; set; }

        public enum Numbers
        {
            None,
            Zero,
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine
        }
        public enum Colors
        {
            None,
            Blue,
            Green,
            Red,
            Yellow
        }
        public enum Actions
        {
            None,
            Skip,
            Reverse,
            Draw2
        }
        public enum Wilds
        {
            None,
            Wild,
            WildDraw4
        }
        public enum Types
        {
            NumberedCard,
            ActionCard,
            WildCard
        }

        public Card(Numbers number, Colors color)
        {
            Number = number;
            Color = color;
            Type = Types.NumberedCard;
        }
        public Card(Colors color, Actions action)
        {
            Color = color;
            Action = action;
            Type = Types.ActionCard;
        }
        public Card(Wilds wild)
        {
            Wild = wild;
            Type = Types.WildCard;
        }

        public override string ToString()
        {
            if (Type == Types.NumberedCard)
            {
                return Number + " " + Color;
            }
            else if (Type == Types.ActionCard)
            {
                return Action + " " + Color;
            }
            else
            {
                return " " + Wild + " ";
            }
        }
    }
}
