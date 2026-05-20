
using CirkusLuna.ClassLibrary.Model;
using CirkusLuna.ClassLibrary.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace CirkusLuna.Pages
{
    public class ShowsModel : PageModel
    {
        //service injected via dependency injection
        private IShowService _showService;

        //list of shows that page will display
        public List<Show> Shows { get; set; } = new List<Show>();

        //constructor
        public ShowsModel(IShowService showService)
        {

            _showService = showService;

        }

        //BindProperty is new for me so ill include it and paste an alternative 
        [BindProperty(SupportsGet = true)]
        public string SearchCity { get; set; } = string.Empty;
        //If i was to use something else than bind property,
        //it would be as as below
        //SearchCity = Request.Query["SearchCity"]

        public void OnGet()
        {
            if (string.IsNullOrEmpty(SearchCity))
            {
                Shows = _showService.GetAll();
            }
            else
            {
                Shows = _showService.GetByCity(SearchCity);

            }

        }
    }
}
