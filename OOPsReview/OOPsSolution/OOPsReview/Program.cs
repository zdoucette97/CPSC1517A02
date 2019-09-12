using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    class Program
    {
        static void Main(string[] args)
        {
            //assume that you know how to do the data input into a console application
            //the input data will simply be placed in a local variable 
            //linear length
            double linearlength = 135.0;
            //height
            double height = 6.5;
            //width
            double width = 8.0;
            //style
            string style = "Neighbour friendly: Spruce";
            //price
            double price = 108.00;

            //situation: have the required data for the class instance
            //options
            //  a) create instance default constructor AND multiple assignment statements
            //  b) create instance using the greedy constructor
            //Solution: b) one statement

            //FencePanel is a non-static class that must be declared
            //Console was a static class (DOES NOT store data)
            //For a non-static class you MUST create an instance of the class to be able to use it.
            //Syntax using the new keyword which requires a reference to the appropriate class constructor 
            FencePanel Panel = new FencePanel(height, width, style, price);

            //handle numerous gates
            //create a class pointer able to reference the Gate class. it will be null be default and doesn't have to be declared as such.
            Gate GateInfo; //like a parking space waiting for cars

            //a structure to hold a collection of Gate
            //the structure to use should hold an unknown number of instances. it will be a List<T> (T represents the datatype)
            //Create the new instance of the List<T> when it is defined.
            List<Gate> gatelist = new List<Gate>();

            //assume you are in a loop to gather multiple gates

            //technique 1 
            //create instance of Gate
            //collect data
            //add to List 
            GateInfo = new Gate(); //start off with the default constructor until you insert information
            height = 6.25;
            width = 4.0;
            style = "Neighbour friendly - 1/2 picket: spruce";
            price = 86.45;
            GateInfo.Height = height;
            GateInfo.Width = width;
            GateInfo.Style = style;
            GateInfo.Price = price;
            gatelist.Add(GateInfo);
            //not efficient as a default constructor as such. using a greedy constructor is best. 

            //technique 2
            //collect data 
            //create instance of Gate
            //add to the list 
            height = 6.25;
            width = 3.0;
            style = "Neighbour friendly: spruce";
            price = 72.45;
            GateInfo = new Gate(height, width, style, price);
            gatelist.Add(GateInfo);

            //create the Estimate 
            //Estimate ClientEstimate = new Estimate(linearlength, panel, gates); this is not possible as the constructor was not declared in the Estimate Class
            Estimate ClientEstimate = new Estimate();
            ClientEstimate.LinearLength = linearlength; //linear length is a property of a class. the assignment rules to not change
            ClientEstimate.TotalPanels = Panel;
            ClientEstimate.TotalGates = gatelist;
            //ClientEstimate.TotalPrice = 450.98; this cannot be done as the price is private. instead you muse use the public double
            ClientEstimate.CalculateTotalPrice();
        }
    }
}
