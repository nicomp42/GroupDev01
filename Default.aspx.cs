using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page {

    static Group01 group01;
    static Group02 group02;
    static Group03 group03;
    static Group04 group04;
    static Group05 group05;
    protected void Page_Load(object sender, EventArgs e)
    {
        instantiateObjects();
    }

    private void instantiateObjects()
    {
        group01 = new Group01();
        group02 = new Group02();
        group03 = new Group03();
        group04 = new Group04();
        group05 = new Group05();
    }

    private void Group01Method()
    {
        int productIDWithMostReturns = group01.GetProductIDWithTheMostReturns();


    }
}