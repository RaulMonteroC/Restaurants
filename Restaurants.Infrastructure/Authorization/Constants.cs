namespace Restaurants.Infrastructure.Authorization;

public static class PolicyNames
{
    public const string HAS_NATIONALITY = "HasNationality";
    public const string AT_LEAST_20 = "AtLeast20";
}

public static class AppClaimTypes
{
    public const string NATIONALITY = "Nationality";
    public const string DATE_OF_BIRTH = "DateOfBirth";
}