/**
Connor Tellep
Jake Reilman
In class github assignment
4/5/17
Web Server App Dev
IT3047C
*/

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class _Default : System.Web.UI.Page
{

    //static variables to hold sql connection, query and reader information
    private static System.Data.SqlClient.SqlConnection conn;
    private static SqlCommand comm;
    private static SqlDataReader reader;

    //static objects to allow methods to be called
    static Group01 group01;
    static Group02 group02;
    static Group03 group03;
    static Group04 group04;
    static Group05 group05;
    protected void Page_Load(object sender, EventArgs e)
    {
        //only run this code once
        if (!IsPostBack)
        {
            OpenConnection();
            instantiateObjects();
            PopulateStateDropdown();
            Group01Method();
            Group02Method();
            Group03Method();
        }
    }

    /**
    * instantiates all of the objects required to call methods
    */
    private void instantiateObjects()
    {
        group01 = new Group01();
        group02 = new Group02();
        group03 = new Group03();
        group04 = new Group04();
        group05 = new Group05();


    }

    //takes a the product ID of the product with the most returns and displays the name and description of the product
    private void Group01Method()
    {
        int productIDWithMostReturns = 0;

        //method can throw an exception
        try
        {
            productIDWithMostReturns = group01.GetProductIDWithTheMostReturns();
        }
        catch (Exception ex)
        {
            //debug statement
            //Response.Write(ex.StackTrace);
        }

        //only runs if the method above succeeds
        if (productIDWithMostReturns != 0)
        {
            comm = new SqlCommand("SELECT tProduct.Description, tName.Name FROM tProduct INNER JOIN tName ON tProduct.NameID = tName.NameID WHERE(tProduct.ProductID = " + productIDWithMostReturns + ")", conn);
            //try to close the reader in case it's stil open, do nothing if we can't
            try
            {
                reader.Close();
            }
            catch (Exception ex)
            { }

            //executes a query
            reader = comm.ExecuteReader();

            //sets reader to the data row
            reader.Read();

            //displays the query data in a label
            lblProductWithMostReturns.Text = reader.GetString(0) + reader.GetString(1);
        }
    }

    private void Group02Method()
    {
        int storeIDWithMostSales = 0;

        //method can throw an exception
        try
        {
            storeIDWithMostSales = group02.GetStoreIDWithTheMostTotalSales();
        }
        catch (Exception ex)
        {
            //debug statement
            //Response.Write(ex.StackTrace);
        }

        if (storeIDWithMostSales != 0)
        {
            comm = new SqlCommand("SELECT Store FROM tStore WHERE StoreID = " + storeIDWithMostSales, conn);
            //try to close the reader in case it's stil open, do nothing if we can't
            try
            {
                reader.Close();
            }
            catch (Exception ex)
            { }

            //executes query
            reader = comm.ExecuteReader();
            
            //sets reader to first data row
            reader.Read();

            //displays query data in a label
            lblStoreWithMostSales.Text = reader.GetString(0);
        }
    }

    private void Group03Method()
    {
        
        int emplIDWorkedMostDays = 0;

        try
        {
            emplIDWorkedMostDays = group03.GetEmplIDWhoWorkedTheMostDays();
        }
        catch (Exception ex)
        {
            //debug statement
            //Response.Write(ex.StackTrace);
        }

        if (emplIDWorkedMostDays != 0)
        {
            comm = new SqlCommand("SELECT FirstName, LastName FROM tEmpl WHERE EmplID =" + emplIDWorkedMostDays, conn);
            //try to close the reader in case it's stil open, do nothing if we can't
            try
            {
                reader.Close();
            }
            catch (Exception ex)
            { }

            //executes query
            reader = comm.ExecuteReader();

            //sets reader to first data row
            reader.Read();

            //displays query data in a label
            lblEmployeeWhoWorkedMost.Text = reader.GetString(0);
        }
    }


    private void OpenConnection()
    {
        //creates a configuration setting object for the connection string
        System.Configuration.ConnectionStringSettings strConn;
        //sets the value of the connections setting object to the connection string
        strConn = ReadConnectionString();

        //initializes the connection object with the value of our connection string
        conn = new System.Data.SqlClient.SqlConnection(strConn.ConnectionString);

        // This could go wrong in so many ways...
        try
        {
            conn.Open();
        }
        catch (Exception ex)
        {
            // Miserable error handling...
            Response.Write(ex.Message);
        }
    }

    /**
     * Returns a settings object that holds the connection string for the database
     */
    private System.Configuration.ConnectionStringSettings ReadConnectionString()
    {
        //string to store the path so the web.config file
        String strPath;
        strPath = HttpContext.Current.Request.ApplicationPath + "/web.config";

        //creates an object that points to the web.config file
        System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(strPath);

        System.Configuration.ConnectionStringSettings connString = null;

        //if the connection string is present, sets the object to equal the connection string in the web.config file
        if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
        {
            connString = rootWebConfig.ConnectionStrings.ConnectionStrings["GroceryStoreSimulatorConnectionString"];
        }

        //returns our connection string settings object
        return connString;
    }

    private void PopulateStateDropdown()
    {
        //variables to hold the data returned by the query and add it to the dropdown menu
        string state;
        ListItem stateItem;

        // Clear the list box, in case we've already loaded something into it.
        ddlStates.Items.Clear();
        // create sql command object with the open connection object
        comm = new SqlCommand("SELECT DISTINCT State FROM tStore", conn);
        //try to close the reader in case it's stil open, do nothing if we can't
        try
        {
            reader.Close();
        }
        catch (Exception ex)
        {}

        //use the reader object to execuet our query
        reader = comm.ExecuteReader();

        //iterate through the dataset line by line
        while (reader.Read())
        {
            //stores the name of the state
            state = reader.GetString(0);
            //creates a list item with the text of the name of the state, and the value of the primary key of the state
            stateItem = new ListItem(state);
            //adds the item to the dropdown menu
            ddlStates.Items.Add(stateItem);
        }
    }

    protected void btnSelectState_Click(object sender, EventArgs e)
    {
        //variables to hold the data returned by the query and add it to the dropdown menu
        List<int> StoreIDs = null;

        try
        {
            StoreIDs = group04.GetIDsOfAllStoresInAState(ddlStates.Text);
        }
        catch(Exception ex)
        {
            //debug statement
            //Response.Write(ex.StackTrace);
        }
        //clears the listbox of any items inside
        lbStores.Items.Clear();

        //iterate through the dataset line by line
        foreach (int StoreID in StoreIDs)
        {
            ListItem storeItem;
            comm = new SqlCommand("SELECT Store FROM tStore WHERE StoreID = '" + StoreID + "'", conn);
            //try to close the reader in case it's stil open, do nothing if we can't
            try
            {
                reader.Close();
            }
            catch (Exception ex)
            {}

            //use the reader object to execuet our query
            reader = comm.ExecuteReader();
            reader.Read();
            string store = reader.GetString(0);
            storeItem = new ListItem(store);
            lbStores.Items.Add(storeItem);
        }
    }
}
