using POTC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POTC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SampleEngine.Order order = new SampleEngine.Order();

            if (Request.Url.AbsoluteUri.Contains("ceroacidez"))
            {
                return Redirect("https://prilosec-otc.safeprocessing.com?lang=sp");
            }

            HttpCookie month = Request.Cookies["birth_month"];
            HttpCookie year = Request.Cookies["birth_year"];
            HttpCookie lang = Request.Cookies["lang"];

            //Check to make sure the cookie exists
            if (month != null && year != null)
            {
                if (!order.ValidAge(int.Parse(month.Value), 1, int.Parse(year.Value)))
                {
                    Response.Redirect("/home/eligibility?lang=" + lang.Value);
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            SampleEngine.Order order = new SampleEngine.Order();

            int birth_month = int.Parse(collection["Month"]);
            int birth_year = int.Parse(collection["Year"]);
            string campaign_status = SampleEngine.Order.GetStatus(ConfigurationManager.AppSettings["ccgpid"]);

            if (campaign_status == "2") //expired campaign
            {
                return Redirect("/expired");
            }
            else if (campaign_status == "3") //inactive campaign
            {
                return Redirect("inactive");
            }
            else
            {
                if (!order.ValidAge(birth_month, 1, birth_year))
                {
                    //Create a new cookie, passing the name into the constructor
                    HttpCookie cMonth = new HttpCookie("birth_month");
                    HttpCookie cYear = new HttpCookie("birth_year");
                    HttpCookie lang = new HttpCookie("lang");

                    //Set the cookies value
                    cMonth.Value = collection["Month"];
                    cYear.Value = collection["Year"];
                    lang.Value = Request.QueryString["lang"];
                    //Set the cookie to expire in 1 minute
                    DateTime dtNow = DateTime.Now;
                    TimeSpan tsMinute = new TimeSpan(0, 0, 5, 0);
                    cMonth.Expires = dtNow + tsMinute;
                    cYear.Expires = dtNow + tsMinute;

                    //Add the cookie
                    Response.Cookies.Add(cMonth);
                    Response.Cookies.Add(cYear);
                    Response.Cookies.Add(lang);
                    Response.Redirect("/home/eligibility?s=" + Request.QueryString["s"] + "&lang=" + Request.QueryString["lang"]);
                    // ViewBag.ErrorMessage = "Sorry! You are not eligible to receive this product. This offer is limited to 18 years of age or older.";
                    //SetForm(odb, collection);
                }
                else
                {
                    if (collection["Medication"].Equals("Prilosec OTC") || collection["Medication"].Equals("Other") || collection["Medication"].Equals("Lanzoprazole") || collection["Medication"].Equals("Omeprazole") || collection["Medication"].Equals("Ranitidine") || collection["Medication"].Equals("Famotidine"))
                    {
                        Response.Redirect("/home/coupon?lang=" + Request.QueryString["lang"]);
                    }
                    else
                    {
                        int sampleID = 0;
                        if (collection["Medication"].Equals("Zantac") || collection["Medication"].Equals("Pepcid") || collection["Medication"].Equals("Tums/Rolaids"))
                        {
                            sampleID = 16;
                        }
                        else if (collection["Medication"].Equals("Nexium") || collection["Medication"].Equals("Prevacid24HR") || collection["Medication"].Equals("Zegerid OTC"))
                        {
                            sampleID = 15;
                        }
                        else
                            sampleID = 15;


                        if (collection["Frequency"].Equals("2to3") || collection["Frequency"].Equals("4to6") || collection["Frequency"].Equals("7"))
                        {
                            Response.Redirect("/home/samples?lang=" + Request.QueryString["lang"] + "&s=" + sampleID.ToString());
                        }
                        else
                        {
                            Response.Redirect("/home/coupon?lang=" + Request.QueryString["lang"]);
                        }

                        Session["Medication"] = collection["Medication"];
                        Session["Frequency"] = collection["Frequency"];
                    }
                }

            }
                    
                //return Redirect("/home/samples");
            

            return View();
        }

        public ActionResult Samples()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Samples(FormCollection collection)
        {
            SampleEngine.Order order = new SampleEngine.Order();
            string _CCGPID = ConfigurationManager.AppSettings["ccgpid"];
            string _DataSourceName = ConfigurationManager.AppSettings["datasource_name"];
            string _Source = string.Empty;
            string bypass = collection["bypass"];
            bool match = true;
            Order odb = new Order();


            order.ccgpid = _CCGPID;
            if (!order.AlreadyRequested(collection["Address"], "", collection["Zip"]))
            {
                //check samples
                if (!order.CampaignExpired())
                {

                    //run through standardization
                    Address address = new Address();
                    address.address1 = collection["Address"].Replace("\"", "").Trim();
                    address.address2 = ""; // collection["Address2"].Replace("\"", "").Trim();
                    address.city = collection["City"].Replace("\"", "").Trim();
                    address.state = collection["State"].Replace("\"", "").Trim();
                    address.zip = collection["Zip"].Replace("\"", "").Trim();

                    if (string.IsNullOrEmpty(bypass) || (bypass != "on"))
                    {
                        AddressValidator.AddressValidator adv = new AddressValidator.AddressValidator();
                        adv.LicenseKey = System.Configuration.ConfigurationManager.AppSettings["LicenseKey"];
                        adv.Username = System.Configuration.ConfigurationManager.AppSettings["Username"];
                        adv.Password = System.Configuration.ConfigurationManager.AppSettings["Password"];

                        adv.address_IN.Address1 = collection["Address"].Replace("\"", "").Trim();
                        adv.address_IN.Address2 = ""; // collection["Address2"].Replace("\"", "").Trim();
                        adv.address_IN.City = collection["City"].Replace("\"", "").Trim();
                        adv.address_IN.State = collection["State"].Replace("\"", "").Trim();
                        adv.address_IN.Zipcode = collection["Zip"].Replace("\"", "").Trim();
                        adv.address_IN.CountryCode = "US";
                        adv.Validate();

                        if (adv.Match)
                        {
                            if (adv.address_OUT.Count > 0)
                            {
                                match = true;
                                address.address1 = adv.address_OUT[0].Address1;
                                address.address2 = adv.address_OUT[0].Address2;
                                address.city = adv.address_OUT[0].City;
                                address.state = adv.address_OUT[0].State;
                                address.zip = adv.address_OUT[0].Zipcode + "-" + adv.address_OUT[0].Zip4;
                                //address.address_classification = adv.address_OUT[0].AddressClassification == 1 ? 0 : 1; //1 - Residential ; 0 - Commercial
                                address.country = adv.address_OUT[0].CountryCode;
                                //address.address_match = true;
                            }
                            else
                            {
                                match = false;
                            }
                        }
                        else
                        {
                            match = false;
                        }
                    }

                    if (match == false)
                    {
                        // return Redirect(Request.Url + "&match=0");
                        ViewBag.match = "0";
                        ViewBag.SampleId = Request.QueryString["s"];
                        ViewBag.ErrorMessage = "Address Invalid";
                        SetForm(odb, collection);

                        return View(odb);
                    }
                    else
                    {
                       
                        order.datasource_name = _CCGPID;
                        order.ccgpid = _CCGPID;
                        order.first_name = collection["FirstName"].Replace("\"", "").Trim().ToUpper();
                        order.middle_initial = "";
                        order.last_name = collection["LastName"].Replace("\"", "").Trim().ToUpper();
                        order.address1 = collection["Address"].Replace("\"", "").Trim().ToUpper();
                        order.city = collection["City"].Replace("\"", "").Trim().ToUpper();
                        order.state = collection["State"].Replace("\"", ""); //state.SelectedValue.ToString();
                        order.zip = collection["Zip"].Replace("\"", "").Trim().ToUpper();
                        order.country = "US";

                        order.gender = "";
                        order.contact_language = "";
                        order.site_language = "en_US";
                        order.ip_address = Request.UserHostAddress;

                        if (!String.IsNullOrEmpty(Request.QueryString["s"]) && !Request.QueryString["s"].Equals("0"))
                        {
                            order.sampleID = !String.IsNullOrEmpty(Request.QueryString["s"]) ? Convert.ToInt32(Request.QueryString["s"]) : 0;
                            order.ccgsku = Convert.ToInt32(SampleEngine.SampleOffer.GetSampleSku(Convert.ToInt32(Request.QueryString["s"]), _CCGPID));
                            order.text_val2 = SampleEngine.SampleOffer.GetSampleDescription(Convert.ToInt32(Request.QueryString["s"]), _CCGPID);
                        }
                        order.optin1 = collection["optin"].Equals("false") ? false : true;
                        order.text_val6 = Request.Path;
                        order.UserAgent = Request.UserAgent.ToLower();

                        order.traffic_source_name = "Minisite";
                        int orderNumber = order.Save();

                        if (orderNumber > 0)
                        {
                            //SendEmail(Order.GetTarget(_dealerNo), orderNumber);
                            Response.Redirect("/home/thankyou?onum=" + orderNumber.ToString() + "&lang=" + Request.QueryString["lang"]);
                        }
                    }


                }
                else
                {
                    Response.Redirect("/home/expired?s=" + Request.QueryString["s"] + "&lang=" + Request.QueryString["lang"]);
                    //ViewBag.ErrorMessage = "";
                }
            }
            else
            {
                Response.Redirect("/home/limit?s=" + Request.QueryString["s"] + "&lang=" + Request.QueryString["lang"]);
                //ViewBag.ErrorMessage = "Sorry! Looks like you've already requested a sample. Unfortunately we can only send you one sample every six months, so keep an eye out and try again soon!";
                // BuildLists();
            }
            return View();
        }
        private void SetForm(Order odb, FormCollection collection)
        {
            odb.FirstName = collection["FirstName"];
            odb.LastName = collection["LastName"];
            odb.Address = collection["Address"].Replace("\"", "").Trim();
            odb.Address2 = collection["Address2"].Replace("\"", "").Trim();
            odb.City = collection["City"].Replace("\"", "").Trim();
            odb.State = collection["State"].Replace("\"", ""); //state.SelectedValue.ToString();
            odb.Zip = collection["Zip"].Replace("\"", "").Trim();
            odb.Email = collection["Email"].Replace("\"", "").Trim();
        }
        public ActionResult Coupon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Coupon(FormCollection collection)
        {
            return View();
        }

        public ActionResult LearnMore()
        {

            return View();
        }
        public ActionResult Thankyou()
        {
            return View();
        }

        public ActionResult Eligibility()
        {
            return View();
        }

        public ActionResult Invalid()
        {
            return View();
        }

        public ActionResult Expired()
        {
            return View();
        }
        public ActionResult Limit()
        {
            return View();
        }
        public ActionResult Redeemed()
        {
            return View();
        }
        public ActionResult Inactive()
        {
            return View();
        }
       
    }
}
