using BudgetPlanner.Web.Models;
using RestSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetPlanner.Web.Helper
{
    public static class APIHelper
    {
        public static IRestResponse Register(RegisterModel model)
        {
            var client = new RestClient("https://localhost:44378");

            var request = new RestRequest("api/account/register", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(model);

            var response = client.Execute(request);
            return response;
        }

        public static AuthenticationResultModel Login(LoginModel model)
        {

            AuthenticationResultModel result = null;
            var client = new RestClient("https://localhost:44378");

            var request = new RestRequest("token", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.Parameters.Clear();
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("grant_type", "password");
            request.AddParameter("username", model.UserName);
            request.AddParameter("password", model.Password);

            var response = client.Execute(request);
            if(response.IsSuccessful)
            {
                result = JsonConvert.DeserializeObject<AuthenticationResultModel>(response.Content);
            }
            return result;
            
        }
    }
}