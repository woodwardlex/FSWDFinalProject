using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSWDFinalProject.DATA.EF;
using FSWDFinalProject.UI.MVC.Models;
using Microsoft.AspNet.Identity;

namespace FSWDFinalProject.UI.MVC.Controllers
{
    public class UserDetailsController : Controller
    {
        private JobBoardDbEntities db = new JobBoardDbEntities();

        // GET: UserDetails
        public ActionResult Index()
        {
            string currentUserID = User.Identity.GetUserId();

            var profile = db.UserDetails.Where(p => p.UserId == currentUserID);

            return View(profile);
        }

        // GET: UserDetails/Details/5
        public ActionResult Details(string id)
        {
            string currentUserID = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(currentUserID);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        //// GET: UserDetails/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: UserDetails/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "UserId,FirstName,LastName,ResumeFilename")] UserDetail userDetail)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.UserDetails.Add(userDetail);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(userDetail);
        //}

        // GET: UserDetails/Edit/5
        public ActionResult Edit(string id, HttpPostedFileBase resume, RegisterViewModel model)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string currentUserId = User.Identity.GetUserId();

            UserDetail userDeets = db.UserDetails.Find(id);
            userDeets.UserId = currentUserId;
            userDeets.FirstName = model.FirstName;
            userDeets.LastName = model.LastName;
            userDeets.ResumeFilename = model.ResumeFilename;

            if (resume == null)
            {
                //retrieve the image and assign to a variable
                string imgName = resume.FileName;

                //declare and assign the extension
                string ext = imgName.Substring(imgName.LastIndexOf('.'));

                //declare a good list of file extensions
                //string[] goodExts = {".pdf" };

                //check the ext variable ToLower() against our list of good exts and verify the content length
                //if good
                if (ext.ToLower() == ".pdf" && (resume.ContentLength <= 4194304))//4mb max by asp.net
                {
                    //rename the file using a guid
                    imgName = Guid.NewGuid() + ext.ToLower();

                    #region Save UnResized value to the webserver
                    //save the NEW file to the webserver
                    resume.SaveAs(Server.MapPath("~/Content/images/Resumes/" + imgName));
                    #endregion


                    if (model.ResumeFilename != null)
                    {
                        string path = Server.MapPath("~/Content/imgstore/resumes/");
                        System.IO.File.Delete(path + model.ResumeFilename);
                    }

                }
                //save it to the object ONLY if all other conditions have been met
                userDeets.ResumeFilename = imgName;
                return HttpNotFound();
            }

            return View(userDeets);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,FirstName,LastName,ResumeFilename")] UserDetail userDetail, HttpPostedFileBase resume, RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {


                //retrieve the image and assign to a variable
                string imgName = resume.FileName;

                //declare and assign the extension
                string ext = imgName.Substring(imgName.LastIndexOf('.'));

                //declare a good list of file extensions
                //string[] goodExts = {".pdf" };

                //check the ext variable ToLower() against our list of good exts and verify the content length
                //if good
                if (ext.ToLower() == ".pdf" && (resume.ContentLength <= 4194304))//4mb max by asp.net
                {
                    //rename the file using a guid
                    imgName = Guid.NewGuid() + ext.ToLower();

                    #region Save UnResized value to the webserver
                    //save the NEW file to the webserver
                    resume.SaveAs(Server.MapPath("~/Content/images/Resumes/" + imgName));
                    #endregion


                    if (model.ResumeFilename != null)
                    {
                        string path = Server.MapPath("~/Content/imgstore/resumes/");
                        System.IO.File.Delete(path + model.ResumeFilename);
                    }

                }
                //save it to the object ONLY if all other conditions have been met
                userDetail.ResumeFilename = imgName;



                db.Entry(userDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDetail);
        }

        // GET: UserDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserDetail userDetail = db.UserDetails.Find(id);
            db.UserDetails.Remove(userDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
