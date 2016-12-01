namespace Ocean.XamarinFormsSamples.Services {
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ocean.XamarinFormsSamples.Model;

    public interface ISpeakerService {

        Task<IEnumerable<Speaker>> Get(String url);

    }
}