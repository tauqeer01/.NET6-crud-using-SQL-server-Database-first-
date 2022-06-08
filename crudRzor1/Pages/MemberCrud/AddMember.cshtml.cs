using crudRzor1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace crudRzor1.Pages.MemberCrud
{
    public class AddMemberModel : PageModel
    {
        public MemberInfo memberInfo = new MemberInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            memberInfo.Name = Request.Form["name"];
            memberInfo.Email = Request.Form["email"];
            memberInfo.Password = Request.Form["password"];
            memberInfo.Address = Request.Form["address"];
            if (memberInfo.Name.Length == 0 || memberInfo.Email.Length == 0 || memberInfo.Password.Length == 0 || memberInfo.Address.Length == 0)
            {
                errorMessage = "all field are required";
                return;
            }
            try
            {
                string connection = "Data source=(localdb)\\mssqllocaldb;Database=CrudRazor;Trusted_Connection=True;";
                using (SqlConnection Con = new SqlConnection(connection))
                {
                    Con.Open();
                    string sql = "Insert into memberMaster" + "(Name,Email, Password, Address) values"  +
                        "(@Name , @Email, @password,@Address)";
                    using (SqlCommand cmd = new SqlCommand(sql, Con))
                    {
                  
                        cmd.Parameters.AddWithValue("@Name", memberInfo.Name);
                        cmd.Parameters.AddWithValue("@Email", memberInfo.Email);
                        cmd.Parameters.AddWithValue("@Password", memberInfo.Password);
                        cmd.Parameters.AddWithValue("@address", memberInfo.Address);
                        cmd.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            memberInfo.Name = "";
            memberInfo.Email = "";
            memberInfo.Password = "";
            memberInfo.Address = "";
            successMessage = "new member added successfully";
            Response.Redirect("/MemberCrud/MemberList");
        }
    }
}
