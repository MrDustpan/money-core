namespace Money.Core.Common.Infrastructure.Email
{
  public class EmailMessage
  {
    public string To { get; set; }
    public string From { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
  }
}