using myProject.Models;
using myProject.ViewModel;
using System.Linq;

namespace myProject.Auth
{
    public class AdminService
    {
        DB01Entities db = new DB01Entities();


        public AdminModel AdminOturumAc(string kadi, string parola)
        {
            AdminModel admin = db.Admin.Where(s => s.adminAdi == kadi && s.adminSifre == parola).Select(x => new AdminModel()
            {
                adminId = x.adminId,
                adminKatId = x.adminKatId,
                adminAdi = x.adminAdi,
                adminSoyadi = x.adminSoyadi,
                adminSifre = x.adminSifre,
                
            }).SingleOrDefault();
            return admin;

        }
    }
}
