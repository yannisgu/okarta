using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Extensions;
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
                    return mapsService.Query().Select(m => new
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Lat = m.Lat,
                        Lon = m.Lon,
                        DownloadPrice = m.DownloadPrice,
                    });
                };

            Post["/"] = _ =>
            {
                var map = JsonConvert.DeserializeObject<Map>(Request.Body.AsString());
                map.Id = Guid.NewGuid();
                map.OwnerId = User.Id;
                return null;
            };

            Post["/{id}"] = _ =>
            {
                return null;
            };

        }
    }
}