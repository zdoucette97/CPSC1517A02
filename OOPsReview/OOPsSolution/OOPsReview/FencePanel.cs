using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPsReview
{
    //-------------------------------Creating a new Class-------------------------------
    //the default permissions is private
    //access to this file from other files must have public permissions
    //if an outside user of ths class wanted access to the class contents, then the class permissions must be public
    public class FencePanel
    {
        
    }

    //-------------------------------PROPERTIES-------------------------------
    //encapsulation: used to manage information in private classes without ruining privacy
    //fully implemented get-set properties should be used to validate the class variables, but autoimplemented will manage the hidden data member itself
    //Properties are associated with a single piece of data and have two sub components:
    //      get: returns a value to the calling agent
    //      set: recieves a value from the calling agent. the keyword to represent the incoming data is value
    //a property DOES NOT have a developer's parameter and should not be declared there.

    //      Auto-Implemented:
    //          a private data member DOES NOT need to be coded. the system will create an internal data member that the system will manage. 
    //          for example: a nullable non-string data value. it will be given either a numeric value or null, unless it needs additional checking such as whether or not it is positive or negative
    public double Height {get; set;}
    public double? Price {get; set;} //the question mark next to a datatype means that is it nullable. this is unecessary for a string which can naturally be nullable

    //      Fully-Implemented:
    //          a private data member WILL be coded for use by this property for additional processing such as validation. 
    private string _Style;

    public string Style //a nullable string data value must be fully implemented
    {
        get
        {
            return _Style; //make sure you reference the private string '_Style' and not the public string 'Style' itself
        }
        set
        {
            if (string.IsNullOrEmpty(value)) //not _Style. the default of a string is null. value is the incoming data  
            {
                _Style = null;
            }
            else
            {
                _Style = value;
                //value = _Style; is INCORRECT. it will overwrite the incoming data
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
                new Exception("Width cannot be 0 or less than 0");
            }
            else
            {
                _Width = value;
            }
        }
    }

    //-------------------------------CONSTRUCTORS-------------------------------
    //all constructors ina single program must be either programmed yourself or done automatically. you must choose between:

    //      System Constructor: A default constructor where the computer assigns values
    public FencePanel()
    {

    }
    //      Greedy Constructor: A constructor that is fed a list of parameters to control actions performed on an object, 
    //thus organizing each possible data value in your class (properties) rather than creating several different methods for a single variable
    public FencePanel(double height, double width, string style, double? price)
    {
        Height = height;
        Width = width;
        //_Width = width; cannot be done as there is no validation in place yet.
        Price = price;
        Style = style;
        //the constructor returns the instance of the object
    }

    //-------------------------------BEHAVIOURS-------------------------------
    //a.k.a a method
    public double EstimatedNumberOfPanels(double linearlength)
    {
        double numberofpanels = linearlength / Width; //Width can be a private data member (_Width) or a property (Width)
        return numberofpanels; //linearlength is nonexistent once method is complete

        //Using a property ensures all validation or excess logic is in play
    }

    public double FenceArea(double linearlength)
    {
        return linearlength * Height; //not declared as private yet, so you can only use the property
    }


}
