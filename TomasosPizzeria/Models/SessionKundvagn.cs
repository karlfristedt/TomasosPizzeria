using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace TomasosPizzeria.Models
{
    public class SessionKundvagn : Kundvagn
    {
        public static Kundvagn GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            if (session.GetString("MinKundVagn") != null)
            {
                var str = session.GetString("MinKundVagn");
                SessionKundvagn kundvagn = JsonConvert.DeserializeObject<SessionKundvagn>(str);
                kundvagn.Session = session;
                return kundvagn;
            }
            else
            {
                SessionKundvagn kundvagn = new SessionKundvagn();
                kundvagn.Session = session;
                return kundvagn;
            }
        }
        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Matratt matratt)
        {
            base.AddItem(matratt);
            Session.SetString("MinKundVagn", JsonConvert.SerializeObject(this));
        }

        public override void RemoveLine(Matratt matratt)
        {
            base.RemoveLine(matratt);
            Session.SetString("MinKundVagn", JsonConvert.SerializeObject(this));
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("MinKundVagn"); 
        }
    }
}
