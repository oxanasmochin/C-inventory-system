using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modeaffär
{
    /// <summary>
    /// This is a Clothes class with properties decribing a clothe/product object name, size, and quantity.
    /// To declare a Clothing's size (ClothingSize property), we first use an enumerator list called sizes
    /// which contains a set of 5 different sizes: xs,s,m,l,xl. We then create ClothingSize property of type size
    /// Class Clothes also has a constructor with all the above mentioned properties.  
    /// </summary>

    class Clothes
    {
        //===============Properties===================

        public string Name { get; set; }
        public enum sizes { XS, S, M, L, XL } //enum is used to declare an enumeration of different sizes.
        public sizes ClothingSize { get; set; }
        public int quantity { get; set; }
        
        //=============Constructor====================

        public Clothes(string Name, sizes ClothingSize, int quantity)
        {
            this.Name = Name;
            this.ClothingSize = ClothingSize;
            this.quantity = quantity;
        }

        /* Method IncreaseQuantity is used to increase quantity of a product when a new stock 
        of the same product is purchased*/
        public int IncreaseQuantity(int q) 
        {
            quantity = quantity + q;

            return quantity;
        }


        /* Method DecreaseQuantity is used to reduce quantity of a product when a new stock 
        of the same product is sold*/
        public int DecreaseQuantity(int q)
        {
            quantity = quantity - q;
            return quantity;
        }
    }
}
