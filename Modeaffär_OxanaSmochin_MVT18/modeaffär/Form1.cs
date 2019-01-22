using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace modeaffär
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// In the Form1 I have first declared objects aStore & aProduct of type Store and Clothes respectively. Then I will need 
        /// two lists, one list contains all the clothes products and the other will contain all the created boutiques objects. 
        /// I also declared variable level of type Clothes.size (item_size) to be able to assign different sizes to the checked 
        /// combobox (cmbSize). And a variable now of type DateTime that will help show current date and time on WFA
        /// </summary>

        public Form1()
        {
            InitializeComponent();
            LoadComboBoxSize(); //this method loads all five different sizes into a combobox

        }

        Clothes aProduct;
        Store aStore;
        List<Store> listStores = new List<Store>();
        List<Clothes> listClothes = new List<Clothes>();
        Clothes.sizes item_size;
        DateTime now = DateTime.Now;

        //===================================METHODS==================================

        /* Check_for_duplicate(string textName, string textAdress) is a method that checks for duplicate in case
         the user wants to create a new boutique with the same name and same adress. This method returns true or false 
         depending on if it finds a newly introduced store object in the list of existent stores */
        bool Check_for_duplicate(string textName, string textAdress) 
        {
            bool found = false;
            foreach (Store item in listStores)
            {
                if (!found)
                {
                    found = (item.StoreName == textName && item.StoreAdress == textAdress);
                }
            }
            return found;
        }

        /* LoadComboBox_Store(List<Store> st, ComboBox cmb) method loads comboboxes with stores. It has two input variables
         list of stores and a combobox. It is used to load as many as one wants comboboxes with list of stores on different 
         pages of TabControl */
        void LoadComboBox_Store(List<Store> st, ComboBox cmb) 
        {
            cmb.Items.Clear();
            foreach (var item in st)
            {
                cmb.Items.Add(item.StoreName + " , " + item.StoreAdress);
            }
        }

        //  LoadComboBox_Products(List<Clothes> cl, ComboBox cmb) method loads comboboxes with products
        void LoadComboBox_Products(List<Clothes> cl, ComboBox cmb) 
        {
            cmb.Items.Clear();
            foreach (var item in cl)
            {
                cmb.Items.Add(item.Name + " , " + item.ClothingSize + " , " + item.quantity);
            }
        }

        /* LoadComboBoxSize() method loads all comboboxes with sizes by converting enumeration into a string. Then
         I loop through this string and add each of its element into a combobox*/
        void LoadComboBoxSize()
        {
            string[] _size = Enum.GetNames(typeof(Clothes.sizes));
            cmbSize.Items.Clear();
            cmbSearchSize.Items.Clear();
            for (int i = 0; i < _size.Length; i++)
            {
                cmbSize.Items.Add(_size[i]);
                cmbSearchSize.Items.Add(_size[i]);
            }
        }
        /* SelectSize(ComboBox cmb) method helps to check which size in the combobox was selected. Instead of writing
         * this code two times as I have 2 comboboxes with size I decided to write a method for it.  */
        Clothes.sizes SelectSize(ComboBox cmb)
        {
            if (cmbSize.Items[0] == cmbSize.SelectedItem)
            {
                item_size = Clothes.sizes.XS;
            }
            if (cmbSize.Items[1] == cmbSize.SelectedItem)
            {
                item_size = Clothes.sizes.S;
            }
            if (cmbSize.Items[2] == cmbSize.SelectedItem)
            {
                item_size = Clothes.sizes.M;
            }
            if (cmbSize.Items[3] == cmbSize.SelectedItem)
            {
                item_size = Clothes.sizes.L;
            }
            if (cmbSize.Items[4] == cmbSize.SelectedItem)
            {
                item_size = Clothes.sizes.XL;
            }
            return item_size;
        }


        bool Check_SelectedCombo(ComboBox cmb) // This method checks if the user has selected an item in a combobox
        {
            bool check = false;
            if (cmb.SelectedItem == null) 
            {
                check = true;
            }
            return check;
        }
        /* this method is used to Load products purchased into a boutique. It checks if the user introduces already existent products into a boutique.
         If it is true then it updates the quantity by increasing it with the quantity introduced by the user. It will always
         add a new created product to the list when it is false.*/
        void LoadProducts(List<Clothes> Lists, Clothes x) 
        {
            bool findSameProduct = false;
            string prod_name = tbProductName.Text.ToUpper();
            foreach (Clothes s in Lists)
            {
                if (s.Name == prod_name && s.ClothingSize == item_size)
                {
                    findSameProduct = true;
                    s.IncreaseQuantity(ToInt(tbQuantity));
                }
            }
            while (findSameProduct = !findSameProduct)
            {
                aProduct = new Clothes(prod_name, item_size, ToInt(tbQuantity));
                Lists.Add(aProduct);
            }
        }

        // Method that converts string into int with TryParse function. It returns -1 in case value introduced is not a number
        int ToInt(TextBox tb) 
        {
            int i = 0;
            if (!Int32.TryParse(tb.Text, out i))
            {
                i = -1;
            }
            return i;
        }

        void ClearLog(TextBox tb) // clears all the textboxes
        {
            tb.Text = string.Empty;
        }

        /*SellClothes method is used to decrease quantity of a particular product that a user is going to sell.  
         * The logic I used here is that I compared if the list of products contains the string of the product one wants to 
         * sell. The reason why I used string manipulation method is that I tried just to compare items in the list of object Clothes
         * with a string product selected in a combobox and it could not compare an object with a string*/
        void SellClothes(List<Clothes> lst, string soldProduct, int soldQuantity)
        {
            string prod = "";
            for (int i = 0; i < lst.Count; i++)
            {
                    prod = (lst[i].Name + " , " + lst[i].ClothingSize +
                    " , " + lst[i].quantity).ToString().Trim();

                if (prod.Contains(soldProduct))
                {
                        listClothes[i].DecreaseQuantity(soldQuantity);
                }
            }
        }

        //=====================================================================================================

        private void btnBoutiques_Click(object sender, EventArgs e)
        {/* when button Boutiques is pressed TabPage for creating new boutiques is opened. I have tabcontrol but for 
            UX design I decided to hide them and to activate each page with boutons. */
            tabControlSystem.SelectedTab = tbBoutique;
            ClearLog(tbView);
        }

        private void btnPurchasing_Click(object sender, EventArgs e)
        {/* when button Storage is pressed TabPage for purchasing products is opened. */
            tabControlSystem.SelectedTab = tbPurchasing;
            label_Data.Text = now.ToLongDateString();
            labelTime.Text = now.ToLongTimeString();
            ClearLog(tbView);
        }

        private void btnSelling_Click(object sender, EventArgs e)
        { /* when button Selling is pressed TabPage for creating selling is opened. */
            tabControlSystem.SelectedTab = tbSelling;
            cmbSellingStores.SelectedIndex = -1;
            labDate.Text = now.ToLongDateString();
            labTime.Text = now.ToLongTimeString();
            ClearLog(tbView);
        }
        private void btnViewInfo_Click(object sender, EventArgs e)
        { /* when button View Info is pressed TabPage for viewing information is opened. */
            tabControlSystem.SelectedTab = tabViewInfo;
            LoadComboBox_Products(listClothes, cmbClothes);
            tbDecript.Text = "Select a boutique from the list below and press button SHOW" +
                " if you want to get info about selected bouique";
        }

        /// <summary>
        /// btnCreate_Click is used to create a new object of a store- i.e., when user introduces name and adress 
        /// of a store and clicks button Create it creates a new boutique object and adds it to all the
        /// comboboxes/listBox in the wfa. I start the below method  handling anomalous situations/exceptions first. 
        /// I took into consideration following exceptions: 
        /// 1. What if list of the stores already contains the same boutique that the user wants to introduce
        /// 2. What if the user does not introduce name of the store or adress and clicks button. Both of the field
        /// are obligatory because it can be a boutique with the same name but different adress. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            string butiq_name = tbButikName.Text.ToUpper(); //converts small letters into big so text is all the same
            string butiq_adress = tbButikAdress.Text.ToUpper();

            if (Check_for_duplicate(butiq_name, butiq_adress) == true)
            {
                MessageBox.Show("This boutique already exists in database!", "Important Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrEmpty(tbButikName.Text) || String.IsNullOrEmpty(tbButikAdress.Text))
            {
                MessageBox.Show("Exception - both fields are obligatory and can't be empty!", "Warning!!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                aStore = new Store(butiq_name, butiq_adress);
                MessageBox.Show(aStore.Message(), "Boutique", MessageBoxButtons.OK, MessageBoxIcon.None);

                listStores.Add(aStore);
                LoadComboBox_Store(listStores, cmbStores);
                LoadComboBox_Store(listStores, cmbSellingStores);
                LoadComboBox_Store(listStores, cmbStores1);
                listBoxStores.Items.Add(aStore.ListBoxItems());
            }

            ClearLog(tbButikName);
            ClearLog(tbButikAdress);
        }

        /// <summary>
        ///  btnAddProducts_Click is used to create a new object of Clothes, when user introduces name, size, quantity 
        ///  and selects boutique which purchased it. By clicking button it creates a new Clothes product and adds it to the
        /// combobox with all products in the wfa. I start the below method  handling anomalous situations/exceptions first. 
        /// I took into consideration following exceptions: 
        /// 1. What if list of products already contains the same product 
        /// 2. What if the user does not introduce name of the product and clicks button add. None of the field should be empty
        /// 3. Similarly, what if the user doesn't introduce size or introduces letters instead of numeric value in the quantity
        /// 4. What if the user clicks to add a new Product without selecting boutique to which he/she goes. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProducts_Click(object sender, EventArgs e)
        {
            string selectedStore = cmbStores.Text;
            string infoStorage = "";

            if (String.IsNullOrEmpty(tbProductName.Text) || String.IsNullOrEmpty(tbQuantity.Text)
                || Check_SelectedCombo(cmbSize) == true || Check_SelectedCombo(cmbStores) == true)
            {
                MessageBox.Show("Exception - all fields are obligatory and can't be empty!", "Warning!!!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ToInt(tbQuantity) == -1)
            {
                MessageBox.Show("Invalid Value of the quantity!", "Warning!!!",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                LoadProducts(listClothes, aProduct);
                LoadComboBox_Products(listClothes, cmbSellingProduct);

                for (int j = 0; j < listStores.Count; j++)
                {
                    if (listStores[j].StoreName + " , " + listStores[j].StoreAdress == selectedStore)
                    {
                        Store tem = listStores[j];

                        LoadProducts(tem.productList, aProduct);
                        infoStorage = tem.Show_allproducts();
                    }
                }
                MessageBox.Show("You have added new products to boutique " + selectedStore + Environment.NewLine + Environment.NewLine +
                    "Products currently existing in the warehouse are: " + Environment.NewLine + infoStorage, "Information");
            }
            ClearLog(tbProductName);
            ClearLog(tbQuantity);
            cmbSize.SelectedIndex = -1;
            cmbStores.SelectedIndex = -1;
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e) // when the user selects another item in the
            //combobox, method SlectSize helps to identify which size was selected
        {
            SelectSize(cmbSize);
        }

        private void tbProductName_TextChanged(object sender, EventArgs e)
        {
            this.Text = tbProductName.Text;
        }
        private void tbQuantity_TextChanged(object sender, EventArgs e)
        {
            this.Text = tbQuantity.Text;
        }

        /* By clicking button show in the view info page and selecting a boutique the user will get info about the
         products that exist in that boutique. Fist I check for exceptions like what if user doesn't select boutique*/
        private void btnShow_Click(object sender, EventArgs e) 
        {
            string selectedStore = cmbStores1.Text;
            string message = "";

            if (Check_SelectedCombo(cmbStores1) == true)
            {
                MessageBox.Show("Exception - you must select a boutique!", "Warning!!!",
                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                foreach (var item in listStores)
                {
                    if (item.StoreName + " , " + item.StoreAdress == selectedStore)
                    {
                        if (item.CheckIf_EmptyProductList() == true)
                        {
                            message = selectedStore + Environment.NewLine +
                            Environment.NewLine + "Stock is empty!";
                        }
                        else
                        {
                            message = selectedStore + Environment.NewLine + item.Show_allproducts() + Environment.NewLine;

                        }
                    }
                }
                MessageBox.Show(message);
            }
            
            cmbStores1.SelectedItem = null; 
        }

        /// <summary>
        ///  btnSell_Click is used to sell an object of Clothes, when user introduces name, size, and quantity. When the user
        ///  selects a boutique from which it wants to sell the combobox of products automatically loads with all the products
        ///  and their quantities respectively. When the user selects the product and writes what quantity it wants to sell then
        ///  he/she can perform selling procedure by reducing products quantity both in the general list of products as 
        ///  well as in the list of products of a particular boutique. 
        /// I took into consideration following exceptions: 
        /// 1. What if a boutique is not created 
        /// 2. what if a product is not selected
        /// 3. what if quantity is not written or is of invalid format
        /// 4. what if the quantity introduced is bigger then the quantity of the product in the stock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSell_Click(object sender, EventArgs e)
        {
            int selStore = cmbSellingStores.SelectedIndex;
            int selClothe = cmbSellingProduct.SelectedIndex;
            
            bool startSell = false;

            if (String.IsNullOrEmpty(tbSellingQuantity.Text) || Check_SelectedCombo(cmbSellingStores) == true)

            {
                MessageBox.Show("Exception - all fields are obligatory and can't be empty!", "Warning!!!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Check_SelectedCombo(cmbSellingProduct) == true || this.cmbSellingProduct.SelectedItem == null)
            {
                MessageBox.Show("This boutique does not have products!", "Attention!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else if (ToInt(tbSellingQuantity) == -1)
            {
                MessageBox.Show("Invalid value! Quantity should be numeric", "Warning!!!",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (listStores[selStore].productList[selClothe].quantity < ToInt(tbSellingQuantity))
            {
                MessageBox.Show("This boutique does not have enough of this products in stock", "Warning!!!",
                 MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            else
            {
                startSell = true;
            }


            if (startSell == true)
            {
                string TrimProd = cmbSellingProduct.Text.Trim();
                int lastindexofcomma = TrimProd.LastIndexOf(",");
                string searchedName = TrimProd.Substring(0, lastindexofcomma);
                SellClothes(listClothes, searchedName, ToInt(tbSellingQuantity));

                Store temp = listStores[selStore];

                if (temp.productList[selClothe].Name + " , " + temp.productList[selClothe].ClothingSize +
                    " , " + temp.productList[selClothe].quantity == cmbSellingProduct.Text)
                {
                    temp.productList[selClothe].DecreaseQuantity(ToInt(tbSellingQuantity));

                    MessageBox.Show(cmbSellingStores.Text + " has sold " + tbSellingQuantity.Text + " pc of " +
                        temp.productList[selClothe].Name + " , " + temp.productList[selClothe].ClothingSize + Environment.NewLine +
                        Environment.NewLine + "Boutique's current stock is: " + Environment.NewLine + temp.Show_allproducts(), "Information");
                }
            }
            ClearLog(tbSellingQuantity);

            cmbSellingProduct.SelectedIndex = -1;
            cmbSellingStores.SelectedIndex= -1;
        }
    


        private void cmbSellingStores_SelectedIndexChanged(object sender, EventArgs e) /* if the user selected a store in the
            Selling produsts page then it automatically loades another combobox with that store products*/
        {
            cmbSellingProduct.Items.Clear();
            int index = cmbSellingStores.SelectedIndex;
            if (index > -1)
            {
                Store temporary = listStores[index];
                LoadComboBox_Products(temporary.productList, cmbSellingProduct);
            }
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            int nrofStores = 0, nrofProducts = 0;
            foreach (var item in listStores)
            {
                nrofStores++;
            }

            foreach (var plugg in listClothes)
            {
                nrofProducts += plugg.quantity;
            }
            tbView.Text = "Today " + now.ToShortDateString() + Environment.NewLine + 
                "Current number of boutiques in the database is: " + nrofStores + Environment.NewLine +
                "Total number of products in all the boutiques is: " + nrofProducts;       
        }

        private void tbView_TextChanged(object sender, EventArgs e)
        {
            tbView.ScrollBars = ScrollBars.Vertical;
        }

        /*Find Product button allows user to search for a particular product and size and find out in which boutiques
         it exists and how much in stock of the searched product there is. I use again here string manipulation to search 
         for a product. If both fields (product name/ and size) are empty message with exception is coming out. */
        private void btnFindProd_Click(object sender, EventArgs e)
        {
            bool found = false;
            bool startSearching = false;
            string product = "";
            string message = "";
            string findProd = (tbSearchProduct.Text + " , " + cmbSearchSize.Text).Trim().ToUpper();

            if (String.IsNullOrEmpty(tbSearchProduct.Text) || Check_SelectedCombo(cmbSearchSize) == true)

            {
                
                MessageBox.Show("Exception - all fields are obligatory and can't be empty!", "Warning!!!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else
            {
                startSearching = true;
            }


            if (startSearching == true)
            {
                for (int i = 0; i < listStores.Count; i++)
                {
                    Store temp = listStores[i];

                    for (int j = 0; j < temp.productList.Count; j++)
                    {

                        product = (temp.productList[j].Name + " , " + temp.productList[j].ClothingSize +
                        " , " + temp.productList[j].quantity).ToString().Trim();

                        if (product.Contains(findProd))
                        {
                            found = true;
                            message += temp.StoreName + " , " + temp.StoreAdress +
                                " --- with the stock of " + temp.productList[j].quantity + "\n";
                        }
                    }
                }

                if (found == true)
                {
                    MessageBox.Show("We have found your product in the following boutiques: " + "\n" + message);
                }
                else
                {
                    MessageBox.Show("The product you searched was not find");
                }
            }

            ClearLog(tbSearchProduct);
            cmbSearchSize.SelectedIndex = -1;

        }
    }
}
    

