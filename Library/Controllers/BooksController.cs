using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Controllers
{
    public class BooksController : LibraryController
    {
        public ActionResult Index()
        {
            Models.LibraryTableAdapters.BooksTableAdapter booksAdapter = new Models.LibraryTableAdapters.BooksTableAdapter();
            Models.Library.BooksDataTable booksTable = new Models.Library.BooksDataTable();

            booksAdapter.Fill(booksTable);
            ViewData["books"] = booksTable.Rows;
            ViewData["title"] = "Kitaplar";
            return View();
        }
        public ActionResult Add(string error = null)
        {
            Models.LibraryTableAdapters.WritersTableAdapter writersAdapter = new Models.LibraryTableAdapters.WritersTableAdapter();
            Models.Library.WritersDataTable writersTable = new Models.Library.WritersDataTable();

            ViewData["title"] = "Kitap ekle";
            if (error != null)
            {
                ViewData["error"] = error;
            }
            writersAdapter.Fill(writersTable);
            ViewData["writers"] = writersTable.Rows;
            return View();
        }
        public void ProcessNewBook(string bookName, string keywords, long writer)
        {
            Models.LibraryTableAdapters.BooksTableAdapter booksAdapter;
            long id;
            DateTime addedOn;
            Models.LibraryTableAdapters.QueriesTableAdapter queriesAdapter = new Models.LibraryTableAdapters.QueriesTableAdapter();

            if (bookName == null)
            {
                Response.Redirect("~/books/add");
                return;
            }
            bookName = bookName.Trim();
            if (bookName == "")
            {
                Response.Redirect("~/books/add");
                return;
            }
            if (bookName.Length > 100)
            {
                Response.Redirect("~/books/add");
                return;
            }
            if (keywords != null)
            {
                keywords = keywords.Trim();
                if (keywords == "")
                {
                    keywords = null;
                }
                else
                {
                    if (keywords.Length > 100)
                    {
                        Response.Redirect("~/books/add");
                        return;
                    }
                }
            }
            try
            {
                booksAdapter = new Models.LibraryTableAdapters.BooksTableAdapter();
                addedOn = DateTime.Now;
                booksAdapter.AddBook(bookName, keywords, addedOn);
                id = booksAdapter.BookId(bookName, addedOn).Value;
                queriesAdapter.AddWriterBook(writer, id);
                Response.Redirect("~/books");
            }
            catch
            {
                Response.Redirect("~/books/add");
            }
        }
        public void Delete(long id)
        {
            Models.LibraryTableAdapters.BooksTableAdapter booksAdapter = new Models.LibraryTableAdapters.BooksTableAdapter();

            booksAdapter.DeleteBook(id);
            Response.Redirect("~/books");
        }
        public ActionResult Update(long id, string error = null)
        {
            Models.LibraryTableAdapters.BookDetailsTableAdapter bookDetailsAdapter = new Models.LibraryTableAdapters.BookDetailsTableAdapter();
            Models.Library.BookDetailsDataTable bookDetailsTable = new Models.Library.BookDetailsDataTable();
            Models.Library.BookDetailsRow bookDetailsRow;
            Models.LibraryTableAdapters.WritersTableAdapter writersAdapter = new Models.LibraryTableAdapters.WritersTableAdapter();
            Models.Library.WritersDataTable writersTable = new Models.Library.WritersDataTable();

            bookDetailsAdapter.FillById(bookDetailsTable, id);
            bookDetailsRow = (Models.Library.BookDetailsRow)bookDetailsTable.Rows[0];
            ViewData["title"] = bookDetailsRow.BookName;
            ViewData["bookRow"] = bookDetailsRow;
            if (error != null)
            {
                ViewData["error"] = error;
            }
            writersAdapter.Fill(writersTable);
            ViewData["writers"] = writersTable.Rows;
            return View();
        }
        public void ProcessBook(long id, string bookName, string keywords, long writer)
        {
            Models.LibraryTableAdapters.BooksTableAdapter booksAdapter;
            Models.LibraryTableAdapters.QueriesTableAdapter queriesAdapter = new Models.LibraryTableAdapters.QueriesTableAdapter();

            if (bookName == null)
            {
                Response.Redirect("~/books/update/" + id.ToString());
                return;
            }
            bookName = bookName.Trim();
            if (bookName == "")
            {
                Response.Redirect("~/books/update/" + id.ToString());
                return;
            }
            if (bookName.Length > 100)
            {
                Response.Redirect("~/books/update/" + id.ToString());
                return;
            }
            if (keywords != null)
            {
                keywords = keywords.Trim();
                if (keywords == "")
                {
                    keywords = null;
                }
                else
                {
                    if (keywords.Length > 100)
                    {
                        Response.Redirect("~/books/update/" + id.ToString());
                        return;
                    }
                }
            }
            try
            {
                booksAdapter = new Models.LibraryTableAdapters.BooksTableAdapter();
                booksAdapter.UpdateBook(bookName, keywords, id);
                queriesAdapter.DeleteWriterBook(id);
                queriesAdapter.AddWriterBook(writer, id);
                Response.Redirect("~/books");
            }
            catch
            {
                Response.Redirect("~/books/update/" + id.ToString());
            }
        }
    }
}