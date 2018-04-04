using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CognitionTesterWeb.Models;
using CognitionTesterWeb.Repositories;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace CognitionTesterWeb.Controllers
{
    public class TextClassificationController : Controller
    {
        public IActionResult TextClassifications_Read([DataSourceRequest] DataSourceRequest request)
        {
            TextClassificationRepository repository = new TextClassificationRepository();

            IQueryable<TextClassificationResult> results = repository.Results;

            return Json(results.ToDataSourceResult(request,
                (item) =>
                {
                    return new TextClassificationResultDto()
                    {
                        Id = item.Id.ToString(),

                        Title = item.Title,

                        AResult = item.ResultItems.Any(m => m.VendorLabel.Equals(
                            "A", StringComparison.InvariantCultureIgnoreCase))
                            ? item.ResultItems.Single(m => m.VendorLabel.Equals(
                                    "A", StringComparison.InvariantCultureIgnoreCase))
                                .VendorResult
                            : string.Empty,
                        BResult = item.ResultItems.Any(m => m.VendorLabel.Equals(
                            "B", StringComparison.InvariantCultureIgnoreCase))
                            ? item.ResultItems.Single(m => m.VendorLabel.Equals(
                                    "B", StringComparison.InvariantCultureIgnoreCase))
                                .VendorResult
                            : string.Empty,
                        CResult = item.ResultItems.Any(m => m.VendorLabel.Equals(
                            "C", StringComparison.InvariantCultureIgnoreCase))
                            ? item.ResultItems.Single(m => m.VendorLabel.Equals(
                                    "C", StringComparison.InvariantCultureIgnoreCase))
                                .VendorResult
                            : string.Empty,
                        Ctime = item.Ctime
                    };
                }));
        }

        public IActionResult AddNew()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult AddNew(TextClassificationModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                TextClassificationRepository repository
                    = new TextClassificationRepository();

                List<TextClassificationResultItem> resultItems
                    = new List<TextClassificationResultItem>();

                Task task1 = Task.Run(() =>
                {
                    try
                    {
                        Task<TextClassificationResultItem> taskA = this.GetItemAResult(model);
                        taskA.Wait();
                        TextClassificationResultItem itemA = taskA.Result;
                        if (itemA != null)
                        {
                            resultItems.Add(itemA);
                        }
                    }
                    catch (Exception ex1)
                    {
                        System.Diagnostics.Trace.WriteLine("GetA: " + ex1.Message + "," + ex1.StackTrace);
                    }
                });
                Task task2 = Task.Run(() =>
                {
                    try
                    {
                        Task<TextClassificationResultItem> taskB = this.GetItemBResult(model);
                        taskB.Wait();
                        TextClassificationResultItem itemB = taskB.Result;
                        if (itemB != null)
                        {
                            resultItems.Add(itemB);
                        }
                    }
                    catch (Exception ex1)
                    {
                        System.Diagnostics.Trace.WriteLine("GetB: " + ex1.Message + "," + ex1.StackTrace);
                    }
                });

                Task task3 = Task.Run(() =>
                {
                    try
                    {
                        Task<TextClassificationResultItem> taskC = this.GetItemCResult(model);
                        taskC.Wait();
                        TextClassificationResultItem itemC = taskC.Result;
                        if (itemC != null)
                        {
                            resultItems.Add(itemC);
                        }
                    }
                    catch (Exception ex1)
                    {
                        System.Diagnostics.Trace.WriteLine("GetC: " + ex1.Message + "," + ex1.StackTrace);
                    }
                });
                Task.WaitAll(task1, task2, task3);

                TextClassificationResult newResult = new TextClassificationResult()
                {
                    Ctime = DateTime.Now,
                    Title = model.Title,
                    Content = model.Content,
                    ResultItems = resultItems
                };

                repository.Add(newResult);

                return RedirectToAction("TextClassification", "Home");
            }

            return View("AddNew", model);
        }

        private async Task<TextClassificationResultItem> GetItemAResult(
            TextClassificationModel model)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            string url = builder.Build().GetValue<string>(
                "CognitionService:TextClassification:url");
            string vendorA_AppId = builder.Build().GetValue<string>(
                "CognitionService:TextClassification:VendorA_AppId");
            string vendorA_SecretKey = builder.Build().GetValue<string>(
                "CognitionService:TextClassification:VendorA_SecretKey");
            string vendor = builder.Build().GetValue<string>(
                "CognitionService:TextClassification:VendorA_CognitionServiceVendor");

            //string vendorA_AppId = builder.Build().GetValue<string>(
            //    "CognitionService:TextClassification:VendorB_AppId");
            //string vendorA_SecretKey = builder.Build().GetValue<string>(
            //    "CognitionService:TextClassification:VendorB_SecretKey");

            var client = new HttpClient
            {
                BaseAddress = new Uri(url),
            };

            client.DefaultRequestHeaders.Add("appId", vendorA_AppId);
            client.DefaultRequestHeaders.Add("appSecret", vendorA_SecretKey);

            FormUrlEncodedContent content = new FormUrlEncodedContent(
                new Dictionary<string, string>
            {
                { "title", model.Title },
                { "text", model.Content }
            });
            HttpResponseMessage msg = await client.PostAsync(url, content);
            string result = await msg.Content.ReadAsStringAsync();

            TextClassificationResultItem item = new TextClassificationResultItem()
            {
                VendorLabel = "A",
                CognitionServiceVendor = vendor,
                VendorResult = result,
            };

            return item;
        }

        private async Task<TextClassificationResultItem> GetItemBResult(
            TextClassificationModel model)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            string url = builder.Build().GetValue<string>(
                "CognitionService:TextClassification:url");
            string vendorB_AppId = builder.Build().GetValue<string>(
                "CognitionService:TextClassification:VendorB_AppId");
            string vendorB_SecretKey = builder.Build().GetValue<string>(
                "CognitionService:TextClassification:VendorB_SecretKey");
            string vendor = builder.Build().GetValue<string>(
                "CognitionService:TextClassification:VendorB_CognitionServiceVendor");

            //string vendorA_AppId = builder.Build().GetValue<string>(
            //    "CognitionService:TextClassification:VendorB_AppId");
            //string vendorA_SecretKey = builder.Build().GetValue<string>(
            //    "CognitionService:TextClassification:VendorB_SecretKey");

            var client = new HttpClient
            {
                BaseAddress = new Uri(url),
            };

            client.DefaultRequestHeaders.Add("appId", vendorB_AppId);
            client.DefaultRequestHeaders.Add("appSecret", vendorB_SecretKey);

            FormUrlEncodedContent content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "title", model.Title },
                    { "text", model.Content }
                });
            HttpResponseMessage msg = await client.PostAsync(url, content);
            string result = await msg.Content.ReadAsStringAsync();

            TextClassificationResultItem item = new TextClassificationResultItem()
            {
                VendorLabel = "B",
                CognitionServiceVendor = vendor,
                VendorResult = result,
            };

            return item;
        }

        private async Task<TextClassificationResultItem> GetItemCResult(
            TextClassificationModel model)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            string url = builder.Build().GetValue<string>(
                "CognitionService:TextClassification:url");
            string vendorC_AppId = builder.Build().GetValue<string>(
                "CognitionService:TextClassification:VendorC_AppId");
            string vendorC_SecretKey = builder.Build().GetValue<string>(
                "CognitionService:TextClassification:VendorC_SecretKey");
            string vendor = builder.Build().GetValue<string>(
                "CognitionService:TextClassification:VendorC_CognitionServiceVendor");

            //string vendorA_AppId = builder.Build().GetValue<string>(
            //    "CognitionService:TextClassification:VendorB_AppId");
            //string vendorA_SecretKey = builder.Build().GetValue<string>(
            //    "CognitionService:TextClassification:VendorB_SecretKey");

            var client = new HttpClient
            {
                BaseAddress = new Uri(url),
            };

            client.DefaultRequestHeaders.Add("appId", vendorC_AppId);
            client.DefaultRequestHeaders.Add("appSecret", vendorC_SecretKey);

            FormUrlEncodedContent content = new FormUrlEncodedContent(
                new Dictionary<string, string>
                {
                    { "title", model.Title },
                    { "text", model.Content }
                });
            HttpResponseMessage msg = await client.PostAsync(url, content);
            string result = await msg.Content.ReadAsStringAsync();

            TextClassificationResultItem item = new TextClassificationResultItem()
            {
                VendorLabel = "C",
                CognitionServiceVendor = vendor,
                VendorResult = result,
            };

            return item;
        }

        public IActionResult Detail(string id)
        {
            TextClassificationRepository repository = new TextClassificationRepository();
            ObjectId objectId = new ObjectId(id);
            var result = repository.FirstOrDefault(m => m.Id == objectId);

            if (result == null)
            {
                return View(new TextClassificationResultDetail());
            }

            TextClassificationResultDetail detail = new TextClassificationResultDetail()
            {
                Title = result.Title,
                Content = result.Content,
                Ctime = result.Ctime.ToLocalTime(),
            };

            if (result?.ResultItems != null)
            {
                foreach (var item in result.ResultItems)
                {
                    if (!string.IsNullOrEmpty(item?.VendorLabel)
                        && item.VendorLabel.IsCaseInsensitiveEqual("A"))
                    {
                        detail.AResult = item.VendorResult;
                    }
                    if (!string.IsNullOrEmpty(item?.VendorLabel)
                        && item.VendorLabel.IsCaseInsensitiveEqual("B"))
                    {
                        detail.BResult = item.VendorResult;
                    }
                    if (!string.IsNullOrEmpty(item?.VendorLabel)
                        && item.VendorLabel.IsCaseInsensitiveEqual("C"))
                    {
                        detail.CResult = item.VendorResult;
                    }
                }
            }

            return View(detail);
        }
    }
}