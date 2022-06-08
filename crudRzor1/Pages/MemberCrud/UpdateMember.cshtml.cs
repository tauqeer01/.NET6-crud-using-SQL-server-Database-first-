using crudRzor1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace crudRzor1.Pages.MemberCrud
{
    public class UpdateMemberModel : PageModel
    {
        public MemberInfo memberInfo = new MemberInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                string CS = "Data source=(localdb)\\mssqllocaldb;Database=CrudRazor;Trusted_Connection=True;";
                using (SqlConnection Con = new SqlConnection(CS))
                {
                    Con.Open();
                    string sql = "Select * from memberMaster where id=@id";
                    using (SqlCommand cmd = new SqlCommand(sql, Con))
                    {
                        // cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            if (reader.Read())
                            {
                                memberInfo.Id = "" + reader.GetInt32(0);
                                memberInfo.Name = reader.GetString(1);
                                memberInfo.Email = reader.GetString(2);
                                memberInfo.Password = reader.GetString(3);
                                memberInfo.Address = reader.GetString(4);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }


        public void OnPost()
        {
            memberInfo.Id = Request.Form["Id"];
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
                    string sql = "Update memberMaster set Name=@Name , Email=@Email, Password=@Password , Address=@Address where Id=@Id";
                    using (SqlCommand cmd = new SqlCommand(sql, Con))
                    {
                        
                        cmd.Parameters.AddWithValue("@Name", memberInfo.Name);
                        cmd.Parameters.AddWithValue("@Email", memberInfo.Email);
                        cmd.Parameters.AddWithValue("@Password", memberInfo.Password);
                        cmd.Parameters.AddWithValue("@address", memberInfo.Address);
                        cmd.Parameters.AddWithValue("@Id", memberInfo.Id);
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

