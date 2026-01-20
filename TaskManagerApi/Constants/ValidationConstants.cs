namespace TaskManagerApi.Constants;

/// <summary>
/// Application-wide constants for validation rules and limits.
/// </summary>
public static class ValidationConstants
{
    /// <summary>
    /// Task validation constants.
    /// </summary>
    public static class Task
    {
        public const int TitleMinLength = 1;
        public const int TitleMaxLength = 200;
        public const int DescriptionMaxLength = 1000;
    }

    /// <summary>
    /// User validation constants.
    /// </summary>
    public static class User
    {
        public const int UsernameMinLength = 3;
        public const int UsernameMaxLength = 50;
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 100;
        public const int EmailMaxLength = 255;
    }

    /// <summary>
    /// Category validation constants.
    /// </summary>
    public static class Category
    {
        public const int NameMinLength = 1;
        public const int NameMaxLength = 100;
    }

    /// <summary>
    /// Pagination constants.
    /// </summary>
    public static class Pagination
    {
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 100;
        public const int MinPageSize = 1;
        public const int DefaultPageNumber = 1;
    }
}