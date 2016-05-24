using System;

namespace Models
{
    public class OrderType
    {
        public String display_name;
        public int value;

        public OrderType() { }

        public OrderType(int value, String display_name)
        {
            this.display_name = display_name;
            this.value = value;
        }

        public override string ToString()
        {
            return display_name;
        }

    }
}
