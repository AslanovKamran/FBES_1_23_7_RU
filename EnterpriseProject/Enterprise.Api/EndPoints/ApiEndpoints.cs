namespace Enterprise.Api.EndPoints;

public static class ApiEndpoints
{
    private const string ApiBase = "api/v1";

    public static class Movies
    {
        private const string Base = $"{ApiBase}/movies";

        public const string GetAll = Base;
        public const string Create = Base;
        public const string GetById = $"{Base}/{{id:guid}}";
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }
}
