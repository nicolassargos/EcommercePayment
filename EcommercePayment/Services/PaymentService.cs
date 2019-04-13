﻿using EcommercePayment.Models;
using EcommercePaymentData.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcommercePayment.Services
{
    public class PaymentService
    {
        HttpClient client = new HttpClient();
        string paymentApiUrl = "http://localhost:51675/api/Payments";

        public PaymentService()
        {

        }

        public async Task<PaymentModel> GetPaymentAsync(int id)
        {
            var url = $"{paymentApiUrl}/{id}";

            var payment = new PaymentModel();

            HttpResponseMessage result;

            try
            {
                result = await client.GetAsync(url);
            }
            catch (Exception ex)
            {
                throw;
            }

            if (result.IsSuccessStatusCode)
            {
                payment = JsonConvert.DeserializeObject<PaymentModel>(await result.Content.ReadAsStringAsync());
            }

            return (payment);
        }


        // process real payment
        public async Task<bool> ProcessPayment(PaymentModel payment)
        {
            var url = $"{paymentApiUrl}/process";

            HttpResponseMessage result;
            bool resultValidation = false;

            try
            {
                result = await client.PostAsJsonAsync(url, payment);
            }
            catch (Exception ex)
            {
                throw;
            }

            if (result.IsSuccessStatusCode)
            {
                resultValidation = JsonConvert.DeserializeObject<bool>(await result.Content.ReadAsStringAsync());
            }
            return resultValidation;
        }
    }
}
