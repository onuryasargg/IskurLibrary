using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class WritersController : LibraryController
    {
        public ActionResult Index()
        {
            Models.LibraryTableAdapters.WritersTableAdapter writersAdapter = new Models.LibraryTableAdapters.WritersTableAdapter();
            Models.Library.WritersDataTable writersTable = new Models.Library.WritersDataTable();

            writersAdapter.Fill(writersTable);
            ViewData["writers"] = writersTable.Rows;
            ViewData["title"] = "Yazarlar";
            return View();
        }
        public ActionResult Add(string error = null)
        {
            ViewData["title"] = "Yazar ekle";
            if (error != null)
            {
                ViewData["error"] = error;
            }
            return View();
        }
        public void ProcessNewWriter(string writerName, string surname, string AKA)
        {
            Models.LibraryTableAdapters.WritersTableAdapter writersAdapter;

            if (writerName == null)
            {
                Response.Redirect("~/writers/add");
                return;
            }
            writerName = writerName.Trim();
            if (writerName == "")
            {
                Response.Redirect("~/writers/add");
                return;
            }
            if (writerName.Length > 100)
            {
                Response.Redirect("~/writers/add");
                return;
            }
            if (surname != null)
            {
                surname = surname.Trim();
                if (surname == "")
                {
                    surname = null;
                }
                else
                {
                    if (surname.Length > 50)
                    {
                        Response.Redirect("~/writers/add");
                        return;
                    }
                }
            }
            if (AKA != null)
            {
                AKA = AKA.Trim();
                if (AKA == "")
                {
                    AKA = null;
                }
                else
                {
                    if (AKA.Length > 50)
                    {
                        Response.Redirect("~/writers/add");
                        return;
                    }
                }
            }
            try
            {
                writersAdapter = new Models.LibraryTableAdapters.WritersTableAdapter();
                writersAdapter.AddWriter(writerName, surname, AKA);
                Response.Redirect("~/writers");
            }
            catch
            {
                Response.Redirect("~/writers/add");
            }
        }
        public void Delete(long id)
        {
            Models.LibraryTableAdapters.WritersTableAdapter writersAdapter = new Models.LibraryTableAdapters.WritersTableAdapter();

            writersAdapter.DeleteWriter(id);
            Response.Redirect("~/writers");
        }
        public ActionResult Update(long id, string error = null)
        {
            Models.LibraryTableAdapters.WritersTableAdapter writersAdapter = new Models.LibraryTableAdapters.WritersTableAdapter();
            Models.Library.WritersDataTable writersTable = new Models.Library.WritersDataTable();
            Models.Library.WritersRow writerRow;

            writersAdapter.FillById(writersTable, id);
            writerRow = (Models.Library.WritersRow)writersTable.Rows[0];
            ViewData["title"] = writerRow.WriterName;
            if (writerRow.IsWriterSurnameNull() == false)
            {
                ViewData["title"] = writerRow.WriterName + " " + writerRow.WriterSurname;
            }
            ViewData["writerRow"] = writerRow;
            if (error != null)
            {
                ViewData["error"] = error;
            }
            return View();
        }
        public void ProcessWriter(long id, string writerName, string surname, string AKA)
        {
            Models.LibraryTableAdapters.WritersTableAdapter writersAdapter;

            if (writerName == null)
            {
                Response.Redirect("~/writers/update/" + id.ToString());
                return;
            }
            writerName = writerName.Trim();
            if (writerName == "")
            {
                Response.Redirect("~/writers/update/" + id.ToString());
                return;
            }
            if (writerName.Length > 100)
            {
                Response.Redirect("~/writers/update/" + id.ToString());
                return;
            }
            if (surname != null)
            {
                surname = surname.Trim();
                if (surname == "")
                {
                    surname = null;
                }
                else
                {
                    if (surname.Length > 50)
                    {
                        Response.Redirect("~/writers/update/" + id.ToString());
                        return;
                    }
                }
            }
            if (AKA != null)
            {
                AKA = AKA.Trim();
                if (AKA == "")
                {
                    AKA = null;
                }
                else
                {
                    if (AKA.Length > 50)
                    {
                        Response.Redirect("~/writers/update/" + id.ToString());
                        return;
                    }
                }
            }
            try
            {
                writersAdapter = new Models.LibraryTableAdapters.WritersTableAdapter();
                writersAdapter.UpdateWriter(writerName, surname, AKA, id);
                Response.Redirect("~/writers");
            }
            catch
            {
                Response.Redirect("~/writers/update/" + id.ToString());
            }
        }
    }
}