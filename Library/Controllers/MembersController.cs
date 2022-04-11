using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

namespace Library.Controllers
{
    public class MembersController : LibraryController
    {
        public ActionResult Index()
        {
            Models.LibraryTableAdapters.MembersTableAdapter membersAdapter = new Models.LibraryTableAdapters.MembersTableAdapter();
            Models.Library.MembersDataTable membersTable = new Models.Library.MembersDataTable();

            membersAdapter.Fill(membersTable);
            ViewData["members"] = membersTable.Rows;
            ViewData["title"] = "Üyeler";
            return View();
        }
        public void Delete(long id)
        {
            Models.LibraryTableAdapters.MembersTableAdapter membersAdapter = new Models.LibraryTableAdapters.MembersTableAdapter();

            membersAdapter.DeleteMember(id);
            Response.Redirect("~/members");
        }
        public ActionResult Update(long id, string error = null)
        {
            Models.LibraryTableAdapters.MemberDetailsTableAdapter memberDetailsAdapter = new Models.LibraryTableAdapters.MemberDetailsTableAdapter();
            Models.Library.MemberDetailsDataTable memberDetailsTable = new Models.Library.MemberDetailsDataTable();
            Models.Library.MemberDetailsRow memberDetailsRow;

            memberDetailsAdapter.FillById(memberDetailsTable, id);
            memberDetailsRow = (Models.Library.MemberDetailsRow)memberDetailsTable.Rows[0];
            ViewData["title"] = memberDetailsRow.MemberName + " " + memberDetailsRow.MemberSurname;
            ViewData["memberRow"] = memberDetailsRow;
            if (error != null)
            {
                ViewData["error"] = error;
            }
            return View();
        }
        public void ProcessMember(long originalId, long memberId, string memberName, string surname, long phone, string eMail, string address, bool banned = false, bool? gender = null)
        {
            Models.LibraryTableAdapters.MembersTableAdapter membersAdapter;
            MailAddress mailAddress;

            if (memberId.ToString().Length != 11)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if (memberName == null)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            memberName = memberName.Trim();
            if (memberName == "")
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if (memberName.Length > 50)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if (surname == null)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            surname = surname.Trim();
            if (surname == "")
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if (surname.Length > 50)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if (phone.ToString().Length != 10)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if (eMail == null)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            eMail = eMail.Trim();
            if (eMail.Length > 100)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            try
            {
                mailAddress = new MailAddress(eMail);
            }
            catch
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if (eMail != mailAddress.Address)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if (address == null)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            address = address.Trim();
            if (address == "")
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            if (address.Length > 200)
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
                return;
            }
            try
            {
                membersAdapter = new Models.LibraryTableAdapters.MembersTableAdapter();
                membersAdapter.UpdateMember(memberId, memberName, surname, phone, eMail, gender, banned, address, originalId);
                Response.Redirect("~/members");
            }
            catch
            {
                Response.Redirect("~/members/update/" + originalId.ToString());
            }
        }
    }
}