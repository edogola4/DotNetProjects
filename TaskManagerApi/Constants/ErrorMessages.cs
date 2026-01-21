namespace TaskManagerApi.Constants;

/// <summary>
/// Application-wide error message constants.
/// </summary>
public static class ErrorMessages
{
    /// <summary>
    /// Authentication error messages.
    /// </summary>
    public static class Auth
    {
        public const string InvalidCredentials = "Invalid email or password";
        public const string UserAlreadyExists = "Username or email already exists";
        public const string RegistrationFailed = "Registration failed";
        public const string JwtSecretNotConfigured = "JWT Secret not configured";
        public const string JwtSettingsNotConfigured = "JWT settings not configured";
    }

    /// <summary>
    /// Task error messages.
    /// </summary>
    public static class Task
    {
        public const string NotFound = "Task not found";
        public const string DueDateInPast = "Due date cannot be in the past";
    }

    /// <summary>
    /// Category error messages.
    /// </summary>
    public static class Category
    {
        public const string NotFound = "Category not found";
        public const string HasAssociatedTasks = "Cannot delete category with associated tasks";
    }

    /// <summary>
    /// General error messages.
    /// </summary>
    public static class General
    {
        public const string NotFound = "Resource not found";
        public const string ValidationFailed = "Validation failed";
        public const string InternalServerError = "An internal server error occurred";
    }
}