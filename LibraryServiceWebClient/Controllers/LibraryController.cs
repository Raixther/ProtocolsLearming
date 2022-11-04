using LibraryServiceWebClient.Models;

using Microsoft.AspNetCore.Mvc;

using ServiceReference;

using System.Diagnostics;

namespace LibraryServiceWebClient.Controllers
{
	public class LibraryController : Controller
	{
		private readonly ILogger<LibraryController> _logger;

		public LibraryController(ILogger<LibraryController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index([FromForm]SearchType searchType, [FromForm]string searchString)
		{
			LibrarySoapClient client = new(LibrarySoapClient.EndpointConfiguration.LibrarySoap);

			if (!string.IsNullOrEmpty(searchString) && searchString.Length>=3)
			{
				switch (searchType)
				{
					case SearchType.Title:

						return View(new BookCategoryViewModel
						{
							Books = client.GetBooksByTitle(searchString)
						});
					case SearchType.Author:

						return View(new BookCategoryViewModel
						{
							Books = client.GetBooksByAuthor(searchString)
						});

					case SearchType.Category:

						return View(new BookCategoryViewModel
						{
							Books = client.GetBooksByCategory(searchString)
						});
				}

			}
			return View(new BookCategoryViewModel { Books =    new Book[] { new Book {Id = "etyet", AgeLimit = 6, Authors =new Author[]{new Author{ Language = "yhry", Name ="dhydh"} },
			Language ="feyyf",  Title ="eeyeye", Category ="aaaaaa", Pages = 68, PublicationDate =555}  }      });
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}