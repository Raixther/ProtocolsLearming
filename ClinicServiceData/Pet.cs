using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicServiceData
{
	[Table("Pets")]
	public class Pet
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }


		[ForeignKey(nameof(Client))]
		public int ClientId { get; set; }


		[Column]
		[StringLength(25)]
		public required string NickName { get; set; }

		[Column]
		public DateTime Birthday { get; set; }

		public required Client Client { get; set; }


		[InverseProperty(nameof(Consultation.Pet))]
		public ICollection<Consultation> Consultations { get; set; } = new HashSet<Consultation>();
	}
}
