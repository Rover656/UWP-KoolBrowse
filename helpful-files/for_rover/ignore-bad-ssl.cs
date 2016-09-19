public sealed partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
    }
 
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        Uri uri = new Uri("https://expired.identrustssl.com/");
 
        Uri localStreamUri = Web.BuildLocalStreamUri("MyTag", "/");
        BadHttpsStreamResolver resolver = new BadHttpsStreamResolver(uri, localStreamUri);
        Web.NavigateToLocalStreamUri(localStreamUri, resolver);
    }
}
 
public sealed class BadHttpsStreamResolver : IUriToStreamResolver
{
    private readonly string baseUri;
    private readonly string localStreamUri;
    private readonly HttpClient hc;
 
    public BadHttpsStreamResolver(Uri baseUri, Uri localStreamUri)
    {
        this.baseUri = baseUri.ToString();
        this.localStreamUri = localStreamUri.ToString();
        HttpBaseProtocolFilter filter = new HttpBaseProtocolFilter();
        // specify here which certificate errors should we ignore
        filter.IgnorableServerCertificateErrors.Add(ChainValidationResult.Expired);
        hc = new HttpClient(filter);
    }
 
    public IAsyncOperation UriToStreamAsync(Uri uri)
    {
        // TODO better uri validation and conversion
        Uri targetUri = new Uri(uri.ToString().Replace(localStreamUri, baseUri));
        return GetInputStream(targetUri).AsAsyncOperation();
    }
 
    public async Task GetInputStream(Uri targetUri)
    {
        try
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, targetUri);
            HttpResponseMessage response = await hc.SendRequestAsync(request);
            IInputStream stream = await response.Content.ReadAsInputStreamAsync();
            return stream;
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Exception {0}", ex);
            return null;
        }
    }
}
