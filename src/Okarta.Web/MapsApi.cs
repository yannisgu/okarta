using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Extensions;
using Nancy.Security;
using Newtonsoft.Json;
using Okarta.Data;
using Okarta.Data.Entities;
using Okarta.Data.Services;

namespace Okarta.Web
{
    public class MapsApi : BaseApiModule
    {

        public MapsApi(IMapsService mapsService, IUserService userService) : base("/api/maps", userService)
        {


            Get["/"] =
                _ =>
                {
                    return mapsService.Query().Select(GetMap);
                };

            Get["/{id}"] =
                _ =>
                {
                    Guid guid;
                    if (Guid.TryParse(_.id, out guid))
                    {
                        return GetMap(mapsService.Get(guid));
                    }
                    return null;
                };

            Post["/"] = _ =>
            {
                this.RequiresAuthentication();
                var map = JsonConvert.DeserializeObject<Map>(Request.Body.AsString());
                map.Id = Guid.NewGuid();
                map.OwnerId = User.Id;
                mapsService.Add(map);
                return null;
            };

            Post["/{id}"] = _ =>
            {
                Guid guid;
                if(Guid.TryParse(_.id, out guid))
                {
                    var map = mapsService.Get(guid);
                    if (map.OwnerId == User.Id)
                    {
                        var updateMap = JsonConvert.DeserializeObject<Map>(Request.Body.AsString());
                        updateMap.Owner = null;
                        updateMap.Id = guid;
                        mapsService.Update(updateMap);
                    }
                    else
                    {
                        return new Response() {StatusCode = HttpStatusCode.Unauthorized};
                    }
                }
                return null;
            };
        }

        public dynamic GetMap(Map map)
        {
            return map;
        }
    }
}