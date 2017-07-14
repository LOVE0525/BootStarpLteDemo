using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LteDemo.Models
{
   
    public class UserModels
    {
        public string UserName { get; set; }
        public string Phone { get; set; }
    }

}