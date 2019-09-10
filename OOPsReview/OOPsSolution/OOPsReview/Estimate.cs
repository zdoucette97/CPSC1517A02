using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    public class Estimate
    {
        //public double TotalPrice { get; set; }
        private double TotalPrice { get; private set; } //the user can get, but the inside of the class can set using a method, thus placing the calculation into the estimate method

        public double LinearLength { get; set; }
        public FencePanel TotalPanels { get; set; } 
        public List<Gate> TotalGates { get; set; } 

        //read only property
        //for example a student with a firstname and lastname; you do not need a fullname string, you can combine them into a fullname read only property


        public double CalculateTotalPrice()
        {
            //using properties of FencePanel
            double numberofpanels = TotalPanels.EstimatedNumberOfPanels(LinearLength);
            if ((int)(numberofpanels * 10.0) > ((int)numberofpanels * 10)) //truntacates
            {
                numberofpanels++;
            }
            //summing calculated prices
            if (TotalPanels.Price == null)
            {
                throw new Exception("Panel price is needed to create estimate");
            }
            else
            {
                TotalPrice += numberofpanels * (double)TotalPanels.Price;
                foreach(var item in TotalGates)
                {
                    TotalPrice += item.Price; //localinstancename.publicdatamember/publicproperty/behaviour
                                              //Estimate class contains the above to be used for an instance
                                              //you are inside the class that has access
                   
                }
            }
            return TotalPrice;
        }
        //foreach loop = list of items. incrementing is done automatically
        //for loop
        //while = while this is true or false
        //until = loop until this happens
    }
}
