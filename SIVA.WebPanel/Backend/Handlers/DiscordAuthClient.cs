using Newtonsoft.Json.Linq;
using OAuth2.Configuration;
using OAuth2.Infrastructure;
using OAuth2.Models;
using RestSharp.Authenticators;

namespace OAuth2.Client.Impl
{
    /// <summary>
    /// Discord client 
    /// </summary>
    public class DiscordAuthClient : OAuth2Client
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscordAuthClient"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="configuration">The configuration.</param>
        public DiscordAuthClient(IRequestFactory factory, IClientConfiguration configuration)
            : base(factory, configuration)
        {
        }

        /// <summary>
        /// Discord client name
        /// </summary>
        public override string Name
        {
            get
            {
                return "Discord";
            }
        }

        /// <summary>
        /// The access code service endpoint
        /// </summary>
        protected override Endpoint AccessCodeServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = "https://discordapp.com",
                    Resource = "/api/oauth2/authorize"
                };
            }
        }

        /// <summary>
        /// The acess token service endpoint
        /// </summary>
        protected override Endpoint AccessTokenServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = "https://discordapp.com",
                    Resource = "/api/oauth2/token"
                };
            }
        }

        /// <summary>
        /// Called just before issuing request to third-party service when everything is ready.
        /// Allows to add extra parameters to request or do any other needed preparations.
        /// </summary>
        protected override void BeforeGetUserInfo(BeforeAfterRequestArgs args)
        {
            args.Client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(AccessToken, "Bearer");
        }

        /// <summary>
        /// Defines URI of service which allows to obtain information about user which is currently logged in.
        /// </summary>
        protected override Endpoint UserInfoServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = "https://discordapp.com",
                    Resource = "/api/users/@me"
                };
            }
        }

        /// <summary>
        /// Should return parsed <see cref="UserInfo"/> from content received from third-party service.
        /// </summary>
        /// <param name="content">The content which is received from third-party service.</param>
        protected override UserInfo ParseUserInfo(string content)
        {
            var response = JObject.Parse(content);
            var userInfo = new UserInfo();
            var userAvatarUrl = "https://cdn.discordapp.com/avatars/" + response.SelectToken("id") + "/"+response.SelectToken("avatar")+".png";
            userInfo.AvatarUri.Normal = userAvatarUrl;
            userInfo.AvatarUri.Large = userAvatarUrl + "?size=" + 256;
            userInfo.AvatarUri.Small = userAvatarUrl + "?size="+64;

            
            userInfo.FirstName = response.SelectToken("username")?.ToString();
            userInfo.LastName = response.SelectToken("discriminatorr")?.ToString();
            userInfo.Id = response.SelectToken("id")?.ToString();
            userInfo.Email = response.SelectToken("email")?.ToString();
            userInfo.ProviderName = this.Name;
            return userInfo;
        }
    }
}
