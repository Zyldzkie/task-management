//public class FeedbackController : Controller
//{
//    private readonly ApplicationDbContext _context;

//    // Constructor to inject ApplicationDbContext
//    public FeedbackController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    // Show the feedback form
//    [HttpGet]
//    public IActionResult FeedbackForm()
//    {
//        return View();
//    }

//    // Handle form submission
//    [HttpPost]
//    public IActionResult SubmitFeedback(FeedbackModel feedback)
//    {
//        if (ModelState.IsValid)
//        {
//            var newFeedback = new Feedback
//            {
//                Username = feedback.Username,
//                Comments = feedback.Comments,
//                Rating = feedback.Rating, // Save the rating (1-5 stars)
//                SubmittedOn = DateTime.Now
//            };

//            _context.Feedbacks.Add(newFeedback);
//            _context.SaveChanges();

//            return RedirectToAction("ThankYou");
//        }

//        return View("FeedbackForm", feedback);
//    }

//    // Thank You page after feedback submission
//    public IActionResult ThankYou()
//    {
//        return View();
//    }
//}
