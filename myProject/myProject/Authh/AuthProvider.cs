using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace myProject.Auth
{

    public class AuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" }); // Farklı domainlerden istek sorunu yaşamamak için

            //Burada kendi authentication yöntemimizi belirleyebiliriz.Veritabanı bağlantısı vs...
            var adminServis = new AdminService();
            var admin = adminServis.AdminOturumAc(context.UserName, context.Password);
            List<string> adminYetkileri = new List<string>();

            if (admin != null)
            {
                string yetki = "";
                if (admin.adminKatId == 1)
                {
                    yetki = "Admin";

                }

                else
                {
                    yetki = "Uye";
                }
                adminYetkileri.Add(yetki);

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                identity.AddClaim(new Claim(ClaimTypes.Role, yetki));
                identity.AddClaim(new Claim(ClaimTypes.PrimarySid, admin.adminId.ToString()));

                AuthenticationProperties propert = new AuthenticationProperties(new Dictionary<string, string>
                {
                    { "adminId", admin.adminId.ToString() },
                    { "adminAdi", admin.adminAdi},
                    { "adminYetkileri",Newtonsoft.Json.JsonConvert.SerializeObject(adminYetkileri) }

               });
                AuthenticationTicket ticket = new AuthenticationTicket(identity, propert);


                context.Validated(ticket);
            }
            else
            {
                context.SetError("Geçersiz istek", "Hatalı kullanıcı bilgisi");
            }



        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}
