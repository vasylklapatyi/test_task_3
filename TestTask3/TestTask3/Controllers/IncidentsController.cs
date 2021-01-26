using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


//create new case, for account and populate description


namespace TestTask3.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class IncidentsController : ControllerBase
	{
		//to make the parameters list of the method smaller and logically clear i decided to group the data from body to a divided class,in advance, the class could be moved to a different file
		public class CreationBody
		{
			public string accountName { get; set; }
			public string ContactFirstName { get; set; }
			public string ContactLastName { get; set; }
			public string ContactEmail { get; set; }
			public string IncidentDescription { get; set; }
		}
		[HttpGet]
		public ActionResult CreateIncident([FromBody]CreationBody model )
		{
			using (DbEntities db = new DbEntities())
			{
				//if account name is not in the system, -> return 404
				if (db.Accounts.FirstOrDefault(x=> x.Name == model.accountName) == null)
					return StatusCode(404);

				//if contact is in the system(check by email), update contact record, link to account if not linked
				//if not, create new contact with first name, last name, email and link to the account
				var contact = db.Contacts.FirstOrDefault(x => x.Email == model.ContactEmail);
				if (contact != null)
				{
					db.Contacts.Update(new Contact()
					{
						AccountName = model.accountName,
						First_Name = model.ContactFirstName,
						Last_Name = model.ContactLastName
					});
				}
				else
				{
					db.Contacts.Add(new Contact()
					{
						Email = model.ContactEmail,
						AccountName = model.accountName,
						First_Name = model.ContactFirstName,
						Last_Name = model.ContactLastName
					});
				}

				db.Incidents.Add(new Incident()
				{
					 Name = model.IncidentDescription
				});
				db.SaveChanges();
			}
			return Ok();
		}
	}
}
