using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IskoWalk.Pages
{
    //PLACEHOLDER CODE (for ui checking, backend remove/edit later)
    public class DashboardModel : PageModel
    {
        public List<WalkRequest> Requests { get; set; } = new();

        public void OnGet()
        {
            ViewData["ActivePage"] = "Dashboard";

            // TEMPORARY FAKE DATA — for UI preview only
            Requests = new List<WalkRequest>
        {
            new WalkRequest{
                Name = "Maria Clara",
                From = "Main Building",
                To = "Pureza LRT Station",
                Time = "Dec 30, 10:29 PM"
            },
            new WalkRequest{
                Name = "Jose Rizal",
                From = "Lagoon",
                To = "Teresa St.",
                Time = "Dec 31, 12:29 AM"
            }
        };
        }
    }

    public class WalkRequest
    {
        public string Name { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Time { get; set; }
    }
}
