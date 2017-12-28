﻿// =====================================================================
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// =====================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Samples.Net40
{
    /// <summary>
    /// This sample is to demo how to refund a customer
    /// </summary>
    public partial class Transaction
    {
        /// <summary>
        /// Refund a customer if the original transaction is already settled.
        /// </summary>
        public void Refund()
        {
            try
            {
                //  Populate POST String
                StringBuilder datastring = new StringBuilder();
                datastring.Append("{");
                datastring.Append("\"Customer\":\"ARRONFIT0003\",");
                datastring.Append("\"Currency\":\"USD\",");
                datastring.Append("\"Amount\":\"10.05\",");
                datastring.Append("\"Type\":\"Credit\",");
                datastring.Append("\"SetupId\":\"Paypal\""); // Replace with your gateway account profile name
                datastring.Append("\"Card\":{");
                datastring.Append("\"Account\":\"5555555555554444\",");
                datastring.Append("\"Cvc\":\"1453\",");
                datastring.Append("\"Tender\":\"CreditCard\",");
                datastring.Append("\"CardName\":\"MasterCard\",");
                datastring.Append("\"ExpDate\":\"0115\",");
                datastring.Append("\"CardHolder\":{");
                datastring.Append("\"Name\":\"jason zhao\"");
                datastring.Append("},");
                datastring.Append("\"Billto\":{");
                datastring.Append("\"Country\":\"US\",");
                datastring.Append("\"State\":\"CA\",");
                datastring.Append("\"City\":\"ANAHEIM\",");
                datastring.Append("\"Line1\":\"2099 S State College Blvd\",");
                datastring.Append("\"Email\":\"support@payfabric.com\"");
                datastring.Append("}");
                datastring.Append("}");
                datastring.Append("}");

                // POST
                byte[] data = System.Text.Encoding.UTF8.GetBytes(datastring.ToString());
                var url = "https://sandbox.payfabric.com/v1/rest/api/transaction/process";
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Headers["authorization"] = new Token().Create();
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = data.Length;
                Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(data, 0, data.Length);
                stream.Close();
                HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                string result = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                httpWebRequest.Abort();
                httpWebResponse.Close();

                // 
                // Sample repsonse is similar to below
                //
                //{
                //    "AVSAddressResponse": null,
                //    "AVSZipResponse": null,
                //    "AuthCode": null,
                //    "CVV2Response": null,
                //    "IAVSAddressResponse": null,
                //    "Message": "Approved",
                //    "OriginationID": "A70E6C184BA5",
                //    "RespTrxTag": null,
                //    "ResultCode": "0",
                //    "Status": "Approved",
                //    "TrxDate": "5\/31\/2014 3:17:27 PM",
                //    "TrxKey": "140531067716"
                //}

            }
            catch (WebException e)
            {
                //  Handling exception from PayFabric
            }
            catch (Exception e)
            {
                //  Handling exception
            }

        }
    }
}
