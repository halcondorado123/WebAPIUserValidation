namespace APIUserValidation.Helpers.SwaggerComments.AccountController
{
    public static class SwaggerTokenGenerateCommentENG
    {
        public static class UserAuthorization
        {
            public const string UserAccountTokenSummary = "User Authentication";
            public const string UserAccountTokenDescription = @"⚡ Description: This endpoint allows validating registered users with their passwords. Upon successful validation, it generates an access token that can be used to authenticate and authorize further requests."
                                                         + "\n\n✅ userName (string): Username."
                                                         + "\n\n✅ password (string): User's password."
                                                         + "\n\n🔑 accessToken (string): Generated access token upon successful authentication.";
        }
    }
}
