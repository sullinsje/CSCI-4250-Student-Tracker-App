namespace StudentTrackerApp.Models;

/// <summary>
/// View model used by the shared Error view to display the current request id.
/// </summary>
public class ErrorViewModel
{
    /// <summary>
    /// The request identifier for the current request (if available).
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Whether the request id should be displayed in the error view.
    /// </summary>
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
