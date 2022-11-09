using System;
using System.Collections.Generic;
using RestSharp;
using System.IO; 

//Done by Zhe Yun 

namespace web_asignment1_Y2S1_2022
{
    public class API
    {
        private static string url = "https://newdatabase1-1d27.restdb.io/rest/email-verification-code";
        private static string temporaryAccessCode = "https://newdatabase1-1d27.restdb.io/rest/temporary-access-code"; 
        private static readonly RestClient client2 = new RestClient(temporaryAccessCode); 
        private static readonly RestClient client = new RestClient(url); 
        private static RestRequest request = new RestRequest();

        //CONFIGURATIONS FOR this url => https://newdatabase1-1d27.restdb.io/rest/temporary-access-code
        public static string GetAllTemporaryAccessCodeRecords()
        {
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "22d48160766d8d1aa43f8ea555e4f9cc79986");
            request.AddHeader("content-type", "application/json");
            var response = client2.Get(request);
            string result = response.Content ?? (response.Content.ToString().Length > 0 ? response.Content.ToString() : "No result returned from the database");
            return result;
        }

        public static void PostTemporaryAccessCodeRecord(string data)
        {
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "22d48160766d8d1aa43f8ea555e4f9cc79986");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            var response = client2.Post(request);
        }

        public static void DeleteTemporaryAccessCodeRecord(string id)
        {
            RestClient client2 = new RestClient($"https://newdatabase1-1d27.restdb.io/rest/temporary-access-code/{id}"); 
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "22d48160766d8d1aa43f8ea555e4f9cc79986");
            request.AddHeader("content-type", "application/json");
            var response = client2.Delete(request); 
        }

        //CONFIGURATIONS FOR this url =>  https://newdatabase1-1d27.restdb.io/rest/email-verification-code
        //Get all the records from the database 
        public static string GetAll()
        {
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "22d48160766d8d1aa43f8ea555e4f9cc79986");
            request.AddHeader("content-type", "application/json");
            var response = client.Get(request);
            string result = response.Content ?? (response.Content.ToString().Length > 0 ? response.Content.ToString() : "No result returned from the database");
            return result; 
        }

        //Delete a certain record from the database through the usage of the object id 
        public static void Delete(string _id)
        {
            RestClient client = new RestClient($"https://newdatabase1-1d27.restdb.io/rest/email-verification-code/{_id}"); 
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "22d48160766d8d1aa43f8ea555e4f9cc79986");
            request.AddHeader("content-type", "application/json");
            var response = client.Delete(request);  
        }

        //Post a record to the database
        public static void Post(string data)
        {
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "22d48160766d8d1aa43f8ea555e4f9cc79986");
            request.AddHeader("content-type", "application/json");
            //what is the data that you would want to send over to this API? 
            request.AddParameter("application/json", data, ParameterType.RequestBody); 
            var response = client.Post(request);
        }

        //Update a record to the database
        public static void Update(string _id, string data)
        {
            RestClient client = new RestClient($"https://newdatabase1-1d27.restdb.io/rest/email-verification-code/{_id}");
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-apikey", "22d48160766d8d1aa43f8ea555e4f9cc79986");
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", data, ParameterType.RequestBody); 
            var response = client.Put(request); 
        }
    }
}