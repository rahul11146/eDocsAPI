using System.ComponentModel.DataAnnotations;

namespace eDocsAPI.Models
{
    public class StoredProcedures
    {
        public const string SP_Project_UPSERT = "[dbo].[SP_Project_UPSERT]";
        public const string SP_Project_GetList = "[dbo].[SP_Project_GetList]";

        public const string SP_User_GetById = "[dbo].[SP_User_GetById]";
    }
}
