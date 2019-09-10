using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public class Gate
    {
        public double Height { get; set; }
        private double _Price;

        public double Price
        {
            get
            {
                return _Price;
            }
            set
            {
                if (value <= 0.0)
                {
                    new Exception("Price cannot be 0 or less than 0");
                }
                else
                {
                    _Price = value;
                }
            }
        }


        private string _Style;

        public string Style
        {
            get
            {
                return _Style;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _Style = null;
                }
                else
                {
                    _Style = value;
                }
            }
        }

        private double _Width;

        public double Width
        {
            get
            {
                return _Width;
            }
            set
            {
                if (value > 0.0)
                {
                    _Width = value;
                }
                else
                {
                    new Exception("Width cannot be 0 or less than 0");
                }
            }
        }

        public FencePanel(double height, double width, string style, double? price)
        {
            Height = height;
            Width = width;
            Price = price;
            Style = style;
        }

        public double GateArea()
        {
            return Width * Height;
        }
    }

    
}
