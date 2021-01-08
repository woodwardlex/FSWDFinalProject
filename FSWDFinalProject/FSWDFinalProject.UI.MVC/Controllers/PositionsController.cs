using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;//added for PixelFormat
using System.Drawing.Drawing2D;//added for CompositingQuality
using System.IO;//added for FileInfo
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FSWDFinalProject.DATA.EF;

namespace FSWDFinalProject.UI.MVC.Controllers
{
    public class PositionsController : Controller
    {
        private JobBoardDbEntities db = new JobBoardDbEntities();

        // GET: Positions
        [Authorize(Roles = "Admin" + "Manager")]
        public ActionResult Index()
        {
            return View(db.Positions.ToList());
        }

        // GET: Positions/Details/5
        [Authorize(Roles = "Admin" + "Manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // GET: Positions/Create
        [Authorize(Roles = "Admin" + "Manager")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Positions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin" + "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PositionId,Title,JobDescription")] Position position, HttpPostedFileBase positionIcon)
        {
            if (ModelState.IsValid)
            {
                #region File Upload
                string file = "NoImage.png";

                if (positionIcon != null)
                {
                    file = positionIcon.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };

                    //Check that the uploaded file ext is in our list of good file extensions
                    if (goodExts.Contains(ext.ToLower()))
                    {
                        //if valid, check file size <= 4mb (Max by default from ASP.NET)
                        //Can change this using the MaxRequestLength in the web.config
                        if (positionIcon.ContentLength <= 4194304)
                        {
                            //create a new file name using a guid
                            file = Guid.NewGuid() + ext;
                            positionIcon.SaveAs(Server.MapPath("~/Content/images/Positions/" + file));
                            #region Resize Image
                            //string savePath = Server.MapPath("~/Content/images/Positions/");

                            ////Points to the contents of the file(positionIcon) and converts it to an 
                            ////image datatype
                            //Image convertedImage = Image.FromStream(positionIcon.InputStream);

                            ////Pixel size
                            //int maxImageSize = 500;
                            //int maxThumbSize = 100;

                            //Resize our image and save it as a default and thumbnail version in our path
                            //ImageService.ResizeImage(savePath, file, convertedImage, maxImageSize, maxThumbSize);
                            #endregion
                        }
                        else
                        {
                            file = "noImage.png";
                        }
                        position.PositionImage = file;
                    }
                }
                #endregion

                db.Positions.Add(position);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(position);
        }

        // GET: Positions/Edit/5
        [Authorize(Roles = "Admin" + "Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // POST: Positions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin" + "Manager")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PositionId,Title,JobDescription")] Position position, HttpPostedFileBase positionIcon)
        {
            if (ModelState.IsValid)
            {
                #region Simple File Upload
                //Check input to see if a file is included
                if (positionIcon != null)
                {
                    //Get filename and save it to our image variable
                    string file = positionIcon.FileName;

                    //Use filename to extract extension and hold it in a variable
                    string ext = file.Substring(file.LastIndexOf("."));

                    //Create a list of valid extensions
                    string[] goodExts = new string[] { ".jpg", ".jpeg", ".png", ".gif" };

                    //Compare our extension against the list 
                    if (goodExts.Contains(ext.ToLower()) && positionIcon.ContentLength <= 4194304)
                    {
                        file = Guid.NewGuid() + ext;

                        //send the file to the webserver
                        positionIcon.SaveAs(Server.MapPath("~/Content/images/Positions/" + file));

                        //Delete the old image
                        if (position.PositionImage != "noImage.png" && position.PositionImage != null)
                        {
                            //delete the previously associated image from the website
                            System.IO.File.Delete(Server.MapPath("~/Content/images/Positions/" + position.PositionImage));
                        }

                        //Update database with the filename
                        position.PositionImage = file;
                    }//end goodExts
                }//end if
                #endregion

                db.Entry(position).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(position);
        }

        // GET: Positions/Delete/5
        [Authorize(Roles = "Admin" + "Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position position = db.Positions.Find(id);
            if (position == null)
            {
                return HttpNotFound();
            }
            return View(position);
        }

        // POST: Positions/Delete/5
        [Authorize(Roles = "Admin" + "Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Position position = db.Positions.Find(id);
            if (position.PositionImage != null && position.PositionImage != "noImage.png")
            {
                //Delete the image file of the record that is being removed
                System.IO.File.Delete(Server.MapPath("~/Content/images/Positions/" + Session["currentImage"].ToString()));
            }


            db.Positions.Remove(position);
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
