using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json.Linq;
using Contact_Managment_System_MVC.Models;
using System.Xml.Linq;

namespace Contact_Managment_System_MVC.Controllers
{
    public class ContactsController : Controller
    {
        // GET: Contacts
        public ActionResult Index()
        {
            return View();
             
        }
        public ActionResult LoadContactsData() {
            using (ContactsEntities db = new ContactsEntities())
            {
           
                List<Contact> Contacts = db.Contacts.ToList<Contact>();
                return Json(Contacts, JsonRequestBehavior.AllowGet);
            }

        }
        public int SaveContact(int? ID,string contact) {

            JObject jContact = JObject.Parse(contact);
            string Fname = (string)jContact["Fname"];
            string Lname = (string)jContact["Lname"];
            Contact contactModel = new Contact();
            contactModel.Id = (int)ID;
            contactModel.Fname = Fname;
            contactModel.Lname = Lname;
            using (ContactsEntities db = new ContactsEntities()) {
                if (ID == 0)
                {
                    db.Contacts.Add(contactModel);

                    db.SaveChanges();

                }
                else
                {
                    db.Entry(contactModel).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }


                return (int)ID;

            }
               
        
        }
        public int DeleteContact(int? ID, string contact)
        {
            Contact contactModel = new Contact();

            using (ContactsEntities db = new ContactsEntities())
            {
               
               contactModel = db.Contacts.Where(x => x.Id == ID).FirstOrDefault<Contact>();
               db.Contacts.Remove(contactModel);
               db.SaveChanges();
                return (int)ID;

            }

   
        }

        ////////////Address Section
        public ActionResult LoadAddressData(int ContactId)
        {
            using (ContactsEntities db = new ContactsEntities())
            {

                List<Address> Address = db.Addresses.Where(w => w.Contact_id == ContactId).ToList();
                return Json(Address, JsonRequestBehavior.AllowGet);
            }

        }
    }
}