public class Feedback
{
    public int FeedbackID { get; set; }
    public int ProjectID { get; set; }
    public string Username { get; set; }
    public string Comments { get; set; }
    public int? Ratings { get; set; }
    public DateTime SubmittedOn { get; set; }
}