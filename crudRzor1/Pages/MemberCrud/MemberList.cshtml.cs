using crudRzor1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace crudRzor1.Pages.MemberCrud
{
    public class MemberListModel : PageModel
    {
        public List<MemberInfo> memberInfos = new List<MemberInfo>();
       

        public void OnGet()
        {
            try
            {
                string connection = "Data source=(localdb)\\mssqllocaldb;Database=CrudRazor;Trusted_Connection=True;";
                using (SqlConnection con = new SqlConnection(connection))
                {
                    string sql = "Select * from memberMaster";
                    using (SqlCommand command = new SqlCommand(sql, con))
                    {
                        con.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                MemberInfo memberInfo = new MemberInfo();
                                memberInfo.Id = "" + reader.GetInt32(0);
                                memberInfo.Name = reader.GetString(1);
                                memberInfo.Email = reader.GetString(2);
                                memberInfo.Address = reader.GetString(4);
                                memberInfo.Created_at = reader.GetDateTime(5).ToString();
                              
                                memberInfos.Add(memberInfo);
                            }
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
    

