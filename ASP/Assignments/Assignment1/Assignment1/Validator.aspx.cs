using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment1
{
    public partial class Validator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
        }

        protected void cvFamilyDiff_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = !txtName.Text.Trim().Equals(txtFamily.Text.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Response.Write("<script>alert('All validations passed!');</script>");
            }
        }
    }
}
