namespace Ocean.XamarinForms.Services {
    using System;
    using System.Threading.Tasks;

    public interface IHttpClient : IDisposable {

        Task<String> GetStringAsync(String url);

    }
}