namespace Core.Utilities.Security.Jwt
{
    //JWT için gereli field'ları içinde bulundurur.
    public class TokenOptions
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
