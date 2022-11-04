using System.ComponentModel.DataAnnotations;

namespace LibraryServiceWebClient.Models
{
	public enum SearchType
	{
		[Display(Name = "Заголовок")]
		Title,

		[Display(Name = "Автор")]
		Author,

		[Display(Name = "Категория")]
		Category
	}
}
