namespace OauthScratch.MvcOwinClient.OpenIdConnect
{
    public static class Constants
    {
        public const string BaseAddress = "http://localhost:3344/core";//The server project URL for this solution (ie the other project)
        //public const string BaseAddress = "http://localhost:3333/core";//The server project URL for the original Thicktecture id server host as used in the client samples

        public const string AccessTokenClaimKey = "access_token";
        public const string SignInAsAuthenticationType = "Cookies";
    }
}