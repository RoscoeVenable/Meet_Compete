using Meet_and_Copmete_Capstone.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Meet_and_Copmete_Capstone
{
    public class Geocoding
    {
        private string GetGeoCodingEvents(Event events)
        {
            return $"https://maps.googleapis.com/maps/api/geocode/json?address={events.Street}+{events.City}+{events.State}&key={Secrets.APIKEY}";
        }
        public async Task<Event> GetGeoCoding(Event events)
        {
            string apiUrl = GetGeoCodingEvents(events);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string jsonResult = await response.Content.ReadAsStringAsync();
            if (jsonResult != null)
            {
                JObject jobject = JObject.Parse(jsonResult);
                string lng1 = (string)jobject["results"][0]["geometry"]["location"]["lng"];
                string lat1 = (string)jobject["results"][0]["geometry"]["location"]["lat"];
                events.Latitude = Convert.ToDouble(lat1);
                events.Longitude = Convert.ToDouble(lng1);
            }
            return events;
            
        }

        private string GetGeoCodingEventees(Eventee eventees)
        {
            return $"https://maps.googleapis.com/maps/api/geocode/json?address={eventees.Street}+{eventees.City}+{eventees.State}&key={Secrets.APIKEY}";
        }
        public async Task<Eventee> GetGeoCodingEventee(Eventee eventees)
        {
            string apiUrl = GetGeoCodingEventees(eventees);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            string jsonResult = await response.Content.ReadAsStringAsync();
            if (jsonResult != null)
            {
                JObject jobject = JObject.Parse(jsonResult);
                string lng1 = (string)jobject["results"][0]["geometry"]["location"]["lng"];
                string lat1 = (string)jobject["results"][0]["geometry"]["location"]["lat"];
                eventees.Latitude = Convert.ToDouble(lat1);
                eventees.Longitude = Convert.ToDouble(lng1);
            }
            return eventees;

        }


        public async Task<bool> GetDistance(Eventee eventee, Event events)
        {
            string distanceRequest = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={events.Latitude},{events.Longitude}&destinations={eventee.Latitude},{eventee.Longitude}&key={Secrets.APIKEY}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(distanceRequest);
            string jsonResult = await response.Content.ReadAsStringAsync();
            if (jsonResult != null)
            {
                JObject jobject = JObject.Parse(jsonResult);
                var rows = jobject["rows"];
                foreach (var item in rows)
                {

                    foreach (var value in item["elements"])
                    {
                        eventee.Distance = Convert.ToInt32(value["distance"]["value"].ToString());
                    }
                }
            }
            return true;
        }
        public async Task<List<Eventee>> GetNearbyPeople(List<Eventee> eventee, Event events)
        {
            await GetGeoCoding(events);
            for (int i = 0; i < eventee.Count; i++)
            {
                await GetGeoCodingEventee(eventee[i]);
                await GetDistance(eventee[i], events);
            }
            return eventee.Where(e => e.Distance < 5000).ToList();
        }

        
        private async Task<bool> GetGeocodes()
        {
            string fullAddress = Uri.EscapeDataString("123 nw 7th st,Oak Island,NC");
            string geocodingRequest = $"https://maps.googleapis.com/maps/api/geocode/json?address={fullAddress}&key={Secrets.APIKEY}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(geocodingRequest);
            string jsonResult = await response.Content.ReadAsStringAsync();
            string lng1 = "";
            string lat1 = "";
            if (jsonResult != null)
            {
                JObject jobject = JObject.Parse(jsonResult);
                lng1 = (string)jobject["results"][0]["geometry"]["location"]["lng"];
                lat1 = (string)jobject["results"][0]["geometry"]["location"]["lat"];
            }

            fullAddress = Uri.EscapeDataString("123 nw 8th st,Oak Island,NC");
            geocodingRequest = $"https://maps.googleapis.com/maps/api/geocode/json?address={fullAddress}&key={Secrets.APIKEY}";
            client = new HttpClient();
            response = await client.GetAsync(geocodingRequest);
            jsonResult = await response.Content.ReadAsStringAsync();
            string lng2 = "";
            string lat2 = "";
            if (jsonResult != null)
            {
                JObject jobject = JObject.Parse(jsonResult);
                lng2 = (string)jobject["results"][0]["geometry"]["location"]["lng"];
                lat2 = (string)jobject["results"][0]["geometry"]["location"]["lat"];
            }

            string distanceRequest = $"https://maps.googleapis.com/maps/api/distancematrix/json?origins={lat1},{lng1}&destinations={lat2},{lng2}&key={Secrets.APIKEY}";
            client = new HttpClient();
            response = await client.GetAsync(distanceRequest);
            jsonResult = await response.Content.ReadAsStringAsync();
            if (jsonResult != null)
            {
                JObject jobject = JObject.Parse(jsonResult);
                var rows = jobject["rows"];
                foreach (var item in rows)
                {

                    foreach (var value in item["elements"])
                    {
                        string temp = value["distance"]["value"].ToString();
                        var temp2 = value["distance"]["value"];

                    }
                }
            }

            return true;

        }
    }
}

