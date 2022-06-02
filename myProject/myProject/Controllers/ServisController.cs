using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Drawing;
using System.Web.Http;
using myProject.Models;
using myProject.ViewModel;

namespace myProject.Controllers
{
    public class ServisController : ApiController
    {

        DB01Entities db = new DB01Entities();
        SonucModel sonuc = new SonucModel();

        [HttpGet]
        [Route("api/adminliste")]
        public List<AdminModel> AdminListe()
        {
            List<AdminModel> liste = db.Admin.Select(x => new AdminModel()
            {
                adminId = x.adminId,
                adminKatId= x.adminKatId,
                adminAdi = x.adminAdi,
                adminSifre = x.adminSifre,
                adminSoyadi = x.adminSoyadi,
            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/adminbyid/{adminId}")]
        public AdminModel AdminById(int adminId)
        {
            AdminModel kayit = db.Admin.Where(s => s.adminId == adminId).Select(x => new AdminModel()
            {
                adminId = x.adminId,
                adminKatId = x.adminKatId,
                adminAdi = x.adminAdi,
                adminSifre = x.adminSifre,
                adminSoyadi = x.adminSoyadi,
            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/adminekle")]
        public SonucModel AdminEkle(AdminModel model)
        {
            if (db.Admin.Count(s => s.adminAdi == model.adminAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kullanıcı Adı Kayıtlıdır!";
                return sonuc;
            }

            Admin yeni = new Admin();
            yeni.adminId = model.adminId;
            yeni.adminKatId = model.adminKatId;
            yeni.adminAdi = model.adminAdi;
            yeni.adminSifre = model.adminSifre;
            yeni.adminSoyadi = model.adminSoyadi;
            

            db.Admin.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/adminduzenle")]
        public SonucModel AdminDuzenle(AdminModel model)
        {
            Admin kayit = db.Admin.Where(s => s.adminId == model.adminId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            kayit.adminId = model.adminId;
            kayit.adminKatId = model.adminKatId;
            kayit.adminAdi = model.adminAdi;
            kayit.adminSifre = model.adminSifre;
            kayit.adminSoyadi = model.adminSoyadi;

            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Düzenlendi";

            return sonuc;
        }

        [HttpDelete]
        [Route("api/adminsil/{adminId}")]
        public SonucModel AdminSil(int adminId)
        {
            Admin kayit = db.Admin.Where(s => s.adminId == adminId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }

            

            db.Admin.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";
            return sonuc;
        }


        [HttpGet]
        [Route("api/dersliste")]
        public List<DersModel> DersListe()
        {
            List<DersModel> dersliste = db.Ders.Select(x => new DersModel()
            {
                dersId = x.dersId,
                dersAdi = x.dersAdi,
                dersKatId = x.dersKatId,
                dersKredi = x.dersKredi,
                dersOgrSayisi = x.Kayit.Count()

            }).ToList();

            return dersliste;
        }

        [HttpGet]
        [Route("api/dersbyid/{dersId}")]
        public DersModel DersById(int dersId)
        {
            DersModel kayit = db.Ders.Where(s => s.dersId == dersId).Select(x => new DersModel()
            {
                dersId = x.dersId,
                dersAdi = x.dersAdi,
                dersKatId = x.dersKatId,
                dersKredi = x.dersKredi,
                dersOgrSayisi = x.Kayit.Count()
            }).SingleOrDefault();
            return kayit;
        }

        [HttpGet]
        [Route("api/odevliste")]
        public List<OdevModel> OdevListe()
        {
            List<OdevModel> odevliste = db.Odev.Select(x => new OdevModel()
            {
                odevAdi = x.odevAdi,
                odevId = x.odevId,
                odevKatId = x.odevKatId
            }).ToList();

            return odevliste;
        }

        [HttpGet]
        [Route("api/odevbyid/{odevId}")]
        public OdevModel OdevById(int odevId)
        {
            OdevModel kayit = db.Odev.Where(s => s.odevId == odevId).Select(x => new OdevModel()
            {
                odevAdi = x.odevAdi,
                odevId = x.odevId,
                odevKatId = x.odevKatId
            }).FirstOrDefault();
            return kayit;
        }

        [HttpGet]
        [Route("api/ogrenciliste")]

        public List<OgrenciModel> OgrenciListe()
        {
            List<OgrenciModel> ogrenciliste = db.Ogrenci.Select(x => new OgrenciModel
            {
                ogrAdi = x.ogrAdi,
                ogrAciklama = x.ogrAciklama,
                ogrId = x.ogrId,
                ogrKatId = x.ogrKatId,
                ogrNo = x.ogrNo,
                ogrSoyadi = x.ogrSoyadi,
                ogrDersSayisi = x.Kayit.Count()
            }).ToList();
            return ogrenciliste;

        }

        [HttpGet]
        [Route("api/ogrencibyid/{ogrId}")]
        public OgrenciModel OgrenciById(int ogrId)
        {
            OgrenciModel kayit = db.Ogrenci.Where(s => s.ogrId == ogrId).Select(x => new OgrenciModel()
            {
                ogrAdi = x.ogrAdi,
                ogrAciklama = x.ogrAciklama,
                ogrId = x.ogrId,
                ogrKatId = x.ogrKatId,
                ogrNo = x.ogrNo,
                ogrSoyadi = x.ogrSoyadi,
                ogrDersSayisi = x.Kayit.Count()
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/ogrenciekle")]

        public SonucModel OgrenciEkle(OgrenciModel model)
        {
            if (db.Ogrenci.Count(s => s.ogrNo == model.ogrNo) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Öğrenci Numarası Kayıtlıdır!";
                return sonuc;
            }
            Ogrenci yeni = new Ogrenci();
            yeni.ogrNo = model.ogrNo;
            yeni.ogrAciklama = model.ogrAciklama;
            yeni.ogrAdi = model.ogrAdi;
            yeni.ogrSoyadi = model.ogrSoyadi;
            db.Ogrenci.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Eklendi.";

            return sonuc;
        }

        [HttpPost]
        [Route("api/odevekle")]

        public SonucModel OdevEkle(OdevModel model)
        {
            if (db.Odev.Count(s => s.odevAdi == model.odevAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ödev Adı Kayıtlıdır!";
                return sonuc;
            }

            Odev yeni = new Odev();
            yeni.odevAdi = model.odevAdi;
            db.Odev.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ödev Eklendi.";

            return sonuc;
        }

        [HttpPost]
        [Route("api/kayitekle")]
        public SonucModel KayitEkle(KayitModel model)
        {
            if (db.Kayit.Count(c => c.kayitDersId == model.kayitDersId & c.kayitOgrId == model.kayitOgrId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrenci Ders Kaydı Önceden Yapılmıştır!";
                return sonuc;
            }

            Kayit yeni = new Kayit();
            yeni.kayitOgrId = model.kayitOgrId;
            yeni.kayitDersId = model.kayitDersId;
            db.Kayit.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kayıt Eklendi";
            return sonuc;
        }

        [HttpPost]
        [Route("api/dersekle")]

        public SonucModel DersEkle(DersModel model)
        {
            if (db.Ders.Count(s => s.dersAdi == model.dersAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ders Adı Kayıtlıdır!";
                return sonuc;
            }

            Ders yeni = new Ders();
            yeni.dersAdi = model.dersAdi;
            yeni.dersKredi = model.dersKredi;
            db.Ders.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ders eklendi.";

            return sonuc;
        }

        [HttpPut]
        [Route("api/odevduzenle")]

        public SonucModel OdevDuzenle(OdevModel model)
        {
            Odev kayit = db.Odev.Where(s => s.odevId == model.odevId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ödev Bulunamadı!";
                return sonuc;
            }

            kayit.odevAdi = model.odevAdi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ödev Düzenlendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/dersduzenle")]

        public SonucModel DersDuzenle(DersModel model)
        {
            Ders kayit = db.Ders.Where(s => s.dersId == model.dersId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ders Bulunamadı!";
                return sonuc;
            }

            kayit.dersAdi = model.dersAdi;
            kayit.dersKredi = model.dersKredi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ders Düzenlendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/ogrenciduzenle")]

        public SonucModel OgrenciDuzenle(OgrenciModel model)
        {
            Ogrenci kayit = db.Ogrenci.Where(s => s.ogrId == model.ogrId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = true;
                sonuc.mesaj = "Öğrenci Düzenlendi!";
                return sonuc;
            }

            kayit.ogrAciklama = model.ogrAciklama;
            kayit.ogrAdi = model.ogrAdi;
            kayit.ogrNo = model.ogrNo;
            kayit.ogrSoyadi = model.ogrSoyadi;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/odevsil/{odevId}")]

        public SonucModel OdevSil(int odevId)
        {
            Odev kayit = db.Odev.Where(s => s.odevId == odevId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ödev Bulunamadı!";
                return sonuc;
            }

            db.Odev.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ödev Silindi.";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/derssil/{dersId}")]

        public SonucModel DersSil(int dersId)
        {
            Ders kayit = db.Ders.Where(s => s.dersId == dersId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ders Bulunamadı!";
                return sonuc;
            }

            db.Ders.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ders Silindi.";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/ogrencisil/{ogrId}")]

        public SonucModel OgrenciSil(int ogrId)
        {
            Ogrenci kayit = db.Ogrenci.Where(s => s.ogrId == ogrId).FirstOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Öğrenci Bulunamadı!";
                return sonuc;
            }

            db.Ogrenci.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Öğrenci Silindi.";
            return sonuc;
        }
        [HttpGet]
        [Route("api/ogrencidersliste/{ogrId}")]
        public List<KayitModel> OgrenciDersListe(int ogrId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitOgrId == ogrId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitDersId = x.kayitDersId,
                kayitOgrId = x.kayitOgrId
            }).ToList();

            foreach (var kayit in liste)
            {
                kayit.ogrBilgi = OgrenciById(kayit.kayitOgrId);
                kayit.dersBilgi = DersById(kayit.kayitDersId);

            }
            return liste;

        }
        [HttpGet]
        [Route("api/dersogrenciliste/{dersId}")]
        public List<KayitModel> DerOgrencisListe(int dersId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitDersId == dersId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitDersId = x.kayitDersId,
                kayitOgrId = x.kayitOgrId
            }).ToList();

            foreach (var kayit in liste)
            {
                kayit.ogrBilgi = OgrenciById(kayit.kayitOgrId);
                kayit.dersBilgi = DersById(kayit.kayitDersId);

            }
            return liste;
        }
    }
}
