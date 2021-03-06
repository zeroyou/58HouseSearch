using System;
using System.Collections.Generic;
using System.Linq;
using HouseCrawler.Core;
using HouseCrawler.Core.Models;

namespace HouseCrawler.Core.Service
{
    public class HouseDashboardService
    {

        private RedisService redis;

        private HouseDapper houseDapper;

        public HouseDashboardService(RedisService redis, HouseDapper houseDapper)
        {
            this.redis = redis;
            this.houseDapper =  houseDapper;
        }


        public List<HouseDashboard> LoadDashboard()
        {
            string houseDashboardJson = redis.ReadCache("HouseDashboard");
            if (string.IsNullOrEmpty(houseDashboardJson))
            {
                List<HouseDashboard> dashboards = houseDapper.GetHouseDashboard();
                redis.WriteCache("HouseDashboard", Newtonsoft.Json.JsonConvert.SerializeObject(dashboards));
                return dashboards;
            }
            else
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<List<HouseDashboard>>(houseDashboardJson);
            }
        }

    }
}
