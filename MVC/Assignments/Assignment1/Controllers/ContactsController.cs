using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using assignment1.Models;
using assignment1.Repository;

namespace assignment1.Controllers
{
    public class ContactsController : Controller
    {
        private ContactContext db = null;
        private IContactRepository contactRepository = null;

        public ContactsController()
        {
            db = new ContactContext();
            contactRepository = new ContactRepository();
        }

        // GET: Contacts
        public async Task<ActionResult> Index()
        {
            return View(await contactRepository.GetAllAsync());
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                await contactRepository.CreateAsync(contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = await db.contacts.FindAsync(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            await contactRepository.DeleteAsync(id);
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