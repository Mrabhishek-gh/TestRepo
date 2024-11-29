using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationTest.Filters;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    
    public class StudentController : Controller
    {
        [LoginFilter]
        public ActionResult GetallStudents()
        {
            
            ViewData["Result"] = TempData["Output"];
            StudentBuisness studentBuisness = new StudentBuisness();
            int x = studentBuisness.CountCustomer();
            ViewData["Count"] = x;
            List<Student> list = studentBuisness.GetAllCustomers();
            return View(list);
        }

        public ActionResult CreateNewRecord()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewRecord(Student student)
        {
            
            StudentBuisness studentBuisness = new StudentBuisness();
            string str = studentBuisness.InsertCustomer(student);

            if(str == "Succesfully Inserted")
            {
                TempData["Output"] = str;
                return View();
            }
            else
            {
                TempData["Output"] = str;
                return View();
            }
            
        }

        public ActionResult UpdatesForm(int id)
        {
            StudentBuisness student = new StudentBuisness();
            Student customer1 = student.GetCustomer(id);
            ViewData["cust"] = customer1;
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult UpdatesForm(int id,Student student)
        {

            StudentBuisness studentBuisness = new StudentBuisness();
            string res = studentBuisness.UpdateCustomer(id, student);

            if(res == "Successfully Updated")
            {
                TempData["Key"] = res;
                return RedirectToAction("GetallStudents");
            }
            else
            {
                TempData["Key"] = res;
                return RedirectToAction("GetallStudents");
            }
            
        }

        public ActionResult DeleteUpdate(int cust_id, string operation)
        {
            if(operation == "DELETE")
            {
                StudentBuisness studentBuisness = new StudentBuisness();
                studentBuisness.DeleteCustomer(cust_id);
                return RedirectToAction("GetallStudents");


            }
            if(operation == "UPDATE")
            {
                return RedirectToAction("UpdatesForm", new { id = cust_id });
            }

            return RedirectToAction("GetallStudents");

        }
        

        public ActionResult Details()
        {
            StudentBuisness studentBuisness = new StudentBuisness();
            List<Student> FinalList1 = studentBuisness.GetAllCustomers();
            List<StudentAdd> FinalList2 = studentBuisness.GetAllCustomersAddress();

            CustomerViewModel viewModel = new CustomerViewModel()
            {
                list1 = FinalList1,
                list2 = FinalList2,
            };
            
            return View(viewModel); 
        }

        public ActionResult Authentication()
        {
            return View();
        }

        [HttpPost]          
        public ActionResult Authentication(Login obj)
        {
            StudentBuisness studentBuisness = new StudentBuisness();
            int res = studentBuisness.Auth(obj);

            if(res == 0)
            {
                return View();
            }
            else 
            {
                Session["username"] = obj.username; 
                Session["password"] = obj.password;
                return RedirectToAction("GetallStudents");
            }

        }

        public ActionResult Logout()
        {
            
            Session.Abandon();

            return RedirectToAction("Authentication");
        }
    }
}