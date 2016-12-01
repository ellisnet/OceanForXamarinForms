namespace Ocean.XamarinFormsSamples.Services {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Ocean.Portable.PrismUnity;
    using Ocean.XamarinForms.Services;
    using Ocean.XamarinFormsSamples.Model;

    public class SpeakerService : ISpeakerService {

        readonly IResolver<IHttpClient> _httpClientResolver;

        public SpeakerService(IResolver<IHttpClient> httpClientResolver) {
            if (httpClientResolver == null) {
                throw new ArgumentNullException(nameof(httpClientResolver));
            }
            _httpClientResolver = httpClientResolver;
        }

        public async Task<IEnumerable<Speaker>> Get(String url) {
            if (String.IsNullOrWhiteSpace(url)) {
                throw new ArgumentException("Value cannot be null or white space.", nameof(url));
            }

            using (var client = _httpClientResolver.Resolve()) {
                var result = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<IEnumerable<Speaker>>(result, new JsonSerializerSettings());
            }
        }

    }
}
