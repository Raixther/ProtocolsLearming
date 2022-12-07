using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicServiceData
{
	[Table("Clients")]
	public class Client
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Column]
		[StringLength(50)]
		public string? Document { get; set; }

		[Column]
		[StringLength(255)]
		public string? Surname { get; set; }

		[Column]
		[StringLength(255)]
		public string? FirstName { get; set; }

		[Column]
		[StringLength(255)]
		public string? Patronimic { get; set; }

		[InverseProperty(nameof(Pet.Client))]
		public ICollection<Pet> Pets { get; set; } = new HashSet<Pet>();

		[InverseProperty(nameof(Consultation.Client))]
		public ICollection<Consultation> Consultations { get; set; } = new HashSet<Consultation>();
	}
}
