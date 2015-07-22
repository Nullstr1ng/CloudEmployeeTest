/*
 * author:  Jayson Ragasa
 * Date:    July 22, 2015
 */

using CloudEmployee.BAL.Net;
using CloudEmployee.DAL.Model;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace CloudEmployee.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region events
        #endregion

        #region vars
        DispatcherTimer tmr;
        #endregion

        #region properties
        private ObservableCollection<Model_customerToday> _CustomerTodayCollection = new ObservableCollection<Model_customerToday>();
        public ObservableCollection<Model_customerToday> CustomerTodayCollection
        {
            get { return _CustomerTodayCollection; }
            set
            {
                if (_CustomerTodayCollection != value)
                {
                    _CustomerTodayCollection = value;
                    base.RaisePropertyChanged("CustomerTodayCollection");
                }
            }
        }

        private bool _ShowProgressIndicator = false;
        public bool ShowProgressIndicator
        {
            get { return _ShowProgressIndicator; }
            set
            {
                if (_ShowProgressIndicator != value)
                {
                    _ShowProgressIndicator = value;
                    base.RaisePropertyChanged("ShowProgressIndicator");
                }
            }
        }
        #endregion

        #region commands

        #endregion

        #region ctor
        public MainViewModel()
        {
            DefaultData();

            if (base.IsInDesignMode)
            {
                this.ShowProgressIndicator = true;
                DesignData();
            }
            else
            {
                this.ShowProgressIndicator = true;

                RuntimeData();

                tmr = new DispatcherTimer()
                {
                    Interval= TimeSpan.FromSeconds(30)
                };
                tmr.Tick += tmr_Tick;
                tmr.Start();
            }
        }
        #endregion

        #region triggered events
        void tmr_Tick(object sender, object e)
        {
            this.ShowProgressIndicator = true;
            RuntimeData();
        }
        #endregion

        #region command methods

        #endregion

        #region methods
        public void DefaultData()
        {

        }

        /// <summary>
        /// Just pure junks. No beautifcation necessary
        /// </summary>
        public void DesignData()
        {
            List<Model_customerToday> temp_list = new List<Model_customerToday>();
            Random r = new Random(DateTime.Now.Second);

            #region magic
            // add my profile first
            temp_list.Add(new Model_customerToday()
            {
                customer = new Model_customer()
                {
                    createdBy = new Model_createdBy()
                    {
                        displayName = "Jayson Ragasa"
                    },
                    emailAddress = "jayson.d.ragasa@gmail.com"
                },
                expectedTime = "July 22, 2015 16:30:00"
            });

            // put them in temp list so we can sort it later.
            string chars = "qwertyuiopasdfghjklzxcvbnm";
            for (int i = 0; i < 50; i++)
            {
                temp_list.Add(new Model_customerToday()
                {
                    customer = new Model_customer()
                    {
                        createdBy = new Model_createdBy()
                        {
                            displayName = Task<string>.Run(() =>
                            {
                                string ret = string.Empty;
                                string c = chars + " ";
                                for (int j = 0; j < 20; j++)
                                {
                                    ret += c[r.Next(0, c.Length)];
                                }

                                return ret;
                            }).Result
                        },
                        emailAddress = Task<string>.Run(() =>
                        {
                            string ret = string.Empty;
                            string c = chars + ".";
                            string[] domains = {
                                               "@gmail.com", "@live.com", "@yahoo.com"
                                           };
                            for (int j = 0; j < 20; j++)
                            {
                                ret += c[r.Next(0, c.Length)];
                            }

                            ret += domains[r.Next(0, domains.Length)];

                            return ret;
                        }).Result
                    },
                    expectedTime = new DateTime(2015, 7, r.Next(1, 12), r.Next(1, 12), r.Next(1, 60), r.Next(1, 60)).AddDays(r.Next(1, 20)).ToString()
                });
            }
            #endregion

            Debug.WriteLine(temp_list.Count);

            temp_list = temp_list.OrderBy(x => DateTime.Parse(x.expectedTime)).Reverse().ToList();
            foreach (Model_customerToday t in temp_list)
            {
                this.CustomerTodayCollection.Add(t);
            }
        }

        public async Task RuntimeData()
        {
            Model_QueueDataBase qd = new Model_QueueDataBase();

            try
            {
                qd = await RequestAPI();

                if (qd.queueData.queue.customersToday.Count > 0) // make sure it has some records
                {
                    this.CustomerTodayCollection.Clear();
                    List<Model_customerToday> temp_list = new List<Model_customerToday>();
                    temp_list.AddRange(qd.queueData.queue.customersToday);
                    temp_list = temp_list.OrderBy(x => DateTime.Parse(x.expectedTime)).Reverse().ToList();
                    foreach (Model_customerToday t in temp_list)
                    {
                        this.CustomerTodayCollection.Add(t);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR @ MainViewModel.RuntimeData() : " + ex.Message + "\r\n" + ex.StackTrace);
            }
            finally
            {
                this.ShowProgressIndicator = false;
            }
        }

        public void PopulateCollection()
        {

        }

        public async Task<Model_QueueDataBase> RequestAPI()
        {
            //string json = Task<string>.Run(() =>
            //{
            //    return HttpClientEx.GetResponseByBasicAuth("https://app.qudini.com/api/queue/gj9fs", "codetest1", "codetest100");
            //}).Result;
            string json = await HttpClientEx.GetResponseByBasicAuth("https://app.qudini.com/api/queue/gj9fs", "codetest1", "codetest100");

            return JsonConvert.DeserializeObject<Model_QueueDataBase>(json); ;
        }
        #endregion
    }
}