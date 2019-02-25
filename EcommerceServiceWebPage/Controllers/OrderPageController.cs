using EcommerceServiceWebPage.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EcommerceServiceWebPage.Controllers
{
    public class OrderPageController : Controller
    {
        // GET: OrderPage
        public ActionResult Index()
        {
            IEnumerable<OrderProduct> _op = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:52866/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var responseTask = client.GetAsync("api/Order/GetProducts");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    //var formatters = new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter() };
                    var readTask = result.Content.ReadAsAsync<IEnumerable<OrderProduct>>();
                    readTask.Wait();
                    _op = readTask.Result;
                }
                else //web api sent error response 
                {
                    _op = Enumerable.Empty<OrderProduct>();
                    //ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            //using (var cc = new WebClient())
            //{
            //    var jsonData = cc.DownloadString("http://localhost:52866/api/Order/GetProducts");
            //    IEnumerable<OrderProduct> data = JsonConvert.DeserializeObject<IEnumerable<OrderProduct>>(jsonData);
            //    var a = "";
            //}
            return View(_op);
        }

        // GET: OrderPage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderPage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderPage/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderPage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderPage/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: OrderPage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrderPage/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
