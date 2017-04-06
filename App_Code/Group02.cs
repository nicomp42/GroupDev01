/*
 * Group Project for IT3047
 * Bill Nicholson
 * nicholdw@ucmauil.uc.edu
 * 
 * /***********************************************************************************************************************************************************************************************
 * Assignment 10
 * Adam Ralston (ralstoat@mail.uc.edu) and Andrew Polley (polleyaw@mail.uc.edu)
 * IT3047C Web Server App Dev
 * The purpose of this assignment was to define a method that will return the Store ID with the most total sales by dollar amount.
 * Due Date: 3//2017
 *
 * Citations: Skeleton of the undefined method and boilerplate was provided by Professor Bill Nicholson.
 **********************************************************************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Get Store ID with the most total sales by dollar amount
/// </summary>
public class Group02 {

    private static System.Data.SqlClient.SqlConnection connection;
    private static SqlCommand command;
    private static SqlDataReader reader;
    public Group02() {
        
    }
    public int GetStoreIDWithTheMostTotalSales() {

        // Opens the connection to the database.
        openConnection();

        // Defines the query.
        string query = "select top 1 st.StoreID, sum(qtyOfProduct * PricePerSellableUnitAsMarked) as TotalSales from tStore st join tTransaction tr on tr.StoreID = st.StoreID join tTransactionDetail td on td.TransactionID = tr.TransactionID group by st.StoreID order by TotalSales desc";

        // Stores the results of the query.
        int ids = 0;

        // Establishes the command for the given query on the connection.
        command = new SqlCommand(query, connection);

        // Attempts to read from the database.
        try
        {
            // Reads from the database.
            reader = command.ExecuteReader();

            // Loops through all items that match the query in the database.
            while (reader.Read())
            {
                // Stores the returns.
                ids = reader.GetInt32(0);
            }

            // Attempts to close the reader.
            try { reader.Close(); }
            // Eats any exceptions if there is an issue closing the reader.
            catch (Exception ex)
            {
            }
        }
        // Eats any exceptions if there was an issue reading from the database.
        catch (Exception ex)
        {

        }
        return ids;
    }


    // Defines the method to open a connection to the database.
    private void openConnection()
    {
        try
        {
            // Creates a connection to the database that can be opened or closed by utilizing the connection string.
            connection = new System.Data.SqlClient.SqlConnection(GetConnectionString("GroceryStoreSimulatorConnectionString").ConnectionString);
            // Opens the connection to execute queries on the database.
            connection.Open();
        }
        // Eats any exceptions.
        catch (Exception ex)
        {
           
        }
    }

    // Defines the method to obtain the connection string from the web.config file.
    private System.Configuration.ConnectionStringSettings GetConnectionString(string nameOfString)
    {
        String path;
        // Establishes the path to the file.
        path = "/Web.config";
        // Obtains the connection string.
        System.Configuration.Configuration webConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(path);
        // Returns the connection string.
        return webConfig.ConnectionStrings.ConnectionStrings[nameOfString];
    }

}