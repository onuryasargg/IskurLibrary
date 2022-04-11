using System.Web.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string keywords = null)
        {
            Models.LibraryTableAdapters.SearchResultsTableAdapter searchAdapter = new Models.LibraryTableAdapters.SearchResultsTableAdapter();
            Models.Library.SearchResultsDataTable searchResultsTable = new Models.Library.SearchResultsDataTable();
            if (keywords != null)
            {
                searchAdapter.Fill(searchResultsTable, keywords.Trim());
                ViewData["results"] = searchResultsTable.Rows;
                ViewData["keywords"] = keywords;
            }
            return View();
        }

        public ActionResult Login(string error = null)
        {
            if (error != null)
            {
                ViewData["error"] = error;
            }
            return View();
        }

        public void CheckLogin(string userName, string password)
        {
            Models.LibraryTableAdapters.UsersTableAdapter usersAdapter = new Models.LibraryTableAdapters.UsersTableAdapter();
            Models.Library.UsersDataTable usersTable = new Models.Library.UsersDataTable();
            string hashedStr;
            Models.Library.UsersRow userRow;


            usersAdapter.Fill(usersTable, userName);
            if (usersTable.Rows.Count != 1)
            {
                Response.Redirect("~/home/login?error=Bilinmeyen kullanıcı");
                return;
            }
            userRow = (Models.Library.UsersRow)usersTable.Rows[0];
            hashedStr = Utility.CalculateSHA(userName, password);
            if (hashedStr != userRow.Password)
            {
                Response.Redirect("~/home/login?error=Hatalı şifre");
                return;
            }
            Session["userId"] = userRow.Id;
            Response.Redirect("~/main", false);
        }
    }
}