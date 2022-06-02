using myProject.Models;
using myProject.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myProject.Auth
{
    public class AdminService
    {
        DB01Entities db = new DB01Entities();


        public AdminModel AdminOturumAc(string kadi, string parola)
        {
            AdminModel admin = db.Admin.Where(s => s.adminAdi == kadi && s.adminSifre == parola).Select(x => new AdminModel() {
                adminAdi = x.adminAdi,
                adminId = x.adminId,
                adminKatId = x.adminKatId,
                adminSifre = x.adminSifre,
                adminSoyadi = x.adminSoyadi

            }).SingleOrDefault();
            return admin;
        }
    }
}