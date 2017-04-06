/*
 * Group Project for IT3047
 * Bill Nicholson
 * nicholdw@ucmauil.uc.edu
 * Edited by: Richard McDonald & Kipp Silber
 * mcdonarf@mail.uc.edu & silberkd@mail.uc.edu
 */
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Collections;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Get IDs of all stores given a State
/// </summary>
public class Group04 {
    private static SqlConnection conn;
    private static SqlDataReader reader;
    private static SqlCommand comm;

    public Group04() {
    }
    public List<int> GetIDsOfAllStoresInAState(string stateID) {
        try {
            OpenConnection("GroceryStoreSimulatorConnectionString");
        }
        catch (Exception) {
            throw new Exception("Group04.GetIDsOfAllStoresInAState(string stateID) connection failed to open.");
        }
        return LoadIndianaStoreList(stateID);
    }

    //Opens connection to GroceryStore db
    public void OpenConnection(string connStrName) {
        System.Configuration.ConnectionStringSettings strConn;
        strConn = ReadConnectionString(connStrName);
        conn = new SqlConnection(strConn.ConnectionString);
        conn.Open();
    }

    private System.Configuration.ConnectionStringSettings ReadConnectionString(string connStrName) {
        string strPath;
        strPath = HttpContext.Current.Request.ApplicationPath + "/Web.config";
        System.Configuration.Configuration rootWebConfig =
            System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(strPath);

        System.Configuration.ConnectionStringSettings connString = null;

        if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0) {
            connString = rootWebConfig.ConnectionStrings.ConnectionStrings[connStrName];

            if (connString != null) {
                Console.WriteLine("connection string = \"{0}\"",
                    connString.ConnectionString);
            }
            else {
                Console.WriteLine("No connection string");
            }
        }
        return connString;
    }

    public List<int> LoadIndianaStoreList(string stateID) {
        //List of StoreID's to be returned.
        List<int> tmpList = new List<int>();

        try { reader.Close(); }
        catch { }
        comm = new SqlCommand("SELECT StoreID FROM dbo.tStore WHERE(State = @ID) GROUP BY StoreID", conn);
        comm.Parameters.Add("@ID", SqlDbType.NChar);
        comm.Parameters["@ID"].Value = stateID;
        reader = comm.ExecuteReader();

        if (reader.HasRows) {
            while (reader.Read()) {
                int storeID;
                storeID = Convert.ToInt32(reader["StoreID"]);
                tmpList.Add(storeID);
            }
        }
        return tmpList;
    }
}