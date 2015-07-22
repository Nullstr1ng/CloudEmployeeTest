using CloudEmployee.BAL.Net;
using CloudEmployee.DAL.Model;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CloudEmployee
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //AuthTest();
        }

        //[Test]
        public void AuthTest()
        {
            string test = Task<string>.Run(() => {
                return HttpClientEx.GetResponseByBasicAuth("https://app.qudini.com/api/queue/gj9fs", "codetest1", "codetest100");
            }).Result;

            Model_QueueDataBase t = JsonConvert.DeserializeObject<Model_QueueDataBase>(test);
            Debug.WriteLine(t.queueData.currentUserRole);
            Debug.WriteLine("total custoers for today: " + t.queueData.queue.customersToday.Count);
            Debug.WriteLine("done");
        }
    }
}
