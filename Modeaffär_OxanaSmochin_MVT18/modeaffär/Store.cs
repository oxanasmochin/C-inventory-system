using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace modeaffär
{
    class Store
    {
        /// <summary>
        /// This is a Store class with properties decribing a boutique's name, adress and products that it has
        /// in stock. It also has a constructor with store's  name and adress. 
        /// I also created a Method that shows all products that belong to a boutique, and methods that prints information
        /// of a particular boutique, a message that helps to load information of boutiques in a listbox, and a method that 
        /// checks weather a boutique's storagy is empty;
        /// </summary>
        /// 
        //=========Properties==============

        public string StoreName { get; set; }
        public string StoreAdress { get; set; }
        public List<Clothes> productList= new List<Clothes>();

        //=========Constructor======================

        public Store(string StoreName, string StoreAdress)
        {
            this.StoreName = StoreName;
            this.StoreAdress = StoreAdress;

        }
        public string Show_allproducts() //method that shows all the products in a particular boutique 
        {
            string show = "";
            int nr=1; 
            foreach (Clothes cl in productList)
            {
                show = show+nr+".    "+cl.Name +"---"+cl.ClothingSize+"--"+cl.quantity+Environment.NewLine; 
                nr++;
            }
            return show; 
        }

        public string Message() // This method is used to print information about what new boutique has been created 
        {
            string message = "A new boutique was added: " + "\n" + "\n" + "Boutique's Name: " + StoreName
                    + "\n" + "Boutique's Adress: " + StoreAdress;
            return message;
        }

        public string ListBoxItems() // Method that loads Boutiques listbox with string which contains Boutique's name and adress
        {
            return StoreName + " , " + StoreAdress;
        }

        public bool CheckIf_EmptyProductList() // Method that is used to check if a boutiques product list is empty
        {
            bool isEmpty = !productList.Any();
            if (isEmpty)
            {
                isEmpty = true;  
            }
            return isEmpty;
        }
    }
}



